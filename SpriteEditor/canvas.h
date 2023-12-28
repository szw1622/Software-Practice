/**
 * @file canvas.h
 * @author Joshua Beatty
 * @brief Header file for canvas.cpp.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Keming Chen
 *
 * @copyright Copyright (c) 2022
 *
 */
#ifndef CANVAS_H
#define CANVAS_H

#include "./image.h"
#include <QImage>
#include <QTimer>
#include <QWidget>
#include <tuple>

class CanvasWidget : public QWidget {
    Q_OBJECT
public:
    explicit CanvasWidget(QWidget* parent, Image* frame, Image* prevFrame, int);
    QTimer cursorFlashTimer;
    Image* frame;
    Image* prevFrame;
    int currentFocusX = 0;
    int currentFocusY = 0;
    int pixelSize = 4;
    bool cursorIsFlashing = false;
    bool onionSkinngEnabled = false;

private:
    /**
   * @brief getPixelCordinate get the current position of the mouse, in pixels
   * realtive to this widget
   * @return the position of the mouse in pixel cordinates of the frame, returns
   * -1,-1 if the cursor is not on the frame
   */
    std::tuple<int, int> getPixelCordinate();
    /**
   * @brief event overriding the main event method to preform event filtering to
   * pass to the hover, press, and realse handlers
   * @param event the event itself
   * @return whether or not the event was handled and consumed
   */
    bool event(QEvent* event);
    /**
   * @brief hoverMoveHandler handles the hover move event and fires the relevant
   * signals
   * @param event the event that cuased the hover move call
   */
    void hoverMoveHandler(QHoverEvent* event);
    /**
   * @brief mousePressHandler handles the mouse press event and fires the
   * relevant signals
   * @param event the event that cuased the mouse press call
   */
    void mousePressHandler(QMouseEvent* event);
    /**
   * @brief mousePressHandler handles the mouse release event and fires the
   * relevant signals
   * @param event the event that cuased the mouse release call
   */
    void mouseReleaseHandler(QMouseEvent* event);
    /**
   * @brief paintEvent overriding the draw event of this custom widget to
   * display the pixels
   * @param event the paint event itself, unused
   */
    void paintEvent(QPaintEvent* event);
public slots:
    /**
   * @brief setFrame sets the current frame of the canvas
   * @param frame the current frame to be displayed
   * @param prevFrame the previus frame, used for onion skinning
   */
    void setFrame(Image* frame, Image* prevFrame);
    /**
   * @brief setScale Sets the scale of the canvas to be drawn at
   * @param pixelSize the pixel size for pixels to be drawn at
   */
    void setScale(int pixelSize);
    /**
   * @brief setOnionSkinngEnabled enable or disable onion skinning
   * @param enabled whether to enable or disable onion skinning
   */
    void setOnionSkinngEnabled(bool enabled);
signals:
    /**
   * @brief mouseReleased fired with the mouse is release
   * @param x the x cordinate in frame pixels where this event happened
   * @param y the y cordinate in frame pixels where this event happened
   */
    void mouseReleased(int x, int y);
    /**
   * @brief moveOverWhileHeld fired with the mouse is moved
   * @param x the x cordinate in frame pixels where this event happened
   * @param y the y cordinate in frame pixels where this event happened
   */
    void moveOverWhileHeld(int x, int y);
    /**
   * @brief moveOverWhileHeld fired with the mouse is pressed
   * @param x the x cordinate in frame pixels where this event happened
   * @param y the y cordinate in frame pixels where this event happened
   */
    void mousePressed(int x, int y);
};

#endif // CANVAS_H
