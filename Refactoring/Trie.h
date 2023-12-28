/**
 * @file Trie.h
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief Header file for Trie.cpp.
 * @version 0.2
 * @date 2022-10-3
 *
 * @copyright Copyright (c) 2022
 *
 */
#ifndef TIRE_H
#define TIRE_H
#include <string>
#include <vector>
#include <map>
using std::map;
using std::string;
using std::vector;

class Trie
{
private:
    // Branches of the trie for the alphabet
    // change the branches structure from a Node* branches[26] to a map<char, Node>
    map<char, Trie> trieMap;
    // Check the node if is the terminate of a word
    bool terminate;

public:
    /**
     * @brief Construct a new Trie:: Trie object
     *
     */
    Trie();
    /**
     * @brief Copy constructor.
     *
     * @param obj The trie to be copied.
     */
    Trie(const Trie &obj);

    /**
     * @brief Override = operation.
     *
     * @param obj The object on the right of =.
     * @return Trie&
     */
    Trie &operator=(Trie obj);
    /**
     * @brief Destroy the Trie:: Trie object
     *
     */
    ~Trie();
    /**
     * @brief Add a word to the tire.
     *
     * @param word
     */
    void addAWord(string word);
    /**
     * @brief Check whether the given word is in the trie.
     *
     * @param word
     * @return true
     * @return false
     */
    bool isAWord(string word);
    /**
     * @brief Search for all words in the trie beginning with the given prefix.
     *
     * @param prefix
     * @return vector<string> A list contains all matched words.
     */
    vector<string> allWordsBeginningWithPrefix(string prefix);

    
    bool hasNextChar(char c);
};
#endif