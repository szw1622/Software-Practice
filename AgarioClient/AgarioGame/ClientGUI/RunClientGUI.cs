namespace ClientGUI
{
    /// <summary> 
    /// Author:    Jiawen Wang
    /// Partner:   Zhuowen Song
    /// Date:      4/14/2022
    /// Course:    CS 3500, University of Utah, School of Computing 
    /// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
    /// 
    /// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source.  All references used in the completion of the assignment are cited in my README file. 
    /// 
    /// File Contents 
    /// This is the launcher of the clientGUI.
    /// </summary>
    internal static class RunClientGUI
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new ClientGUI());
        }
    }
}