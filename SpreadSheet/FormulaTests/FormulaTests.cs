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
/// This file contains tests for functions in Formula class.
/// </summary>
/// 

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SpreadsheetUtilities;
using System.Collections.Generic;

namespace FormulaTests
{
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
    //
    //Hence, I'm only testing FormulaError of division by zero in this test class.
    [TestClass()]
    public class FormulaTests
    {
        /// <summary>
        /// Simple isValid function
        /// </summary>
        /// <param name="variable">token to be checked</param>
        /// <returns>true if token is valid, which is only valid when equals "_x"</returns>
        static bool simple_isValid(string variable)
        {
            if (!variable.Equals("_x"))
                return false;
            else
                return true;
        }

        static string simple_normalize(string variable)
        {
            return "@";
        }

        /*-------------------------------- Start Testing --------------------------------*/
        /// <summary>
        /// Test when violate OneTokenRule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateOneToken()
        {
            Formula a = new("", s => s, s => true);
        }

        /// <summary>
        /// Test when violate equal parenthesis rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateParenthesis1()
        {
            Formula a = new("1+2)", s => s, s => true);
        }

        /// <summary>
        /// Test when violate equal parenthesis rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateParenthesis2()
        {
            Formula a = new("((1+2)", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Starting Token Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateStartToken1()
        {
            Formula a = new(")2", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Starting Token Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateStartToken2()
        {
            Formula a = new("@2", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Starting Token Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateStartToken3()
        {
            Formula a = new("+2", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Ending Token Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateEndToken1()
        {
            Formula a = new("1(", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Ending Token Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateEndToken2()
        {
            Formula a = new("1/", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Ending Token Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateEndToken3()
        {
            Formula a = new("1$", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Following Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateFollowingRule1()
        {
            Formula a = new("(@+3)", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Following Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateFollowingRule2()
        {
            Formula a = new("(_x+$)", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Following Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateFollowingRule3()
        {
            Formula a = new("_x+@", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Following Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateFollowingRule4()
        {
            Formula a = new("(_x+3)%", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Following Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateFollowingRule5()
        {
            Formula a = new("(3@)", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Following Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateFollowingRule6()
        {
            Formula a = new("(_x@)", s => s, s => true);
        }

        /// <summary>
        /// Test when violate Following Rule
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ViolateFollowingRule7()
        {
            Formula a = new("()@", s => s, s => true);
        }

        /// <summary>
        /// Test if the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void AfterNormalizeNotVar1()
        {
            Formula a = new("_x", simple_normalize, s => true);
        }


        /// <summary>
        /// Test if the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void AfterNormalizeNotValid()
        {
            Formula a = new("_x", s => s.ToUpper(), simple_isValid);
        }

        /// <summary>
        /// Test formula with valid variable that satisfy
        /// minimum format and isValid
        /// </summary>
        [TestMethod()]
        public void TestFormulaIsValid1()
        {
            Formula a = new("_x+3", s => s, simple_isValid);
        }

        /// <summary>
        /// Test formula with valid variable that satisfy
        /// minimum format but not isValid
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestFormulaIsValid2()
        {
            Formula a = new("_Y+3", s => s, simple_isValid);
        }

        /// <summary>
        /// Test whether formula error of division by zero is returned
        /// </summary>
        [TestMethod()]
        public void FormulaErrorInEvaluate1()
        {
            Formula a = new("6/0", s => s, s => true);
            FormulaError er = new();
            Assert.AreEqual(er.GetType(), a.Evaluate(null).GetType());
        }

        /// <summary>
        /// Test whether formula error of division by zero is returned
        /// </summary>
        [TestMethod()]
        public void InEvaluate2()
        {
            Formula a = new("x6/0", s => s, s => true);
            FormulaError er = new();
            Assert.AreEqual(er.GetType(), a.Evaluate(x=>6).GetType());
        }

        /// <summary>
        /// Test whether formula error of division by zero is returned
        /// </summary>
        [TestMethod()]
        public void FormulaErrorInEvaluate3()
        {
            Formula a = new("6/x6", s => s, s => true);
            FormulaError er = new();
            Assert.AreEqual(er.GetType(), a.Evaluate(x => 0).GetType());
        }

        /// <summary>
        /// Test whether formula error of division by zero is returned
        /// </summary>
        [TestMethod()]
        public void FormulaErrorInEvaluate4()
        {
            Formula a = new("(6/x6)", s => s, s => true);
            FormulaError er = new();
            Assert.AreEqual(er.GetType(), a.Evaluate(x => 0).GetType());
        }

        /// <summary>
        /// Test whether formula error of division by zero is returned
        /// </summary>
        [TestMethod()]
        public void FormulaErrorInEvaluate5()
        {
            Formula a = new("6/(2-2)", s => s, s => true);
            FormulaError er = new();
            Assert.AreEqual(er.GetType(), a.Evaluate(null).GetType());
        }

        /// <summary>
        /// Test Formula constructor works
        /// </summary>
        [TestMethod()]
        public void Formula1()
        {
            Formula a = new("2+3-4");
        }

        /// <summary>
        /// Test check whether the token that immediately follows a number, a variable, or a closing parenthesis
 		///is either an operator or a closing parenthesis.
 		///If not, throw an FormulaFormatException.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Formula2()
        {
            Formula a = new("2(");
        }

        /// <summary>
        /// Test Formula works
        /// </summary>
        [TestMethod()]
        public void Formula3()
        {
            Formula a = new("(2+_x)/3");
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate1()
        {
            Formula a = new("2+3-4", s => s, s => true);
            Assert.AreEqual(1.0,a.Evaluate( x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate2()
        {
            Formula a = new("3*2+3*2.5", s => s, s => true);
            Assert.AreEqual(13.5, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate3()
        {
            Formula a = new("(4*2.3)", s => s, s => true);
            Assert.AreEqual(9.2, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate4()
        {
            Formula a = new("(4/   2.5)", s => s, s => true);
            Assert.AreEqual(1.6, a.Evaluate(x=>3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate5()
        {
            Formula a = new("(4*_x)", s => s, s => true);
            Assert.AreEqual(12.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate6()
        {
            Formula a = new("(6/_x)", s => s, s => true);
            Assert.AreEqual(2.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate7()
        {
            Formula a = new("(2-1)", s => s, s => true);
            Assert.AreEqual(1.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate8()
        {
            Formula a = new("2*(2*1)", s => s, s => true);
            Assert.AreEqual(4.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate9()
        {
            Formula a = new("2/(2/1)", s => s, s => true);
            Assert.AreEqual(1.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate10()
        {
            Formula a = new("2+(2+1)", s => s, s => true);
            Assert.AreEqual(5.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate11()
        {
            Formula a = new("(2+2*3)", s => s, s => true);
            Assert.AreEqual(8.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test Formula & Evaluate works
        /// </summary>
        [TestMethod()]
        public void FormulaEvaluate12()
        {
            Formula a = new("(" +
                "2-2*3)", s => s, s => true);
            Assert.AreEqual(-4.0, a.Evaluate(x => 3));
        }

        /// <summary>
        /// Test GetVariables method 
        /// </summary>
        [TestMethod()]
        public void TestGetVariable1()
        {
            Formula f = new("x+Y+z", s => s.ToUpper(), s => true);
            Formula g = new("x+z", s => s.ToUpper(), s => true);

            List<String> a = new(f.GetVariables());
            List<String> b = new(g.GetVariables());
            Assert.AreNotEqual(a.Count, b.Count);
        }

        /// <summary>
        /// Test GetVariables method 
        /// </summary>
        [TestMethod()]
        public void TestGetVariable2()
        {
            Formula a = new("x+X+z", s => s.ToUpper(), s => true);
            Formula b = new("x+z", s => s.ToUpper(), s => true);
            IEnumerator<string> a1 = a.GetVariables().GetEnumerator();
            IEnumerator<string> b1 = b.GetVariables().GetEnumerator();
            while (a1.MoveNext() && b1.MoveNext())
            {
                Assert.AreEqual(a1.Current, b1.Current);
            }
        }

        /// <summary>
        /// Test ToString method
        /// </summary>
        [TestMethod()]
        public void TestToString1()
        {
            Formula a = new("x + Y", s => s.ToUpper(), s => true);
            Formula b = new("x +  y", s => s.ToUpper(), s => true);
            Assert.AreEqual(a.ToString(), b.ToString());
        }

        /// <summary>
        /// Test ToString method
        /// </summary>
        [TestMethod()]
        public void TestToString2()
        {
            Formula a = new("x + B", s => s.ToUpper(), s => true);
            Formula b = new("x +  y", s => s.ToUpper(), s => true);
            Assert.AreNotEqual(a.ToString(), b.ToString());
        }

        /// <summary>
        /// Test Equals when obj is null
        /// </summary>
        [TestMethod()]
        public void TestEquals1()
        {
            Formula a = new("1+2", s => s, s => true);
            Assert.IsFalse(a.Equals(null));
        }

        /// <summary>
        /// Test Equals when obj is not formula
        /// </summary>
        [TestMethod()]
        public void TestEquals2()
        {
            Formula a = new("1+2", s => s, s => true);
            Assert.IsFalse(a.Equals(2));
        }

        /// <summary>
        /// Test Equals without normalize, result false
        /// </summary>
        [TestMethod()]
        public void TestEquals3()
        {
            Formula a = new("x+y", s => s, s => true);
            Formula b = new("x +Y", s => s, s => true);
            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Test Equals with normalize, result true
        /// </summary>
        [TestMethod()]
        public void TestEquals4()
        {
            Formula a = new("x+y", s => s.ToUpper(), s => true);
            Formula b = new("x +Y", s => s.ToUpper(), s => true);
            Assert.IsTrue(a.Equals(b));
        }

        /// <summary>
        /// Test Equals with normalize, result false
        /// </summary>
        [TestMethod()]
        public void TestEquals5()
        {
            Formula a = new("x+y2", s => s.ToUpper(), s => true);
            Formula b = new("x2 +y", s => s.ToUpper(), s => true);
            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Test Equals result true
        /// </summary>
        [TestMethod()]
        public void TestEquals6()
        {
            Formula a = new("2.0+x7", s => s.ToUpper(), s => true);
            Formula b = new("2.000 + x7", s => s.ToUpper(), s => true);
            Assert.IsTrue(a.Equals(b));
        }

        /// <summary>
        /// Test Equals result true
        /// </summary>
        [TestMethod()]
        public void TestEquals7()
        {
            Formula a = new("3.000", s => s.ToUpper(), s => true);
            Formula b = new("2.000", s => s.ToUpper(), s => true);
            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Test == without normalize, result false
        /// </summary>
        [TestMethod()]
        public void TestDoubleEqual1()
        {
            Formula a = new("x+ y", s => s, s => true);
            Formula b = new("x+ Y", s => s, s => true);
            Assert.IsFalse(a == b);
        }

        /// <summary>
        /// Test == with normalize, result true
        /// </summary>
        [TestMethod()]
        public void TestDoubleEqual2()
        {
            Formula a = new("x+ y", s => s.ToUpper(), s => true);
            Formula b = new("x + Y", s => s.ToUpper(), s => true);
            Assert.IsTrue(a == b);
        }

        /// <summary>
        /// Test == result true
        /// </summary>
        [TestMethod()]
        public void TestDoubleEqual3()
        {
            Formula a = new("2.0+y", s => s.ToUpper(), s => true);
            Formula b = new("2.000 + Y", s => s.ToUpper(), s => true);
            Assert.IsTrue(a == b);
        }

        /// <summary>
        /// Test == result false
        /// </summary>
        [TestMethod()]
        public void TestDoubleEqual4()
        {
            Formula a = new("2.0+Y", s => s.ToUpper(), s => true);
            Formula b = new("2.000 + x", s => s.ToUpper(), s => true);
            Assert.IsFalse(a == b);
        }

        /// <summary>
        /// Test != without normalize, result false
        /// </summary>
        [TestMethod()]
        public void TestNotEqual1()
        {
            Formula a = new("x+ y", s => s, s => true);
            Formula b = new("x+   y", s => s, s => true);
            Assert.IsFalse(a != b);
        }

        /// <summary>
        /// Test != without normalize, result true
        /// </summary>
        [TestMethod()]
        public void TestNotEqual2()
        {
            Formula a = new("x+ y", s => s, s => true);
            Formula b = new("x+ Y", s => s, s => true);
            Assert.IsTrue(a != b);
        }

        /// <summary>
        /// Test != with normalize, result false
        /// </summary>
        [TestMethod()]
        public void TestNotEqual3()
        {
            Formula a = new("x+ y", s => s.ToUpper(), s => true);
            Formula b = new("x+  Y", s => s.ToUpper(), s => true);
            Assert.IsFalse(a != b);
        }

        /// <summary>
        /// Test != with normalize, result true
        /// </summary>
        [TestMethod()]
        public void TestNotEqual4()
        {
            Formula a = new("x+ y", s => s.ToUpper(), s => true);
            Formula b = new("X+ X", s => s.ToUpper(), s => true);
            Assert.IsTrue(a != b);
        }

        /// <summary>
        /// Test != result false
        /// </summary>
        [TestMethod()]
        public void TestNotEqual5()
        {
            Formula a = new("2.0000  +  y", s => s.ToUpper(), s => true);
            Formula b = new("2.0+ y", s => s.ToUpper(), s => true);
            Assert.IsFalse(a != b);
        }

        /// <summary>
        /// Test != result true
        /// </summary>
        [TestMethod()]
        public void TestNotEqual6()
        {
            Formula a = new("2.0000  +  y", s => s.ToUpper(), s => true);
            Formula b = new("2.0+ x", s => s.ToUpper(), s => true);
            Assert.IsTrue(a != b);
        }

        /// <summary>
        /// Test GetHashCode not equal
        /// </summary>
        [TestMethod()]
        public void TestGetHashCode1()
        {
            Formula a = new("x+y", s => s, s => true);
            Formula b = new("x+Y", s => s, s => true);
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        /// <summary>
        /// Test GetHashCode equal
        /// </summary>
        [TestMethod()]
        public void TestGetHashCode2()
        {
            Formula a = new("x+y", s => s.ToUpper(), s => true);
            Formula b = new("x+   Y", s => s.ToUpper(), s => true);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

    }
}