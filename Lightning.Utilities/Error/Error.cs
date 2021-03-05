using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    public class Error
    {
        public delegate bool Handler(Error err);
        public string Description { get; set; }
        public string Name { get; set; }

    }
}
