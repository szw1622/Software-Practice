/**
 * @file pixel.cpp
 * @author Keming Chen, Matthew Whitaker
 * @brief Defines methods for the Pixel class.
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Zhuowen Song
 *
 * @copyright Copyright (c) 2022
 *
 */

#include "pixel.h"

Pixel::Pixel()
    : red(255)
    , green(255)
    , blue(255)
    , alpha(255)
{
}

Pixel::Pixel(int red, int green, int blue, int alpha)
    : red(red)
    , green(green)
    , blue(blue)
    , alpha(alpha)
{
}

// Returns the red component of the color
int Pixel::getRed()
{
    return red;
}

// Returns the green component of the color
int Pixel::getGreen()
{
    return green;
}

// Returns the blue component of the color
int Pixel::getBlue()
{
    return blue;
}

// Returns the alpha component of the color
int Pixel::getAlpha()
{
    return alpha;
}
