using FileLogger.FileLogger;
using Microsoft.Extensions.Logging;

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
/// This is a custom file log provider.
/// </summary>
///
namespace FileLogger
{
	public class CustomFileLogProvider : ILoggerProvider
	{

		CustomFileLogger logger;
		public ILogger CreateLogger(string categoryName, bool appendToEnd)
		{
			this.logger = new CustomFileLogger(categoryName, appendToEnd);
			return logger;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return this.CreateLogger(categoryName, false);
		}

		public void Dispose()
		{
			logger?.Close();
			logger = null;
		}
	}
}