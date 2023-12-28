/**
 * @file Trie.cpp
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief A Trie store words (words can only be consist of letter a-z).
 * @version 0.2
 * @date 2022-10-03
 *
 * @copyright Copyright (c) 2022
 *
 */
#include "Trie.h"
using std::swap;

/**
 * @brief Construct a new Trie:: Trie object
 *
 */
Trie::Trie()
{
    terminate = false;
}

/**
 * @brief Copy constructor.
 *
 * @param obj - The trie to be copied.
 */
Trie::Trie(const Trie &obj)
{
    terminate = obj.terminate;

    // Copy all elements in the nextNode of obj to nextNode of this.
    for (auto entry : obj.trieMap)
    {
        trieMap[entry.first] = entry.second;
    }
}

/**
 * @brief Override = operation.
 *
 * @param obj The object on the right of =.
 * @return Trie&
 */
Trie &Trie::operator=(Trie obj)
{
    // Exchange values.
    swap(this->trieMap, obj.trieMap);
    swap(this->terminate, obj.terminate);
    return *this;
}

/**
 * @brief Destroy the Trie:: Trie object
 */
Trie::~Trie()
{
}

/**
 * @brief Add passed word into the tree.
 *
 * @param word - Given word
 */
void Trie::addAWord(string word)
{
    // If the length of word equals 0, all letters in the word has been converted to a tire,
    // add the recursive call has reached the end, so just set the terminate flag.
    if (word.length() == 0)
    {
        terminate = true;
        return;
    }

    char firstChar = word[0];
    // If the trie's nextNode does not contain element on the letter's index, create a new trie and save the address to the index.
    if (!hasNextChar(firstChar))
    {
        trieMap[firstChar] = Trie();
    }

    // Cut the first letter(already handled), then pass the remaining string to the new created trie's addWord method to add the remaining string recursively.
    trieMap[firstChar].addAWord(word.substr(1));
}

bool Trie::isAWord(string word)
{
    // If the recursive call has reached the end, check if the current node is a terminate node, return true if yes, else return false.
    if (word.length() == 0)
    {
        if (terminate)
            return true;
        else
            return false;
    }

    char firstChar = word[0];
    /**
     * @brief if the calculated index already has a trie, the current letter matched.
     *        We continue to pass in the remaining letters to the isAWord of the nextNode to recursively check the exsistent of the remaining letters.
     *        if the calculated index does not has a trie, then the current letter unmatched, return false directly.
     */
    if (!hasNextChar(firstChar))
        return false;
    else
        return trieMap[firstChar].isAWord(word.substr(1));
}

/**
 * @brief A helper method, to check whether the character has the next node in the trie.
 *
 * @param c
 * @return true
 * @return false
 */
bool Trie::hasNextChar(char c)
{
    return trieMap.find(c) != trieMap.end();
}

/**
 * @brief Search for all words in the trie beginning with the given prefix.
 *
 * @param prefix
 * @return vector<string> A list contains all matched words.
 */
vector<string> Trie::allWordsBeginningWithPrefix(string prefix)
{
    vector<string> words;

    // Move the pointer to the node that corrosponds to the last letter in the prefix.
    Trie currentTrie = *this;

    for (long unsigned i = 0; i < prefix.length(); i++)
    {
        char c = prefix[i];

        if (currentTrie.hasNextChar(c))
        {
            currentTrie = currentTrie.trieMap[c];
        }
        // Return empty vector if the trie does not contain the prefix.
        else
        {
            return words;
        }
    }

    // Instead of using recursion, here I use breadth first search algorithm.
    vector<Trie> pointerList;
    vector<string> trackWordsList;

    // Start from the last character of the prefix
    pointerList.push_back(currentTrie);
    trackWordsList.push_back(prefix);

    /**
     * @brief Start from the first node, go through all of the next level nodes of the current node.
     *        If a child node of the scanning node contains a character, add the pointer of this child node into a stack with node pointers.
     *        If the scanning node is the end of a word, add this word into a words container.
     *        If the node pointer stack is not empty, pop a node to scan.
     *
     */
    while (pointerList.size() > 0)
    {

        Trie currentPointer = pointerList.back();
        string currentWord = trackWordsList.back();
        pointerList.pop_back();
        trackWordsList.pop_back();

        // Go through next level nodes of the current node.
        for (auto const &[character, nextTrie] : currentPointer.trieMap)
        {
            string appendedPrefix = currentWord;
            pointerList.push_back(nextTrie);
            appendedPrefix.push_back(character);
            trackWordsList.push_back(appendedPrefix);
        }

        if (currentPointer.terminate)
        {
            words.push_back(currentWord);
        }
    }

    return words;
}