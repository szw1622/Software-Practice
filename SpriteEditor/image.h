/**
 * @file image.h
 * @author Joshua Beatty, Keming Chen, Matthew Whitaker
 * @brief Header file for image.cpp
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Keming Chen
 *
 * @copyright Copyright (c) 2022
 *
 */

#ifndef IMAGE_H
#define IMAGE_H

#include "pixel.h"

class Image {

public:
    /**
     * @brief Image Constructs a new frame (image) with the given width and height.
     */
    Image(int width, int height);

    // Rule of 3 destructor, copy constructor, and assignment operator, for memory management

    /**
     * Cleans up image resources and frees memory
     */
    ~Image();

    /**
     * @brief Image Copy constructor. Creates an independent copy of imageToCopy
     */
    Image(const Image& imageToCopy);

    /**
     * @brief operator= Assignment operator. Creates an independent copy of other, and then points this image to that copy, and frees newly unused resources.
     */
    Image& operator=(Image other);

    /**
     * @brief setPixel Sets the pixel at the given x and y location
     * @param x
     * @param y
     * @param pixel
     */
    void setPixel(int x, int y, Pixel pixel);

    /**
     * @brief getPixel Gets the pixel at the given x and y location
     * @param x
     * @param y
     * @return
     */
    const Pixel getPixel(int x, int y);

    // Public members
    int width;
    int height;

private:
    // Holds a two-dimensional array of colors (pixels)
    Pixel** pixels;
};

#endif // IMAGE_H
