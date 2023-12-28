/**
 * @file mainwindow.h
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief Header file for gamedata.cpp.
 * @version 0.1
 * @date 2022-10-26
 *
 * @copyright Copyright (c) 2022
 *
 */
#ifndef GAMEDATA_H
#define GAMEDATA_H

#include <QObject>
#include <QTimer>
#include <queue>
#include <QRandomGenerator>

using std::queue;

class GameData : public QObject
{
    Q_OBJECT
public:
    explicit GameData(QObject *parent = nullptr);
    const static int RED = 0, GREEN = 1, BLUE = 2, YELLOW = 3;

  private:

    queue<int> flashSequence;
    int startPushes=3;

    int totalPushes;
    int computerPushes;
    int userPushes;
    int levelUpPushes=5;

    double angle;
    double radius=200;

    QTimer *flashTimer;
    QTimer *rotateTimer;

public slots:

    void startGameButtonPushed();
    void nextRoundButtonPushed();
    void colorButtonPushed(int color);
    void letButtonDown();
    void upAllButton();
    void rotate();

signals:
    void userPushesIncreased(int value);
    void colorUnmatch();
    void readyToFlash();
    void down(int color);
    void up();
    void userTurn();
    void readyToNextRound();
    void buttonsRotated(double radius, double angle);
};

#endif // GAMEDATA_H
