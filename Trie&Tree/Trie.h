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
 * This class provides the interface and all implementation should be done in Trie.cpp
 */

#include <string>
#include <vector>
#include "Node.h"

class Trie
{

public:
    Node *root;
    // Store all of the words in the tree
    std::vector<std::string> words;
    // A default constructor.
    Trie();
    // A destructor
    ~Trie();
    // A copy constructor
    Trie(const Trie &);
    // copy assignment operator
    Trie &operator=(const Trie copyTrie);
    
    void addAWord(std::string);

    bool isAWord(std::string);

    std::vector<std::string> allWordsBeginningWithPrefix(std::string);

    void preTraverse(Node *, std::vector<std::string> &, std::string);
};