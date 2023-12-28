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
 * This class holds the data necessary for Haru to work and that provides class methods to access enough haru functionality
 * to allow other code to place single letters on a pdf page with a position and orientation.
 */

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <math.h>
#include "hpdf.h"
#include "HaruPDF.h"

/// @brief The constructor of haru pdf, set up the basic features of haru pdf
HaruPDF::HaruPDF()
{
    pdf = HPDF_New(NULL, NULL);
    page = HPDF_AddPage(pdf);
    HPDF_Page_SetSize(page, HPDF_PAGE_SIZE_A5, HPDF_PAGE_PORTRAIT);

    font = HPDF_GetFont(pdf, "Courier-Bold", NULL);

    HPDF_Page_SetTextLeading(page, 20);
    HPDF_Page_SetGrayStroke(page, 0);

    HPDF_Page_BeginText(page);

    HPDF_Page_SetFontAndSize(page, font, 30);
}

/// @brief argv are the command line arguments,
///        argv[0] is the name of the executable program
///        This makes an output pdf named after the program's name
/// @param filename
void HaruPDF::namePDF(char *filename)
{
    strcpy(fname, filename);
    strcat(fname, ".pdf");
}

/// @brief Place characters one the page.
/// @param character
void HaruPDF::placeCharacter(char character)
{
    char buf[2];

    buf[0] = character; // The character to display
    buf[1] = 0;
    HPDF_Page_ShowText(page, buf);
}

/// @brief Modify how to display the character
/// @param rad1 The Letter Angle
/// @param x The x coordinate of character on the page
/// @param y The y coordinate of character on the page
void HaruPDF::setTextMatrix(double rad1, double x, double y)
{
    HPDF_Page_SetTextMatrix(page,
                                cos(rad1), sin(rad1), -sin(rad1), cos(rad1),
                                x, y);
}

/// @brief Save the pdf
void HaruPDF::saveToFile(){
    /* end writing */
    HPDF_Page_EndText (page);
    /* save the document to a file */
    HPDF_SaveToFile (pdf, fname);
    /* clean up */
    HPDF_Free (pdf);
}
