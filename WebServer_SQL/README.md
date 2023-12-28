```
Author:     Jiawen Wang
Partner:    Zhuowen Song
Date:       29-April-2022
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  ashhwang
Repo(WebServer):        https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-nine---web-server-sql-www
Commit #:   -------------------------
Repo(Agario):           https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-tuningin_agario
Solution:   Spreadsheet
Copyright:  CS 3500, Zhuowen Song and Jiawen Wang - This work may not be copied for use in Academic Coursework.
```

# Database Table Summary
We have 6 tables in our data base.
1. MainTable - Name/ID - Collect data when one player start the game, record the name and ID of player.
2. StartTime - ID/StartTime - Collect data when one player start the game, record the ID and the start time of this playing.
3. DeadTime - ID/DeadTime - Collect data when one player dead in the game, record the ID and the dead time of this playing.
4. MaxMass - ID/MaxMass - Collect data when one player dead in the game, record the ID and the max mass of this playing.
5. LifeTime - ID/LifeTime - Collect data when one player dead in the game, record the ID and the life time of this playing.
6. RankTable - ID/Rank/Time - Collect data when player rank changed, record the ID, rank and rank change time.

We use ID as the core variable to relate tables.

# Extent of work
In this assignment, we have accomplished:
1. The ability to serve as a web server and return a basic welcome (index.html) page.
2. The ability to return a web page with a chart of highscores.
3. The ability to take in a request to store a highscore.
4. The ability to return a web page showing a chart/graph/etc. of some other type of information that you save in your database.
5. The ability for your client code to contact the database and store information therein.

# Partnership
All code was completed via pair programming.

# Branching
We write all of the code on the main branch

# Testing
We used different kind of brower and enter the URLS to test the connection of webserver and the display of web page.
We ran the game with different situation as many times as possible to test whether the data are recorded correctly into the database.

# Time Expenditures:

    1. Assignment One:    FormulaEvaluator           Predicted Hours:  15        Actual Hours:  26
    2. Assignment Two:    DependencyGraph            Predicted Hours:  15        Actual Hours:  22
    3. Assignment Three:  Formula                    Predicted Hours:  15        Actual Hours:  26
    4. Assignment Four:   Spreadsheet                Predicted Hours:  20        Actual Hours:  15
    5. Assignment Five:   Spreadsheet                Predicted Hours:  20        Actual Hours:  22
    6. Assignment Six:    GUI                        Predicted Hours:  15        Actual Hours:  16(Jiawen Wang: 8 Zhuowen Song: 8)
    7. Assignment seven:  Networking_and_Logging     Predicted Hours:  20        Actual Hours:  60(Jiawen Wang: 30 Zhuowen Song: 30)
    8. Assignment eight:  Agario                     Predicted Hours:  20        Actual Hours:  50(Jiawen Wang: 25 Zhuowen Song: 25) Debug: 5
    9. Assignment nine:   SQL and a Web Server       Predicted Hours:  20        Actual Hours:  50(Jiawen Wang: 25 Zhuowen Song: 25) Debug: 5
