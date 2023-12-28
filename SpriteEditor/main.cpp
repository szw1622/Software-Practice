/**
 * @file main.cpp
 * @author Joshua Beatty, Matthew Whitaker
 * @brief Main method for project
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Zhuowen Song
 *
 * @copyright Copyright (c) 2022
 *
 */

#include "mainwindow.h"
#include "model.h"

#include <QApplication>
#include <QIcon>

int main(int argc, char* argv[])
{
    QApplication a(argc, argv);

    Model model;
    MainWindow w(model);

    w.setWindowIcon(QIcon(":/icons/Pallete.png"));
    w.setWindowTitle("Sprite Editor");
    w.show();

    return a.exec();
}
