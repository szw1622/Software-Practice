/**
 * @file mainwindow.cpp
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief Design of the main window.
 * @version 0.1
 * @date 2022-10-26
 *
 * @copyright Copyright (c) 2022
 *
 */
#include "mainwindow.h"
#include "ui_mainwindow.h"

/**
 * @brief MainWindow::MainWindow
 *        Set up the sheet.
 * @param parent
 */
MainWindow::MainWindow(GameData &gameData, QWidget *parent)
    : QMainWindow(parent), ui(new Ui::MainWindow) {
  ui->setupUi(this);

  // Set all of the bottons on the sheet
  ui->redButton->setStyleSheet(
      QString("QPushButton {background-color: rgb(150,0,0);} "
              "QPushButton:pressed {background-color: rgb(255,0,0);}"));
  ui->blueButton->setStyleSheet(
      QString("QPushButton {background-color: rgb(0,0,150);} "
              "QPushButton:pressed {background-color: rgb(0,0,255);}"));
  ui->yellowButton->setStyleSheet(
      QString("QPushButton {background-color: rgb(150,150,0);} "
              "QPushButton:pressed {background-color: rgb(255,255,0);}"));
  ui->greenButton->setStyleSheet(
      QString("QPushButton {background-color: rgb(0,150,0);} "
              "QPushButton:pressed {background-color: rgb(0,255,0);}"));

  ui->nextRoundButton->setEnabled(false);
  setButtonsPosition(200, 0);
  setButtonsEnabled(false);

  // Connect events while button is clicked
  connect(ui->startButton, &QPushButton::clicked, &gameData,
          &GameData::startGameButtonPushed);
  connect(ui->nextRoundButton, &QPushButton::clicked, &gameData,
          &GameData::nextRoundButtonPushed);
  connect(ui->redButton, &QPushButton::clicked, this,
          &MainWindow::redButtonPushed);
  connect(ui->blueButton, &QPushButton::clicked, this,
          &MainWindow::blueButtonPushed);
  connect(ui->yellowButton, &QPushButton::clicked, this,
          &MainWindow::yellowButtonPushed);
  connect(ui->greenButton, &QPushButton::clicked, this,
          &MainWindow::greenButtonPushed);
  connect(this, &MainWindow::buttonPushed, &gameData,
          &GameData::colorButtonPushed);

  connect(&gameData, &GameData::down, this, &MainWindow::downButton);
  connect(&gameData, &GameData::up, this, &MainWindow::upAllButton);

  connect(&gameData, &GameData::userTurn, this, &MainWindow::userTurn);
  connect(&gameData, &GameData::readyToFlash, this, &MainWindow::beforeFlash);

  connect(&gameData, &GameData::userPushesIncreased, ui->progressBar,
          &QProgressBar::setValue);
  connect(&gameData, &GameData::readyToNextRound, this,
          &MainWindow::readyToNextRound);
  connect(&gameData, &GameData::colorUnmatch, this, &MainWindow::gameOver);
  connect(&gameData, &GameData::buttonsRotated, this,
          &MainWindow::setButtonsPosition);
}

/**
 * @brief MainWindow::~MainWindow The distructor
 */
MainWindow::~MainWindow() { delete ui; }

// Send signal to tell the model which button is pushed
void MainWindow::redButtonPushed() { emit buttonPushed(GameData::RED); }
void MainWindow::greenButtonPushed() { emit buttonPushed(GameData::GREEN); }
void MainWindow::blueButtonPushed() { emit buttonPushed(GameData::BLUE); }
void MainWindow::yellowButtonPushed() { emit buttonPushed(GameData::YELLOW); }

/**
 * @brief MainWindow::readyToNextRound
 */
void MainWindow::readyToNextRound() {
  ui->infoBar->setText(QString("Geat job, click on next round when ready."));
  setButtonsEnabled(false);
  ui->nextRoundButton->setEnabled(true);
}

/**
 * @brief MainWindow::beforeFlash - Do preparation for flashing
 */
void MainWindow::beforeFlash() {
  // Initial the ui while starting
  ui->progressBar->setValue(0);
  ui->infoBar->setText(QString("Memorize it!"));
  ui->startButton->setEnabled(false);
  ui->nextRoundButton->setEnabled(false);
  setButtonsEnabled(false);
}

/**
 * @brief MainWindow::userTurn - Set ui and button for the player turn
 */
void MainWindow::userTurn() {
  ui->infoBar->setText(QString("Your turn"));
  setButtonsEnabled(true);
}

/**
 * @brief MainWindow::setButtonsEnabled - A helper method to set all of the
 * buttons enabled or disable
 * @param enabled
 */
void MainWindow::setButtonsEnabled(bool enabled) {
  ui->redButton->setEnabled(enabled);
  ui->blueButton->setEnabled(enabled);
  ui->yellowButton->setEnabled(enabled);
  ui->greenButton->setEnabled(enabled);
}

/**
 * @brief MainWindow::gameOver - Event when the user click the wrong button
 */
void MainWindow::gameOver() {
  ui->startButton->setEnabled(true);
  setButtonsEnabled(false);
  ui->infoBar->setText(QString("Game Over"));
}

/**
 * @brief MainWindow::setButtonsPosition - A helper function for rotating
 */
void MainWindow::setButtonsPosition(double radius, double angle) {
  int centerX = this->size().width() / 2;
  int centerY = this->size().height() / 2 - 50;

  int buttonSizeX = 200;
  int buttonSizeY = 150;

  double buttonPositionX = centerX - buttonSizeX / 2.0;
  double buttonPositionY = centerY - buttonSizeY / 2.0;

  double sinRadius = radius * qSin(angle / 180.0);
  double cosRadius = radius * qCos(angle / 180.0);

  ui->redButton->setGeometry(QRect(buttonPositionX - cosRadius,
                                   buttonPositionY - sinRadius, buttonSizeX,
                                   buttonSizeY));
  ui->blueButton->setGeometry(QRect(buttonPositionX + sinRadius,
                                    buttonPositionY - cosRadius, buttonSizeX,
                                    buttonSizeY));
  ui->yellowButton->setGeometry(QRect(buttonPositionX - sinRadius,
                                      buttonPositionY + cosRadius, buttonSizeX,
                                      buttonSizeY));
  ui->greenButton->setGeometry(QRect(buttonPositionX + cosRadius,
                                     buttonPositionY + sinRadius, buttonSizeX,
                                     buttonSizeY));
}

/**
 * @brief MainWindow::upAllButton - Set all button up
 */
void MainWindow::upAllButton() {
  ui->blueButton->setDown(false);
  ui->redButton->setDown(false);
  ui->greenButton->setDown(false);
  ui->yellowButton->setDown(false);
}

/**
 * @brief MainWindow::downButton - Set the selected color button down
 * @param color
 */
void MainWindow::downButton(int color) {
  if (color == GameData::RED) {
    ui->redButton->setDown(true);
  }
  if (color == GameData::GREEN) {
    ui->greenButton->setDown(true);
  }
  if (color == GameData::BLUE) {
    ui->blueButton->setDown(true);
  }
  if (color == GameData::YELLOW) {
    ui->yellowButton->setDown(true);
  }
}
