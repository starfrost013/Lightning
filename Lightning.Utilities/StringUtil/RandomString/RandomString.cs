using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.Utilities
{
    /// <summary>
    /// RandomString [NuCore.Utilities]
    /// 
    /// October 19, 2021
    /// 
    /// Defines a random string - a string containing random characters.
    /// </summary>
    public class RandomString
    {

        /// <summary>
        /// The string generated as a result of <see cref="GenerateString"/>. This is currently for future use.
        /// </summary>
        private string GeneratedString { get; set; }
        
        /// <summary>
        /// The string generator settings - see <see cref="RandomStringSettings"/>.
        /// </summary>
        public RandomStringSettings Settings { get; set; }

        /// <summary>
        /// Creates a new <see cref="RandomString"/>.
        /// </summary>
        public RandomString()
        {
            Settings = new RandomStringSettings(); 
        }

        /// <summary>
        /// Creates a new <see cref="RandomString"/> with length <see cref="NLength"/>;
        /// </summary>
        /// <param name="NLength"></param>
        public RandomString(int NLength)
        {
            Settings = new RandomStringSettings { Length = NLength };
        }

        public RandomString(int NLength, RandomStringFlags NFlags)
        {
            Settings = new RandomStringSettings { Length = NLength, Flags = NFlags };
        }

        /// <summary>
        /// Generates a random string using the current <see cref="RandomStringSettings"/> (see <see cref="Settings"/>);
        /// </summary>
        /// <returns>A generated string of the length <see cref="RandomStringSettings.Length"/>.</returns>
        public string GenerateString()
        {
            StringBuilder SB = new StringBuilder();

            for (int i = 0; i < Settings.Length; i++)
            {

                char CurrentChar = GenerateChar();
                bool CharAccepted = false;

                while (!CharAccepted)
                {
                    if (Settings.Flags.HasFlag(RandomStringFlags.AlphaLowercase))
                    {
                        if (CurrentChar > 0x40
                        && CurrentChar < 0x5B) CharAccepted = true;
                    }

                    // not using elseif as we are checking for all flags
                    if (Settings.Flags.HasFlag(RandomStringFlags.AlphaUppercase))
                    {
                        if (CurrentChar > 0x60
                        && CurrentChar < 0x7B) CharAccepted = true;
                    }

                    if (Settings.Flags.HasFlag(RandomStringFlags.Numeric))
                    {
                        if (CurrentChar > 0x60
                        && CurrentChar < 0x7B) CharAccepted = true;
                    }

                    if (Settings.Flags.HasFlag(RandomStringFlags.Special))
                    {
                        if ((CurrentChar > 0x32
                        && CurrentChar < 0x7E)
                        || (CurrentChar > 0xA0)) CharAccepted = true;
                    }

                    if (Settings.Flags.HasFlag(RandomStringFlags.All)) CharAccepted = true;

                    if (!CharAccepted) CurrentChar = GenerateChar(); // character rejected, generate enough
                }

                SB.Append(CurrentChar);

            }

            GeneratedString = SB.ToString();
            return GeneratedString; 

        }

        private char GenerateChar()
        {
            Random Rnd = new Random();
            char Ch = (char)Rnd.Next(0, 0xFF); // generate a random character
            return Ch;
        }

    }
}
