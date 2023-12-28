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
 * This class provides the interface and all implementation should be done in Spiral.cpp
 */

#include "hpdf.h"

class Spiral 
{
    double centerX;
    double centerY;
    double angle;
    double radius;
    double radius_growth_rate;
    double angle_turn_rate;

    double angle2;
    double rad1;
    double rad2;
    double x;
    double y;

public:

Spiral(double, double, double, double);

    Spiral& operator++ ();   
    Spiral operator++ (int);


    double getTextX();
    double getTextY();
    double get_spiral_angle();
    double getLetterAngle();

};