/**
 * @file preview.h
 * @author Joshua Beatty
 * @brief Header file for preview.cpp.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Keming Chen
 *
 * @copyright Copyright (c) 2022
 *
 */
#ifndef PREVIEW_H
#define PREVIEW_H

#include "./image.h"
#include <QWidget>

class PreviewWidget : public QWidget {
    Q_OBJECT
public:
    explicit PreviewWidget(QWidget* parent, double scale, Image* frame);
    Image* frame;
    int scale;
    /**
   * @brief paintEvent overriding the draw event of this custom widget to
   * display the pixels
   * @param event the paint event itself, unused
   */
    void paintEvent(QPaintEvent* event);
    /**
   * @brief setFrame setting the current frame that is displayed
   * @param frame the frame to display
   */
    void setFrame(Image* frame);
public slots:
    /**
   * @brief setScale setting the scalar multiple of the preview to display
   * @param newScale the new scale for the preview to be displayed
   */
    void setScale(int newScale);
};

#endif // PREVIEW_H
