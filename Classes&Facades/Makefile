LIBHARU = ./libharu
HPDF = $(LIBHARU)/include
CC = g++ -std=c++11
CFLAGS = -Wall

clean:
	rm -f spiralPDF *.o *.exe

HaruPDF.o: HaruPDF.cpp HaruPDF.h $(HPDF)
	$(CC) $(CFLAGS) -I$(LIBHARU)/include -c HaruPDF.cpp

Spiral.o: Spiral.cpp Spiral.h $(HPDF)
	$(CC) $(CFLAGS) -I$(LIBHARU)/include -c Spiral.cpp

spiralPDF.o: spiralPDF.cpp HaruPDF.o Spiral.h HaruPDF.h $(HPDF)
	$(CC) $(CFLAGS) -I$(LIBHARU)/include -c spiralPDF.cpp

spiralPDF: spiralPDF.o HaruPDF.o Spiral.o $(HPDF)
	$(CC) $(CFLAGS) -I$(LIBHARU)/include -o spiralPDF spiralPDF.o HaruPDF.o Spiral.o -L$(LIBHARU)/src/ -lhpdfs