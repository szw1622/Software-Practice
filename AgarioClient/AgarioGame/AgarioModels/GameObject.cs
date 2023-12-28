using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
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
/// This file represent a game object of the game.
/// </summary>
namespace AgarioModels
{
    public class GameObject
    {
        /// <summary>
        /// The number id of this object
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// The x value of center location of this object
        /// </summary>
        public float X
        {
            get { return location.X; }
            set { location.X = value; }
        }

        /// <summary>
        /// The y value of center location of this object
        /// </summary>
        public float Y
        {
            get { return location.Y; }
            set { location.Y = value; }
        }

        /// <summary>
        /// The ARGBcolor of this object
        /// </summary>
        public int ARGBColor { get; set; }

        /// <summary>
        /// This value is used to determine how big to draw the circle
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Location of this object.
        /// Json ignore this value.
        /// </summary>
        [JsonIgnore]
        public Vector2 location;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GameObject()
        {

        }

    }
}
