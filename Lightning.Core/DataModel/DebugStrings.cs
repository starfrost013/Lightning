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
            Strings.Add("COME ON INGERLAND, SCORE SOME FACKIN PENALTIES");
            Strings.Add("Brought to you by Raid: Shadow Legends");
            Strings.Add("Emerald died for this - and it was absolutely worth it!");
            Strings.Add("KFC > McDonalds, change my mind");
            Strings.Add("I'm voting Party that Teaches How not to Pay the NHK License Fee - are you?");
            Strings.Add("Probably not faster than Unity yet, but we'll get there!");
            Strings.Add("Certified 100% managed code");
            Strings.Add("This is happenin!");
            Strings.Add("8/30/21");
            Strings.Add("Guaranteed not an Unreal Licensed Product™!");
            Strings.Add("kek!");
            Strings.Add("Lightning - it's tiny and that's how it should be!");

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