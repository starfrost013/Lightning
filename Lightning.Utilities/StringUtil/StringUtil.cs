using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NuCore.Utilities
{
    public static class StringUtil
    {

        public static bool ContainsCaseInsensitive(this string Text, string Value, StringComparison SC = StringComparison.CurrentCultureIgnoreCase) => Text.IndexOf(Value, SC) >= 0;

        /// <summary>
        /// Checks if a string contains numerical characters.
        /// </summary>
        /// <param name="Text">The string you wish to check for numerical characters.</param>
        /// <returns>A boolean value that determines if the string contains numeric values.</returns>
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


        private static bool IsNumeric(byte Byte) => (Byte >= 0x30 && Byte <= 0x3A); // May 4, 2021: Fix this entire hting being fucking broken

        /// <summary>
        /// Checks if a string contains numerical characters exclusively.
        /// </summary>
        /// <param name="Text">The string you wish to check for numerical characters.</param>
        /// <returns>A boolean value determining if the string passed exclusively contains numerical characters.</returns>
        public static bool ExclusivelyContainsNumeric(this string Text)
        {
            if (!ContainsNumeric(Text))
            {
                return false;
            }
            else
            {
                List<byte> TextByteArray = Text.ToByteList(); 

                foreach (byte Byte in TextByteArray)
                {
                    if (!IsNumeric(Byte)) return false;
                }
            }

            return true; 
        }

        /// <summary>
        /// Converts a string to a <see cref="List{T}"/> of bytes.
        /// </summary>
        /// <param name="Text">Extension method - call on object</param>
        /// <returns>A <see cref="(List{byte})"/> containing each character of the string in a byte representation</returns>
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
        /// Convert .NET formatted strings to X(A)ML / SGML-based formatted ones. Replaces the most common characters with their XML mnemonic / NCE equivalents.
        /// </summary>
        /// <param name="NormalString">The string to process.</param>
        /// <returns>A processed string formatted for use in xML (SGML / HTML / XAML / XML).</returns>
        public static string Cs2Xaml(this string NormalString)
        {
            if (NormalString == null)
            {
                return null;
            }

            string ProcessedString = NormalString;

            if (ProcessedString.Contains("&")) ProcessedString = ProcessedString.Replace("<", "&amp;");
            if (ProcessedString.Contains("<")) ProcessedString = ProcessedString.Replace("<", "&lt;");
            if (ProcessedString.Contains(">")) ProcessedString = ProcessedString.Replace(">", "&gt;");
            if (ProcessedString.Contains(0xA0.ToString())) ProcessedString = ProcessedString.Replace(0xA0.ToString(), "&nbsp;"); // Nonbreaking Space
            if (ProcessedString.Contains("&#10;")) ProcessedString = ProcessedString.Replace("&#10;", "\n");
            if (ProcessedString.Contains("&#13;")) ProcessedString = ProcessedString.Replace("&#13;", "\r");

            return ProcessedString;
        }

        /// <summary>
        /// Removes the day of a week from a datetime represented as a string.
        /// 
        /// This is required for appveyor builds.
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns>A string with the days of the week removed.</returns>
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

        /// <summary>
        /// Counts the number of newlines in the string <see cref="Str"/>.
        /// </summary>
        /// <param name="Str">Extension method, call on string objct</param>
        /// <returns>An int with the number of newlines. If the string is null, returns 0.</returns>
        public static int CountNewlines(this string Str)
        {
            if (Str == null) return 0;

            int NewLineCount = 0;

            foreach (char Character in Str)
            {
                if (Character == '\n') NewLineCount++;
            }

            return NewLineCount; 
        }

        /// <summary>
        /// Returns the string with ;ine ID <paramref name="LineID"/>
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public static string GetLineWithId(this string Str, int LineID)
        {
            int LineCount = Str.CountNewlines(); 

            if (LineCount == 0)
            {
                if (LineID != 0) // zero-based for now
                {
                    return null;
                }
                else
                {
                    return Str; 
                }
            }
            else
            {
                if (LineID > LineCount)
                {
                    return null;
                }
                else
                {
                    string[] SplitLines = Str.Split('\n');

                    return SplitLines[LineID];
                }
            }
        }

        public static string Append(this string StringToAppendTo, char Character) => Append<char>(StringToAppendTo, Character);

        public static string Append<T>(this string StringToAppendTo, T StringToConvert)
        {
            Type TypeOfString = typeof(T); 
            
            // i'd check that the user isn't being an idiot
            // but i have more important things to do
            return $"{StringToAppendTo}{StringToConvert.ToString()}";
        }

    }
}
