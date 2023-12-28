/**
 * @file model.cpp
 * @author Keming Chen, Matthew Whitaker, Zhuowen Song
 * @brief Source file for model.h.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Joshua Beatty
 *
 * @copyright Copyright (c) 2022
 *
 */

#include "model.h"

#include "image.h"
#include "pixel.h"
#include <QFile>
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>
#include <QObject>
#include <QPoint>
#include <QTimer>
#include <QtMath>
#include <QColor>
#include <vector>

Model::Model(QObject* parent)
    : QObject{ parent }
{
    animationTimer = new QTimer(this);
    connect(animationTimer, &QTimer::timeout, this, &Model::previewNextFrame);
    loadedFile = false;
}

void Model::newProject(int width, int height)
{
    // initialize variables, and notify view when something are set.
    toggle(false);
    emit previewImage(nullptr);
    frames.clear();
    setFrameSize(width, height);
    loadedFile = true;
    currentFrame = 0;
    frames.push_back(Image(width, height));
    emit updateImageInCanvas(&frames[currentFrame], nullptr);
    emit setFrameCount(1);
    fps = 10;
    toggle(true);
    emit resetView();
}

void Model::addFrame()
{
    if (!loadedFile)
        return;
    frames.insert(frames.begin() + currentFrame, frames[currentFrame]);
    currentFrame++;
    emit updateImageInCanvas(
        &frames[currentFrame],
        frames.size() == 1 ? nullptr
                           : &frames[currentFrame - 1 < 0 ? frames.size() - 1
                                                          : currentFrame - 1]);
    emit setFrameCount(frames.size());
    emit currentFrameChanged(currentFrame);
}

// Sets the width and height of all frames
void Model::setFrameSize(int width, int height)
{
    this->width = width;
    this->height = height;
}

void Model::removeCurrentFrame()
{
    if (!loadedFile)
        return;
    if (frames.size() != 1) {
        frames.erase(frames.begin() + currentFrame);
        currentFrame--;
        currentFrame = currentFrame < 0 ? 0 : currentFrame;
        emit updateImageInCanvas(
            &frames[currentFrame],
            frames.size() == 1 ? nullptr
                               : &frames[currentFrame - 1 < 0 ? frames.size() - 1
                                                              : currentFrame - 1]);
        emit setFrameCount(frames.size());
        emit currentFrameChanged(currentFrame);
    }
}

void Model::toggle(bool isPlaying)
{
    if (!isPlaying) {
        animationTimer->stop();
    }
    else {
        animationTimer->stop();
        if (fps != 0)
            animationTimer->start(1000 / fps);
    }
}

void Model::setPixel(int x, int y, bool drawing)
{
    // draw/erase the pixel at given position.
    if (drawing) {
        frames[currentFrame].setPixel(x, y, currentColor);
    }
    else {
        frames[currentFrame].setPixel(x, y, Pixel(0, 0, 0, 0));
    }
}

void Model::drawRectangle(int startX, int startY, int endX, int endY,
    bool fill)
{
    if (!loadedFile)
        return;

    // swap end and start to let start < end.
    if (endX < startX) {
        auto temp = startX;
        startX = endX;
        endX = temp;
    }
    if (endY < startY) {
        auto temp = startY;
        startY = endY;
        endY = temp;
    }

    // fill rectangle.
    if (fill) {
        for (int i = startX; i <= endX; i++) {
            for (int j = startY; j <= endY; j++) {
                setPixel(i, j, true);
            }
        }
        return;
    }

    // if not fill, draw border only.
    // upper/lower edges.
    for (int i = startX; i <= endX; i++) {
        setPixel(i, startY, true);
        setPixel(i, endY, true);
    }

    // right/left edges.
    for (int i = startY; i <= endY; i++) {
        setPixel(startX, i, true);
        setPixel(endX, i, true);
    }

    // notify canvas to update the image.
    emit updateImageInCanvas(
        &frames[currentFrame],
        frames.size() == 1 ? nullptr
                           : &frames[currentFrame - 1 < 0 ? frames.size() - 1
                                                          : currentFrame - 1]);
}

void Model::setCurrentFrame(int currentFrame)
{
    if (!loadedFile)
        return;
    this->currentFrame = currentFrame;
    emit updateImageInCanvas(
        &frames[currentFrame],
        frames.size() == 1 ? nullptr
                           : &frames[currentFrame - 1 < 0 ? frames.size() - 1
                                                          : currentFrame - 1]);
}

void Model::fill(int x, int y, double tolerance)
{

    vector<QPoint> pointStack;

    // Get the color at position (x, y) and store it's RGB values to instance
    // variables.
    Pixel originPixel = frames[currentFrame].getPixel(x, y);
    double transparency = originPixel.getAlpha() / 255.0;
    int originRed = originPixel.getRed() * transparency;
    int originGreen = originPixel.getGreen() * transparency;
    int originBlue = originPixel.getBlue() * transparency;

    // Mark whether the (x, y) position has been filled.
    bool visited[width][height];

    // Initialize the 2D array.
    for (int i = 0; i < width; i++) {
        for (int j = 0; j < height; j++) {
            visited[i][j] = false;
        }
    }

    // Push back the position where to start filling.
    pointStack.push_back(QPoint(x, y));

    // Start filling by using the breadth first search.
    while (!pointStack.empty()) {

        // Pop out a position.
        QPoint point = pointStack.back();
        pointStack.pop_back();

        int currentX = point.x();
        int currentY = point.y();

        // If the position is not visited (not filled), try fill that position and
        // it's neighbors.
        if (!visited[currentX][currentY]) {

            // Mark it is visited
            visited[currentX][currentY] = true;

            // Calculate the difference between it and origin color to decide whether
            // fill this block and it's neighbors.
            Pixel pixel = frames[currentFrame].getPixel(currentX, currentY);
            double currentTransparency = pixel.getAlpha() / 255.0;
            double difference = (qFabs(pixel.getRed() * currentTransparency - originRed) + qFabs(pixel.getGreen() * currentTransparency - originGreen) + qFabs(pixel.getBlue() * currentTransparency - originBlue));
            bool inTolerance = difference / 3.0 <= tolerance / 100 * 255;

            // If the difference is in tolerance, fill the block and it's neighbors.
            if (inTolerance) {

                frames[currentFrame].setPixel(currentX, currentY, currentColor);

                if (currentX > 0) {
                    pointStack.push_back(QPoint(currentX - 1, currentY));
                }
                if (currentX < width - 1) {
                    pointStack.push_back(QPoint(currentX + 1, currentY));
                }

                if (currentY > 0) {
                    pointStack.push_back(QPoint(currentX, currentY - 1));
                }
                if (currentY < height - 1) {
                    pointStack.push_back(QPoint(currentX, currentY + 1));
                }
            }
        }
    }

    // Notify canvas to update image.
    emit updateImageInCanvas(
        &frames[currentFrame],
        frames.size() == 1 ? nullptr
                           : &frames[currentFrame - 1 < 0 ? frames.size() - 1
                                                          : currentFrame - 1]);
}

// Saves the current sprite data to the provided filepath.
void Model::save(QString path)
{

    if (!loadedFile)
        return;
    // Loop through each frame and copy data to Json format
    QJsonObject jsonFrameData;
    for (int i = 0; i < frames.size(); ++i) {
        QJsonArray frameArray;
        for (int y = 0; y < height; ++y) {
            QJsonArray rowArray;
            for (int x = 0; x < width; ++x) {
                QJsonArray colorArray;
                Pixel pixel = frames[i].getPixel(x, y);
                colorArray.push_back(pixel.getRed());
                colorArray.push_back(pixel.getGreen());
                colorArray.push_back(pixel.getBlue());
                colorArray.push_back(pixel.getAlpha());
                rowArray.push_back(colorArray);
            }
            frameArray.push_back(rowArray);
        }
        jsonFrameData.insert(QStringLiteral("frame%1").arg(i), frameArray);
    }

    // Compile this all into our final Json object
    QJsonObject jsonFullData;
    jsonFullData.insert("width", width);
    jsonFullData.insert("height", height);
    jsonFullData.insert("numberOfFrames", (int)frames.size());
    jsonFullData.insert("frames", jsonFrameData);

    // Wrap our object in a JSON document
    QJsonDocument finalJsonDocument;
    finalJsonDocument.setObject(jsonFullData);

    // Open the file and input stream
    QFile file(path);
    if (!file.open(QIODevice::WriteOnly)) {
        emit errorMessage(QString("There was an error saving to %1").arg(path));
        return;
    }
    QTextStream inputStream(&file);

    // Write the data and then close the file
    inputStream << finalJsonDocument.toJson();
    file.close();
}

// Loads sprite data into memory from the given filepath, if possible.
void Model::load(QString path)
{
    toggle(false);
    emit previewImage(nullptr);

    // Open the file
    QFile file(path);

    if (!file.open(QIODevice::ReadOnly)) {
        emit errorMessage(QString("Unable to open file %1").arg(path));
        return;
    }

    QByteArray fileData = file.readAll();
    file.close();

    auto jsonDocument = QJsonDocument::fromJson(fileData);

    // If we have a valid document, load it into the program.
    if (!jsonDocument.isObject()) {
        emit errorMessage(QString("%1 is in an invalid format.").arg(path));
        return;
    }
    QJsonObject jsonObject = jsonDocument.object();

    // Read in the project's height and width
    int height = jsonObject.value("height").toInt();
    int width = jsonObject.value("width").toInt();

    // Clear space for the new project in memory.
    emit setFrameCount(0);
    emit updateImageInCanvas(nullptr, nullptr);
    frames.clear();
    setFrameSize(width, height);

    // Read in the number of frames
    int numberOfFrames = jsonObject.value("numberOfFrames").toInt();
    auto jsonFrames = jsonObject.value("frames").toObject();

    // Loop through each frame and add it
    for (int frameN = 0; frameN < numberOfFrames; ++frameN) {
        auto jsonFrame = jsonFrames.value(QStringLiteral("frame%1").arg(frameN)).toArray();
        frames.push_back(Image(width, height));

        // Loop through each row of pixels and read the row
        for (int yCoord = 0; yCoord < height; ++yCoord) {
            auto frameRow = jsonFrame.at(yCoord).toArray();

            // Loop through each pixel in the row and set its color in memory
            for (int xCoord = 0; xCoord < width; ++xCoord) {
                auto framePixel = frameRow.at(xCoord).toArray();

                // TODO add check for invalid array here

                int red = framePixel.at(0).toInt();
                int green = framePixel.at(1).toInt();
                int blue = framePixel.at(2).toInt();
                int alpha = framePixel.at(3).toInt();

                frames[frameN].setPixel(xCoord, yCoord, Pixel(red, green, blue, alpha));
            }
        }
    }

    currentFrame = 0;
    emit updateImageInCanvas(
        &frames[currentFrame],
        numberOfFrames == 1 ? nullptr
                            : &frames[currentFrame - 1 < 0 ? numberOfFrames - 1
                                                           : currentFrame - 1]);
    emit setFrameCount(numberOfFrames);
    fps = 10;
    toggle(true);
    emit resetView();
    loadedFile = true;
}

void Model::changeColor(QColor userColor)
{
    currentColor = Pixel(userColor.red(), userColor.green(), userColor.blue(),
        userColor.alpha());
}

void Model::previewNextFrame()
{
    // If there are no frames, stop the animation, else play the animation.
    if (frames.size() != 0) {
        previewFrame = (previewFrame + 1) % frames.size();
        emit previewImage(&frames[previewFrame]);
    }
    else {
        animationTimer->stop();
    }
}

void Model::setFPS(int fps)
{
    this->fps = fps;
    toggle(true);
}
