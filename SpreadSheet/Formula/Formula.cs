// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens

/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   None
/// Date:      02/02/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file is a more generalized evaluator
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

//Some notes about this class:
//Since I am assigned to check "Parenthesis/Operator Following Rule - Any token that immediately
//follows an opening parenthesis or an operator must be either a number, a variable,
//or an opening parenthesis", I need to check whether the following token is a variable
//or not in Formula function.
//
//And to check whether a token is a variable, I'm checking whether the token meets the minimum
//format of a variable, which is, "any letter or underscore followed by any number of letters and/or
//digits and/or underscores would form valid variable names." After checking, I added variable that
//meets minimum format to valid_tokens list.
//
//In API of Evaluate function, I'm asked to throw FormulaError "if no undefined variables".
//But since I'm looping valid_tokens list, there is no "undefined variable" in it, I therefore
//did not return FormulaError about "undefined variable" in Evaluate function.
namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        //Store tokens after splitting the given expression
        private IEnumerable<string> tokens;
        //Store valid tokens, including variables that meet minimum format
        private List<string> valid_tokens = new();


        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            tokens = GetTokens(formula);
            int lparen_num = 0; //Keep track of number of left parenthesis
            int rparen_num = 0; //Keep track of number of right parenthesis
            
            //Make sure:
            //1.There must be at least one token
            //2.The first token of an expression must be a number, a variable, or an opening parenthesis.
            //3.The last token of an expression must be a number, a variable, or a closing parenthesis.
            //If not the case, throw FormulaFormatException.
            if (tokens.Count() == 0)
            {
                throw new FormulaFormatException("There must be at least one token.");
            }
            if(!(is_num(tokens.First()) || is_lparen(tokens.First()) || is_var(tokens.First())))
            {
                throw new FormulaFormatException("The first token of an expression must be a number, a variable, or an opening parenthesis.");
            }
            else if(!(is_num(tokens.Last()) || is_rparen(tokens.Last()) || is_var(tokens.Last())))
            {
                throw new FormulaFormatException("The last token of an expression must be a number, a variable, or a closing parenthesis.");
            }

            for(int i = 0; i < tokens.Count()-1; i++)
            {
                //Check whether the token that immediately follows an opening parenthesis or an operator
                //is either a number, a variable, or an opening parenthesis.
                //If not, throw an FormulaFormatException.
                if (tokens.ElementAt<string>(i).Equals("(") || tokens.ElementAt<string>(i).Equals("+") || tokens.ElementAt<string>(i).Equals("-") ||
                    tokens.ElementAt<string>(i).Equals("*") || tokens.ElementAt<string>(i).Equals("/"))
                {
                    if (!(is_num(tokens.ElementAt<string>(i + 1)) || is_var(tokens.ElementAt<string>(i + 1))  || is_lparen(tokens.ElementAt<string>(i + 1))))
                    {
                        throw new FormulaFormatException("What follows an opening parenthesis or an operator must either be a number, a variable, or an opening parenthesis.");
                    }
                }
                //Check whether the token that immediately follows a number, a variable, or a closing parenthesis
                //is either an operator or a closing parenthesis.
                //If not, throw an FormulaFormatException.
                else if (is_num(tokens.ElementAt<string>(i)) || is_var(tokens.ElementAt<string>(i)) || is_rparen(tokens.ElementAt<string>(i)))
                {
                    if(!(tokens.ElementAt<string>(i+1).Equals("+") || tokens.ElementAt<string>(i+1).Equals("-") || tokens.ElementAt<string>(i+1).Equals("*") ||
                       tokens.ElementAt<string>(i+1).Equals("/") || tokens.ElementAt<string>(i + 1).Equals(")")))
                    {
                        throw new FormulaFormatException("What follows a number, a variable, or a closing parenthesis must be either an operator or a closing parenthesis.");
                    }
                }
            }

            foreach(string t in tokens)
            {
                if (is_var(t))
                {

                    //Check whether normalize(token) is a legal variable
                    //If not, throw an exception
                    if (!is_var(normalize(t)))
                    {
                        throw new FormulaFormatException("normalize(token) is not a legal variable");
                    }
                    //Check whether isValid(normalize(v)) is true
                    //If not, throw an exception
                    else if (!isValid(normalize(t)))
                    {
                        throw new FormulaFormatException("isValid(normalize(v)) is false");
                    }
                    valid_tokens.Add(normalize(t));
                }
                else if(is_num(t) || is_opr(t) || is_lparen(t) || is_rparen(t))
                {
                    valid_tokens.Add(t);
                }
                if (is_lparen(t))
                {
                    lparen_num++;
                }
                else if (is_rparen(t))
                {
                    rparen_num++;
                }
            }
            //Check whether the number of left parenthesis and right parenthesis are the same
            //If not, throw an FormulaFormatException
            if (lparen_num != rparen_num)
            {
                throw new FormulaFormatException("The total number of opening parentheses must equal the total number of closing parentheses.");
            }

        }

        /// <summary>
        /// Check if token is operator
        /// </summary>
        /// <param name="token">token to be checked</param>
        /// <returns>true if token is operator, false otherwise</returns>
        private bool is_opr(string token)
        {
            if(token.Equals("+") || token.Equals("-") || token.Equals("*") || token.Equals("/"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if token is double
        /// </summary>
        /// <param name="token">token to be chekced</param>
        /// <returns>true if token is double, false otherwise</returns>
        private bool is_num(string token)
        {
            if(double.TryParse(token, out double to_double))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if token is variable that meet the minimum format
        /// </summary>
        /// <param name="token">token to be checked</param>
        /// <returns>true if token is the variable that meet the minimum format, false otherwise</returns>
        private bool is_var(string token)
        {
            string pattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            Match m = Regex.Match(token, pattern);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Chck if token is left parenthesis
        /// </summary>
        /// <param name="token">token to be checked</param>
        /// <returns>true if token is left parenthesis, false otherwise</returns>
        private bool is_lparen(string token)
        {
            if (token.Equals("("))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Chck if token is right parenthesis
        /// </summary>
        /// <param name="token">token to be checked</param>
        /// <returns>true if token is right parenthesis, false otherwise</returns>
        private bool is_rparen(string token)
        {
            if (token.Equals(")"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<double> val = new();
            Stack<string> opr = new();

            try
            {
                foreach (string t in valid_tokens)
                {
                    if (t.Equals("*") || t.Equals("/") || t.Equals("("))
                    {
                        opr.Push(t);
                    }
                    else if (is_num(t))
                    {
                        double num = double.Parse(t);
                        if (opr.Count() > 0 && (opr.Peek() == "*" || opr.Peek() == "/"))
                        {
                            if (opr.Peek() == "*")
                            {

                                val.Push(val.Pop() * num);
                                opr.Pop();
                            }
                            else if (opr.Peek() == "/")
                            {
                                if (num == 0)
                                {
                                    return new FormulaError("Cannot divided by zero");
                                }
                                else
                                {
                                    val.Push(val.Pop() / num);
                                    opr.Pop();
                                }
                            }
                        }
                        else
                        {
                            val.Push(num);
                        }
                    }
                    else if (is_var(t))
                    {
                        double num = lookup(t);
                        if (opr.Count() > 0 && (opr.Peek() == "*" || opr.Peek() == "/"))
                        {
                            if (opr.Peek() == "*")
                            {

                                val.Push(val.Pop() * num);
                                opr.Pop();
                            }
                            else if (opr.Peek() == "/")
                            {
                                if (num == 0)
                                {
                                    return new FormulaError("Cannot divided by zero");
                                }
                                else
                                {
                                    val.Push(val.Pop() / num);
                                    opr.Pop();
                                }
                            }
                        }
                        else
                        {
                            val.Push(num);
                        }
                    }
                    else if (t.Equals("+") || t.Equals("-"))
                    {
                        if (opr.Count > 0 && (opr.Peek() == "+" || opr.Peek() == "-"))
                        {
                            add_abst_at_top(val, opr);
                            opr.Push(t);
                        }
                        else
                        {
                            opr.Push(t);
                        }
                    }
                    //Must be right parenthesis
                    else
                    {
                        if (opr.Count > 0 && (opr.Peek() == "+" || opr.Peek() == "-"))
                        {
                            add_abst_at_top(val, opr);
                        }
                        opr.Pop();
                        if (opr.Count() > 0 && (opr.Peek() == "*" || opr.Peek() == "/"))
                        {
                            double first_val = val.Pop();
                            double second_val = val.Pop();

                            if (opr.Peek() == "*")
                            {
                                val.Push(first_val * second_val);
                                opr.Pop();
                            }
                            else if (opr.Peek() == "/")
                            {
                                if (first_val == 0)
                                {
                                    return new FormulaError("Cannot divided by zero");
                                }
                                else
                                {
                                    val.Push(second_val / first_val);
                                    opr.Pop();
                                }
                            }
                        }
                    }
                }
            }
            //thrown when lookup can not find varibale's value
            catch (ArgumentException)
            {
                return new FormulaError("There is undefined variable.");
            }

            if (opr.Count == 0)
            {
                //do nothing for now
            }
            else
            {
                double second_val = val.Pop();
                double first_val = val.Pop();
                if (opr.Peek() == "+")
                {
                    val.Push(first_val + second_val);
                }
                else if (opr.Peek() == "-")
                {
                    val.Push(first_val - second_val);
                }
            }
            return val.Pop();
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
        private static void add_abst_at_top(Stack<double> val, Stack<string> opr)
        {
            double second_val;
            double first_val;
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

        }


        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            HashSet<string> variables = new();
            foreach(string t in valid_tokens)
            {
                if (is_var(t))
                {
                    variables.Add(t);
                }
            }
            return variables;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(string t in valid_tokens)
            {
                if (is_num(t))
                {
                    sb.Append(double.Parse(t).ToString());
                }
                else
                {
                    sb.Append(t);
                }
                
            }
            return sb.ToString();
        }

        /// <summary>
        ///  <change> make object nullable </change>
        ///
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(obj, null) || !obj.GetType().Equals(this.GetType()))
            {
                return false;
            }
            else
            {
                Formula obj_formula = (Formula)obj!;

                for (int i = 0; i<this.valid_tokens.Count(); i++)
                {
                    if (double.TryParse(obj_formula.valid_tokens[i], out double obj_num) && double.TryParse(this.valid_tokens[i], out double this_num))
                    {
                        if(obj_num != this_num)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if(obj_formula.valid_tokens[i] != this.valid_tokens[i])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        /// <summary>
        ///   <change> We are now using Non-Nullable objects.  Thus neither f1 nor f2 can be null!</change>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// 
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            if (f1.Equals(f2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///   <change> We are now using Non-Nullable objects.  Thus neither f1 nor f2 can be null!</change>
        ///   <change> Note: != should almost always be not ==, if you get my meaning </change>
        ///   Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            if (!f1.Equals(f2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}


// <change>
//   If you are using Extension methods to deal with common stack operations (e.g., checking for
//   an empty stack before peeking) you will find that the Non-Nullable checking is "biting" you.
//
//   To fix this, you have to use a little special syntax like the following:
//
//       public static bool OnTop<T>(this Stack<T> stack, T element1, T element2) where T : notnull
//
//   Notice that the "where T : notnull" tells the compiler that the Stack can contain any object
//   as long as it doesn't allow nulls!
// </change>
