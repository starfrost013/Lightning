using Lightning.Core;
using Lightning.Core.API;
using Lightning.Utilities;
using System;

/// <summary>
/// Lightning SDK Platform
/// 
/// © 2021 starfrost
/// 
/// CONFIDENTIAL.
/// 
/// This source code and software is not open-source software and should not be treated as such!
/// 
/// Beginning of development        March 2, 2021
/// </summary>
namespace Lightning
{
    
    /// <summary>
    /// Program class for the Lightning Launcher.
    /// </summary>
    public class Program
    {
        public static DataModel Engine { get; set; }

        /// <summary>
        /// Launches Lightning. This is very much not like the Emerald launcher. lol.
        /// </summary>
        /// <param name="Args"></param>

        static void Main(string[] Args)
        {
            // Handle command-line arguments.
            LaunchArgsResult LAR = LaunchArgs.HandleArgs(Args);

            // Based on the action LaunchArgs(
            switch (LAR.Action)
            {
                case LaunchArgsAction.DoNothing:
                    return; // Returns
                case LaunchArgsAction.LaunchGameXML:
                    // Write some basic information to the screen
                    // Set the colour to blue temporarily
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Lightning");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Load version information
                    LVersion.LoadVersion();

                    // Display version information
                    string LVersionString = LVersion.GetVersionString();
                    Console.WriteLine($"© 2021 starfrost. All rights reserved. Version {LVersionString}");

                    // Turns out this is how we will init after all. 
                    Engine = new DataModel();
                    DataModel.Init(LAR.Arguments);

                    // -- EXITS AFTER END OF SESSION -- 
                    Exit();

                    return; 
            }

        }

        /// <summary>
        /// Exits the Engine. 
        /// </summary>
        public static void Exit()
        {
            // This is the last line of code that runs. 
            Environment.Exit(0);

        }
    }
}
