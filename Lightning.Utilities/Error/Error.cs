using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    /// <summary>
    /// Lightning
    /// 
    /// Error
    /// 
    /// Non-instanceable object
    /// </summary>
    public class Error
    {
        public delegate bool Handler(Error err);
        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
