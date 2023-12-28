CC = g++ -std=c++11
CFLAGS = -Wall
FFLAGS = -fsanitize=undefined -fsanitize=address

TrieTest: TrieTest.o Trie.o Node.o
	$(CC) $(CFLAGS) $(FFLAGS) -I./ -o TrieTest TrieTest.o Trie.o Node.o

Node.o: Node.cpp Node.h
	$(CC) $(CFLAGS) $(FFLAGS) -I./ -c -g Node.cpp

Trie.o: Trie.cpp  Trie.h
	$(CC) $(CFLAGS) $(FFLAGS) -I./ -c -g Trie.cpp

TrieTest.o: TrieTest.cpp
	$(CC) $(CFLAGS) $(FFLAGS) -I./ -c -g TrieTest.cpp

simpleTest: TrieTest TrieTest.o Trie.o Node.o
	./TrieTest "simpleDictionary.txt" "simpleQueries.txt"

clean:
	rm -f spiralPDF *.o TrieTest