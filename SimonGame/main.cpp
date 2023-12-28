/**
 * @file main.cpp
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief The main class to show the window.
 * @version 0.1
 * @date 2022-10-25
 *
 * @copyright Copyright (c) 2022
 *
 */
#include "mainwindow.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    GameData g;
    MainWindow w(g);
    w.show();
    return a.exec();
}
