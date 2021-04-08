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
    
    public class Program
    {
        /// <summary>
        /// TEMP
        /// </summary>
        public static DataModel DataModelX { get; set; }

        /// <summary>
        /// Launch arguments to be passed to the engine. 
        /// </summary>
        public static LaunchArgs LaunchArgs { get; set; }

        static void Main(string[] Args)
        {
            // Handle command-line arguments.
            HandleArgs(Args);

            // Write some basic information to the screen
            Console.WriteLine("Lightning");

            LVersion.LoadVersion();
        
            string LVersionString = LVersion.GetVersionString();
            Console.WriteLine($"© 2021 starfrost. All rights reserved. Version {LVersionString}");

            DataModelX = new DataModel();
            DataModel.Init(); 

            DataModelX.ATest_Serialise();
            Logging.Log("------ Serialised using DDMS  [Test - 2021/03/26] ------\n");
            DataModelX.InstanceDump();

            // yes this breaks all the rules 
            // this code won't be here for long
            RenderService.ATest_RenderServiceQuerySettings(); 

        }

        public static void HandleArgs(string[] Args)
        {
            LaunchArgs = new LaunchArgs();

            switch (Args.Length)
            {
                case 0:
                    MessageBox.Show("Lightning [GameXML]\nGameXML: path to the LGX (Lightning Game XML) file you wish to load.", "Lightning Game Engine", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                default:
                    // only one arg so we can be dumb but we're not going to for extensibility
                    foreach (string Argument in Args)
                    {
                        switch (Argument)
                        {
                            default:
                                LaunchArgs.GameXMLPath = Argument;
                                continue; 
                        }
                    }
                    return; 


            }
        }
    }
}
