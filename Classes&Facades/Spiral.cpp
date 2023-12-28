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
 * This class contains the design of spiral and getter methods.
 */

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <math.h>
#include "Spiral.h"
#include "hpdf.h"

/// @brief The constructor of a spiral object, 
///        which is used to tell haru pdf how to show the character
/// @param enter_center_X The given x coordinate of center of the spiral
/// @param enter_center_Y The given y coordinate of center of the spiral
/// @param enter_radius The given radius the spiral
Spiral::Spiral(double enter_center_X, double enter_center_Y, double enter_angle, double enter_radius)
{
    centerX = enter_center_X;
    centerY = enter_center_Y;
    radius = enter_radius;
    angle2 = enter_angle + 90;

    // Enforce a minimum radius to preserve the appearance of the text
    // const double min_r = 30;
    // if(radius < min_r)
    //     radius = min_r;

    rad1 = (angle2 - 90) / 180 * M_PI;  // the angle of the letter
    rad2 = angle2 / 180 * M_PI;

    // coordinate of character
    x = (centerX) + cos(rad2) * radius;
    y = (centerY) + sin(rad2) * radius;
}

/// @brief Overload ++ pre-increment operator
Spiral& Spiral::operator++(){
    
    double radius_growth_rate = 1;
    radius += radius_growth_rate;
    double angle_turn_rate = 1000 * (1/radius);
    angle2 -= angle_turn_rate;

    rad1 = (angle2 - 90) / 180 * M_PI;
    rad2 = angle2 / 180 * M_PI;

    return *this;
}

/// @brief Overload ++ post-increment operator
Spiral Spiral::operator++ (int) { // postfix ++
    Spiral result(*this);
    ++(*this);
    return result;
}

// Return the x coordinate of character
double Spiral::getTextX(){
    x = centerX + cos(rad2) * radius;
    return x;
}

// Return the y coordinate of character
double Spiral::getTextY(){
    y = centerY + sin(rad2) * radius;
    return y;
}

// Return the angle of character
double Spiral::getLetterAngle(){
    return rad1;
}

