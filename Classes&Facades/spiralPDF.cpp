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
 * This is a test program for this project, which can make a test pdf file.
 */

#include "Spiral.h"
#include "HaruPDF.h"
#include <stdlib.h>
#include <stdio.h>
#include <iostream>

// The main method which recieve the testing content and make file
int main (int argc, char **argv) 
{
    // Set up the pdf and the spiral
    HaruPDF pdf;
    Spiral test_spiral = Spiral(0,0, 90.0, 100.0);

    // name the pdf
    pdf.namePDF(argv[0]);

    // If wrong input
    if (argc < 2) {
        std::cout << "Error: No input" << std::endl;
        return 0;
    }

    // split the input text
    char *text = argv[1];

    // go over every character of the input text
    for (int i = 0; text[i]!= 0; i++) 
    {
        pdf.setTextMatrix(test_spiral.getLetterAngle(), test_spiral.getTextX() + 210, test_spiral.getTextY() + 300);
        pdf.placeCharacter(text[i]);

        // let the spiral go ahead
        test_spiral++;
    }
    
    pdf.saveToFile();
}