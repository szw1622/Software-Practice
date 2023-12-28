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
 * This is a test file for trie class
 */

#include <iostream>
#include <fstream>
#include "Trie.h"

using namespace std;

int main(int argc, char **argv)
{
    ifstream dictionary_words_file;
    ifstream queries_words_file;

    // If the input command is wrong
    if (argc != 3)
    {
        cout << "Error input" << endl;
        return 0;
    }

    string dictionary_file_name = argv[1];
    string queries_file_name = argv[2];

    Node testNode;
    Trie testTrie;

    dictionary_words_file.open(dictionary_file_name);

    // Create a scanner to scan each line of file
    string scanningLine;

    if (dictionary_words_file.is_open())
    {
        while (dictionary_words_file >> scanningLine)
        {
            testTrie.addAWord(scanningLine);
        }

        // Test copy trie
        Trie *testCopyTrie = new Trie(testTrie);
        while (dictionary_words_file >> scanningLine)
        {
            if (!testCopyTrie->isAWord(scanningLine))
            {
                cout << ("Fail copy") << endl;
            }
        }
        dictionary_words_file.close();
        delete testCopyTrie;
    }
    else
    {
        cout << "Wrong dictionary file" << endl;
    }

    queries_words_file.open(queries_file_name);

    if (queries_words_file.is_open())
    {
        while (queries_words_file >> scanningLine)
        {
            if (testTrie.isAWord(scanningLine))
            {
                cout << (scanningLine + " is found") << endl;
            }
            else
            {
                cout << (scanningLine + " word is not found, did you mean:") << endl;
                std::vector<std::string> words_start_with_scanning = testTrie.allWordsBeginningWithPrefix(scanningLine);
                if (words_start_with_scanning.size() > 0)
                {
                    for (std::string currString : words_start_with_scanning)
                    {
                        cout << ("   " + currString) << endl;
                    }
                }
                else
                {
                    cout << "   no alternatives found" << endl;
                }
            }
        }
        std::vector<std::string> onlyCat = testTrie.allWordsBeginningWithPrefix("cat");
        cout << onlyCat[0] + " is start with cat" << endl;
        queries_words_file.close();
    }
    else
    {
        cout << "Wrong queries file" << endl;
    }

    return 0;
}