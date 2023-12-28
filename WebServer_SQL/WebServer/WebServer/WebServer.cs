using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer;

/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   Zhuowen Song
/// Date:      4/29/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file act as a server to repond request sent by we browser.
/// </summary>
///


//Wait for web browser to send request
DataBaseInterface db = new();
Networking web_server = new Networking(NullLogger.Instance, OnClientConnect, OnDisconnect, onMessage, '\n');
web_server.WaitForClients(11001, false);
Console.ReadLine();



/// <summary>
/// Basic connect handler - i.e., a browser has connected!
/// Print an information message
/// </summary>
/// <param name="channel"> the Networking connection</param>
void OnClientConnect(Networking channel)
{
    Console.WriteLine("Browser has connected!");
}


/// <summary>
///   <para>
///     When a request comes in (from a browser) this method will
///     be called by the Networking code.  Each line of the HTTP request
///     will come as a separate message.  The "line" we are interested in
///     is a PUT or GET request.  
///   </para>
///   <para>
///     The following messages are actionable:
///   </para>
///   <para>
///      get highscore - respond with a highscore page
///   </para>
///   <para>
///      get scores/name - along with a name, respond with a list of scores for the particular user
///   <para>
///      get scores/name/highmass/highrank/startime/endtime - insert the appropriate data
///      into the database.
///   </para>
///   </para>
///   <para>
///     create - contact the DB and create the required tables and seed them with some dummy data
///   </para>
///   <para>
///    fancy - create a web page with fancy style
///   </para>
///   <para>
///     otherwise send a page not found error
///   </para>
///   <para>
///     Warning: when you send a response, the web browser is going to expect the message to
///     be line by line (new line separated) but we use new line as a special character in our
///     networking object.  Thus, you have to send _every line of your response_ as a new Send message.
///   </para>
/// </summary>
/// <param name="channel"> provided by the Networking code, contains socket and message</param>
/// <param name="message"> request sent by web browser</param>
void onMessage(Networking channel, string message)
{
    //Return a main page
    if (message == "GET / HTTP/1.1")
    {
        foreach (var line in BuildMainPage().Split('\n'))
        {
            channel.Send(line);
        }
        channel.Disconnect();
    }
    //Return a highscores page
    else if (message == "GET /highscores HTTP/1.1")
    {
        foreach (var line in BuildHighScorePage().Split('\n'))
        {
            channel.Send(line);
        }
        channel.Disconnect();
    }
    //Return each player's all scores record page
    else if (message.Contains("scores") && message.Count(f => f == '/') == 3 && !message.Contains("highscores"))

    {
        string[] sp = message.Split('/');
        string checkingName = sp[2].Substring(0, sp[2].Length - 5);
        foreach (var line in BuildIndividualScorePage(checkingName).Split('\n'))
        {
            channel.Send(line);
        }
        channel.Disconnect();
    }
    //Create a table in database, assign dummy data into it, and show the data to web page
    else if (message == "GET /create HTTP/1.1")
    {
        foreach (var line in CreateDBTablePage().Split('\n'))
        {
            channel.Send(line);
        }
        channel.Disconnect();
    }
    //Add user entered [name]/[highmass]/[highrank]/[starttime]/[endtime] to database
    else if (message.Count(f => f == '/') == 7)
    {
        string[] sp = message.Split('/');
        string name = sp[2];
        string highMass = sp[3];
        string highRank = sp[4];
        string startTime = sp[5];
        string endTime =sp[6].Substring(0, sp[6].Length - 5);
        foreach (var line in SelfDefinedDataPage(name,highMass,highRank,startTime,endTime).Split('\n'))
        {
            channel.Send(line);
        }
        channel.Disconnect();
    }
    //Return a web page with fancy style
    else if(message.Contains("fancy"))
    {
        foreach (var line in FancyPage().Split('\n'))
        {
            channel.Send(line);
        }
        channel.Disconnect();
    }
    //Else, build Page Not Found page
    else
    {
        foreach (var line in PageNotFound().Split('\n'))
        {
            channel.Send(line);
        }
        channel.Disconnect();
    }
}


/// <summary>
/// Handle disconnection from web browser
/// </summary>
void OnDisconnect(Networking channel)
{
    Debug.WriteLine($"Goodbye {channel.RemoteAddressPort}");
}

/// <summary>
/// Create the HTTP response header, containing items such as
/// the "HTTP/1.1 200 OK" line.
/// 
/// See: https://www.tutorialspoint.com/http/http_responses.htm
/// 
/// Warning, don't forget that there have to be new lines at the
/// end of this message!
/// </summary>
/// <param name="length"> how big a message are we sending</param>
/// <param name="type"> usually html, but could be css</param>
/// <returns>returns a string with the response header</returns>
string BuildHTTPResponseHeader(int length, string type = "text/html")
{
    return $@"
HTTP/1.1 200 OK
Connection: close
Content-Type: text/html; charset=UTF-8
";
}

/// <summary>
///   Create a web page!  The body of the returned message is the web page
///   "code" itself. Usually this would start with the doctype tag followed by the HTML element.  Take a look at:
///   https://www.sitepoint.com/a-basic-html5-template/
/// </summary>
/// <returns> A string the represents a web page.</returns>
string BuildHTTPBody()
{
    string highScoreLink = "http://localhost:11001/highscores";

    return $@"
<!DOCTYPE html>
<html>
<body>
<h1 style=""color:blue;"">Welcome to Agario Score Board!</h1>
<a href={highScoreLink}>High Scores!</a>
<p><br>""http://localhost:11001""shows the main page</p>
<p><br>""http://localhost:11011/highscores""shows the highscores of each player</p>
<p><br>""http://localhost:11001/scores/[name]"" shows player with [name] the start,end,life time and the max mass when dies</p>
<p><br>""http://localhost:11001/create"" create a table in database and add dummy data in it, will show dummy data or table has been created</p>
<p><br>""http://localhost:11001/fancy"" create a fancy we page</p>
<p><br>""http://localhost:11001/scores/[name]/[highmass]/[highrank]/[starttime]/[endtime]"" Add given data to database and show that data has been added</p>
other links will lead to a page not found page
</body>
</html>";
}

/// <summary>
/// Create a response message string to send back to the connecting
/// program (i.e., the web browser).  The string is of the form:
/// 
///   HTTP Header
///   [new line]
///   HTTP Body
///  
///  The Header must follow the header protocol.
///  The body should follow the HTML doc protocol.
/// </summary>
/// <returns> the complete HTTP response</returns>
string BuildMainPage()
{
    string message = BuildHTTPBody();
    string header = BuildHTTPResponseHeader(message.Length);

    return header + message;
}

/// <summary>
/// Build highscores page that show all the highest scores each player get 
/// </summary>
string BuildHighScorePage()
{
    string message = db.HighScores();
    string header = BuildHTTPResponseHeader(message.Length);

    return header + message;
}

/// <summary>
/// Build a page that contains each player's start/end/life time and the mass at end time
/// Also includes a graph showing the mass and the corresponding dead time
/// </summary>
string BuildIndividualScorePage(String Name)
{
    string message = db.IndividualPlayerScore(Name);
    string header = BuildHTTPResponseHeader(message.Length);

    return header + message;
}

/// <summary>
/// Build a page body string when there is some odd request that is not stated in the instruction sample URL
/// </summary>
string BuildPageNotFoundBody()
{
    return $@"
<!DOCTYPE html>
<html>
<body>
<h1>Page Not Found!</h1>
<p>Please enter a correct link address.</p>
</body>
</html>";
}

/// <summary>
/// Build a page when there is some odd request that is not stated in the instruction sample URL
/// </summary>
string PageNotFound()
{
    string message = BuildPageNotFoundBody();
    string header = BuildHTTPResponseHeader(message.Length);

    return header + message;
}

/// <summary>
/// Respond request of building a table in database, and return the dummy data in the created table
/// or giving information that the table has been created
/// </summary>
string CreateDBTablePage()
{
    string message = db.CreateTable();
    string header = BuildHTTPResponseHeader(message.Length);

    return header + message;
}

/// <summary>
/// Build a respond string when web browser request to enter 
/// [name]/[highmass]/[highrank]/[starttime]/[endtime] into database
/// </summary>
string SelfDefinedDataBody()
{
    return $@"
<!DOCTYPE html>
<html>
<body>
<h1>The given data has been wriiten into database!</h1>
</body>
</html>";
}

/// <summary>
/// Build a page when web browser request to enter 
/// [name]/[highmass]/[highrank]/[starttime]/[endtime] into database
/// </summary>
string SelfDefinedDataPage(string name, string highmass, string highrank, string starttime, string endtime)
{
    db.SelfDefinedData(name, highmass, highrank, starttime, endtime);
    string message = SelfDefinedDataBody();
    string header = BuildHTTPResponseHeader(message.Length);

    return header + message;
}

/// <summary>
/// Build a respond string that design a fancy page 
/// </summary>
string FancyBody()
{
    return db.FancyPage();
}

/// <summary>
/// Build a fancy page when web browser request so
/// </summary>
string FancyPage()
{
    string message = FancyBody();
    string header = BuildHTTPResponseHeader(message.Length);

    return header + message;
}
