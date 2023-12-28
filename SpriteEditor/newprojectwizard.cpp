/**
 * @file newprojectwizard.cpp
 * @author Matthew Whitaker
 * @brief Defines methods for the new project wizard
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Zhuowen Song
 *
 * @copyright Copyright (c) 2022
 *
 */

#include "newprojectwizard.h"

#include <QWizard>
#include <QDialog>
#include <QLabel>
#include <QSpinBox>
#include <QVBoxLayout>

NewProjectWizard::NewProjectWizard()
    : width(32)
    , height(32)
{
    // Create a page in the wizard
    QWizardPage* newProjectPage = new QWizardPage(this);
    newProjectPage->setTitle("Create New Project");
    newProjectPage->setButtonText(QWizard::WizardButton::FinishButton, "Create Project");

    // Add labels and spin boxes to layout
    QLabel* introLabel = new QLabel(tr("Select size for new project:"));
    introLabel->setWordWrap(true);

    QLabel* widthLabel = new QLabel("Width: ");

    // Setup width spin box
    QSpinBox* widthSpinBox = new QSpinBox;
    widthSpinBox->setMaximum(512);
    widthSpinBox->setMinimum(2);
    widthSpinBox->setValue(32);
    connect(widthSpinBox, &QSpinBox::valueChanged, this, &NewProjectWizard::setWidth);

    QLabel* heightLabel = new QLabel("Height: ");

    // Setup height spin box
    QSpinBox* heightSpinBox = new QSpinBox;
    heightSpinBox->setMaximum(512);
    heightSpinBox->setMinimum(2);
    heightSpinBox->setValue(32);
    connect(heightSpinBox, &QSpinBox::valueChanged, this, &NewProjectWizard::setHeight);

    // Add everything to the layout
    QVBoxLayout* layout = new QVBoxLayout;
    layout->addWidget(introLabel);
    layout->addWidget(widthLabel);
    layout->addWidget(widthSpinBox);
    layout->addWidget(heightLabel);
    layout->addWidget(heightSpinBox);
    newProjectPage->setLayout(layout);

    // Initialize the wizard with the page
    addPage(newProjectPage);
    setModal(true);
}

// Overridden from base class
void NewProjectWizard::accept()
{
    emit newProject(width, height);

    QDialog::accept();
}

void NewProjectWizard::setWidth(int width)
{
    this->width = width;
}

void NewProjectWizard::setHeight(int height)
{
    this->height = height;
}
