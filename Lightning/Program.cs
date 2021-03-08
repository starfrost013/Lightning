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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lightning");

            // TEMP CODE
            Lightning.Core.Version.LoadVersion();

            string LVersionString = Lightning.Core.Version.GetVersionString();

            Console.WriteLine($"© 2021 starfrost. All rights reserved. Version {LVersionString}");
        }
    }
}
