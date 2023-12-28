/**
 * @file canvas.cpp
 * @author Joshua Beatty
 * @brief The canvas for painting.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Keming Chen
 *
 * @copyright Copyright (c) 2022
 *
 */
#include "canvas.h"
#include <QApplication>
#include <QEvent>
#include <QHoverEvent>
#include <QPainter>

CanvasWidget::CanvasWidget(QWidget* parent, Image* frame, Image* prevFrame,
    int pixelSize)
    : QWidget{ parent }
    , frame(frame)
    , pixelSize(pixelSize)
    , prevFrame(prevFrame)
{
    this->setAttribute(Qt::WA_Hover, true);
    setScale(pixelSize);

    QObject::connect(&cursorFlashTimer, &QTimer::timeout, this, [this]() {
        cursorIsFlashing = !cursorIsFlashing;
        update();
    });
    cursorFlashTimer.start(400);
}

std::tuple<int, int> CanvasWidget::getPixelCordinate()
{
    int xPixelPos = QWidget::mapFromGlobal(QCursor::pos()).x() / pixelSize;
    int yPixelPos = QWidget::mapFromGlobal(QCursor::pos()).y() / pixelSize;

    auto scrollAreaWidget = this->parentWidget()->parentWidget();
    int xMousePosScrollArea = scrollAreaWidget->mapFromGlobal(QCursor::pos()).x();
    int yMousePosScrollArea = scrollAreaWidget->mapFromGlobal(QCursor::pos()).y();

    if (xPixelPos < 0 || yPixelPos < 0 || xPixelPos > frame->width - 1 | yPixelPos > frame->height - 1 || xMousePosScrollArea > scrollAreaWidget->width() || yMousePosScrollArea > scrollAreaWidget->height() || xMousePosScrollArea < 0 || yMousePosScrollArea < 0) {
        xPixelPos = -1;
        yPixelPos = -1;
    }

    return std::tuple(xPixelPos, yPixelPos);
}

bool CanvasWidget::event(QEvent* event)
{
    switch (event->type()) {
    case QEvent::HoverMove:
        hoverMoveHandler(static_cast<QHoverEvent*>(event));
        return false;
        break;
    case QEvent::MouseButtonPress:
        mousePressHandler(static_cast<QMouseEvent*>(event));
        return false;
        break;
    case QEvent::MouseButtonRelease:
        mouseReleaseHandler(static_cast<QMouseEvent*>(event));
        return false;
        break;
    default:
        break;
    }
    QWidget::event(event);
    return false;
}

void CanvasWidget::hoverMoveHandler(QHoverEvent* event)
{
    if (qApp->mouseButtons() & Qt::LeftButton) {
        auto pixelPos = getPixelCordinate();
        int x = std::get<0>(pixelPos);
        int y = std::get<1>(pixelPos);
        if (x != -1) {
            emit moveOverWhileHeld(x, y);
        }
    }
    update();
}

void CanvasWidget::mousePressHandler(QMouseEvent* event)
{
    if (qApp->mouseButtons() & Qt::LeftButton && event->position().x() > 0 && event->position().y() > 0) {
        auto pixelPos = getPixelCordinate();
        int x = std::get<0>(pixelPos);
        int y = std::get<1>(pixelPos);
        if (x != -1) {
            emit mousePressed(x, y);
        }
    }
}

void CanvasWidget::mouseReleaseHandler(QMouseEvent* event)
{
    if (event->button() == Qt::LeftButton) {
        auto pixelPos = getPixelCordinate();
        int x = std::get<0>(pixelPos);
        int y = std::get<1>(pixelPos);
        emit mouseReleased(x, y);
    }
}

void CanvasWidget::paintEvent(QPaintEvent* event)
{
    QPainter painter(this);
    currentFocusX = QWidget::mapFromGlobal(QCursor::pos()).x() / pixelSize;
    currentFocusY = QWidget::mapFromGlobal(QCursor::pos()).y() / pixelSize;

    for (int x = 0; x < frame->width; x++) {
        for (int y = 0; y < frame->height; y++) {
            auto color = frame->getPixel(x, y);
            painter.fillRect(x * pixelSize, y * pixelSize, pixelSize, pixelSize,
                QColor::fromRgb(color.getRed(), color.getGreen(),
                                 color.getBlue(), color.getAlpha()));
            if (onionSkinngEnabled && prevFrame) {
                auto colorOnion = prevFrame->getPixel(x, y);
                auto bg = colorOnion;
                auto ratio = (colorOnion.getAlpha() / 255) * 0.25;
                auto alpha = color.getAlpha() == 0 ? 255 * 0.25 : 255;
                auto red = color.getRed() * (1 - ratio) + bg.getRed() * ratio;
                auto green = color.getGreen() * (1 - ratio) + bg.getGreen() * ratio;
                auto blue = color.getBlue() * (1 - ratio) + bg.getBlue() * ratio;
                painter.fillRect(x * pixelSize, y * pixelSize, pixelSize, pixelSize,
                    QColor::fromRgb(red, green, blue, alpha));
            }
        }
    }

    painter.setPen(QPen(QColor::fromRgb(200, 200, 200), 2, Qt::SolidLine,
        Qt::RoundCap, Qt::RoundJoin));
    if (cursorIsFlashing)
        painter.setPen(QPen(QColor::fromRgb(50, 50, 50), 1, Qt::SolidLine,
            Qt::RoundCap, Qt::RoundJoin));
    painter.drawLine(currentFocusX * pixelSize - 1, currentFocusY * pixelSize - 1,
        currentFocusX * pixelSize + pixelSize + 1,
        currentFocusY * pixelSize - 1);
    painter.drawLine(currentFocusX * pixelSize - 1, currentFocusY * pixelSize - 1,
        currentFocusX * pixelSize - 1,
        currentFocusY * pixelSize + pixelSize + 1);
    painter.drawLine(currentFocusX * pixelSize + pixelSize + 1,
        currentFocusY * pixelSize + pixelSize + 1,
        currentFocusX * pixelSize + pixelSize + 1,
        currentFocusY * pixelSize - 1);
    painter.drawLine(currentFocusX * pixelSize + pixelSize + 1,
        currentFocusY * pixelSize + pixelSize + 1,
        currentFocusX * pixelSize - 1,
        currentFocusY * pixelSize + pixelSize + 1);

    painter.setPen(QPen(QColor::fromRgb(0, 0, 0), 1, Qt::SolidLine, Qt::RoundCap,
        Qt::RoundJoin));
    painter.drawLine(frame->width * pixelSize + 1, 0,
        frame->width * pixelSize + 1, frame->height * pixelSize + 1);
    painter.drawLine(0, frame->height * pixelSize + 1,
        frame->width * pixelSize + 1, frame->height * pixelSize + 1);
}

void CanvasWidget::setFrame(Image* frame, Image* prevFrame)
{
    this->frame = frame;
    this->prevFrame = prevFrame;
    if (frame != nullptr) {
        this->resize(frame->width * pixelSize + 1, frame->height * pixelSize + 1);
        this->parentWidget()->setMinimumWidth(frame->width * pixelSize + 1);
        this->parentWidget()->setMinimumHeight(frame->height * pixelSize + 1);
    }
}

void CanvasWidget::setScale(int pixelSize)
{
    this->pixelSize = pixelSize;
    setFrame(frame, prevFrame);
}

void CanvasWidget::setOnionSkinngEnabled(bool enabled)
{
    onionSkinngEnabled = enabled;
    update();
}
