#if DEBUG
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// DebugStrings
    /// 
    /// July 12, 2021
    /// 
    /// Provides messages for debugging purposes
    /// </summary>
    public static class DebugStrings
    {
        public static List<string> Strings { get; set; }

        private static bool DEBUGSTRINGS_INITIALISED { get; set; }
        public static void Init()
        {
            Strings = new List<string>();

            Strings.Add("Because Lightning is better than Thunder");
            Strings.Add("Who wins: One england team boi, or the penalty shootout?");
            Strings.Add("Brought to you by NordVPN");
            Strings.Add("Emerald died for this - and it was absolutely worth it!");
            Strings.Add("KFC > McDonalds, change my mind");
            Strings.Add("I'm voting Party that Teaches How not to Pay the NHK License Fee - are you?");
            Strings.Add("Probably not faster than Unity yet, but we'll get there!");
            Strings.Add("Certified 95% manage");
            Strings.Add("Physics engine is fucked again sorry");
            Strings.Add("2022...sometime...hopefully");
            Strings.Add("Guaranteed not an Unreal Licensed Product™!");
            Strings.Add("Asuka was best girl after all");
            Strings.Add("Tiny and that's how it should be!");
            Strings.Add("A-Level Further Maths was a mistake");

            DEBUGSTRINGS_INITIALISED = true; 
        }

        public static string GetDebugString()
        {
            if (!DEBUGSTRINGS_INITIALISED) Init();

            int LengthMinusOne = Strings.Count - 1;

            Random Rnd = new Random(); // tfw cosmo forgets to read docs

            if (LengthMinusOne == -1)
            {
                return null;
            }
            else
            {
                int StringID = Rnd.Next(0, LengthMinusOne);
                return Strings[StringID]; 
            }
            
        }
    }
}
#endif