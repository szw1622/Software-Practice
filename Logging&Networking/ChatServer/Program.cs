using FileLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatServer
{
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
    /// This is the launcher of the ServerGUI.
    /// </summary>
    ///
    class GUI_Application
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServiceCollection services = new ServiceCollection();

            using (CustomFileLogProvider provider = new CustomFileLogProvider())
            {
                services.AddLogging(configure =>
                {
                    configure.AddConsole();
                    configure.AddProvider(provider);
                    configure.SetMinimumLevel(LogLevel.Debug);
                });


                using (ServiceProvider serviceProvider = services.BuildServiceProvider())
                {
                    ILogger<ServerGUI> logger = serviceProvider.GetRequiredService<ILogger<ServerGUI>>();
                    ApplicationConfiguration.Initialize();
                    Application.Run(new ServerGUI());
                }
            }
        }
    }
}