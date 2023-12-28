/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   None
/// Date:      01/16/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file contains tests for the evaluate method in Evaluator class.
/// </summary>
/// 
using FormulaEvaluator;
using System.Text.RegularExpressions;

/// <summary>
/// This is to check whether the Evaluator method will correctly evaluate a valid expression.
/// </summary>
void check_validexpression(String expression, Evaluator.Lookup look, int expected, String description)
{
    if (Evaluator.Evaluate(expression, look) == expected)
        Console.WriteLine(description);
}


/// <summary>
/// This is to check whether the exception is being thrown when evaluating the valid expression.
/// </summary>
void check_throw_exception(int test_num, String expression, Evaluator.Lookup look, String error_message)
{
    try
    {
        Evaluator.Evaluate(expression, look);
    }
    catch (Exception)
    {
        Console.WriteLine($"ExceptionTest {test_num} error being thrown: {error_message}");
    }
}


/// <summary>
/// This is to set certain variable with certain value, if the given variable does not
/// have a value, then throw an exception
/// </summary>
static int simple_lookup(string variable)
{
    if (variable == "A6")
        return 6;
    else if (variable == "AaB15")
        return 15;
    else
        throw new ArgumentException($"No value found for variable {variable}");
}

//The folowing is to test Evaluator with valid expression
check_validexpression("2+3-4", null, 1, "Pass test1 '2+3-4'");
check_validexpression("3*2+3*5", null, 21, "Pass test2 '2+3*5'");
check_validexpression("4/2+5*6-20", null, 12, "Pass test3 '4/2+5*6'");
check_validexpression("(4*3)", null, 12, "Pass test4 '(4*3)'");
check_validexpression("(4/3)", null, 1, "Pass test5 '(4/3)'");
check_validexpression("2-3*(2+4)", null, -6, "Pass test6 '2+3*(2+4)'");
check_validexpression("2-10/(2+3)", null, 0, "Pass test7 '2+10/(2+3)'");
check_validexpression("A6+5", simple_lookup, 11, "Pass test8 'A6+5'");
check_validexpression("AaB15-20", simple_lookup, -5, "Pass test9 'AaB15-20'");
check_validexpression("1+A6*5", simple_lookup, 31, "Pass test10 '1+A6*5'");
check_validexpression("AaB15/3+A6*2", simple_lookup, 17, "Pass test11 'AaB15/3+A6*2'");
check_validexpression("(A6/6+3)+(AaB15*1-7)", simple_lookup, 12, "Pass test12 '(A6/6+3)+(AaB15*1-7)'");
check_validexpression(" 2+3 ", null, 5, "Pass test13 ' 2+3 '");
check_validexpression(" 2 + 3 ", null, 5, "Pass test14 ' 2 + 3 '");

//The following is to test Evaluator with invalid expression that will throw exception
check_throw_exception(1, "+1", null, "When encounter integer, try to pop value stack but the stack is empty");
check_throw_exception(2, "/1", null, "When encounter integer, try to pop value stack but the stack is empty");
check_throw_exception(3, "*1", null, "The value stack is empty when try to pop value stack");
check_throw_exception(4, "2/0", null, "Cannot divide by zero");
check_throw_exception(5, "-1+2", null, "Negative number must be thrown");
check_throw_exception(6, "1+", null, "When encounter '+' or '-', try to pop value stack but the stack contains less than 2 values");
check_throw_exception(7, "1-", null, "When encounter '+' or '-', try to pop value stack but the stack contains less than 2 values");
check_throw_exception(8, "(1+)", null, "When encounter ')', try to pop value stack but the stack contains less than 2 values");
check_throw_exception(9, "2+3)", null, "When encounter ')', a '(' isn't found where expected");
check_throw_exception(10, "(2*", null, "When encounter ')', try to pop value stack but the stack contains less than 2 values");
check_throw_exception(11, "(2/0)", null, "Cannot divide by zero");
check_throw_exception(12, "A", simple_lookup, "Include invalid variable");
check_throw_exception(13, "aA*8", simple_lookup, "Include invalid variable");
check_throw_exception(14, "aA8L", simple_lookup, "Include invalid variable");
check_throw_exception(15, "A0+10", simple_lookup, "The variable does not link to a value");
check_throw_exception(16, "+A6", (x)=>5, "When encounter integer, try to pop value stack but the stack is empty");
check_throw_exception(17, "A6/0", (x) => 5, "Cannot divide by zero");
check_throw_exception(18, "2+2 2", null, "Include invalid variable");
check_throw_exception(19, "(2+3", null, "There is no exactly two values and one operator in the stacks when oprator stack is not empty at the end of expression");
check_throw_exception(20, "((2+3)", null, "There is no exactly two values and one operator in the stacks when oprator stack is not empty at the end of expression");
check_throw_exception(21, "(2+3))", null, "The stack is empty after '(' was thrown");
check_throw_exception(22, "", null, "There isn't exactly one value on the value stack when the operator stack is empty at the end of expression");
