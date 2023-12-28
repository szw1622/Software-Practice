using Microsoft.Extensions.Logging;
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
/// This file contains code for the customFileLogger, which is a custom file logger 
/// which will output the information that we usually send to the console, to a file.
/// </summary>
/// 
namespace FileLogger
{
	namespace FileLogger
	{
		class CustomFileLogger : ILogger
		{
			StreamWriter writer;

			string categoryName;

			/// <summary>
			/// Creates a new logger of the specific log level, and start wrting
			/// </summary>
			/// <param name="outputFile"></param>
			public CustomFileLogger(string categoryName, bool appendToEnd)
			{
				this.categoryName = "CS3500_Assignment7";
				categoryName = "CS3500_Assignment7";
				string fileName = "LOG_" + categoryName + ".txt";
				writer = new StreamWriter(fileName, appendToEnd);
			}

			/// <summary>
			/// Build our file logger, which has different log level.
			/// </summary>
			/// <typeparam name="TState"></typeparam>
			/// <param name="logLevel"></param>
			/// <param name="eventId"></param>
			/// <param name="state"></param>
			/// <param name="exception"></param>
			/// <param name="formatter"></param>
			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
			{
				StringBuilder sb = new StringBuilder();
				switch (logLevel)
				{
					case LogLevel.Information:
						sb.Append("- Infor - ".ShowTimeAndThread());
						break;
					case LogLevel.Error:
						sb.Append("- Error - ".ShowTimeAndThread());
						break;
					case LogLevel.Warning:
						sb.Append("- Warni - ".ShowTimeAndThread());
						break;
					case LogLevel.Critical:
						sb.Append("- Criti - ".ShowTimeAndThread());
						break;
					case LogLevel.Debug:
						sb.Append("- Debug - ".ShowTimeAndThread());
						break;
					case LogLevel.Trace:
						sb.Append("- Trace - ".ShowTimeAndThread());
						break;
				}
				sb.Append(state.ToString());
				writer.WriteLine(sb.ToString());

				writer.Close();
				string fileName = "LOG_" + categoryName + ".txt";
				writer = new StreamWriter(fileName, true);
			}

			/// <summary>
			/// Closes this logger.
			/// </summary>
			public void Close()
			{
				writer.Close();
			}

			/// <summary>
			/// To implement the logger
			/// </summary>
			/// <param name="logLevel"></param>
			/// <returns></returns>
			/// <exception cref="NotImplementedException"></exception>
            public bool IsEnabled(LogLevel logLevel)
            {
                throw new NotImplementedException();
            }

			/// <summary>
			/// To implement the logger
			/// </summary>
			/// <param name="logLevel"></param>
			/// <returns></returns>
			/// <exception cref="NotImplementedException"></exception>
			public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }
        }
	}
}
