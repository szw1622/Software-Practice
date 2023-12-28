using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   Zhuowen Song
/// Date:      4/4/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file is an extension to show the time and thread for the logger.
/// </summary>
/// 
namespace FileLogger
{
    internal static class ShowTimeAndThreadClass
    {
		public static string ShowTimeAndThread(this string s)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(DateTime.Now);
			sb.Append(" (");
			sb.Append(Thread.CurrentThread.ManagedThreadId);
			sb.Append(") ");
			return sb.ToString() + s;
		}
	}
}
