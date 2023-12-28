/*
 * Author:      Zhuowen Song
 * Date:        9/22/2022
 * Course:      CS 3505, University of Utah, School of Computing
 * Assignment:  Classes, Facades, and Makefiles
 * Copyright:   CS 3505 and Zhuowen Song - This work may not be copied for use in Academic Coursework.
 *
 * I, Zhuowen Song, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.
 *
 * File Contents:
 * This class represents a node of the trie tree, which can contain characters 'a-z'
 * Every node can have child node. The number of child nodes is up to 26.
 */

#include "Node.h"

#include <vector>
#include <string>
#include <iostream>
using namespace std;

/// @brief The dault constructor
Node::Node()
{
    // Set pointers that are pointing to a meaningless memory address as nullptr
    for (int i = 0; i < 26; i++)
    {
        childNode[i] = nullptr;
    }
}

/// @brief A copy constructor
Node::Node(const Node &cloneSample)
    : is_end_of_word(cloneSample.is_end_of_word)
{
    is_end_of_word = cloneSample.is_end_of_word;
    character = cloneSample.character;
    hasNext = cloneSample.hasNext;

    // Copy every child node from clone sample
    for (unsigned i = 0; i < 26; i++)
    {
        if (cloneSample.childNode[i])
            childNode[i] = new Node(*cloneSample.childNode[i]);
        else
            childNode[i] = nullptr;
    }
}

/// @brief copy assignment operator
/// @param cloneSample 
/// @return A copied node
Node &Node::operator=(Node cloneSample)
{
    std::swap(childNode, cloneSample.childNode);
    std::swap(is_end_of_word, cloneSample.is_end_of_word);
    std::swap(character, cloneSample.character);
    std::swap(hasNext, cloneSample.hasNext);
    return *this;
}

/// @brief A destructor, delect every child node of this node
Node::~Node()
{
    for (unsigned int i = 0; i < 26; i++)
    {
        delete childNode[i];
    }
}