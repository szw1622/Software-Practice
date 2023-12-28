/**
 * @file mainwindow.cpp
 * @author Joshua Beatty, Zhuowen Song, Keming Chen, Matthew Whitaker
 * @brief Design of the main window.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Matthew Whitaker
 *
 * @copyright Copyright (c) 2022
 *
 */

#include "mainwindow.h"
#include "./ui_mainwindow.h"
#include "./canvas.h"
#include "./preview.h"
#include "./image.h"
#include "./newprojectwizard.h"
#include <QLayout>
#include <QScrollBar>
#include <QColorDialog>
#include <QColor>
#include <QCheckBox>
#include <QFileDialog>
#include <QMessageBox>
#include "model.h"

MainWindow::MainWindow(Model& model, QWidget* parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    // Set up icons of buttons
    ui->paintButton->setIcon(QIcon(QPixmap(":/icons/colorIcon.jpg")));
    ui->paintButton->setIconSize(QSize(65, 65));
    ui->eraserButton->setIcon(QIcon(QPixmap(":/icons/eraserIcon.png")));
    ui->eraserButton->setIconSize(QSize(65, 65));
    ui->fillButton->setIcon(QIcon(QPixmap(":/icons/fillIcon.png")));
    ui->fillButton->setIconSize(QSize(65, 65));
    ui->rectangleButton->setIcon(QIcon(QPixmap(":/icons/rectangleIcon.jpg")));
    ui->rectangleButton->setIconSize(QSize(65, 65));
    ui->filledRectangleButton->setIcon(QIcon(QPixmap(":/icons/filledRectangle.png")));
    ui->filledRectangleButton->setIconSize(QSize(65, 65));

    //Canvas setup
    Image* emptyFrame = new Image(0, 0);
    auto canvas = new CanvasWidget(ui->scrollAreaWidgetContents, emptyFrame, emptyFrame, 10);
    auto preview = new PreviewWidget(ui->previewFrame, 2, emptyFrame);
    connect(ui->scrollArea->verticalScrollBar(), &QScrollBar::valueChanged, this, [canvas](int value) {
        canvas->update();
    });

    //Zooming
    connect(ui->zoomSlider, &QSlider::sliderMoved, canvas, &CanvasWidget::setScale);
    connect(ui->preivewScaleSlider, &QSlider::sliderMoved, preview, &PreviewWidget::setScale);
    connect(ui->zoomPreviewSpinBox, &QSpinBox::valueChanged, preview, &PreviewWidget::setScale);
    connect(ui->zoomPreviewSpinBox, &QSpinBox::valueChanged, this, [this](int newVal){
        if(ui->preivewScaleSlider->value() != newVal)
            ui->preivewScaleSlider->setValue(newVal);
    });
    connect(ui->preivewScaleSlider, &QSlider::valueChanged, this, [this](int newVal){
        if(ui->zoomPreviewSpinBox->value() != newVal)
            ui->zoomPreviewSpinBox->setValue(newVal);
    });

    // Save and Open Actions
    connect(ui->actionOpen, &QAction::triggered, this, &MainWindow::showFileOpenDialog);
    connect(ui->actionSave, &QAction::triggered, this, &MainWindow::showSaveDialog);
    connect(this, &MainWindow::fileOpened, &model, &Model::load);
    connect(this, &MainWindow::fileSaved, &model, &Model::save);

    // New Project
    connect(ui->actionNew, &QAction::triggered, this, &MainWindow::showNewProjectDialog);
    connect(&newProjectWizard, &NewProjectWizard::newProject, &model, &Model::newProject);

    // Canvas Updates
    connect(&model, &Model::updateImageInCanvas, canvas, &CanvasWidget::setFrame);

    // Error Handling
    connect(&model, &Model::errorMessage, this, &MainWindow::showErrorDialog);

    // Color button
    connect(ui->changeColorButton, &QPushButton::clicked, this, &MainWindow::colorPannel);
    connect(this, &MainWindow::chooseColor, &model, &Model::changeColor);

    // Color button
    connect(ui->paintButton, &QPushButton::clicked, this, &MainWindow::changeToPainting);

    // Eraser button
    connect(ui->eraserButton, &QPushButton::clicked, this, &MainWindow::changeToEraser);
    connect(this, &MainWindow::chooseEraser, &model, &Model::changeColor);

    // Rectangle button
    connect(ui->rectangleButton, &QPushButton::clicked, this, &MainWindow::changeToRectangle);
    connect(this, &MainWindow::drawRectangle, &model, &Model::drawRectangle);

    // Filled Rectangle button
    connect(ui->filledRectangleButton, &QPushButton::clicked, this, &MainWindow::changeToFilledRectangle);
    connect(this, &MainWindow::drawRectangle, &model, &Model::drawRectangle);

    // Fill button
    connect(ui->fillButton, &QPushButton::clicked, this, &MainWindow::changeToFill);
    connect(this, &MainWindow::fill, &model, &Model::fill);

    // Mouse pressing
    connect(canvas, &CanvasWidget::mousePressed, this, &MainWindow::mousePressed);
    connect(canvas, &CanvasWidget::moveOverWhileHeld, this, &MainWindow::mousePressing);

    // Mouse releasing
    connect(canvas, &CanvasWidget::mouseReleased, this, &MainWindow::mouseReleasing);

    // Drawing
    connect(this, &MainWindow::drawing, &model, &Model::setPixel);

    // Animation
    connect(&model, &Model::previewImage, preview, &PreviewWidget::setFrame);
    connect(ui->fpsSpinBox, &QSpinBox::valueChanged, &model, &Model::setFPS);
    connect(ui->fpsSpinBox, &QSpinBox::valueChanged, this, [this](int newVal) {
        if (ui->fpsSlider->value() != newVal)
            ui->fpsSlider->setValue(newVal);
    });
    connect(ui->fpsSlider, &QSlider::valueChanged, this, [this](int newVal) {
        if (ui->fpsSpinBox->value() != newVal)
            ui->fpsSpinBox->setValue(newVal);
    });

    //Frame selection
    connect(&model, &Model::setFrameCount, this, &MainWindow::setFrameCount);
    connect(ui->frameSelector, &QSlider::sliderMoved, &model, &Model::setCurrentFrame);

    //Onion skinning
    connect(ui->onionSkinningCheckBox, &QCheckBox::toggled, canvas, &CanvasWidget::setOnionSkinngEnabled);

    //Reset function
    connect(&model, &Model::resetView, this, [this, canvas]() {
        ui->frameSelector->setValue(0);
        ui->frameSelector->setMinimum(0);
        ui->onionSkinningCheckBox->setChecked(false);
        canvas->setOnionSkinngEnabled(false);
        ui->fpsSpinBox->setValue(10);

        auto defaultColor = QColor(255, 255, 255, 255);
        auto colorFrame = ui->colorFrame;
        QPalette pal = QPalette();
        pal.setColor(QPalette::Window, defaultColor);
        colorFrame->setAutoFillBackground(true);
        colorFrame->setPalette(pal);
        emit chooseColor(defaultColor);
        changeToPainting();
    });

    connect(ui->addFrameButton, &QPushButton::clicked, &model, &Model::addFrame);
    connect(ui->deleteFrameButton, &QPushButton::clicked, &model, &Model::removeCurrentFrame);
    connect(&model, &Model::currentFrameChanged, this, [this](int currentFrame) {
        ui->frameSelector->setValue(currentFrame);
    });

    changeToPainting();
}

// Deconstructor
MainWindow::~MainWindow()
{
    delete ui;
}

// Shows the user a dialog where they can configure settings for a new project
void MainWindow::showNewProjectDialog()
{
    newProjectWizard.show();
}

// Shows the user a dialog where they can pick a .ssp file to open
void MainWindow::showFileOpenDialog()
{
    QString fileName = QFileDialog::getOpenFileName(this, "Select a Sprite Project", "~/", "Sprite Projects (*.ssp)");
    if (!fileName.isEmpty()) {
        emit fileOpened(fileName);
    }
}

// Shows the user a dialog where they can pick a directory and filename for saving their project
void MainWindow::showSaveDialog()
{
    QString fileName = QFileDialog::getSaveFileName(this, "Save Sprite Project", "", "Sprite Project (*.ssp)");
    if (!fileName.isEmpty()) {
        emit fileSaved(fileName);
    }
}

// Shows the user an error dialog with the given message
void MainWindow::showErrorDialog(QString message)
{
    QMessageBox::critical(this, "An error has occurred", message);
}

// Let the user choose drawing color
void MainWindow::colorPannel()
{
    // Get the color
    QColor currentColor = QColorDialog::getColor();
    if (!currentColor.isValid())
        return;

    // Show the current color
    auto colorFrame = ui->colorFrame;
    QPalette pal = QPalette();
    pal.setColor(QPalette::Window, currentColor);
    colorFrame->setAutoFillBackground(true);
    colorFrame->setPalette(pal);

    emit chooseColor(currentColor);
}

void MainWindow::changeToEraser()
{
    // up the buttons
    ui->fillButton->setDown(false);
    ui->rectangleButton->setDown(false);
    ui->filledRectangleButton->setDown(false);

    // Change and show the mode
    filling = false;
    rectangling = false;
    filledRectangle = false;
    erasing = true;
    ui->modeBox->setText("erasing");
}

void MainWindow::changeToRectangle()
{
    // up the buttons
    ui->eraserButton->setDown(false);

    // change and show the mode
    rectangling = true;
    filling = false;
    filledRectangle = false;
    erasing = false;
    ui->modeBox->setText("Drawing a rectangle");

    // down the button
    ui->rectangleButton->setDown(true);
}

void MainWindow::changeToFilledRectangle()
{
    // up the button
    ui->eraserButton->setDown(false);

    // change and show the mode
    filledRectangle = true;
    filling = false;
    rectangling = false;
    erasing = false;
    ui->modeBox->setText("Drawing a filled rectangle");

    // down the button
    ui->filledRectangleButton->setDown(true);
}

void MainWindow::changeToPainting()
{
    // up all buttons
    ui->fillButton->setDown(false);
    ui->rectangleButton->setDown(false);
    ui->eraserButton->setDown(false);
    ui->filledRectangleButton->setDown(false);

    // change and show the mode
    filling = false;
    rectangling = false;
    filledRectangle = false;
    erasing = false;
    ui->modeBox->setText("painting");
}

void MainWindow::changeToFill()
{
    // up the button
    ui->eraserButton->setDown(false);

    // change and show the mode
    filling = true;
    filledRectangle = false;
    rectangling = false;
    erasing = false;
    ui->modeBox->setText("Filling");

    // down the button
    ui->fillButton->setDown(true);
}

// Press and moving
void MainWindow::mousePressing(int x, int y)
{
    if (!filling && !rectangling && !filledRectangle) {
        if (erasing) {
            emit drawing(x, y, false);
        }
        else {
            emit drawing(x, y, true);
        }
    }
}

void MainWindow::mouseReleasing(int x, int y)
{
    if (x == -1)
        return;

    // just fill
    if (filling) {
        try {
            int tolerance = ui->toleranceSpinBox->value() * 100; // tolerance need to be checked
            emit fill(pressedPositionX, pressedPositionY, tolerance);
        }
        catch (...) {
        }
    }
    // just draw a rectangle
    else if (rectangling) {
        emit drawRectangle(pressedPositionX, pressedPositionY, x, y, false);
    }
    // draw a filled rectangle
    else if (filledRectangle)
        emit drawRectangle(pressedPositionX, pressedPositionY, x, y, true);
}

// First press
void MainWindow::mousePressed(int x, int y)
{
    if (!filling && !rectangling && !filledRectangle) {
        if (erasing) {
            emit drawing(x, y, false);
        }
        else {
            emit drawing(x, y, true);
        }
    }
    // record the pressed position
    else {
        pressedPositionX = x;
        pressedPositionY = y;
    }
}

// Shows the user an error dialog with the given message
void MainWindow::setFrameCount(int frameCount)
{
    ui->frameSelector->setMaximum(frameCount - 1);
}
