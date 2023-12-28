/*
 * Author:      Zhuowen Song
 * Date:        9/1/2022
 * Course:      CS 3505, University of Utah, School of Computing
 * Assignment:  An Ecological Simulation
 * Copyright:   CS 3505 and Zhuowen Song - This work may not be copied for use in Academic Coursework.
 *
 * I, Zhuowen Song, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  All references used in the completion of the assignment are cited in my README file.
 *
 * File Contents:
 * Simulation for fox-rabbit populations
 */

#include <iostream>
#include <cmath>
using namespace std;

// Declare the functions that need to be used.
void updatePopulations(double g, double p, double c, double m, double K, double &numRabbits, double &numFoxes);
void incrementCounter(int *);
void plotCharacter(int, char);
void plotPopulations(double, double, double);

// Static global variables to store the population of foxes and rabbits.
// static const double count_of_rabbit;
// static const double* count_of_fox;

/// @brief This function has parameters for iterations, number of rabbits, and number of foxes in that order.
///        This function should set the parameters needed for the update equation, and run the simulation for iterations steps or until the predator or prey population goes below 1.
/// @param iterations - Each iteration of the simulation should call population update function.
/// @param foxCount - The initial count of foxes
/// @param rabbitCount - The initial count of rabbits
void runSimulation(int iterations, double rabbits, double foxes)
{
    // local static const values
    static const double rabbitGrowth = 0.2;
    static const double predationRate = 0.0022;
    static const double foxPreyConversion = 0.6;
    static const double foxMortalityRate = 0.2;
    static const double carryCapacity = 1000.0;

    double count_of_fox = foxes;
    double count_of_rabbit = rabbits;
    int runTimes = 0;

    while (runTimes < iterations)
    {
        if (count_of_fox < 1 || count_of_rabbit < 1)
        {
            return;
        }
        // Draw the plot first, then update the population, in case the population is negative after update.
        plotPopulations(count_of_rabbit, count_of_fox, 0.1);
        cout << endl;
        updatePopulations(rabbitGrowth, predationRate, foxPreyConversion, foxMortalityRate, carryCapacity, count_of_rabbit, count_of_fox);
        incrementCounter(&runTimes);
    }
}

/// @brief A main function that that asks the user for initial rabbit and fox populations,
///        then calls runSimulation with a value of 500 iterations and the initial rabbit and fox populations.
/// @return 1 if something wrong or 1 is succeed.
int main()
{
    double rabbits;
    double foxes;
    int iterations;

    cout << "Enter the population of rabbits:";
    cin >> rabbits;

    // Enter rabbit
    if (cin.fail() || rabbits < 0)
    {
        cerr << "Wrong value" << endl;
        return 1;
    }
    else
    {
        cout << "You entered " << rabbits << endl;
    }

    // Enter fox
    cout << "Enter the population of foxes:";
    cin >> foxes;
    if (cin.fail() || foxes < 0)
    {
        cerr << "Wrong value" << endl;
        return 1;
    }
    else
    {
        cout << "You entered " << foxes << endl;
    }

    runSimulation(500, rabbits, foxes);
    return 0;
}

/// @brief A population update function that takes in the model parameters and then the number of rabbits and number of foxes with a pass-by-reference style.
/// @param g - the rate of increase of rabbits
/// @param p - fraction of prey eaten per predator
/// @param c - fraction of eaten prey turned into new predators
/// @param m - a per capita mortality rate for foxes
/// @param K - How many rabbits can be supported by the environment
/// @param numRabbits - The initial count of rabbits
/// @param numFoxes - The initial count of foxes
void updatePopulations(double g, double p, double c, double m, double K, double &numRabbits, double &numFoxes)
{
    double R = numRabbits;
    double F = numFoxes;

    // The equations are:
    // deltaRabbit = gR(1-R/K) - pRF
    // deltaFoxes = cpRF - mF
    double deltaRabbit = g * R * (1 - R / K) - p * R * numFoxes;
    double deltaFoxes = c * p * R * F - m * F;

    numRabbits += deltaRabbit;
    numFoxes += deltaFoxes;
}

/// @brief A helper function to print the character at right position.
/// @param number - represent the position of the character
/// @param character - the character which need to be printed
void plotCharacter(int number, char character)
{
    if (number < 1)
    {
        cout << character;
        return;
    }
    for (int i = 0; i < number; i++)
    {
        cout << " ";
    }
    cout << character;
}

/// @brief A void function that print the rabbit and the fox under the given scale.
/// @param numRabbits - Number of rabbits
/// @param numFoxes - Number of foxes
/// @param scale - the scale of the image
void plotPopulations(double numRabbits, double numFoxes, double scale)
{
    // Get the correct position of the rabbits and the foxes under the scale.
    int rabbitPos = floor(numRabbits * scale);
    int foxPos = floor(numFoxes * scale);

    // Print * if overlap
    if (rabbitPos == foxPos)
    {
        plotCharacter(rabbitPos, '*');
        return;
    }

    if (rabbitPos < foxPos)
    {
        foxPos = foxPos - rabbitPos - 1;
        plotCharacter(rabbitPos, 'r');
        plotCharacter(foxPos, 'F');
        return;
    }

    if (rabbitPos > foxPos)
    {
        rabbitPos = rabbitPos - foxPos - 1;
        plotCharacter(foxPos, 'F');
        plotCharacter(rabbitPos, 'r');
        return;
    }
}

// A helper function incrementCounter that returns void and has a pointer to an integer parameter. The function should add 1 to the value pointed to by the pointer.
void incrementCounter(int *counter)
{
    *counter = *counter + 1;
}