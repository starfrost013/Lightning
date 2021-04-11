using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lightning.Utilities
{
    public static class StringUtil
    {

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

        /// <summary>
        /// Removes the day of a week from a datetime represented as a string.
        /// 
        /// This is required for appveyor builds.
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns></returns>
        public static string RemoveDaysOfWeek(this string DateTime)
        {
            string Monday = "Mon";
            string Tuesday = "Tue";
            string Wednesday = "Wed";
            string Thursday = "Thu";
            string Friday = "Fri";
            string Saturday = "Sat";
            string Sunday = "Sun";

            // If the string contains any of the strings we want to remove,
            // remove them. Use the case insensitive method for this
            if (DateTime.ContainsCaseInsensitive(Monday)) DateTime = DateTime.Replace(Monday, "");
            if (DateTime.ContainsCaseInsensitive(Tuesday)) DateTime = DateTime.Replace(Tuesday, "");
            if (DateTime.ContainsCaseInsensitive(Wednesday)) DateTime = DateTime.Replace(Wednesday, "");
            if (DateTime.ContainsCaseInsensitive(Thursday)) DateTime = DateTime.Replace(Thursday, "");
            if (DateTime.ContainsCaseInsensitive(Friday)) DateTime = DateTime.Replace(Friday, "");
            if (DateTime.ContainsCaseInsensitive(Saturday)) DateTime = DateTime.Replace(Saturday, "");
            if (DateTime.ContainsCaseInsensitive(Sunday)) DateTime = DateTime.Replace(Sunday, "");

            return DateTime;

             
        }

        
    }
}
