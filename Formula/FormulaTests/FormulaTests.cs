using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SpreadsheetUtilities;

/// <summary>
///This is a test class for Formula and is intended
///to contain all Formula Unit Tests
///
/// Author:    Zhuowen Song 
/// Partner:   None 
/// Date:      2/4/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Zhuowen Song - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Zhuowen Song, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    [This code simulation a formula evaluator which uses standard infix notation, and respect the usual precedence rules] 
///</summary>
namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        private static string up(string s)
        {
            return s.ToUpper();
        }
        private static bool A(string s)
        {
            return s.StartsWith("A");
        }

        //Test build formula      ---------------------------------------------------------------------------------------
        [TestMethod]
        public void buildFormula1()
        {
            Formula tester = new Formula("1 + 1");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula2()
        {
            Formula f = new Formula(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula3()
        {
            Formula f = new Formula("");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula4()
        {
            Formula f = new Formula("1 + b2", up, A);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula5()
        {
            Formula f = new Formula("(2+3)(");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula6()
        {
            Formula f = new Formula("-2+3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula7()
        {
            Formula f = new Formula("2+#3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula8()
        {
            Formula f = new Formula("-");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula9()
        {
            Formula f = new Formula("2b");
        }

        [TestMethod]
        public void buildFormula10()
        {
            Formula f1 = new Formula("x + Y", up, s => true);
            Formula f2 = new Formula("X+Y");

            Assert.AreEqual(f1, f2);
        }

        [TestMethod]
        public void buildFormula11()
        {
            Formula f1 = new Formula("1 + 1");
            Formula f2 = new Formula("1.00+1.0");

            Assert.AreEqual(f1, f2);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula12()
        {
            Formula f = new Formula("*3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void buildFormula13()
        {
            Formula f = new Formula("/3");
        }

        //Test calculate formula      ---------------------------------------------------------------------------------------
        [TestMethod]
        public void calculateFormula1()
        {
            Formula f = new Formula("2+3");
            object r = f.Evaluate(null);
            Assert.AreEqual(5.0, r);
        }

        [TestMethod]
        public void calculateFormula2()
        {
            Formula f = new Formula("5 - x1");
            object r = f.Evaluate(s => 1.0);
            Assert.AreEqual(4.0, r);
        }

        [TestMethod]
        public void calculateFormula3()
        {
            Formula f = new Formula("2+3*5");
            object r = f.Evaluate(null);
            Assert.AreEqual((double)17, r);
        }

        [TestMethod]
        public void calculateFormula4()
        {
            Formula f = new Formula("(2+3)*5");
            object r = f.Evaluate(null);
            Assert.AreEqual((double)25, r);
        }

        [TestMethod]
        public void calculateFormula5()
        {
            Formula f = new Formula("(2*3)");
            object r = f.Evaluate(null);
            Assert.AreEqual((double)6, r);
        }

        [TestMethod]
        public void calculateFormula6()
        {
            Formula f = new Formula("(5/2)");
            object r = f.Evaluate(null);
            Assert.AreEqual(2.5, r);
        }

        [TestMethod]
        public void calculateFormula7()
        {
            Formula f = new Formula("x1 / 2");
            object r = f.Evaluate(s => 10.0);
            Assert.AreEqual(5.0, r);
        }

        [TestMethod]
        public void calculateFormula8()
        {
            Formula f = new Formula("2/0");
            object r = f.Evaluate(null);
            Assert.AreEqual(0, 0);
        }

        //other testers         -------------------------------------------------------------------------------------------
        [TestMethod]
        public void testGetVariables1()
        {
            Formula f = new Formula("x+y*z", up, s => true);
            List<string> formula = new List<string>(f.GetVariables());
            List<string> result = new List<string>();
            result.Add("X");
            result.Add("Y");
            result.Add("Z");
            for(int i = 0; i < formula.Count; i++)
            {
                Assert.AreEqual(result[i], formula[i]);
            }
        }

        [TestMethod]
        public void testGetVariables2()
        {
            Formula f = new Formula("x+X*z", up, s => true);
            List<string> formula = new List<string>(f.GetVariables());
            List<string> result = new List<string>();
            result.Add("X");
            result.Add("Z");
            for (int i = 0; i < formula.Count; i++)
            {
                Assert.AreEqual(result[i], formula[i]);
            }
        }

        [TestMethod]
        public void testGetVariables3()
        {
            Formula f = new Formula("x+X*z");
            List<string> formula = new List<string>(f.GetVariables());
            List<string> result = new List<string>();
            result.Add("x");
            result.Add("X");
            result.Add("z");
            for (int i = 0; i < formula.Count; i++)
            {
                Assert.AreEqual(result[i], formula[i]);
            }
        }

        [TestMethod]
        public void testToString1()
        {
            Formula f = new Formula("x + Y * z");
            string formula = f.ToString();
            string result = "x+Y*z";
            Assert.ReferenceEquals(result, f);
        }

        [TestMethod]
        public void testToString2()
        {
            Formula f = new Formula("x + Y * z", up, s => true);
            string formula = f.ToString();
            string result = "X+Y*Z";
            Assert.ReferenceEquals(result, f);
        }

        [TestMethod]
        public void testEquals1()
        {
            Assert.IsTrue(new Formula("x1+y2", up, s => true).Equals(new Formula("X1  +  Y2")));
        }

        [TestMethod]
        public void testEquals2()
        {
            Assert.IsFalse(new Formula("x1+y2").Equals(new Formula("X1+Y2")));
        }

        [TestMethod]
        public void testEquals3()
        {
            Assert.IsFalse(new Formula("x1+y2").Equals(new Formula("y2+x1")));
        }

        [TestMethod]
        public void testEquals4()
        {
            Assert.IsTrue(new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void testEquals5()
        {
            Formula f1 = new Formula("1+2");
            f1 = null;
            Formula f2 = new Formula("1+2");
            f2 = null;
            Assert.IsTrue(f1 == f2);
        }

        [TestMethod]
        public void testEquals6()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1+2");
            Assert.IsTrue(f1 == f2);
        }

        [TestMethod]
        public void testEquals7()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1+2");
            Assert.IsFalse(f1 != f2);
        }

        [TestMethod]
        public void testHashCode()
        {
            Formula f1 = new Formula("1+2");
            int f = f1.GetHashCode();
            string r = "1+2";
            int rh = r.GetHashCode();
            Assert.IsTrue(f == rh);
        }
    }
}