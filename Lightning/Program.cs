using Lightning.Core;
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

        static void Main(string[] Args)
        {
            Console.WriteLine("Lightning");

            // TEMP CODE
            LVersion.LoadVersion();

            string LVersionString = LVersion.GetVersionString();
            Console.WriteLine($"© 2021 starfrost. All rights reserved. Version {LVersionString}");

            DataModelX = new DataModel();
            DataModelX.ATest_Serialise();
            Logging.Log("------ Serialised using DDMS  [Test - 2021/03/26] ------\n");
            DataModelX.InstanceDump(); 


        }
    }
}
