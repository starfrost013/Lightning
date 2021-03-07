using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lightning.Utilities
{
    public static class StringUtil
    {
        /*
         * these are all obsolete, not sure if they should be removed
        public static string ToStringEmerald(this Point XPoint)
        {
            return $"{XPoint.X},{XPoint.Y}";
        }

        
        /// <summary>
        /// Split a string into X/Y positions. Static class that can be used from any class.
        /// </summary>
        /// <param name="SplitString"></param>
        /// <returns></returns>
        public static Point SplitXY(this String SplitString, bool Invert = false)
        {
            try
            {
                string[] Split = SplitString.Split(',');

                if (Split.Length != 2)
                {
                    Console.WriteLine("Error converting string to position - must be 2 positions supplied");
                    return new Point(-1, -1);
                }

                // Convert the string components to two double-precision floats
                double X = Convert.ToDouble(Split[0]);
                double Y = Convert.ToDouble(Split[1]);

                // Create a new coordinatepoint

                Point XY = new Point(-2.9471261526665, -2.9471258177776);

                if (Invert)
                {
                    // ATCF is *flipped* for some bizarre reason
                    XY = new Point(Y, X);
                }
                else
                {
                    XY = new Point(X, Y);
                }

                Debug.Assert(XY.X != -2.9471261526665 && Y != -2.9471258177776);

                return XY;
            }
            catch (FormatException err) // essentially this means that an overflow number was
            {
                Console.WriteLine($"Error converting string to position - invalid position\n\n{err}");

                //Todo: Generic type parameter based (for proving skills) user input validation
                return new Point(-1, -1);
            }
        }
        
        
         * removed splitrgb and splitargb as they are obsolete
         * Lightning will use DataModel objects with this functionality
        

        /// <summary>
        /// Splits a string into an Emerald GameDLL version. Modified 2020-04-30 for scriptdomains.
        /// </summary>
        /// <param name="SplitString">The string to split.</param>
        /// <returns></returns>
        public static List<int> SplitVersion(this String SplitString)
        {
            try
            {
                string[] _1 = SplitString.Split('.');

                // If we don't have 3 versions then error out
                if (_1.Length != 4)
                {
                    MessageBox.Show($"Error converting string to version - must be 4 version components supplied", "Emerald Game Engine Error 42", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                List<int> Version = new List<int>();

                // Build the version

                foreach (string _2 in _1)
                {
                    Version.Add(Convert.ToInt32(_2));
                }

                return Version;
            }
            // Error Condition: Attempted to convert an invalid portion of a string. 
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to version - invalid version information\n\n{err}", "Emerald Game Engine Error 56", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        
        /// <summary>
        /// Convert from string to version. 
        /// </summary>
        /// <param name="SplitString"></param>
        /// <returns></returns>
        public static EVersion ToVersion(this String SplitString)
        {
            try
            {
                string[] _1 = SplitString.Split('.');

                // If we don't have 3 versions then error out
                if (_1.Length != 4)
                {
                    MessageBox.Show($"Error converting string to version - must be 4 version components supplied", "Emerald Game Engine Error 60", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                EVersion Version = new EVersion();

                // set the version information
                Version.Major = Convert.ToInt32(_1[0]);
                Version.Minor = Convert.ToInt32(_1[1]);
                Version.Build = Convert.ToInt32(_1[2]);
                Version.Revision = Convert.ToInt32(_1[3]);

                return Version;

            }
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to version - invalid version component supplied!\n\n{err}", "Emerald Game Engine Error 61", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        */

        public static List<string> InnerXml_Parse(this String InnerXml)
        {
            // xml preprocessing
            string[] PreSplit = InnerXml.Split('<');

            List<string> FinalList = new List<string>();

            // Strip it to the name

            foreach (string SplitV1 in PreSplit)
            {
                // Skip entry strings
                if (SplitV1 == "") continue;

                string[] SplitGreaterThan = SplitV1.Split('>');

                // Split each node into its respective value
                foreach (string XMLNodeOrValue in SplitGreaterThan)
                {
                    // remove remaining XML-related characters (Version 515)
                    string XMLNodeOrValuePost = XMLNodeOrValue.Replace("/", "");
                    if (XMLNodeOrValuePost == "") continue; // skip the strings that are not like the other 

                    // only add once
                    if (!FinalList.Contains(XMLNodeOrValuePost)) FinalList.Add(XMLNodeOrValuePost);

                }
            }

            return FinalList;
        }

        public static double RoundNearest(double x, double amount) => Math.Round((x * amount) / amount);


        public static string ConvertArrayToString(this string[] String, bool UseNewline = false, LineEnding LE = LineEnding.Windows) // UseNewline and LineEnding added on conversion to UL5
        {
            StringBuilder SB = new StringBuilder();

            foreach (string StringComponent in String)
            {

                if (UseNewline)
                {
                    SB.Append(StringComponent);
                    continue;
                }
                else
                {
                    switch (LE)
                    {
                        case LineEnding.Windows:
                            SB.Append($"{StringComponent}\r\n");
                            continue;
                        case LineEnding.Unix:
                            SB.Append($"{StringComponent}\n");
                            continue;
                    }
                }


            }

            return SB.ToString();
        }

        public static int GetMonthsBetweenTwoDates(DateTime Initial, DateTime EndDate) // DO NOT DO THE ABSOLUTE!
        {
            return 12 * (Initial.Year - EndDate.Year) - Initial.Month + EndDate.Month;
        }


        public static string PadZero(int X, int Zeros = 1)
        {
            // we don't do this for >10
            if (X > 9) return X.ToString();

            StringBuilder SB = new StringBuilder();

            for (int i = 0; i < Zeros; i++) //AAA
            {
                SB.Append("0");
            }

            SB.Append(X.ToString());

            return SB.ToString();
        }

        public static bool ContainsCaseInsensitive(this string Text, string Value, StringComparison SC = StringComparison.CurrentCultureIgnoreCase) => Text.IndexOf(Value, SC) >= 0;

        /// <summary>
        /// Checks if a string contains numerical characters.
        /// </summary>
        /// <param name="Text">The string you wish to check for numerical characters.</param>
        /// <returns>v2.1 only</returns>
        public static bool ContainsNumeric(this string Text)
        {
            List<byte> TextByteArray = Text.ToByteList();
            // v3.0: pervasive result classes and user input validation
            if (TextByteArray == null) return false;

            foreach (byte TextByte in TextByteArray)
            {
                if (IsNumeric(TextByte)) return true;
            }

            return false;
        }


        private static bool IsNumeric(byte Byte) => (Byte >= 0x48 && Byte <= 0x59);

        /// <summary>
        /// Gets the first index of a string that contains a numeric character.
        /// </summary>
        /// <param name="Text">The string you wish to pass</param>
        /// <returns>The index of the first numeric byte in the string, or -1 if there are no numerical bytes in the string.</returns>
        public static int GetFirstIndexOfNumeric(this string Text)
        {
            List<byte> TextByteList = Text.ToByteList();

            for (int i = 0; i < TextByteList.Count; i++)
            {
                byte TextByte = TextByteList[i];

                if (IsNumeric(TextByte)) return i;
            }

            return -1;
        }

        public static List<byte> ToByteList(this string Text)
        {
            List<byte> ByteList = new List<byte>();

            foreach (char TextCharacter in Text)
            {
                // ASCII :heart:?
                try
                {
                    byte TextByte = Convert.ToByte(TextCharacter);
                    ByteList.Add(TextByte);
                }
                catch (FormatException)
                {
                    return null;
                }
            }


            return ByteList;
        }

        /// <summary>
        /// Convert SGML-based (X(A)ML/HTML) formatted string to plaintext - replaces the most common mnemonics with their character equivalents
        /// </summary>
        /// <param name="HTXAMLFormattedString"></param>
        /// <returns></returns>
        public static string Xaml2Cs(this string HTXAMLFormattedString)
        {
            string Fnl = HTXAMLFormattedString;

            if (Fnl.Contains("&amp;"))
            {
                Fnl = Fnl.Replace("&amp;", "&");
            }

            if (Fnl.Contains("&lt;")) Fnl = Fnl.Replace("&lt;", "<");
            if (Fnl.Contains("&gt;")) Fnl = Fnl.Replace("&gt;", ">");
            if (Fnl.Contains("&nbsp;")) Fnl = Fnl.Replace("&nbsp;", 0xA0.ToString());

            return Fnl;

        }

        /// <summary>
        /// Convert a byte array to a string.
        /// </summary>
        /// <param name="ByteArray"></param>
        /// <returns></returns>

        public static string ByteArrayToString(this byte[] ByteArray)
        {
            List<byte> ByteList = ByteArray.ToList();
            return ByteListToString(ByteList);
        }

        /// <summary>
        /// Convert a byte array to a hex string. (v722.1)
        /// </summary>
        /// <param name="ByteArray"></param>
        /// <returns></returns>

        public static string ByteArrayToHexString(this byte[] ByteArray)
        {
            List<byte> ByteList = ByteArray.ToList();
            return ByteListToHexString(ByteList);
        }

        /// <summary>
        /// Convert a list of bytes to a string.
        /// </summary>
        /// <param name="ByteList"></param>
        /// <returns></returns>
        public static string ByteListToString(this List<byte> ByteList)
        {
            if (ByteList == null)
            {
                return null;
            }
            else
            {
                if (ByteList.Count == 0)
                {
                    return "";
                }
                else
                {
                    StringBuilder SB = new StringBuilder();

                    foreach (byte Byte in ByteList)
                    {
                        SB.Append(Byte);
                    }

                    return SB.ToString();
                }
            }

        }

        /// <summary>
        /// Convert a list of bytes to a hex string. TODO: Reduce code reuse
        /// </summary>
        /// <param name="ByteList"></param>
        /// <returns></returns>
        public static string ByteListToHexString(this List<byte> ByteList)
        {
            if (ByteList == null)
            {
                return null;
            }
            else
            {
                if (ByteList.Count == 0)
                {
                    return "";
                }
                else
                {
                    StringBuilder SB = new StringBuilder();

                    foreach (byte Byte in ByteList)
                    {
                        int NumericalByte = Convert.ToInt32(Byte);

                        SB.Append(NumericalByte.ToString("X"));
                    }

                    return SB.ToString();
                }
            }

        }

    }
}
