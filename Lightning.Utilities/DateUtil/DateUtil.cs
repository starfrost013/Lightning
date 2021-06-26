using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    public static class DateUtil
    {
        public static bool IsYearLeapYear(this int Year)
        {
            if (Year % 4 == 0)
            {
                if (Year % 100 == 0)
                {
                    if (Year % 400 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true; 
            }
            else
            {
                return false; 
            }
        }
    }
}
