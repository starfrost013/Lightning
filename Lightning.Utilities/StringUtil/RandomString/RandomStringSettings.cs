using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    public class RandomStringSettings
    {
        /// <summary>
        /// Backing field for <see cref="Length"/>
        /// </summary>
        private int _length { get; set; }
        public int Length
        {
            get
            {
                return _length;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Cannot set length of RandomString below zero!");
                }
                else
                {
                    _length = value; 
                }
            }

        }

        public RandomStringFlags Flags { get; set; }

        public RandomStringSettings()
        {
            Flags = new RandomStringFlags();
            if (Length == 0) Length = 1;
        }
    }
}
