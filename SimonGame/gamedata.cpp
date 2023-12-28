/**
 * @file gameData.cpp
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief Contains the game data.
 * @version 0.1
 * @date 2022-10-26
 *
 * @copyright Copyright (c) 2022
 *
 */
#include "gamedata.h"

/**
 * @brief GameData::GameData - Inherit from QBject
 * @param parent
 */
GameData::GameData(QObject *parent) : QObject{parent} {
  rotateTimer = new QTimer();
  connect(rotateTimer, &QTimer::timeout, this, &GameData::rotate);
}

/**
 * @brief GameData::colorButtonPushed - Event while the player pushes the button
 * @param color
 */
void GameData::colorButtonPushed(int color) {
   // Get the correct color
  int queueColor = flashSequence.front();
  flashSequence.pop();
   // Check color
  if (color == queueColor) {
      // tell the view the player click correctly
    emit userPushesIncreased(++userPushes / (double)totalPushes * 100);
      // If is the last button
    if (userPushes == totalPushes) {
        // tell the view finish this round
      emit readyToNextRound();
      rotateTimer->stop();
    }
    // game over
  } else {
    rotateTimer->stop();

    emit colorUnmatch();
  }
}

/**
 * @brief MainWindow::startButtonPushed - Event while start button is clicked
 */
void GameData::startGameButtonPushed() {
  // Initial counting variables
  angle = 0;
  totalPushes = startPushes;
  computerPushes = 0;
  userPushes = 0;

  // Disable buttons, prepare for gaming
  flashSequence = queue<int>();
  emit readyToFlash();
  emit buttonsRotated(radius, angle);
  letButtonDown();
}

/**
 * @brief GameData::letButtonDown - Computer pushes a random button once
 */
void GameData::letButtonDown() {
  if (computerPushes++ == totalPushes) {
    emit userTurn();
  } else {
    int color = QRandomGenerator::global()->bounded(4);
    flashSequence.push(color);
    emit down(color);
    QTimer::singleShot(3000 / totalPushes, this, &GameData::upAllButton);
  }
}

/**
 * @brief GameData::upAllButton - Let all buttons up
 */
void GameData::upAllButton() {
  emit up();
    // make flash speed up next round
  QTimer::singleShot(3000 / totalPushes, this, &GameData::letButtonDown);
}

/**
 * @brief GameData::nextRoundButtonPushed - Event while next round button is pushed
 */
void GameData::nextRoundButtonPushed() {
    // One more move
  totalPushes++;
   // Check the level whether over than start retate level.
  if (totalPushes >= levelUpPushes) {
    rotateTimer->start(200 / totalPushes);
  } else {
    angle = 0;
  }
  computerPushes = 0;
  userPushes = 0;

  flashSequence = queue<int>();
  emit readyToFlash();

  letButtonDown();
}

/**
 * @brief GameData::rotate
 */
void GameData::rotate() { emit buttonsRotated(radius, ++angle); }
