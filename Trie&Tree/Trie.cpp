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
 * This class represents a trie tree
 */

#include "Trie.h"
#include <iostream>
#include <vector>
#include <string>

using namespace std;

/// @brief A default constructor, initialize the root node of the tree
Trie::Trie()
{
    root = new Node;
}

/// @brief A destructor, remove the root of the tree
Trie::~Trie()
{
    delete root;
}

/// @brief A copy constructor
/// @param copyTrie 
Trie::Trie(const Trie &cloneSample)
{
    root = new Node(*(cloneSample.root));
    for (int i = 0; i < (int)cloneSample.words.size(); i++)
    {
        words.push_back(cloneSample.words[i]);
    }
}

Trie &Trie::operator=(Trie cloneSample)
{
    std::swap(root, cloneSample.root);
    std::swap(words, cloneSample.words);
    return *this;
}

/// @brief Add passed word into the tree.
/// @param input - Given word
void Trie::addAWord(string input)
{
    // If no input, just return
    if (input.length() == 0)
    {
        return;
    }
    // Add word from the root
    Node *scanning = root;

    for (int i = 0; i < (int)input.length(); i++)
    {
        // Get the index of the character
        int index = input[i] - 'a';
        // If the node with this character is not exist in the tree
        if (scanning->childNode[index] == nullptr)
        {
            // Make space for the new node
            scanning->hasNext = true;
            scanning->childNode[index] = new Node();
            scanning->childNode[index]->character = input[i];
        }
        // Move to the added node
        scanning = scanning->childNode[index];
    }
    // Record the added word
    words.push_back(input);
    // Mark the last node as the end of word.
    scanning->is_end_of_word = true;
}

/// @brief Check whether the input word is already in the tree
/// @param input - The word need to be looked up in the tree
/// @return If the word is found in the Trie, 
///         the method should return true, otherwise return false. 
bool Trie::isAWord(std::string input)
{
    // A Trie should report that an empty string is not in the Trie.
    if (input.length() == 0)
    {
        return false;
    }
    // Scan word from the root
    Node *scanning = root;
    for (int i = 0; i < (int)input.length(); i++)
    {
        int index = input[i] - 'a';
        if (scanning->childNode[index] != nullptr)
        {
            // If the next character is already in the tree
            // Move the scanner to the next node
            scanning = scanning->childNode[index];
        }
        else
        {
            return false;
        }
    }
    return scanning->is_end_of_word;
}

/// @brief This method can get all the words in the Trie that begin with the passed in argument prefix string
/// @param prefix - The starting characters
/// @return - Vector contains words in the tree that begin with prefix
///         - Empty vector is no word
///         - All words in the tree if prefix is empty
std::vector<std::string> Trie::allWordsBeginningWithPrefix(std::string prefix)
{
    // Create the container vector to store the result word strings
    std::vector<std::string> resultWords;
    // Return all words if prefix is empty
    if (prefix.length() == 0)
    {
        return words;
    }

    // Scan from root
    Node *lastNodeOfPrefix = root;
    for (int i = 0; i < (int)prefix.length(); i++)
    {
        int index = prefix[i] - 'a';
        // Get the last character of the prefix
        lastNodeOfPrefix = lastNodeOfPrefix->childNode[index];
    }
        // If the last character of the prefix is not in the tree
    if (lastNodeOfPrefix == nullptr)
        // Return empty vector
        return resultWords;
        // If the prefix is a word, add it into the result
    if (lastNodeOfPrefix->is_end_of_word)
        resultWords.push_back(prefix);
        // Recusive search
    preTraverse(lastNodeOfPrefix, resultWords, prefix);
    return resultWords;
}

/// @brief This is a helper method to do a recusive search for the words begin with prefix
/// @param scanning - The last node
/// @param resultWords - The vector to store the words
/// @param buildingWord 
void Trie::preTraverse(Node *scanning, std::vector<std::string> &resultWords, std::string buildingWord)
{
    // Return if the scanning node is the last character on this branch
    if (!scanning->hasNext)
        return;
    // Go over every child node of the scanning node
    for (Node *child : scanning->childNode)
    {
        // If there is a character among child nodes of scanning node
        if (child != nullptr)
        {
            if (child->is_end_of_word)
            {
                buildingWord = buildingWord + child->character;
                resultWords.push_back(buildingWord);
            }
            preTraverse(child, resultWords, buildingWord + child->character);
        }
    }
}