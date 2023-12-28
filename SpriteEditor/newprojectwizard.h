/**
 * @file newprojectwizard.h
 * @author Matthew Whitaker
 * @brief Header file for newprojectwizard.cpp
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Zhuowen Song
 *
 * @copyright Copyright (c) 2022
 *
 */

#ifndef NEWPROJECTWIZARD_H
#define NEWPROJECTWIZARD_H

#include <QWizard>

class NewProjectWizard : public QWizard {
    Q_OBJECT
public:
    /**
     * @brief NewProjectWizard Instantiates the new project wizard and prepares it to be shown
     */
    NewProjectWizard();

    /**
     * @brief accept Called when the user finishes the new project wizard
     */
    void accept() override;

private:
    int width;
    int height;

private slots:
    /**
     * @brief setWidth Allows the user to set the width for the new project
     * @param width
     */
    void setWidth(int width);

    /**
     * @brief setHeight Allows the user to set the height for the new project
     * @param height
     */
    void setHeight(int height);

signals:
    /**
     * @brief newProject Emits when a new project should be created with a given width and height
     * @param width
     * @param height
     */
    void newProject(int width, int height);
};

#endif // NEWPROJECTWIZARD_H
