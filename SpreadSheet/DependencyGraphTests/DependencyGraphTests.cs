/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   None
/// Date:      01/25/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file contains tests for functions in DependencyGraph class.
/// </summary>
/// 

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;


namespace DevelopmentTests
{
    /// <summary>
    ///This is a test class for DependencyGraphTest and is intended
    ///to contain all DependencyGraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DependencyGraphTest
    {

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.AreEqual(0, t.Size);
        }


        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyRemoveTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);
        }


        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyEnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            Assert.IsTrue(e2.MoveNext());
            Assert.AreEqual("y", e2.Current);
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.GetDependees("y").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }


        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void SimpleReplaceTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }



        ///<summary>
        ///It should be possibe to have more than one DG at a time.
        ///</summary>
        [TestMethod()]
        public void StaticTest()
        {
            DependencyGraph t1 = new DependencyGraph();
            DependencyGraph t2 = new DependencyGraph();
            t1.AddDependency("x", "y");
            Assert.AreEqual(1, t1.Size);
            Assert.AreEqual(0, t2.Size);
        }




        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.AreEqual(4, t.Size);
        }


        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void EnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }




        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void ReplaceThenEnumerate()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }



        /// <summary>
        ///Using lots of data
        ///</summary>
        [TestMethod()]
        public void StressTest()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();

            // A bunch of strings to use
            const int SIZE = 200;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }

            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }

            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 4; j < SIZE; j += 4)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Add some back
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j += 2)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove some more
            for (int i = 0; i < SIZE; i += 2)
            {
                for (int j = i + 3; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
            }
        }

        /// <summary>
        /// Test size of dependees(s)
        /// </summary>
        [TestMethod()]
        public void test_dependee_sizeof_s()
        {
            DependencyGraph t = new();
            t.AddDependency("a", "b");
            Assert.AreEqual(0, t["a"]);
            t.AddDependency("b", "a");
            Assert.AreEqual(1, t["a"]);
            t.AddDependency("c", "a");
            t.AddDependency("d", "a");
            t.AddDependency("e", "a");
            Assert.AreEqual(4, t["a"]);
            t.RemoveDependency("c", "a");
            t.RemoveDependency("d", "a");
            Assert.AreEqual(2, t["a"]);
        }

        /// <summary>
        /// Test HasDependents for dependee that does not have dependents
        /// </summary>
        [TestMethod()]
        public void test_HasDependents_nodependent()
        {
            DependencyGraph t = new();
            Assert.IsFalse(t.HasDependents("a"));
            IEnumerator<string> e = t.GetDependents("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            t.AddDependency("a", "b");
            Assert.IsFalse(t.HasDependents("b"));
        }

        /// <summary>
        /// Test HasDependents for dependee that have dependent
        /// </summary>
        [TestMethod()]
        public void test_HasDependents_hasdependent()
        {
            DependencyGraph t = new();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.IsTrue(t.HasDependents("a"));

            IEnumerator<string> e = t.GetDependents("a").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            string s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            string s2 = e.Current;
            Assert.AreEqual("b", s1);
            Assert.AreEqual("c", s2);
        }

        /// <summary>
        /// Test HasDependees for dependent that does not have dependees
        /// </summary>
        [TestMethod()]
        public void test_HasDependees_nodependee()
        {
            DependencyGraph t = new();
            Assert.IsFalse(t.HasDependees("a"));
            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            t.AddDependency("a", "b");
            Assert.IsFalse(t.HasDependees("a"));
        }

        /// <summary>
        /// Test HasDependees for dependent that have dependee
        /// </summary>
        [TestMethod()]
        public void test_HasDependees_hasdependee()
        {
            DependencyGraph t = new();
            t.AddDependency("a", "b");
            t.AddDependency("c", "b");
            t.AddDependency("f", "a");
            t.AddDependency("b", "d");
            Assert.IsTrue(t.HasDependees("b"));

            IEnumerator<string> e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            string s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            string s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.AreEqual("a", s1);
            Assert.AreEqual("c", s2);
        }   

        /// <summary>
        /// Test add ordered pair(s,t) when not exist in dependency graph
        /// </summary>
        [TestMethod()]
        public void add_dependency_not_exist()
        {
            DependencyGraph t = new();
            t.AddDependency("a", "b");
            Assert.AreEqual(1, t.Size);
            IEnumerator<string> e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            string s1 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.AreEqual("a", s1);

            t.AddDependency("a", "c");
            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            string s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.AreEqual("a", s1);
            Assert.AreEqual(2, t.Size);

        }

        /// <summary>
        /// Test add ordered pair(s,t) when already exists in dependency graph
        /// Size of dependency graph should remain the same as the size before adding
        /// the duplicate ordered-pair
        /// </summary>
        [TestMethod()]
        public void add_dependency_exist()
        {
            DependencyGraph t = new();
            t.AddDependency("a", "b");
            t.AddDependency("a", "b");
            IEnumerator<string> e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            string s1 = e.Current;
            Assert.AreEqual("a", s1);
            Assert.IsFalse(e.MoveNext());
            Assert.AreEqual(1, t.Size);
        }

        /// <summary>
        /// Test remove ordered pair(s,t) when not exist in dependency graph
        /// The size of dependency graph should be the same as before removing
        /// the pair that does not exists.
        /// </summary>
        [TestMethod()]
        public void remove_dependency_not_exist()
        {
            DependencyGraph t = new();
            t.RemoveDependency("a", "b");
            Assert.AreEqual(0, t.Size);

            t.AddDependency("c", "d");
            t.RemoveDependency("a", "b");
            IEnumerator<string> e = t.GetDependees("b").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
            Assert.AreEqual(1, t.Size);

            t.RemoveDependency("c", "f");
            Assert.AreEqual(1, t.Size);
        }

        /// <summary>
        /// Test remove ordered-pair (s,t) when exists in dependency graph
        /// </summary>
        [TestMethod()]
        public void remove_dependency_exist()
        {
            DependencyGraph t = new();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.RemoveDependency("b", "d");
            t.RemoveDependency("a", "b");
            Assert.AreEqual(2, t.Size);
        }

        /// <summary>
        /// Test ReplaceDependents where dependents of certain string does not exist
        /// </summary>
        [TestMethod()]
        public void replace_dependent_noexist()
        {
            DependencyGraph t = new();
            t.ReplaceDependents("a", new HashSet<string>() {"b", "c"});
            Assert.AreEqual(2, t.Size);
        }

        /// <summary>
        /// Test ReplaceDependents when new dependents set is empty
        /// </summary>
        [TestMethod()]
        public void replace_dependent_empty()
        {
            DependencyGraph t = new();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.ReplaceDependents("b", new HashSet<string>());
            Assert.AreEqual(2, t.Size);
            t.ReplaceDependents("a", new HashSet<string>());
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        /// Test ReplaceDependents where dependents of certain string exist
        /// </summary>
        [TestMethod()]
        public void replace_dependent_exist()
        {
            DependencyGraph t = new();
            t.AddDependency("g", "a");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.ReplaceDependents("a", new HashSet<string>() {"b", "c", "f"});
            Assert.AreEqual(6, t.Size);
            t.ReplaceDependents("a", new HashSet<string>());
            Assert.AreEqual(3, t.Size);
        }

        /// <summary>
        /// Test ReplaceDependees where dependees of certain string does not exist
        /// </summary>
        [TestMethod()]
        public void replace_dependee_noexist()
        {
            DependencyGraph t = new();
            t.ReplaceDependees("a", new HashSet<string>() { "b", "c" });
            Assert.AreEqual(2, t.Size);
        }

        /// <summary>
        /// Test ReplaceDependees when new dependees set is empty
        /// </summary>
        [TestMethod()]
        public void replace_dependee_empty()
        {
            DependencyGraph t = new();
            t.AddDependency("b", "a");
            t.AddDependency("c", "a");
            t.ReplaceDependees("b", new HashSet<string>());
            Assert.AreEqual(2, t.Size);
            t.ReplaceDependees("a", new HashSet<string>());
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        /// Test ReplaceDependees where dependenes of certain string exist
        /// </summary>
        [TestMethod()]
        public void replace_dependee_exist()
        {
            DependencyGraph t = new();
            t.AddDependency("g", "a");
            t.AddDependency("a", "f");
            t.AddDependency("b", "f");
            t.AddDependency("c", "m");
            t.ReplaceDependees("f", new HashSet<string>() { "a", "b", "z" });
            Assert.AreEqual(5, t.Size);
            t.ReplaceDependees("f", new HashSet<string>());
            Assert.AreEqual(2, t.Size);
        }

    }
}
