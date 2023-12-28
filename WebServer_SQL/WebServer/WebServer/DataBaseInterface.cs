using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebServer
{
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
    /// This file access database based on the request given by web browser and return a string 
    /// that is ready to be used as respond to web browser by web server
    /// </summary>
    ///
    public class DataBaseInterface
    {

        /// <summary>
        /// The information necessary for the program to connect to the Database
        /// </summary>
        public static readonly string connectionString;

        /// <summary>
        /// Upon construction of this static class, build the connection string
        /// </summary>
        static DataBaseInterface()
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<DataBaseInterface>();
            IConfigurationRoot Configuration = builder.Build();
            var SelectedSecrets = Configuration.GetSection("WebServerSecrets");

            connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = SelectedSecrets["ServerURL"],
                InitialCatalog = SelectedSecrets["DBName"],
                UserID = SelectedSecrets["UserName"],
                Password = SelectedSecrets["DBPassword"],
                ConnectTimeout = 15 // if the server doesn't connect in X seconds, give up
            }.ConnectionString;
        }

        /// <summary>
        /// Build a string to respond web browser and give every player's highest score and display
        /// on a table
        /// </summary>
        /// <returns></returns>
        public string HighScores()
        {

            StringBuilder highScores = new();
            highScores.Append(@"
        <!DOCTYPE html>
        <html>
        <head>
        <style>
        table, th, td {
          border: 1px solid black;
        }");
            highScores.AppendFormat($@"
        </style>
        </head>
          <body>
            <h1>Highscores Board</h1>
              <td><a href=http://localhost:11001>Return to the Main Page</a></td>
            <table>
               <tr>
                  <th>Start Time</th>
                  <th>Max Mass</th>
               </tr>");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query1 = $@"
SELECT Name,Max(MaxMass) As HighScore
FROM MainTable
JOIN MaxMass ON MaxMass.ID = MainTable.ID
JOIN LifeTime ON LifeTime.ID = MainTable.ID
GROUP BY Name";

                using (SqlCommand command = new SqlCommand(query1, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Name = reader["Name"].ToString();
                            string MaxMass = reader["HighScore"].ToString();

                            string nameLink = "http://localhost:11001/scores/" + $"{Name}";
                            highScores.AppendFormat($@"
        <tr>
        <td><a href={nameLink}>{Name}</a></td>
        <td>{MaxMass}</td>
        </tr>");
                        }
                    }
                }
            }
            highScores.Append(@"
        </table>
        </body>
        </html>");
            return highScores.ToString();
        }


        /// <summary>
        /// Return page states individual player's score, including start/end/life time and max mass of each game
        /// Also contains a graph showing the mass of that player over dead time
        /// </summary>
        /// <param name="Name">player name</param>
        /// <returns>string to build page</returns>
        public string IndividualPlayerScore(String Name)
        {
            StringBuilder individualScore = new();
            individualScore.Append(@"
<!DOCTYPE html>
<html>
<head>
<script src=""https://cdn.jsdelivr.net/npm/chart.js@2.8.0""></script>
<style>
table, th, td {
  border: 1px solid black;
}");
            string highScoresLink = "http://localhost:11001/highscores";
            individualScore.AppendFormat($@"
</style>
</head>
  <body>
    <h1>{Name}'s scores page</h1>
    <td><br><a href={highScoresLink}>Return to the HighScores Page</a></td>
    <td><br><a href=http://localhost:11001>Return to the Main Page</a></td>
    <table>
       <tr>
          <th>Start Time</th>
            <th>Dead Time</th>
          <th>Life Time</th>
          <th>Max Mass</th>
       </tr>");

            List<string> deadTimeList = new();
            List<string> maxMassList = new();
            //StringBuilder iterateStartTime = new();
            //StringBuilder iterateMaxMass = new();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query1 = $@"
SELECT *
FROM MainTable
JOIN StartTime ON StartTime.ID = MainTable.ID
JOIN DeadTime ON DeadTime.ID = StartTime.ID
JOIN LifeTime ON LifeTime.ID = DeadTime.ID
JOIN MaxMass ON MaxMass.ID = StartTime.ID
WHERE Name='{Name}'";

                using (SqlCommand command = new SqlCommand(query1, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string StartTime = reader["StartTime"].ToString();
                            string DeadTime = reader["DeadTime"].ToString();
                            string LifeTime = ((double)reader["lifeTime"] / 1000).ToString();
                            string MaxMass = reader["MaxMass"].ToString();

                            deadTimeList.Add(DeadTime);
                            maxMassList.Add(MaxMass);
                            //iterateStartTime.Append(StartTime + ",");
                            //iterateMaxMass.Append(MaxMass + ",");

                            individualScore.AppendFormat($@"
<tr>
<td>{StartTime}</td>
<td>{DeadTime}</td>
<td>{LifeTime} seconds</td>
<td>{MaxMass}</td>
</tr>");


                        }
                    }
                }
            }
            StringBuilder iterateDeadTime = new();
            for (int i = 0; i < deadTimeList.Count; i++)
            {
                iterateDeadTime.Append($"'{deadTimeList[i]}',");
            }
            StringBuilder iterateMaxMass = new();
            for (int i = 0; i < maxMassList.Count; i++)
            {
                iterateMaxMass.Append($"'{maxMassList[i]}',");
            }

            // Add design to chart. Source: https://www.w3schools.com/ai/ai_chartjs.asp
            individualScore.AppendLine(string.Format(@"
</table>
<canvas id='myChart' style='width: 100 %; max - width:700px'></canvas>
<script>
var xValues = [{0}];
var yValues = [{1}];
new Chart('myChart', {{
  type: 'line',
  data:           {{
            	labels: xValues,
    	datasets:                [{{                	backgroundColor: 'rgba(0,0,0,1.0)',
      			borderColor: 'rgba(0,0,0,0.1)',
      			data: yValues }}]
  }},
  options: {{
    title: {{
      display: true,
      text: ""{2}'s MaxMass at Dead Time""
            }}
            }}
}});
</script>
</body>
</html>", iterateDeadTime, iterateMaxMass, Name));
            return individualScore.ToString();
        }

        /// <summary>
        /// Create a table in database through web browser and give dummy data to it
        /// Return information if table has been created
        /// </summary>
        /// <returns>string to build page</returns>
        public string CreateTable()
        {
            StringBuilder buildTable = new();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                //check if table exist in sql 
                //source: https://www.delftstack.com/howto/mysql/check-table-existence-in-mysql/
                string checkExistQuery = "SELECT Count(*) FROM information_schema.tables WHERE table_name = \'DummyTable\'";
                using (SqlCommand existCmd = new SqlCommand(checkExistQuery, con))
                {
                    bool exists = Convert.ToInt32(existCmd.ExecuteScalar().ToString()) == 1;
                    if (exists)
                    {
                        buildTable.Append(@"
<!DOCTYPE html>
<html>
<body>
<h2>You already created a table in database, there is not much you can do.</h2>
</body>
</html>
");
                    }
                    else
                    {
                        buildTable.Clear();
                        buildTable.Append(@"
<!DOCTYPE html>
<html>
<body>
<h1>You created a dummy table in database!</h1>
<td><a href=http://localhost:11001>Return to the Main Page</a></td>
");
                        string createTableQuery = @"
                CREATE TABLE DummyTable(
                DummyData varchar(50)
                );

                INSERT INTO DummyTable
                VALUES ('LOL');";
                        using (SqlCommand command = new SqlCommand(createTableQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    buildTable.Append($@"
                <p>Here is your dummy data: {reader["DummyData"]}
                ");
                                }
                            }
                        }

                    }
                }
            }

            buildTable.Append(@"
</body>
</html>");

            return buildTable.ToString();

        }

        /// <summary>
        /// Add given data from web page to database
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="highmass">highmass</param>
        /// <param name="highrank">highrank</param>
        /// <param name="starttime">starttime</param>
        /// <param name="endtime">endtime</param>
        public void SelfDefinedData(string name, string highmass, string highrank, string starttime, string endtime)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                StringBuilder startTimeFormat = new(starttime);
                startTimeFormat.Replace("%20", " ");
                startTimeFormat.Replace("-", "");
                StringBuilder endTimeFormat = new(starttime);
                endTimeFormat.Replace("%20", " ");
                endTimeFormat.Replace("-", "");

                string query1 = $@"
INSERT INTO MainTable(Name,ID) VALUES('{name}',0);
INSERT INTO MaxMass(ID,MaxMass) VALUES(0,{highmass});
INSERT INTO RankTable(ID,Rank) VALUES(0,{highrank});
INSERT INTO StartTime(ID,StartTime) VALUES(0,'{startTimeFormat.ToString()}');
INSERT INTO DeadTime(ID,DeadTime) VALUES(0,'{endTimeFormat.ToString()}');";

                using (SqlCommand command = new SqlCommand(query1, con))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("Inserted!");
                    }
                }
                
            }
        }


        /// <summary>
        /// Use milliseconds to get the start and end time
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        /// <summary>
        /// Create fancy page string respond and ready to be used by server
        /// </summary>
        /// <returns>fancy page respond string</returns>
        public String FancyPage()
        {
            StringBuilder LeaderBoard = new();
            LeaderBoard.Append(@"
<!DOCTYPE html>
<html>
<head>
<script src=""https://cdn.jsdelivr.net/npm/chart.js@2.8.0""></script>
<style>
h1 {
  color:#c00;
  font-family:sans-serif;
  font-size:2em;
  margin-bottom:0;
}

table {  
  font-family:sans-serif;
  th, td {
    padding:.25em .5em;
    text-align:left;
    &:nth-child(2) {
      text-align:right;
    }
  }
  td {
    background-color:#eee;    
  }
  th {
    background-color:#009;
    color:#fff;
  }
}

.zigzag {
  border-collapse:separate;
  border-spacing:.25em 1em;
  tbody tr:nth-child(odd) {
    transform:rotate(2deg);
  }
  thead tr,
  tbody tr:nth-child(even) {
    transform:rotate(-2deg);
  }
}
}");
            LeaderBoard.AppendFormat($@"
</style>
</head>
  <body>
    <h1>Leader Board</h1>
    <td><br><a href=http://localhost:11001>Return to the Main Page</a></td>
    <table class = ""zigzag"">
       <tr>
          <th>Name</th>
          <th>Rank</th>
       </tr>");

            StringBuilder buildTable = new();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();

                string query = $@"
SELECT *
FROM MainTable
JOIN RankTable ON RankTable.ID = MainTable.ID";


                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["ID"].Equals(0))
                                continue;
                            string Name = reader["Name"].ToString();
                            string Rank = reader["Rank"].ToString();

                            LeaderBoard.AppendFormat($@"
        <tr>
        <td>{Name}</td>
        <td>{Rank}</td>
        </tr>");
                        }
                    }
                }
            }
            LeaderBoard.Append(@"
        </table>
        </body>
        </html>");
            return LeaderBoard.ToString();
        }
    }
}
