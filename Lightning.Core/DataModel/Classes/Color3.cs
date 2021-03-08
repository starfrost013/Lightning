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
            throw new NotImplementedException();
        }
    }
}
