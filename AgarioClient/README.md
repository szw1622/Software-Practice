```
Author:     Jiawen Wang
Partner:    Zhuowen Song
Date:       14-April-2022
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  ashhwang
Repo:       https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-tuningin_agario
Commit #:   d0201340c33d472e9fe3de547c736be919581586 for AgarioModels
            3b27f3ec1f0e1715968d60659b6581c85834009e for ClientGUI
            f0ab79eb204a98592c3a98f07852b07e7a13bd7b for Communications
Solution:   Agario
Copyright:  CS 3500, Zhuowen Song and Jiawen Wang - This work may not be copied for use in Academic Coursework.
```

# Overview of the Game solution
1.MVC pattern is applied in this game:
  AgarioModel peoject represents model and ClientGUI project represents View and Controll.
2.We are using our own Networking class to help communicate between client and server.

# Branching
We wanted to create a new branch to add logger so that it won't change the previously working functionality. But later we accidently push the unfinished branch and encounter conflict with merging this branch to main branch locally. You will see two branches for Agario solution. One is the main branch and one is called Add_Logger. But since we can't merge Add_Logger branch to main branch locally and thus can't push to GitHub, we are finishing all the works on main branch. Thus, please ignore the Add_Loggger branch and only check main branch.

Main branch commit number: d0201340c33d472e9fe3de547c736be919581586 for AgarioModels
                           3b27f3ec1f0e1715968d60659b6581c85834009e for ClientGUI
                           f0ab79eb204a98592c3a98f07852b07e7a13bd7b for Communications

# Testing
We ran the game with different situation as many times as possible to test whether we get the expected functionalities of the game right.

# Partnership
We together programming through the entire solution. Since we had hard time understanding the functionality of each project and especially methods in ClientGUI class, we decided to complete programing together to make sure that the logic behind each functionality is as correct as possible.

# Time Expenditures:

    1. Assignment One:    FormulaEvaluator           Predicted Hours:  15        Actual Hours:  26
    2. Assignment Two:    DependencyGraph            Predicted Hours:  15        Actual Hours:  22
    3. Assignment Three:  Formula                    Predicted Hours:  15        Actual Hours:  26
    4. Assignment Four:   Spreadsheet                Predicted Hours:  20        Actual Hours:  15
    5. Assignment Five:   Spreadsheet                Predicted Hours:  20        Actual Hours:  22
    6. Assignment Six:    GUI                        Predicted Hours:  15        Actual Hours:  16(Jiawen Wang: 8 Zhuowen Song: 8)
    7. Assignment seven:  Networking_and_Logging     Predicted Hours:  20        Actual Hours:  60(Jiawen Wang: 30 Zhuowen Song: 30)
    8. Assignment eight:  Agario                     Predicted Hours:  20        Actual Hours:  50(Jiawen Wang: 25 Zhuowen Song: 25) Debug: 5
   
# Evalutate Ability
When we first glanced all the instructions, we thought that Assignment 8 is easier than last assignment, but still have difficult parts. At first we thought that since we only need to display the game objects at client side, we don't need that much time to achieve it. Then we expected our finishing time would be quite as long as the last assignment. Yet, We spent a lot of time figuring out the logic of moving client player and draw game objects on screen scale. This may be because it takes us longer to absorb and apply new knowledge in programming.
