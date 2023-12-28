/**
 * @file image.cpp
 * @author Keming Chen, Matthew Whitaker
 * @brief Implements methods for the Image class
 * @version 0.1
 * @date 2022-11-15
 * @reviewer Keming Chen
 *
 * @copyright Copyright (c) 2022
 *
 */

#include "image.h"
#include <optional>

// Creates a new Image instance with the given width and height
Image::Image(int width, int height)
    : width(width)
    , height(height)
{
    pixels = new Pixel*[width];
    for (int x = 0; x < width; ++x) {
        pixels[x] = new Pixel[height];
        for (int y = 0; y < height; ++y) {
            pixels[x][y] = Pixel();
        }
    }
}

// Cleans up memory for all of these pixels
Image::~Image()
{
    for (int i = 0; i < width; i++) {
        delete[] pixels[i];
    }
    delete[] pixels;
}

// Copies the data from one Image to a new Image.
Image::Image(const Image& imageToCopy)
{
    width = imageToCopy.width;
    height = imageToCopy.height;

    pixels = new Pixel*[height];
    for (int x = 0; x < width; ++x) {
        pixels[x] = new Pixel[height];
        for (int y = 0; y < height; ++y) {
            pixels[x][y] = imageToCopy.pixels[x][y];
        }
    }
}

// Assigns the value of the given image to this image, making a copy of all the
// data.
Image& Image::operator=(Image other)
{
    std::swap(width, other.width);
    std::swap(height, other.height);
    std::swap(pixels, other.pixels);

    return *this;
}

void Image::setPixel(int x, int y, Pixel pixel)
{
    if (x >= 0 && x < width && y >= 0 && y < height)
        pixels[x][y] = pixel;
}

const Pixel Image::getPixel(int x, int y)
{
    if (x >= 0 && x < width && y >= 0 && y < height)
        return pixels[x][y];
    return Pixel();
}
