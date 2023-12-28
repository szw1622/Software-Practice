/*
 * Author:      Zhuowen Song
 * Date:        9/12/2022
 * Course:      CS 3505, University of Utah, School of Computing
 * Assignment:  Classes, Facades, and Makefiles
 * Copyright:   CS 3505 and Zhuowen Song - This work may not be copied for use in Academic Coursework.
 *
 * I, Zhuowen Song, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  All references used in the completion of the assignment are cited in my README file.
 *
 * File Contents:
 * This class provides the interface and all implementation should be done in HaruPDF.cpp
 */

#include "hpdf.h"

class HaruPDF 
{
    HPDF_Doc  pdf;
    HPDF_Page page;
    char fname[256];
    HPDF_Font font;
    float rad1;
    unsigned int i;

public:

HaruPDF();
    void namePDF(char*);
    void placeCharacter(char);
    void setTextMatrix(double, double, double);
    void saveToFile();
};