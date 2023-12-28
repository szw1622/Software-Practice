
// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

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
/// This file form the dependency graph and some functions to access dependees and dependents.
/// </summary>
/// 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        private int graph_size;

        //the key of dependee_set is dependent and the value is the collection of
        //corresponding dependees
        private Dictionary<string, HashSet<string>> dependee_set;

        //the key of dependent_set is dependee and the value is the collection of
        //corresponding dependents
        private Dictionary<string, HashSet<string>> dependent_set;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            graph_size = 0;
            dependee_set = new();
            dependent_set = new();
        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return graph_size; }
        }


        /// <summary>
        ///  The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        /// <param name="s">dependent</param>
        /// <returns>size of dependees of input dependent</returns>
        public int this[string s]
        {
           
            get { return GetDependees(s).Count<string>(); }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        /// <param name="s">dependee</param>
        /// <returns>true if input dependee has dependents</returns>
        public bool HasDependents(string s)
        {
            if (dependent_set.ContainsKey(s) && dependent_set[s].Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        /// <param name="s">dependent</param>
        /// <returns>true if input dependent has dependees</returns>
        public bool HasDependees(string s)
        {
            if (dependee_set.ContainsKey(s) && dependee_set[s].Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        /// <param name="s">dependee</param>
        /// <returns>corresponding dependents of input dependee</returns>
        public IEnumerable<string> GetDependents(string s)
        {
            if (!dependent_set.ContainsKey(s))
            {
                HashSet<string> empty_set = new();
                return empty_set;
            }
            else
            {
                return dependent_set[s];
            }
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        /// <param name="s">dependent</param>
        /// <returns>corresponding dependees of input dependent s</returns>
        public IEnumerable<string> GetDependees(string s)
        {
            if (!dependee_set.ContainsKey(s))
            {
                HashSet<string> empty_set = new();
                return empty_set;
            }
            else
            {
                return dependee_set[s];
            }
           
        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            if (!dependent_set.ContainsKey(s))
            {
                dependent_set.Add(s, new HashSet<string>());
                dependent_set[s].Add(t);
                graph_size++;

            }
            else if (dependent_set[s].Contains(t))
            {
                dependent_set[s].Add(t);
            }
            else
            {
                dependent_set[s].Add(t);
                graph_size++;
            }
            if (!dependee_set.ContainsKey(t))
            {
                dependee_set.Add(t, new HashSet<string>());
                dependee_set[t].Add(s);
            }
            else
            {
                dependee_set[t].Add(s);
            }

        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s">dependee</param>
        /// <param name="t">dependent</param>
        public void RemoveDependency(string s, string t)
        {
            if (dependent_set.ContainsKey(s) && dependent_set[s].Contains(t))
            {
                dependent_set[s].Remove(t);
                dependee_set[t].Remove(s);
                graph_size--;
            }

        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        /// <param name="s">Dependee that need to replace its dependents</param>
        /// <param name="newDependents">New dependents that corresponding dependee needs replace with</param>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            
            if (!dependent_set.ContainsKey(s))
            {
                dependent_set.Add(s, new HashSet<string>());
            }
            else
            {
                foreach (string dependent_of_s in dependent_set[s])
                {
                    dependent_set[s].Remove(dependent_of_s);
                    dependee_set[dependent_of_s].Remove(s);
                    graph_size--;

                }
            }
            if(newDependents.Count<string>() != 0)
            {
                foreach (string new_dependent_of_s in newDependents)
                {
                    AddDependency(s, new_dependent_of_s);
                }
            }
            
        }

        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        /// <param name="s">Dependent that need to replace its dependees</param>
        /// <param name="newDependees">New dependees that corresponding dependent needs replace with</param>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            if (!dependee_set.ContainsKey(s))
            {
                dependee_set.Add(s, new HashSet<string>());
            }
            else
            {
                foreach (string dependee_of_s in dependee_set[s])
                {
                    dependee_set[s].Remove(dependee_of_s);
                    dependent_set[dependee_of_s].Remove(s);
                    graph_size--;
                }
            }
            if (newDependees.Count<string>() != 0)
            {
                foreach (string new_dependee_of_s in newDependees)
                {
                    AddDependency(new_dependee_of_s, s);
                }
            }
                
        }

    }

}
