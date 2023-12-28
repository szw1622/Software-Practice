/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   None
/// Date:      02/18/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file contains tests for testing Spreadsheet.cs
/// </summary>
/// 

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using SS;
using System.Collections.Generic;
using System.Xml;

namespace SpreadsheetTests
{
    /// <summary>
    /// Class to tests functions in Spreadsheet.cs
    /// </summary>
    [TestClass]
    public class SpreadsheetTests
    {
        /// <summary>
        /// Test GetCellContents with invalid name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsExc()
        {
            Spreadsheet sp = new();
            sp.GetCellContents("_A@");
        }

        /// <summary>
        /// Test GetCellContents with empty named cell
        /// </summary>
        [TestMethod()]
        public void TestGetCellContents1()
        {
            Spreadsheet sp = new();
            Assert.AreEqual("", sp.GetCellContents("A1"));
        }

        /// <summary>
        /// Test GetCellContents with non-empty named cell
        /// </summary>
        [TestMethod()]
        public void TestGetCellContents2()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "5");
            Assert.AreEqual(5.0, sp.GetCellContents("A1"));
        }
        /// <summary>
        /// Test GetCellContents with non-empty named cell and change its content
        /// </summary>
        [TestMethod()]
        public void TestGetCellContents3()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "2.5");
            sp.SetContentsOfCell("A1", "5.9"); //change content of A1
            string f = "=A1+5";
            sp.SetContentsOfCell("A2", f);
            Assert.AreEqual(5.9, sp.GetCellContents("A1"));
            Formula g = new("A1+5");
            Assert.AreEqual(g, sp.GetCellContents("A2"));
        }

        /// <summary>
        /// Test GetNamesOfAllNonemptyCell with no non-empty cells
        /// </summary>
        [TestMethod()]
        public void TestGetNamesOfAllNonemptyCell1()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "");
            sp.SetContentsOfCell("A2", "");
            Assert.IsFalse(sp.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        /// <summary>
        /// Test GetNamesOfAllNonemptyCell with non-empty cells
        /// </summary>
        [TestMethod()]
        public void TestGetNamesOfAllNonemptyCell2()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "5");
            sp.SetContentsOfCell("A2", "2.6");
            List<string> r1 = new();
            r1.Add("A1");
            r1.Add("A2");
            foreach (string name in sp.GetNamesOfAllNonemptyCells())
            {
                Assert.IsTrue(r1.Contains(name));
            }
        }

        /// <summary>
        /// Test SetCellContents(double) with null name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void DoubleSetCellContentsExc1()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell(null, "2.5"); //null name
        }

        /// <summary>
        /// Test SetCellContents(double) with invalid name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void DoubleSetCellContentsExc2()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("_A@", "2.5"); //invalid name
        }

        /// <summary>
        /// Test SetCellContents(double) works when changing cell content
        /// from double, string or formula to double
        /// </summary>
        [TestMethod()]
        public void DoubleSetCellContents1()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "2.5");
            sp.SetContentsOfCell("A1", "5"); //change cell content from double to double
            Assert.AreEqual(5.0, sp.GetCellContents("A1"));
            sp.SetContentsOfCell("A2", "A1");
            sp.SetContentsOfCell("A2", "9.9"); //change cell content from string to double
            Assert.AreEqual(9.9, sp.GetCellContents("A2"));
            string f = "=A1+5";
            sp.SetContentsOfCell("A3", f);
            sp.SetContentsOfCell("A3", "3.8");
            Assert.AreEqual(3.8, sp.GetCellContents("A3"));
        }

        /// <summary>
        /// Test SetCellContents(double) works
        /// </summary>
        [TestMethod()]
        public void DoubleSetCellContents2()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "2.5");
            string f = "=A1*2";
            string g = "=B1+A1";
            sp.SetContentsOfCell("B1", f);
            sp.SetContentsOfCell("C1", g);
            List<string> r1 = new();
            r1.Add("A1");
            r1.Add("B1");
            r1.Add("C1");
            foreach (string name in sp.SetContentsOfCell("A1", "4.6"))
            {
                Assert.IsTrue(r1.Contains(name));
            }
        }

        /// <summary>
        /// Test SetCellContents(string) with null name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void StringSetCellContentsExc1()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell(null, "A1"); //null name
        }

        /// <summary>
        /// Test SetCellContents(string) with invalid name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void StringSetCellContentsExc2()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("_A@", "A1"); //invalid name
        }


        /// <summary>
        /// Test SetCellContents(string) works when changing cell content
        /// from double, string or formula to string
        /// </summary>
        [TestMethod()]
        public void StringSetCellContents1()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "2.5");
            sp.SetContentsOfCell("A1", "_x"); //change cell content from double to double
            Assert.AreEqual("_x", sp.GetCellContents("A1"));
            sp.SetContentsOfCell("A2", "A1");
            sp.SetContentsOfCell("A2", "_x"); //change cell content from string to double
            Assert.AreEqual("_x", sp.GetCellContents("A2"));
            string f = "=A1+5";
            sp.SetContentsOfCell("A3", f);
            sp.SetContentsOfCell("A3", "_x");
            Assert.AreEqual("_x", sp.GetCellContents("A3"));
        }

        /// <summary>
        /// Test SetCellContents(string) works
        /// </summary>
        [TestMethod()]
        public void StringSetCellContents2()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "2.5");
            string f = "=A1*2";
            string g = "=B1+A1";
            sp.SetContentsOfCell("B1", f);
            sp.SetContentsOfCell("C1", g);
            List<string> r1 = new();
            r1.Add("A1");
            r1.Add("B1");
            r1.Add("C1");
            foreach (string name in sp.SetContentsOfCell("A1", "_x"))
            {
                Assert.IsTrue(r1.Contains(name));
            }
        }

        /// <summary>
        /// test SetContentsOfCell with empty string
        /// </summary>
        [TestMethod()]
        public void StringSetContent3()
        {
            Spreadsheet s = new();
            s.SetContentsOfCell("A1", "");
            Assert.AreEqual("", s.GetCellContents("A1"));
        }

        /// <summary>
        /// Test SetCellContents(formula) with null name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void FormulaSetCellContentsExc1()
        {
            Spreadsheet sp = new();
            string f = "=_x+5";
            sp.SetContentsOfCell(null, f); //null name
        }

        /// <summary>
        /// Test SetCellContents(formula) with invalid name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void FormulaSetCellContentsExc2()
        {
            Spreadsheet sp = new();
            string f = "=_x+5";
            sp.SetContentsOfCell("_A@", f); //invalid name
        }


        /// <summary>
        /// Test SetCellContents(formula) with CirculaException
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void FormulaSetCellContentsExc4()
        {
            Spreadsheet sp = new();
            string f = "=B1*2";
            sp.SetContentsOfCell("A1", f);
            string g = "=C1*2";
            sp.SetContentsOfCell("B1", g);
            string k = "=A1*2";
            sp.SetContentsOfCell("C1", k);
            List<string> tst = new();
            tst.Add("A1");
            tst.Add("C1");
            foreach (string s in sp.GetNamesOfAllNonemptyCells())
            {
                Assert.IsTrue(tst.Contains(s));
            }
            sp.SetContentsOfCell("A1", "5");
        }

        /// <summary>
        /// Test SetCellContents(formula) works when changing cell content
        /// from double, string or formula to formula
        /// </summary>
        [TestMethod()]
        public void FormulaSetCellContents1()
        {
            Spreadsheet sp = new();
            sp.SetContentsOfCell("A1", "2.5");
            sp.SetContentsOfCell("A1", "_x"); //change cell content from double to double
            Assert.AreEqual("_x", sp.GetCellContents("A1"));
            sp.SetContentsOfCell("A2", "A1");
            sp.SetContentsOfCell("A2", "_x"); //change cell content from string to double
            Assert.AreEqual("_x", sp.GetCellContents("A2"));
            string f = "=A1+5";
            sp.SetContentsOfCell("A3", f);
            sp.SetContentsOfCell("A3", "_x");
            Assert.AreEqual("_x", sp.GetCellContents("A3"));
        }

        /// <summary>
        /// Test SetCellContents(formula) works
        /// </summary>
        [TestMethod()]
        public void FormulaSetCellContents2()
        {
            Spreadsheet sp = new();
            string k = "=_x+1";
            sp.SetContentsOfCell("A1", k);
            string f = "=A1*2";
            string g = "=B1+A1";
            sp.SetContentsOfCell("B1", f);
            sp.SetContentsOfCell("C1", g);
            List<string> r1 = new();
            r1.Add("A1");
            r1.Add("B1");
            r1.Add("C1");
            string h = "=_x+9";
            foreach (string name in sp.SetContentsOfCell("A1", h))
            {
                Assert.IsTrue(r1.Contains(name));
            }
        }

        /// <summary>
        /// test formula SetContentsOfCell with IsValid, Normalize
        /// </summary>
        [TestMethod()]
        public void SetContentsFormula3()
        {
            AbstractSpreadsheet s = new Spreadsheet(s => true, s => s.ToUpper(), "");
            s.SetContentsOfCell("b1", "=A1+5");
            s.SetContentsOfCell("a1", "6");
            Assert.AreEqual(11.0, s.GetCellValue("B1"));
            Assert.AreEqual(6.0, s.GetCellValue("A1"));
            Assert.AreEqual(11.0, s.GetCellValue("b1"));
            Assert.AreEqual(6.0, s.GetCellValue("a1"));
        }


        /// <summary>
        /// Test SetCellContents with complex operation
        /// </summary>
        [TestMethod()]
        public void TestSetCellContentComplex()
        {
            Spreadsheet sp = new Spreadsheet();
            List<string> r1 = new();
            r1.Add("A1");
            CollectionAssert.AreEqual(r1, (List<string>)sp.SetContentsOfCell("A1", "2.5"));

            List<string> r2 = new();
            r2.Add("A2");
            string f = "=A1+5";
            CollectionAssert.AreEqual(r2, (List<string>)sp.SetContentsOfCell("A2", f));
            Assert.AreEqual(2.5, sp.GetCellContents("A1"));

            List<string> r3 = new();
            r3.Add("A1");
            r3.Add("A2");
            CollectionAssert.AreEqual(r3, (List<string>)sp.SetContentsOfCell("A1", "5"));

            Assert.AreEqual(5.0, sp.GetCellContents("A1"));
            Formula g = new("A1+5");
            Assert.AreEqual(g, sp.GetCellContents("A2"));

            List<string> NonEmptyCells = new();
            NonEmptyCells.Add("A1");
            NonEmptyCells.Add("A2");
            foreach (string n in sp.GetNamesOfAllNonemptyCells())
            {
                Assert.IsTrue(NonEmptyCells.Contains(n));
            }

        }

        /// <summary>
        /// test Spreadsheet constructor 4 input works
        /// </summary>
        [TestMethod()]
        public void foruconstructor()
        {
            using (XmlWriter writer = XmlWriter.Create("save.txt")) // NOTICE the file with no path
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "5");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A2");
                writer.WriteElementString("contents", "3");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A3");
                writer.WriteElementString("contents", "happy");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // NOTICE: opening the file created by this test (not a pre-existing file)
            AbstractSpreadsheet ss = new Spreadsheet("save.txt", s => true, s => s, "");

            ss.SetContentsOfCell("x1", "=A1+A2");
            Assert.AreEqual(8.0, ss.GetCellValue("x1"));
        }

        /// <summary>
        /// test GetSavedVersion works
        /// </summary>
        [TestMethod()]
        public void save1()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s, "happy");
            ss.Save("ss1.txt");
            Assert.AreEqual("happy", ss.GetSavedVersion("ss1.txt"));
        }

        /// <summary>
        /// test GetSavedVersion with invalid file content
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void save2()
        {
            using (XmlWriter writer = XmlWriter.Create("save.txt")) // NOTICE the file with no path
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spread");
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.GetSavedVersion("save.txt");

        }

        /// <summary>
        /// Test GetSavedVersion with nonexisted file
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void save3()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.GetSavedVersion("waht.txt");
        }

        /// <summary>
        /// write spreadsheet to txt works
        /// </summary>
        [TestMethod()]
        public void save4()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s, "v");
            ss.SetContentsOfCell("a1", "x34");
            ss.SetContentsOfCell("x34", "5");
            ss.Save("work.txt");
        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void save5()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s, "v");
            ss.Save(null);
        }

        /// <summary>
        /// write spreadsheet to txt works
        /// </summary>
        [TestMethod()]
        public void save6()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("a1", "=x34+1");
            a.SetContentsOfCell("x34", "5");
            a.Save("work.txt");
            AbstractSpreadsheet ss = new Spreadsheet("work.txt", s => true, s => s, "default");
            Assert.AreEqual(6.0, ss.GetCellValue("a1"));
            Assert.AreEqual(5.0, ss.GetCellValue("x34"));
        }

        /// <summary>
        /// test constructor with no match version
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void noMatchVersion()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("a1", "=x34+1");
            a.SetContentsOfCell("x34", "5");
            a.Save("work.txt");
            AbstractSpreadsheet ss = new Spreadsheet("work.txt", s => true, s => s, "wo.txt");
            Assert.AreEqual(6.0, ss.GetCellValue("a1"));
            Assert.AreEqual(5.0, ss.GetCellValue("x34"));
        }

        /// <summary>
        /// test spreadsheet constructor 4 input with wrong cell's contents
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void forconstructor2()
        {
            using (XmlWriter writer = XmlWriter.Create("save.txt")) // NOTICE the file with no path
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cell");
                writer.WriteElementString("haha", "A1");
                writer.WriteElementString("contents", "5");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            // NOTICE: opening the file created by this test (not a pre-existing file)
            AbstractSpreadsheet ss = new Spreadsheet("save.txt", s => true, s => s, "");

        }


        /// <summary>
        /// test ss constructor with 4 input and nonexisted file
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void forconstructor3()
        {
            AbstractSpreadsheet ss = new Spreadsheet("kjl.txt", s => true, s => s, "");
        }

        /// <summary>
        /// test spreadsheet constructor 4 input with no correct "cell" section
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void forconstructor4()
        {
            using (XmlWriter writer = XmlWriter.Create("save.txt")) // NOTICE the file with no path
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cells");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "5");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            // NOTICE: opening the file created by this test (not a pre-existing file)
            AbstractSpreadsheet ss = new Spreadsheet("save.txt", s => true, s => s, "");

        }

        /// <summary>
        /// test SetContentsOfCell with invalid double name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void setContentsOfCellExc1()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => s.Substring(0, 1) == "AA", s => s.ToUpper(), "v");
            ss.SetContentsOfCell("bb", "5");
        }

        /// <summary>
        /// test SetContentsOfCell with invalid string name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void setContentsOfCellExc2()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => s.Substring(0, 1) == "AA", s => s.ToUpper(), "v");
            ss.SetContentsOfCell("bb", "hb");
        }

        /// <summary>
        /// test SetContentsOfCell with invalid string name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void setContentsOfCellExc3()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => s.Substring(0, 1) == "AA", s => s.ToUpper(), "v");
            ss.SetContentsOfCell("bb", "=A1+5");
        }

        /// <summary>
        /// test SetContentsOfCell works with IsValid
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void setContentsOfCell2()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => s.Substring(0, 1) == "AA", s => s.ToUpper(), "v");
            ss.SetContentsOfCell("aa", "5");
        }


        /// <summary>
        /// test Spreadsheet Changed status
        /// </summary>
        [TestMethod()]
        public void Changed1()
        {
            using (XmlWriter writer = XmlWriter.Create("sa.txt")) // NOTICE the file with no path
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "5");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A2");
                writer.WriteElementString("contents", "3");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // NOTICE: opening the file created by this test (not a pre-existing file)
            AbstractSpreadsheet ss = new Spreadsheet("sa.txt", s => true, s => s, "");

            ss.SetContentsOfCell("A2", "3");
            Assert.IsFalse(ss.Changed);
        }

        /// <summary>
        /// test Spreadsheet Changed status
        /// </summary>
        [TestMethod()]
        public void Changed2()
        {
            using (XmlWriter writer = XmlWriter.Create("save.txt")) // NOTICE the file with no path
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A2");
                writer.WriteElementString("contents", "3");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A3");
                writer.WriteElementString("contents", "hap");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A4");
                writer.WriteElementString("contents", "=A1+A2");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // NOTICE: opening the file created by this test (not a pre-existing file)
            AbstractSpreadsheet ss = new Spreadsheet("save.txt", s => true, s => s, "");

            ss.SetContentsOfCell("A2", "9");
            Assert.IsTrue(ss.Changed);
            ss.SetContentsOfCell("A1", "");
            Assert.IsFalse(ss.Changed);
            ss.SetContentsOfCell("A3", "hap");
            Assert.IsFalse(ss.Changed);
            ss.SetContentsOfCell("A4", "=A1+A2");
            Assert.IsFalse(ss.Changed);
        }

        /// <summary>
        /// Creating Cell with invalid Formula throws FormulaFormatException, Commented out as this just sets conents to text
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void testSetInvalidFormula()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=B1+9");
            sheet.SetContentsOfCell("B1", "=(c3+");

        }

        /// <summary>
        /// Test GetCellValue with invalid input
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void getCellValExc()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s.ToUpper(), "v");
            ss.SetContentsOfCell("_a", "2");
            ss.GetCellValue("_a");
        }

        /// <summary>
        /// Test GetCellValue when no such named cell in sheet
        /// </summary>
        [TestMethod()]
        public void getCellVal1()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("a1", "2");
            ss.SetContentsOfCell("b2", "=b1");
            Assert.AreEqual("", ss.GetCellValue("b3"));
        }

        /// <summary>
        /// Test GetCellValue with formulaerror happen
        /// </summary>
        [TestMethod()]
        public void getCellValerror()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("a1", "=3/0");
            FormulaError er = new();
            Assert.AreEqual(er.GetType(), ss.GetCellValue("a1").GetType());
        }

        /// <summary>
        /// Test GetCellValue with invalid input happen
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void getCellValerror2()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => s.Substring(0, 1) == "AA", s => s.ToUpper(), "v");
            ss.SetContentsOfCell("_", "=8");

        }

        /// <summary>
        /// Test GetCellValue with invalid input
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void getCellConetnts()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s.ToUpper(), "v");
            ss.SetContentsOfCell("_a", "2");
            ss.GetCellValue("_a");
        }

        /// <summary>
        /// test ss 4 input constructor with version not match
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void versionNotMatch()
        {
            using (XmlWriter writer = XmlWriter.Create("save.txt")) // NOTICE the file with no path
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            AbstractSpreadsheet ss = new Spreadsheet("save.txt", s => true, s => s, "v");
        }

        /// <summary>
        /// wrong "spreadsheet" name in file
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void fourconstructorEXC()
        {
            using (XmlWriter writer = XmlWriter.Create("save.txt"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cells");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "=A2+A3");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("namesss", "A2");
                writer.WriteElementString("contents", "3");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A3");
                writer.WriteElementString("contents", "=A2+A1");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // NOTICE: opening the file created by this test (not a pre-existing file)
            AbstractSpreadsheet ss = new Spreadsheet("save.txt", s => true, s => s, "");


        }
    }
}
