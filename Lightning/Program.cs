using Lightning.Core;
using Lightning.Core.API;
using NuCore.Utilities;
using System;
using System.Globalization;

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
            // January 23, 2022: Fix bug where DDMS parsing crashes on de-DE locale and other countries that use comma period separators (thx AdrUlb)
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // Handle command-line arguments.
            LaunchArgsResult LAR = LaunchArgs.HandleArgs(Args);

            // Based on the action LaunchArgs
            switch (LAR.Action)
            {
                case LaunchArgsAction.DoNothing:
                    return; // Returns
                case LaunchArgsAction.LaunchGameXML:
                    // Write some basic information to the screen
                    // Set the colour to blue temporarily

                    string InitString = "Lightning";

                    for (int i = 0; i < InitString.Length; i++)
                    {
                        if (i % 3 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        Console.Write(InitString[i]);
                    }

                    Console.ForegroundColor = ConsoleColor.White; 

                    Console.WriteLine(" Software Development Kit (pre-release, January 2022)");
                    Console.WriteLine($"© 2021-2022 starfrost/Lightning Dev Team. All rights reserved.\n");

                    // Load version information
                    LVersion.LoadVersion();

                    // Display platform information
                    Platform.PopulatePlatformInformation();

                    Console.WriteLine($"Running on {Platform.PlatformName}"); // 2021-06-26
                    Console.WriteLine($"Platform version {Platform.Version.OSBrandName}, build {Platform.Version.OSBuildNumber} (update version {Platform.Version.OSUpdateVersion})\n");
                    // Display version information
                    string LVersionString = LVersion.GetVersionString();
                    Console.WriteLine($"Engine version {LVersionString}");
                    
                    // Turns out this is how we will init after all. 
                    DataModel.Init(LAR.Arguments);

                    // -- EXITS AFTER END OF SESSION -- 
                    Environment.Exit(0);

                    return; 
            }
        }
    }
}
