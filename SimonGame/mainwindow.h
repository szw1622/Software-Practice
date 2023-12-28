/**
 * @file mainwindow.h
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief Header file for mainwindow.cpp.
 * @version 0.1
 * @date 2022-10-25
 *
 * @copyright Copyright (c) 2022
 *
 */
#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QTimer>
#include <QDebug>
#include <QtMath>
#include "gamedata.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE


class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(GameData& gameData, QWidget *parent = nullptr);
    ~MainWindow();

    void startGame();
    void userTurn();
    void setButtonsEnabled(bool enabled);
    void gameOver();


private:
    Ui::MainWindow *ui;


signals:
    void buttonPushed(int color);

public slots:
    void beforeFlash();
    void setButtonsPosition(double radius, double angle);
    void upAllButton();
    void downButton(int color);
    void readyToNextRound();

    void blueButtonPushed();
    void redButtonPushed();
    void yellowButtonPushed();
    void greenButtonPushed();
};
#endif // MAINWINDOW_H
