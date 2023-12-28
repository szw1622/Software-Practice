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
 * This class provides the interface and all implementation should be done in Node.cpp
 */

#ifndef NODE_H
#define NODE_H

#include <iostream>
#include <vector>
using namespace std;

class Node
{

public:
    // Branches of the trie for the alphabet
    Node* childNode[26];
    // Check the node if is the end of a word
    bool is_end_of_word = false;
    // The character this node is holding
    string character;
    // The flag for whether has a next node
    bool hasNext = false;
    // A default constructor.
    Node();
    // A destructor
    ~Node();
    // A copy constructor
    Node(const Node&);
    // copy assignment operator
    Node& operator=(const Node);
};

#endif