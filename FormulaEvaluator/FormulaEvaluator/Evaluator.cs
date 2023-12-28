using System.Linq;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    /// <summary> 
    /// Author:    Zhuowen Song 
    /// Partner:   None 
    /// Date:      1/17/2022
    /// Course:    CS 3500, University of Utah, School of Computing 
    /// Copyright: CS 3500 and Zhuowen Song - This work may not be copied for use in Academic Coursework. 
    /// 
    /// I, Zhuowen Song, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source.  All references used in the completion of the assignment are cited in my README file. 
    /// 
    /// File Contents 
    /// 
    ///    [This code simulation a formula evaluator which uses standard infix notation, and respect the usual precedence rules] 
    /// </summary>
    public class Evaluator
    {
        /// <summary>
        ///   The function is a delegate, which is used to look up the value of the variable
        /// 
        /// </summary>
        public delegate int Lookup(String variable_name);

        /// <summary>
        ///   The function evaluates a formula. You should be aware of the following edge cases
        ///   1. Only accept integers or integer variables in the formula
        ///   2. No negative integers, "-5" or "-(5/5)" are bad formulas
        ///   3. The white space will be ignored
        /// 
        /// </summary>
        /// <param expression> expression represents the formula entered </param>
        /// <param variableEvaluator> variableEvaluator represents the rules to look up the value of variable </param>
        /// <returns> The int result of the formula will be returned </returns>
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            //Create two empty to seprate the operators and the integers
            Stack<String> ValueStack = new();
            Stack<string> OperatorStack = new();

            //Split the given string, and push the integers and operators into the stacks that they should go.
            string[] substrings = splitString(expression);
            List<string> sampleOperators = new()
            {
                "(",
                ")",
                "-",
                "*",
                "/",
                "+"
            };

            //Process the tokens from left to right. For each token t do the operation that should to
            for (int i = 0; i < substrings.Length; i++)
            {
                string t = substrings[i];
                //If t is a white blank, ignore it
                if(t.Equals(" ") || t.Equals(""))
                {
                    continue;
                }
                //When t is a VALUE---------------------------------------------------------------------------------------
                if (!sampleOperators.Contains(t))
                {
                    //When t is an integer--------------------------------------------------------------------------------
                    if (Int32.TryParse(t, out int intT))
                    {
                        if (OperatorStack.Count != 0)
                        {
                            //If * is on the top of operator stack
                            if (OperatorStack.Peek().Equals("*"))
                            {
                                if(ValueStack.Count == 0)
                                    throw new ArgumentException("The value stack is empty");
                                //Pop the value stack and pop the operator stack
                                string valPoped = ValueStack.Pop();
                                OperatorStack.Pop();
                                //apply the poped operator *
                                try
                                {
                                    int intPoped = Int32.Parse(valPoped);
                                    //Push the result onto the value stack
                                    ValueStack.Push($"{intPoped * intT}");
                                    continue;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine($"Unable to parse '{valPoped}' when t is an intger and * is on the top of operator stack");
                                }
                            }
                            //If / is on the top fo operator stack, almost same as *
                            else if (OperatorStack.Peek().Equals("/"))
                            {
                                if (ValueStack.Count == 0)
                                    throw new ArgumentException("The value stack is empty");
                                //Pop the value stack and pop the operator stack
                                string valPoped = ValueStack.Pop();
                                OperatorStack.Pop();
                                //apply the popped operator /
                                try
                                {
                                    int intPoped = Int32.Parse(valPoped);
                                    //Push the result onto the value stack
                                    if (intT == 0)
                                        throw new ArgumentException("Can not divide " + valPoped + " by 0.");
                                    ValueStack.Push($"{intPoped / intT}");
                                    continue;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine($"Unable to parse '{valPoped}' when t is an intger and / is on the top of operator stack");
                                }
                            }
                        }
                        ValueStack.Push(t);
                        continue;
                    }
                    //When t is a variable--------------------------------------------------------------------------------
                    else
                    {
                        if(!checkToken(t))
                            throw new ArgumentException("Illegal token: " + t);
                        //using the looked-up value of t instead of t
                        intT = variableEvaluator(t);
                        if (OperatorStack.Count != 0)
                        {
                            //If * is on the top of operator stack
                            if (OperatorStack.Peek().Equals("*"))
                            {
                                if (ValueStack.Count == 0)
                                    throw new ArgumentException("The value stack is empty");
                                //Pop the value stack and pop the operator stack
                                string valPoped = ValueStack.Pop();
                                OperatorStack.Pop();
                                //apply the popped operator *
                                try
                                {
                                    int intPoped = Int32.Parse(valPoped);
                                    //Push the result onto the value stack
                                    ValueStack.Push($"{intPoped * intT}");
                                    continue;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine($"Unable to parse '{valPoped}'when t is a variable and * is on the top of operator stack");
                                }
                            }
                            //If / is on the top fo operator stack, almost same as *
                            else if (OperatorStack.Peek().Equals("/"))
                            {
                                if (ValueStack.Count == 0)
                                    throw new ArgumentException("The value stack is empty");
                                //Pop the value stack and pop the operator stack
                                string valPoped = ValueStack.Pop();
                                OperatorStack.Pop();
                                //apply the popped operator /
                                try
                                {
                                    int intPoped = Int32.Parse(valPoped);
                                    //Push the result onto the value stack
                                    if(intT == 0)
                                        throw new ArgumentException("Can not divide " + valPoped + " by 0.");
                                    ValueStack.Push($"{intPoped / intT}");
                                    continue;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine($"Unable to parse '{valPoped}' when t is an intger and / is on the top of operator stack");
                                }
                            }
                        }
                        ValueStack.Push($"{intT}");
                        continue;
                    }
                }
                //When t is + or -   -------------------------------------------------------------------------------------
                if ((t.Equals("+") || t.Equals("-")))
                {
                    if (OperatorStack.Count != 0)
                    {
                        //If + is at the top of the operator stack
                        if (OperatorStack.Peek().Equals("+"))
                        {
                            if (ValueStack.Count < 2)
                                throw new ArgumentException("The value stack contains fewer than 2 values");
                            //pop the value stack twice and the operator stack once
                            string valTwo = ValueStack.Pop();
                            string valOne = ValueStack.Pop();
                            OperatorStack.Pop();
                            //apply the popped operator to the popped numbers
                            try
                            {
                                int intTwo = Int32.Parse(valTwo);
                                int intOne = Int32.Parse(valOne);
                                //push the result onto the value stack
                                ValueStack.Push($"{intOne + intTwo}");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when t is + or - and + is on the top of operator stack");
                            }
                        }
                        //If - is at the top of the operator stack
                        else if (OperatorStack.Peek().Equals("-"))
                        {
                            if (ValueStack.Count < 2)
                                throw new ArgumentException("The value stack contains fewer than 2 values");
                            //pop the value stack twice and the operator stack once
                            string valTwo = ValueStack.Pop();
                            string valOne = ValueStack.Pop();
                            OperatorStack.Pop();
                            //apply the popped operator to the popped numbers
                            try
                            {
                                int intTwo = Int32.Parse(valTwo);
                                int intOne = Int32.Parse(valOne);
                                //push the result onto the value stack
                                ValueStack.Push($"{intOne - intTwo}");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when t is + or - and - is on the top of operator stack");
                            }
                        }
                    }
                    OperatorStack.Push(t);
                    continue;
                }
                //When t is * or /    ------------------------------------------------------------------------------------
                if (t.Equals("*") || t.Equals("/"))
                {
                    OperatorStack.Push(t);
                    continue;
                }
                //When t is (    -----------------------------------------------------------------------------------------
                if(t.Equals("("))
                {
                    OperatorStack.Push(t);
                    continue;
                }
                //When t is )    -----------------------------------------------------------------------------------------
                if (t.Equals(")"))
                {
                    //If + is at the top of the operator stack
                    if (OperatorStack.Peek().Equals("+"))
                    {
                        if (ValueStack.Count < 2)
                            throw new ArgumentException("The value stack contains fewer than 2 values");
                        //pop the value stack twice and the operator stack once
                        string valTwo = ValueStack.Pop();
                        string valOne = ValueStack.Pop();
                        OperatorStack.Pop();
                        //apply the popped operator to the popped numbers
                        try
                        {
                            int intTwo = Int32.Parse(valTwo);
                            int intOne = Int32.Parse(valOne);
                            //Push the result onto the value stack
                            ValueStack.Push($"{intOne + intTwo}");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when t is ) and + is on the top of operator stack");
                        }
                    }
                    //If - is at the top of the operator stack
                    else if (OperatorStack.Peek().Equals("-"))
                    {
                        if (ValueStack.Count < 2)
                            throw new ArgumentException("The value stack contains fewer than 2 values");
                        //pop the value stack twice and the operator stack once
                        string valTwo = ValueStack.Pop();
                        string valOne = ValueStack.Pop();
                        OperatorStack.Pop();
                        //apply the popped operator to the popped numbers
                        try
                        {
                            int intTwo = Int32.Parse(valTwo);
                            int intOne = Int32.Parse(valOne);
                            //Push the result onto the value stack
                            ValueStack.Push($"{intOne - intTwo}");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when t is ) and - is on the top of operator stack");
                        }
                    }
                    List<string> checkList = OperatorStack.ToList<string>();
                    //The top of the operator stack should be a '('. Pop it.
                    if(OperatorStack.Count == 0)
                        throw new ArgumentException("A '(' isn't found where expected");
                    if (!OperatorStack.Peek().Equals("("))
                        throw new ArgumentException("A '(' isn't found where expected");
                    OperatorStack.Pop();
                    //If * is at the top of the operator stack
                    if (OperatorStack.Count != 0)
                    {
                        if (OperatorStack.Peek().Equals("*") && ValueStack.Count >= 2)
                        {
                            if (ValueStack.Count < 2)
                                throw new ArgumentException("The value stack contains fewer than 2 values");
                            //pop the value stack twice and the operator stack once
                            string valTwo = ValueStack.Pop();
                            string valOne = ValueStack.Pop();
                            OperatorStack.Pop();
                            //Apply the popped operator to the popped numbers
                            try
                            {
                                int intTwo = Int32.Parse(valTwo);
                                int intOne = Int32.Parse(valOne);
                                //Push the result onto the value stack
                                ValueStack.Push($"{intOne * intTwo}");
                                continue;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when t is ) and * is on the top of operator stack");
                            }
                        }
                        //If * is at the top of the operator stack
                        else if (OperatorStack.Peek().Equals("/") && ValueStack.Count >= 2)
                        {
                            if (ValueStack.Count < 2)
                                throw new ArgumentException("The value stack contains fewer than 2 values");
                            //pop the value stack twice and the operator stack once
                            string valTwo = ValueStack.Pop();
                            string valOne = ValueStack.Pop();
                            OperatorStack.Pop();
                            //Apply the popped operator to the popped numbers
                            try
                            {
                                int intTwo = Int32.Parse(valTwo);
                                int intOne = Int32.Parse(valOne);
                                //Push the result onto the value stack
                                if (intTwo == 0)
                                    throw new ArgumentException("Can not divide " + valOne + " by 0.");
                                ValueStack.Push($"{intOne / intTwo}");
                                continue;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when t is ) and / is on the top of operator stack");
                            }
                        }
                    }
                }
                //All of the token t should have a way to go, if not print what is it
                else
                {
                    throw new ArgumentException("Illegal token: " +t);
                }
            }

            //When the last token has been processed
            //And Operator stack is empty
            if (OperatorStack.Count == 0)
            {
                if (ValueStack.Count != 1)
                    throw new ArgumentException("There isn't exactly one value on the value stack");
                //Pop the final result and report as the value of the expression
                string popedResult = ValueStack.Pop();
                try
                {
                    return Int32.Parse(popedResult);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{popedResult}' when output the result.");
                }
            }
            //And Operator stack is empty
            else
            {
                //If + is at the top of the operator stack
                if (OperatorStack.Peek().Equals("+") && ValueStack.Count >= 2)
                {
                    //pop the value stack twice and the operator stack once
                    string valTwo = ValueStack.Pop();
                    string valOne = ValueStack.Pop();
                    OperatorStack.Pop();
                    //apply the popped operator to the popped numbers
                    try
                    {
                        int intTwo = Int32.Parse(valTwo);
                        int intOne = Int32.Parse(valOne);
                        return intOne + intTwo;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when + is on the top of operator stack at the final calculation");
                    }
                }
                //If - is at the top of the operator stack
                else if (OperatorStack.Peek().Equals("-") && ValueStack.Count >= 2)
                {
                    //pop the value stack twice and the operator stack once
                    string valTwo = ValueStack.Pop();
                    string valOne = ValueStack.Pop();
                    OperatorStack.Pop();
                    //apply the popped operator to the popped numbers
                    try
                    {
                        int intTwo = Int32.Parse(valTwo);
                        int intOne = Int32.Parse(valOne);
                        return intOne - intTwo; ;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{valTwo}' or '{valOne}' when - is on the top of operator stack at the final calculation");
                    }
                }
                //When the last operator is (, the value stack should only have one value
                else if (OperatorStack.Peek().Equals("("))
                {
                    string valResult = ValueStack.Pop();
                    //apply the popped operator to the popped numbers
                    try
                    {
                        return Int32.Parse(valResult);;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{valResult}' when ( is on the top of operator stack at the final calculation");
                    }
                }
                else
                    throw new ArgumentException("There isn't exactly one operator on the operator stack or exactly two numbers on the value stack");
            }
            //-1 should not turn out, which means the error.
            return -1;
        }

        /// <summary>
        ///   The function splits the formula into operations and values
        /// 
        /// </summary>
        /// <param s> s represents the formula entered </param>
        /// <returns> A array of splited formula </returns>
        private static string[] splitString(String s)
        {
            string[] substrings = Regex.Split(s, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)|( )");
            return substrings;
        }

        /// <summary>
        ///   The function check whether the toke is legal or not.
        /// 
        /// </summary>
        /// <param token> token represents the token which need to be checked </param>
        /// <returns> True if it is legal variable, and false if it is not. </returns>
        private static Boolean checkToken(String token)
        {
            //Create sample lists
            char[] tokenChars = token.ToCharArray();
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            List<char> charSmaple = characters.ToCharArray().ToList<char>();
            List<char> numberSmaple = numbers.ToCharArray().ToList<char>();
            //Find the index of last character and the first number.
            //If the first number at the more forward index than the last character, the token is illegal
            int theLastCharIndex = int.MaxValue;
            int theFirstNumIndex = -1;
            //Check theLastCharIndex 
            for (int i = 0; i< token.Length; i++)
            {
                if (charSmaple.Contains(tokenChars[i]))
                {
                    theLastCharIndex = i;
                }
            }
            //Check theFirstNumIndex 
            for (int j = 0; j < token.Length; j++)
            {
                if (numberSmaple.Contains(tokenChars[j]))
                {
                    theFirstNumIndex = j;
                    break;
                }
            }
            if(theLastCharIndex < theFirstNumIndex)
                return true;
            return false;
        }
    }
}