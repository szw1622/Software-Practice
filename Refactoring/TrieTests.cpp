/**
 * @file Trie.h
 * @author Keming Chen (ukeming@icloud.com) & Zhuowen Song
 * @brief The unit tests of Trie.cpp
 * @version 0.2
 * @date 2022-10-3
 *
 * @copyright Copyright (c) 2022
 *
 */
#include <iostream>
#include <fstream>
#include "Trie.h"
#include <gtest/gtest.h>

using namespace std;

// Common cases -----------------------------------------------------------------------------

/**
 * @brief Simplely add and check a word "apple"
 *
 */
TEST(TrieUnitTests, AddAndCheckAWord)
{
  Trie TestTrie;
  TestTrie.addAWord("apple");
  EXPECT_EQ(true, TestTrie.isAWord("apple"));
  EXPECT_EQ(false, TestTrie.isAWord("app"));
  EXPECT_EQ(false, TestTrie.isAWord("appleTree"));
}

/**
 * @brief Simplely add and check a long string
 *
 */
TEST(TrieUnitTests, AddAndCheckALongString)
{
  Trie TestTrie;
  TestTrie.addAWord("adkjfhjkahdjfhwhjeahfjhweahfiuawhuehfawhefhajwehfkjhawkjehfkjawhejkfhkjlawhekjfhawjkehfjkhawejfhjkawhef");
  EXPECT_EQ(true, TestTrie.isAWord("adkjfhjkahdjfhwhjeahfjhweahfiuawhuehfawhefhajwehfkjhawkjehfkjawhejkfhkjlawhekjfhawjkehfjkhawejfhjkawhef"));
}

/**
 * @brief Add a word "apple" to a trie and use copy constructor to copy it.
 *
 */
TEST(TrieUnitTests, CopyConstruct)
{
  Trie TestTrie;
  TestTrie.addAWord("apple");
  Trie CopyTrie = Trie(TestTrie);
  EXPECT_EQ(true, TestTrie.isAWord("apple"));
}

/**
 * @brief Add a word "apple" to a trie and copy it.
 *
 */
TEST(TrieUnitTests, EqualOperation)
{
  Trie TestTrie;
  TestTrie.addAWord("apple");
  Trie CopyTrie = TestTrie;
  EXPECT_EQ(true, TestTrie.isAWord("apple"));
}

/**
 * @brief Test check the words begin with simple prefix.
 *
 */
TEST(TrieUnitTests, SimplePrefix)
{
  Trie TestTrie;
  TestTrie.addAWord("apple");
  TestTrie.addAWord("alolo");
  TestTrie.addAWord("aplol");
  TestTrie.addAWord("application");
  vector<string> result;
  result = TestTrie.allWordsBeginningWithPrefix("ap");
  EXPECT_EQ(3, result.size());
  EXPECT_EQ("application", result[0]);
  EXPECT_EQ("apple", result[1]);
  EXPECT_EQ("aplol", result[2]);
}

/**
 * @brief Add a word "apple" to a trie and copy it.
 *
 */
TEST(TrieUnitTests, StressTest)
{
  ifstream dictionary_words_file;
  ifstream queries_words_file;

  // The words file reference:
  // https://github.com/dwyl/english-words
  string dictionary_file_name = "Dictionary.txt";
  string queries_file_name = "LookingFor.txt";

  Trie TestTrie;

  // Add 37015 words to the trie
  dictionary_words_file.open(dictionary_file_name);
  string scanningLine;
  while (dictionary_words_file >> scanningLine)
  {
    TestTrie.addAWord(scanningLine);
  }
  dictionary_words_file.close();

  // Check 37015 words in the trie
  queries_words_file.open(queries_file_name);
  while (queries_words_file >> scanningLine)
  {
    EXPECT_TRUE(TestTrie.isAWord(scanningLine));
  }
}

// Edge cases -------------------------------------------------------------------------------

/**
 * @brief Check an empty string is a word in an empty trie.
 *
 */
TEST(TrieUnitTests, CheckEmptyTrie)
{
  Trie TestTrie;
  EXPECT_EQ(false, TestTrie.isAWord(""));
}

/**
 * @brief Check an empty string is a word in an empty copied trie.
 *
 */
TEST(TrieUnitTests, CheckEmptyCopiedTrie)
{
  Trie EmptyTrie;
  Trie CopyTrie1 = Trie(EmptyTrie);
  Trie CopyTrie2 = EmptyTrie;
  EXPECT_EQ(false, CopyTrie1.isAWord(""));
  EXPECT_EQ(false, CopyTrie2.isAWord(""));
}

// Conner cases -------------------------------------------------------------------------------

/**
 * @brief Using two ways to copy the trie. Check the containers of the copied tries.
 *
 */
TEST(TrieUnitTests, AddWordToEmptyCopiedTrie)
{
  Trie EmptyTrie;
  Trie CopyTrie1 = Trie(EmptyTrie);
  CopyTrie1.addAWord("apple");
  Trie CopyTrie2 = EmptyTrie;
  CopyTrie2 = CopyTrie1;
  EXPECT_EQ(false, EmptyTrie.isAWord("apple"));
  EXPECT_EQ(true, CopyTrie1.isAWord("apple"));
  EXPECT_EQ(true, CopyTrie2.isAWord("apple"));
}

/**
 * @brief Test the words begin with empty string. Which should return all of the words in the trie tree.
 * 
 */
TEST(TrieUnitTests, EmptyPrefix)
{
  Trie TestTrie;
  TestTrie.addAWord("apple");
  TestTrie.addAWord("alolo");
  TestTrie.addAWord("aplol");
  TestTrie.addAWord("application");
  vector<string> result;
  result = TestTrie.allWordsBeginningWithPrefix("");
  EXPECT_EQ(4, result.size());
  EXPECT_EQ("application", result[0]);
  EXPECT_EQ("apple", result[1]);
  EXPECT_EQ("aplol", result[2]);
  EXPECT_EQ("alolo", result[3]);
}
