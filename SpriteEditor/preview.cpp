/**
 * @file preview.cpp
 * @author Joshua Beatty
 * @brief The preview widget, used for displaying the animation.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Keming Chen
 *
 * @copyright Copyright (c) 2022
 *
 */
#include "preview.h"
#include <QPainter>

PreviewWidget::PreviewWidget(QWidget* parent, double scale, Image* frame)
    : QWidget{ parent }
    , frame(frame)
{
    setScale(scale);
}

void PreviewWidget::paintEvent(QPaintEvent* event)
{
    if (frame == nullptr)
        return;
    QPainter painter(this);
    for (int x = 0; x < frame->width; x++) {
        for (int y = 0; y < frame->height; y++) {
            auto color = frame->getPixel(x, y);
            painter.fillRect(x * scale, y * scale, scale, scale,
                QColor::fromRgb(color.getRed(), color.getGreen(),
                                 color.getBlue(), color.getAlpha()));
        }
    }
}

void PreviewWidget::setFrame(Image* newFrame)
{
    frame = newFrame;
    setScale(scale);
}

void PreviewWidget::setScale(int newScale)
{
    scale = newScale;
    if (frame == nullptr)
        return;
    this->resize(frame->width * scale, frame->height * scale);
    update();
}
