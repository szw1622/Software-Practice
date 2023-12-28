CC = g++
CFLAGS = -Wall
# Change to your googletest path
GOOGLETEST = /root/cs3505/googletest

test: clean TrieTests
	./TrieTests

TrieTests: TrieTests.o Trie.o
	$(CC) $(CFLAGS) -o TrieTests TrieTests.o Trie.o -L$(GOOGLETEST)/lib -lgtest -lgtest_main -lpthread

TrieTests.o: TrieTests.cpp
	$(CC) $(CFLAGS) -c TrieTests.cpp -I$(GOOGLETEST)/googletest/include/

TrieTest.o: TrieTest.cpp Trie.cpp Trie.h
	$(CC) $(CFLAGS) -c TrieTest.cpp

Trie.o: Trie.cpp Trie.h
	$(CC) $(CFLAGS) -c Trie.cpp

clean:
	rm -f Trie.o TrieTest.o TrieTests.o TrieTest TrieTests