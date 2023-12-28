
using System.Collections;
using System.Text.RegularExpressions;
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
/// This file takes a method that evaluate an expression.
/// </summary>
namespace FormulaEvaluator
{
    public class Evaluator
    {
        //This is a delegate that looks up the value of a variable.
        public delegate int Lookup(String variable_name);

        //This stacks will store number or value of variable in the expression.
        //private Stack<int> val = new();

        //This stack will store operator in the expression.
        //private Stack<String> opr = new();

        /// <summary>
        /// This method takes in an expression, evaluate it and give the result. 
        /// 
        /// The valid tokens in the expression are four operator symbols: + - * /, left parentheses, 
        /// right parentheses, non-negative integers, whitespace, and variables consisting of one or 
        /// more letters followed by one or more digits.
        /// </summary>
        /// <param name="expression"> expression represents the expression that needs to evaluate </param>
        /// <param name="variableEvaluator"> variableEvaluator represents the method that looks up the value of a variable in the expression </param>
        /// <returns> The result </returns>
        /// <exception cref="ArgumentException"></exception>

        /// <summary>
        /// This method takes in an expression, evaluate it and give the result.
        /// 
        /// The valid tokens in the expression are four operator symbols: + - * /, left parentheses, 
        /// right parentheses, non-negative integers, whitespace, and variables consisting of one or 
        /// more letters followed by one or more digits.
        /// </summary>
        /// <param name="expression"> expression represents the expression that needs to evaluate </param>
        /// <param name="variableEvaluator"> variableEvaluator represents the method that looks up the value of a variable in the expression </param>
        /// <returns> The result of the expression. </returns>
        /// <exception cref="ArgumentException"> Exception being thrown when possible errors occur while evaluating. </exception>
        /// <exception cref="DivideByZeroException">Exception being thrown when possible errors occur while evaluating. </exception>
        public static int Evaluate(string expression, Lookup variableEvaluator)
        {
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            Stack<int> val = new();
            Stack<string> opr = new();
            foreach (string token in substrings)
            {
                //This remove the whitespace at the start and the end of the expression
                //Reference: String.Trim Method https://docs.microsoft.com/en-us/dotnet/api/system.string.trim?view=net-6.0
                token.Trim(' ');

                if(token == "")
                {
                    continue;
                }
                else if(token == " ")
                {
                    continue;
                }
                //The method of determine whether a string is an integer
                //Reference: Int32.TryParse Method https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-6.0
                else if (int.TryParse(token, out int string_to_int))
                {
                    if(string_to_int < 0)
                    {
                        throw new ArgumentException("Throw negative number.");
                    }
                    else
                    {
                        if (opr.Count > 0 && (opr.Peek() == "*" || opr.Peek() == "/"))
                        {
                            multiple_divide_at_top(val, opr, string_to_int);
                        }
                        else
                        {
                            val.Push(string_to_int);
                        }
                    }
                }
                else if (token == "+" | token == "-")
                {
                    if (opr.Count > 0 && (opr.Peek() == "+" | opr.Peek() == "-"))
                    {
                        add_abst_at_top(val, opr);
                        opr.Push(token);
                    }
                    else
                    {
                        opr.Push(token);
                    }
                }
                else if (token == "*" | token == "/")
                {
                    opr.Push(token);
                }
                else if (token == "(")
                {
                    opr.Push(token);
                }
                else if (token == ")")
                {

                    if (opr.Count > 0 && (opr.Peek() == "+" | opr.Peek() == "-"))
                    {
                        add_abst_at_top(val, opr);
                    }
                    if(opr.Count == 0)
                    {
                        throw new ArgumentException("The stack is empty after '(' was thrown.");
                    }
                    else
                    {
                        if (opr.Count > 0 && opr.Peek() != "(")
                        {
                            throw new ArgumentException("'(' is not found.");
                        }
                        else
                        {
                            opr.Pop();
                        }
                    }
                        
                    if (opr.Count > 0 && (opr.Peek() == "*" | opr.Peek() == "/"))
                    {
                        int first_val;
                        int second_val;
                        if (val.Count < 0)
                        {
                            throw new ArgumentException("The value stack contains fewer than 2 values when trying to pop.");
                        }
                        else
                        {
                            second_val = val.Pop();
                            first_val = val.Pop();
                            if (opr.Peek() == "*")
                            {
                                val.Push(first_val * second_val);
                                opr.Pop();
                            }
                            else if (opr.Peek() == "/")
                            {
                                if (second_val == 0)
                                {
                                    throw new DivideByZeroException("Cannot divide by zero.");
                                }
                                val.Push(first_val / second_val);
                                opr.Pop();
                            }
                        }

                    } 
                }
                else
                {
                    string pattern = "(^[a-zA-Z]+[0-9]+$)";
                    Regex rgx = new Regex(pattern);
                    Match match = rgx.Match(token);
                    if (match.Success)
                    {
                        if (opr.Count > 0 && (opr.Peek() == "*" | opr.Peek() == "/"))
                        {
                            multiple_divide_at_top(val, opr, variableEvaluator(token));
                        }
                        else
                        {
                            val.Push(variableEvaluator(token));
                        }       
                    }
                    else
                    {
                        throw new ArgumentException("This is not a valid variable.");
                    }
                }
            }
            if (opr.Count == 0)
            {
                if (val.Count != 1)
                {
                    throw new ArgumentException("There is not exactly one value in value stack.");
                }     
            }
            else
            {
                if (val.Count != 2 | opr.Count != 1)
                {
                    throw new ArgumentException("There is not exactly two value in value stack or exactly one operator in operator stack.");
                }
                else
                {
                    int second_val = val.Pop();
                    int first_val = val.Pop();

                    if (opr.Peek() == "+")
                    {
                        val.Push(first_val + second_val);
                    }
                    else if (opr.Peek() == "-")
                    {
                        val.Push(first_val - second_val);
                    }
                }
                
            }
            return val.Pop();
        }

        /// <summary>
        /// This method is applied when token is integer or variable and the "*" or "/" is at the
        /// top of the opreator stack. This is to pop the top two values in value stack, and apply
        /// "*" or "/" at the top of operator stack to the two values, and calculate the result.
        /// Finally, push the result in the value stack.
        /// </summary>
        /// <param name="val">The value stack that contains the values in expression so far.</param>
        /// <param name="opr">The operator stack the contains the operators in expression so far.</param>
        /// <param name="number_in_expression">The number in expression, including the number of variable.</param>
        /// <exception cref="ArgumentException">Exception being thrown when possible errors occur while evaluating.</exception>
        /// <exception cref="DivideByZeroException">Exception being thrown when possible errors occur while evaluating.</exception>
        private static void multiple_divide_at_top(Stack<int> val, Stack<String> opr, int number_in_expression)
        {
            if (opr.Peek() == "*")
            {
                if (val.Count == 0)
                {
                    throw new ArgumentException("The value stack is empty.");
                }
                val.Push(val.Pop() * number_in_expression);
                opr.Pop();
            }
            else if (opr.Peek() == "/")
            {
                if (number_in_expression == 0)
                {
                    throw new ArgumentException("Cannot divide by zero.");
                }
                if (val.Count == 0)
                {
                    throw new ArgumentException("The value stack is empty.");
                }
                val.Push(val.Pop() / number_in_expression);
                opr.Pop();
            }
        }

        /// <summary>
        /// This method is applied to two cases:
        /// 1.when token is "+" or "-" and "+" or "-" is at the top of the operator stack.
        /// 2.when token is ")" and "+" or "-" is at the top of the operator stack.
        /// This is to pop the top two values in value stack, and apply "+" or "-" at
        /// the top of operator stack to the two values, and calculate the result.
        /// Finally, push the result to the value stack.
        /// </summary>
        /// <param name="val">The value stack that contains the values in expression so far.</param>
        /// <param name="opr">The operator stack the contains the operators in expression so far.</param>
        /// <exception cref="ArgumentException">Exception being thrown when possible errors occur while evaluating.
        /// </exception>
        private static void add_abst_at_top(Stack<int> val, Stack<String> opr)
        {
            int second_val;
            int first_val;
            if (val.Count >= 2)
            {
                second_val = val.Pop();
                first_val = val.Pop();
                if (opr.Peek() == "+")
                {
                    val.Push(first_val + second_val);
                    opr.Pop();
                }
                else if (opr.Peek() == "-")
                {
                    val.Push(first_val - second_val);
                    opr.Pop();
                }
            }
            else
            {
                throw new ArgumentException("The value stack contains fewer than 2 values when trying to pop.");
            }

        }
    }
}