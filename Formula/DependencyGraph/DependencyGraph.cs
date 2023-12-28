// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary> 
/// Author:    Zhuowen Song 
/// Partner:   None 
/// Date:      1/28/2022
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
        public Dictionary<string, HashSet<string>> Dependents { get; }
        public Dictionary<string, HashSet<string>> Dependees { get; }

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            Dependents = new Dictionary<string, HashSet<string>>();
            Dependees = new Dictionary<string, HashSet<string>>();
        }



    /// <summary>
    /// The number of ordered pairs in the DependencyGraph.
    /// </summary>
    public int Size
    {
       get 
            { 
                return Dependents.Sum(i => i.Value.Count); 
            }
    }


    /// <summary>
    /// The size of dependees(s).
    /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
    /// invoke it like this:
    /// dg["a"]
    /// It should return the size of dependees("a")
    /// </summary>
    public int this[string s]
    {
      get 
            { 
                return GetDependees(s).Count(); 
            }
    }


    /// <summary>
    /// Reports whether dependents(s) is non-empty.
    /// </summary>
    public bool HasDependents(string s)
    {
            if (Dependents.ContainsKey(s) && Dependents[s].Count > 0)
                return true;
            return false;
        }


    /// <summary>
    /// Reports whether dependees(s) is non-empty.
    /// </summary>
    public bool HasDependees(string s)
    {
            if (Dependees.ContainsKey(s) && Dependees[s].Count > 0)
                return true;
            return false;
        }


    /// <summary>
    /// Enumerates dependents(s).
    /// </summary>
    public IEnumerable<string> GetDependents(string s)
    {
            if (Dependents.ContainsKey(s))
                return new HashSet<string>(Dependents[s]);
            return new HashSet<string>();
        }

    /// <summary>
    /// Enumerates dependees(s).
    /// </summary>
    public IEnumerable<string> GetDependees(string s)
    {
            if(Dependees.ContainsKey(s))
                return new HashSet<string>(Dependees[s]);
            return new HashSet<string>();
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
            //Add t as denpendency of s
            if (Dependents.ContainsKey(s))
            {
                if (Dependents[s].Contains(t)) return;
                Dependents[s].Add(t);
            }
            else
            {
                Dependents.Add(s, new HashSet<string> { t });
            }
            //Add s as dependee of t
            if (Dependees.ContainsKey(t))
            {
                if (Dependees[t].Contains(s)) return;
                Dependees[t].Add(s);
            }
            else
            {
                Dependees.Add(t, new HashSet<string> { s });
            }
        }


    /// <summary>
    /// Removes the ordered pair (s,t), if it exists
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    public void RemoveDependency(string s, string t)
    {
            //Add t as denpendency of s
            if (Dependents.ContainsKey(s))
            {
                if (Dependents[s].Contains(t))
                {
                    Dependents[s].Remove(t);
                    Dependees[t].Remove(s);
                }
            }
        }


    /// <summary>
    /// Removes all existing ordered pairs of the form (s,r).  Then, for each
    /// t in newDependents, adds the ordered pair (s,t).
    /// </summary>
    public void ReplaceDependents(string s, IEnumerable<string> newDependents)
    {
            foreach(string removing in GetDependents(s))
            {
                RemoveDependency(s, removing);
            }
            foreach (string adding in newDependents)
            {
                AddDependency(s,adding);
            }
        }


    /// <summary>
    /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
    /// t in newDependees, adds the ordered pair (t,s).
    /// </summary>
    public void ReplaceDependees(string s, IEnumerable<string> newDependees)
    {
            foreach (string removing in GetDependees(s))
            {
                RemoveDependency(removing, s);
            }
            foreach (string adding in newDependees)
            {
                AddDependency(adding, s);
            }
        }

  }

}
