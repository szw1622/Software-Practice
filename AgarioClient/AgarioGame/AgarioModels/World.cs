using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
/// This file represent a world object of the game. The world object of every client should keeping update as
/// the same scene of the server world object.
/// 
/// An object contains all of the player and food objects.
/// 
/// 
/// </summary>
namespace AgarioModels
{
    public class World
    {
        public const int worldWidth = 5000;
        public const int worldHeight = 5000;

        /// <summary>
        /// Using dictionary to store the game objects, key represents the number id, and the value represents the objects.
        /// </summary>
        public Dictionary<long, Food> Foods = new();
        public Dictionary<long, Player> Players = new();

        /// <summary>
        /// This logger is used to record the game object add or remove from the world.
        /// </summary>
        public ILogger world_logger = NullLogger.Instance;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public World()
        {
            
            }

        
    }
}