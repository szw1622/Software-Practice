using FormulaEvaluator;
using System.Linq;
using static FormulaEvaluator.Evaluator;
/// <summary> 
/// Author:    Zhuowen Song 
/// Partner:   None 
/// Date:      1/21/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Zhuowen Song - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Zhuowen Song, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    [Here is the first test for evaluator] 
/// </summary>
/// 


//Build a delegate dictionary to look up the value of the variable
int dictionary(string variable)
{
    switch (variable)
    {
        case ("x1"):
            return 11;
        case ("b2"):
            return 9;
        case ("c3"):
            return 7;
        case ("d4"):
            return 6;
        case ("e5"):
            return 3;
        default:
            throw new ArgumentException("Can not look up " + variable + " it may not a variable.");
    }
}

Lookup dc = new Lookup(dictionary);

//Test basic calculations
void test1()
{
    Console.WriteLine("5     +3 = " + Evaluator.Evaluate("5+3", dc));
}
void test2()
{
    Console.WriteLine("x1=" + dc("x1"));
    Console.WriteLine("5 - x1 = " + Evaluator.Evaluate("5 - x1", dc));
}
void test3()
{
    Console.WriteLine("2*3 = " + Evaluator.Evaluate("2*3", dc));
}
void test4()
{
    Console.WriteLine("6/2 = " + Evaluator.Evaluate("6 /2", dc));
}
void test5()
{
    Console.WriteLine("(5+3) = " + Evaluator.Evaluate("(5+3)", dc));
}
void test6()
{
    Console.WriteLine("b2=" + dc("b2"));
    Console.WriteLine("(5 - b2) = " + Evaluator.Evaluate("(5) - b2", dc));
}
void test7()
{
    Console.WriteLine("(2*3) = " + Evaluator.Evaluate("(2*3)", dc));
}
void test8()
{
    Console.WriteLine("(6/2) = " + Evaluator.Evaluate("(6 /2)", dc));
}
void test9()
{
    Console.WriteLine("5*(7-2) = " + Evaluator.Evaluate("5*(7-2)", dc));
}
void test10()
{
    Console.WriteLine("(1+2)*6 =  " + Evaluator.Evaluate("(1+2)*6", dc));
}
void test11()
{
    Console.WriteLine("10/(7-2) = " + Evaluator.Evaluate("10/(7-2)", dc));
}
void test12()
{
    Console.WriteLine("(6+4)/5 = " + Evaluator.Evaluate("(6+4)/5", dc));
}
void test13()
{
    Console.WriteLine("2+5+(8-9)*9/3 = " + Evaluator.Evaluate("2+5+(8-9)*9/3", dc));
}
void test14()
{
    Console.WriteLine("2+3+4+5-2-3-4-5+1*2*3*4-4/2*3/1 = " + Evaluator.Evaluate("2+3+4+5-2-3-4-5+1*2*3*4-4/2*3/1", dc));
}
void test15()
{
    Console.WriteLine("(1+2+3+4-5-7-6+7*6)-10/2 = " + Evaluator.Evaluate("(1+2+3+4-5-7-6+7*6)-10/2", dc));
}
void test16()
{
    Console.WriteLine("x1 +b2-c3 +d4-e5 = " + Evaluator.Evaluate("x1 +b2-c3 +d4-e5", dc));
}
void test17()
{
    Console.WriteLine("(x1+b2/e5)+c3*d4 = " + Evaluator.Evaluate("(x1+b2/e5)+c3*d4", dc));
}

//Test throw arguements
void testIllegalToken()
{
    try
    {
        Evaluator.Evaluate("1@12", dc);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine("wrong token");
    }
}
void testEmptyVariable()
{
    try
    {
        Evaluator.Evaluate("1+2+m0", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("m0 pick out");
    }
}
void testDivideZero()
{
    try
    {
        Evaluator.Evaluate("2/0", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("can not divide 0");
    }
}
void testEmoji()
{
    try
    {
        Evaluator.Evaluate(" -A- ", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("-A-");
    }
}
void testEmptyString()
{
    try
    {
        Evaluator.Evaluate("", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("no value in the value stack");
    }
}
void testNoValueInValueStack()
{
    try
    {
        Evaluator.Evaluate("///", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("no value in the value stack");
    }
}
void testIllegalToken2()
{
    try
    {
        Evaluator.Evaluate("3AB4", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Illegal token: 3AB4");
    }
}
void testIllegalToken3()
{
    try
    {
        Evaluator.Evaluate("   A   A   A   QAQ", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Illegal token: A");
    }
}
void testNoValueInValueStack2()
{
    try
    {
        Evaluator.Evaluate("*12", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack is empty");
    }
}
void testNoValueInValueStack3()
{
    try
    {
        Evaluator.Evaluate("/12", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack is empty");
    }
}
void testNoValueInValueStack4()
{
    try
    {
        Evaluator.Evaluate("*x1", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack is empty");
    }
}
void testNoValueInValueStack5()
{
    try
    {
        Evaluator.Evaluate("/x1", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack is empty");
    }
}
void testNoEnoughValueInValueStack1()
{
    try
    {
        Evaluator.Evaluate("+1+1", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack contains fewer than 2 values");
    }
}
void testNoEnoughValueInValueStack2()
{
    try
    {
        Evaluator.Evaluate("-1-1", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack contains fewer than 2 values");
    }
}
void testNoEnoughValueInValueStack3()
{
    try
    {
        Evaluator.Evaluate("--1", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack contains fewer than 2 values");
    }
}
void testNoEnoughValueInValueStack4()
{
    try
    {
        Evaluator.Evaluate("--1", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack contains fewer than 2 values");
    }
}
void testNoEnoughValueInValueStack5()
{
    try
    {
        Evaluator.Evaluate("(*1+2)", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("The value stack contains fewer than 2 values");
    }
}
void testWrongOperation()
{
    try
    {
        Evaluator.Evaluate("1+2)", dc);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("A '(' isn't found where expected");
    }
}

test1();
test2();
test3();
test4();
test5();
test6();
test7();
test8();
test9();
test10();
test11();
test12();
test13();
test14();
test15();
test16();
test17();
testIllegalToken();
testEmptyVariable();
testDivideZero();
testEmoji();
testEmptyString();
testNoValueInValueStack();
testIllegalToken2();
testIllegalToken3();
testNoValueInValueStack2();
testNoValueInValueStack3();
testNoValueInValueStack4();
testNoValueInValueStack5();
testNoEnoughValueInValueStack1();
testNoEnoughValueInValueStack2();
testNoEnoughValueInValueStack3();
testNoEnoughValueInValueStack4();
testNoEnoughValueInValueStack5();
testWrongOperation();
Console.Read();