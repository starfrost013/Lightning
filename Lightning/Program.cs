using Lightning.Core;
using System;

/// <summary>
/// Lightning SDK Platform
/// 
/// © 2021 starfrost
/// 
/// CONFIDENTIAL.
/// 
/// This source code is not open-source software and should be treated as such!
/// </summary>
namespace Lightning
{
    
    public class Program
    {
        /// <summary>
        /// TEMP
        /// </summary>
        public static DataModel DataModelX { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Lightning");

            // TEMP CODE
            LVersion.LoadVersion();

            string LVersionString = LVersion.GetVersionString();
            Console.WriteLine($"© 2021 starfrost. All rights reserved. Version {LVersionString}");

            DataModelX = new DataModel();


        }
    }
}
