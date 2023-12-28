﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
/// This file represent a player object of the game. A player object is also a GameObject.
/// </summary>
namespace AgarioModels
{
    public class Player : GameObject
    {
        /// <summary>
        /// The name value of this game object(player)
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Player()
        {
        }
    }
}
