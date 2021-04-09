using Lightning.Core;
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
        /// <summary>
        /// TEMP
        /// </summary>
        public static DataModel DataModelX { get; set; }

        /// <summary>
        /// Launches Lightning. This is very much not like the Emerald launcher. lol.
        /// </summary>
        /// <param name="Args"></param>

        static void Main(string[] Args)
        {
            // Handle command-line arguments.
            LaunchArgsResult LAR = HandleArgs(Args);

            // Based on the action LaunchArgs(
            switch (LAR.Action)
            {
                case LaunchArgsAction.DoNothing:
                    return; // Returns
                case LaunchArgsAction.LaunchGameXML:
                    // Write some basic information to the screen
                    Console.WriteLine("Lightning");

                    // Load version information
                    LVersion.LoadVersion();

                    // Display version information
                    string LVersionString = LVersion.GetVersionString();
                    Console.WriteLine($"© 2021 starfrost. All rights reserved. Version {LVersionString}");

                    // Test code

                    
                    DataModelX = new DataModel();
                    DataModel.Init(LAR.Arguments);

                    // -- EXITS AFTER END OF SESSION -- 
                    Exit();

                    return; 
            }

            

        }



        public static LaunchArgsResult HandleArgs(string[] Args)
        {
            LaunchArgsResult LAR = new LaunchArgsResult();

            switch (Args.Length)
            {
                case 0:
                    MessageBox.Show("Lightning [GameXML]\nGameXML: path to the LGX (Lightning Game XML) file you wish to load.", "Lightning Game Engine", MessageBoxButton.OK, MessageBoxImage.Information);

                    LAR.Action = LaunchArgsAction.DoNothing;
                    
                    return LAR;
                default:
                    LAR.Action = LaunchArgsAction.LaunchGameXML;
                    // only one arg so we can be dumb but we're not going to for extensibility
                    foreach (string Argument in Args)
                    {
                        switch (Argument)
                        {
                            default:
                                LAR.Arguments.GameXMLPath = Argument;
                                continue; 
                        }
                    }

                    return LAR; 


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
