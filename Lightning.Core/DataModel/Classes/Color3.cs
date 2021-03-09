using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class Color3 : SerialisableObject
    {
        public override string ClassName => "Color3"; 
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        // result class?

        /// <summary>
        /// Convert a relative colour string to a Color3 value.
        /// </summary>
        /// <param name="Colour"></param>
        /// <returns></returns>
        public static Color3 FromRelative(string Colour)
        {
            string[] Spx = Colour.Split(',');

            if (Spx.Length != 2)
            {
                // todo: Errors.xml
                return null; // do we use a result class for this?
            }
            else
            {
                double R00 = Convert.ToDouble(Spx[0]);
                double R01 = Convert.ToDouble(Spx[1]);
                double R02 = Convert.ToDouble(Spx[2]);

                // Error check
                if (R00 < 0 || R00 > 1
                 || R01 < 0 || R01 > 1
                 || R02 < 0 || R02 > 1)
                {
                    return null; //result class?
                }
                    
                Color3 C3 = new Color3
                {
                    R = Convert.ToByte(R00 * 255),
                    G = Convert.ToByte(R01 * 255),
                    B = Convert.ToByte(R02 * 255)
                };

                return C3; 

                
            }
        }

        public static Color3 FromHex(string Colour)
        {
            if (Colour.Contains("#"))
            {
                Colour = Colour.Replace("#", "");
            }

            if (Colour.Length != 6)
            {
                // THROW ERROR IF FAIL
                return null;
                // THROW ERROR IF FAIL - ERRORS.XML
            }
            else
            {
                string R = Colour.Substring(0, 2);
                string G = Colour.Substring(2, 2);
                string B = Colour.Substring(4, 2);

                try
                {
                    byte FR = byte.Parse(R, System.Globalization.NumberStyles.HexNumber);
                    byte FG = byte.Parse(G, System.Globalization.NumberStyles.HexNumber);
                    byte FB = byte.Parse(B, System.Globalization.NumberStyles.HexNumber);

                    Color3 C3 = new Color3();

                    C3.R = FR;
                    C3.G = FG;
                    C3.B = FB;

                    return C3; 
                }
                catch (FormatException)
                {
                    // TEMP - ERRORS.XML
                    return null;
                }

            }


        }
    }
}
