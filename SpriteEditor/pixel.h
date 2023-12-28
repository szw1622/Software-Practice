/**
 * @file pixel.h
 * @author Keming Chen, Matthew Whitaker
 * @brief Header file for pixel.cpp
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Zhuowen Song
 *
 * @copyright Copyright (c) 2022
 *
 */

#ifndef PIXEL_H
#define PIXEL_H

class Pixel {
public:
    /**
     * @brief Pixel Creates a Pixel object with the default color of white
     */
    Pixel();

    /**
     * @brief Pixel Creates a Pixel object
     * @param red The red component of this color (0-255)
     * @param green The green component of this color (0-255)
     * @param blue The blue component of this color (0-255)
     * @param alpha The alpha component of this color (0-255)
     */
    Pixel(int red, int green, int blue, int alpha);

    /**
     * @brief getRed
     * @return The red component of this color
     */
    int getRed();

    /**
     * @brief getGreen
     * @return The green component of this color
     */
    int getGreen();

    /**
     * @brief getBlue
     * @return The blue component of this color
     */
    int getBlue();

    /**
     * @brief getAlpha
     * @return The alpha component of this color
     */
    int getAlpha();

private:
    int red, green, blue, alpha;
};

#endif // PIXEL_H
