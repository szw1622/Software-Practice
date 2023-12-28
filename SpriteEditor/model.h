/**
 * @file model.h
 * @author Keming Chen, Matthew Whitaker, Zhuowen Song
 * @brief Header file for model.cpp.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Joshua Beatty
 *
 * @copyright Copyright (c) 2022
 *
 */
#ifndef MODEL_H
#define MODEL_H

#include "image.h"
#include "pixel.h"
#include <QFile>
#include <QObject>
#include <QColor>
#include <QTimer>
#include <vector>

using std::string;
using std::vector;

/**
 * @brief The Model class.
 * Contains primary functions for the editor.
 */
class Model : public QObject {
    Q_OBJECT
public:
    explicit Model(QObject* parent = nullptr);

    /**
   * @brief newProject Creates a new project
   * @param width
   * @param height
   */
    void newProject(int width, int height);

    /**
   * @brief addFrame
   */
    void addFrame();

    /**
   * @brief set size for all frames
   * @param width
   * @param height
   */
    void setFrameSize(int width, int height);

    /**
   * @brief remove the current frame
   */
    void removeCurrentFrame();

    /**
   * @brief start/stop playing animation
   */
    void toggle(bool isPlaying);

    /**
   * @brief set color for a pixel at given position.
   * @param x
   * @param y
   * @param drawing if false, remove color at given position.
   */
    void setPixel(int x, int y, bool drawing);

    /**
   * @brief draw a rectengle starts and ends at given position.
   * @param startX
   * @param startY
   * @param endX
   * @param endY
   * @param fill
   */
    void drawRectangle(int startX, int startY, int endX, int endY, bool fill);

    /**
   * @brief setCurrentFrame
   * @param index
   */
    void setCurrentFrame(int index);

    /**
   * @brief fill color start at given position.
   * @param x
   * @param y
   */
    void fill(int x, int y, double tolerance);

    /**
   * @brief save project to given path.
   * @param path
   */
    void save(QString path);

    /**
   * @brief load project from given path.
   * @param path
   */
    void load(QString path);

    /**
   * @brief changeColor change the color of holding sample pixel
   */
    void changeColor(QColor);

private:
    int width, height;

    int currentFrame, previewFrame;

    int fps;

    Pixel currentColor;

    QTimer* animationTimer;

    vector<Image> frames;

    bool loadedFile;

    QFile currentFile;

public slots:
    /**
   * @brief playAnimation play animation by stepping to the next frame.
   */
    void previewNextFrame();
    /**
   * @brief playAnimation play animation by stepping to the next frame.
   */
    void setFPS(int fps);

signals:

    /**
   * @brief previewImage Notifies preview window to update it's image.
   * @param image
   */
    void previewImage(Image* image);

    /**
   * @brief updateImageInCanvas Notifies canvas to reset Image.
   * @param image The current image
   * @param pevImage The image of previous frame, used for onion skinning.
   */
    void updateImageInCanvas(Image* image, Image* pevImage);

    /**
   * @brief errorMessage Broadcasts an error message signal.
   * @param message The error message to show in the view.
   */
    void errorMessage(QString message);

    /**
   * @brief setFrameCount Broadcasts the current frame count.
   * @param frameCount the current frame count.
   */
    void setFrameCount(int frameCount);

    /**
   * @brief currentFrameChanged Broadcasts the current selected frame.
   * @param currentFrame the current selected frame
   */
    void currentFrameChanged(int currentFrame);

    /**
   * @brief resetView Notifies the window to reset itself.
   */
    void resetView();
};

#endif // MODEL_H
