/**
 * @file mainwindow.h
 * @author Joshua Beatty, Zhuowen Song, Keming Chen, Matthew Whitaker
 * @brief Header file for mainwindow.cpp.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Matthew Whitaker
 *
 * @copyright Copyright (c) 2022
 *
 */

#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "model.h"
#include "newprojectwizard.h"

QT_BEGIN_NAMESPACE
namespace Ui {
class MainWindow;
}
QT_END_NAMESPACE

class MainWindow : public QMainWindow {
    Q_OBJECT

public:
    /**
     * @brief MainWindow Constructs the MainWindow with the given model
     */
    MainWindow(Model& model, QWidget* parent = nullptr);

    /**
     * @brief Cleans up resources for this file.
     */
    ~MainWindow();

private:
    Ui::MainWindow* ui;
    int pressedPositionX;
    int pressedPositionY;
    // Modes of drawing
    bool filling;
    bool rectangling;
    bool filledRectangle;
    bool erasing;

    NewProjectWizard newProjectWizard;

private slots:
    /**
     * @brief showNewProjectDialog Called to show a dialog that allows the user to create a new project
     */
    void showNewProjectDialog();

    /**
     * @brief showFileOpenDialog Called to show the file open dialog to the user
     */
    void showFileOpenDialog();

    /**
     * @brief showSaveDialog Called to show the file save dialog to the user
     */
    void showSaveDialog();

    /**
     * @brief showErrorDialog Should be called to show an error dialog to the user.
     * @param message The message to show to the user.
     */
    void showErrorDialog(QString message);

    /**
     * @brief colorPannel Called to open color pannel and let the user change color
     */
    void colorPannel();

    /**
     * @brief changeToEraser Called to change the drawing mode as erase(paint white)
     */
    void changeToEraser();

    /**
     * @brief changeToRectangle Called to change the drawing mode as drag and draw a rectangle
     */
    void changeToRectangle();

    /**
     * @brief changeToFilledRectangle Called to change the drawing mode as drag and draw a filled rectangle
     */
    void changeToFilledRectangle();

    /**
     * @brief changeToPainting Called to change the drawing mode as just paint one pixel
     */
    void changeToPainting();

    /**
     * @brief changeToFill Called to change the drawing mode as fill one area
     */
    void changeToFill();

    /**
     * @brief mousePressed Called when the user is pressing and move the mouse
     * @param x position X
     * @param y position Y
     */
    void mousePressing(int x, int y);

    /**
     * @brief mouseLeasing Called when the user release the mouse
     * @param x position X
     * @param y position Y
     */
    void mouseReleasing(int x, int y);

    /**
     * @brief mousePressing Called when the user press the mouse first time. Record the starting point if not drawing.
     * @param x
     * @param y
     */
    void mousePressed(int x, int y);

    /**
     * @brief setFrameCount Set the number of frames to be displaied on the frame slider
     * @param frameCount
     */
    void setFrameCount(int frameCount);

signals:
    /**
     * @brief fileOpened Called when the user would like a file to be opened with the given filename.
     * @param filename
     */
    void fileOpened(QString filename);

    /**
     * @brief fileSaved Called when the user would like a file to be saved with the given filename.
     * @param filename
     */
    void fileSaved(QString filename);

    /**
     * @brief chooseColor Called when the user would like to change color by using the color pannel.
     * @param currentColor
     */
    void chooseColor(QColor currentColor);

    /**
     * @brief chooseEraser Called when the user would like to use eraser.
     */
    void chooseEraser(QColor whiteColor);

    /**
     * @brief drawRectangle Called when the user would like to draw rectangle.
     */
    void drawRectangle(int startX, int startY, int endX, int endY, bool fill);

    /**
     * @brief fill Called when the user would like to use fill tool.
     */
    void fill(int x, int y, double tolerance);

    /**
     * @brief drawing Tells the model where to draw
     */
    void drawing(int x, int y, bool isDrawing);
};
#endif // MAINWINDOW_H
