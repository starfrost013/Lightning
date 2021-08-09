using System;
using System.Collections.Generic;
using System.Text;

namespace Polaris.UI
{
    /// <summary>
    /// ObjectInsertionEventArgs
    /// 
    /// August 9, 2021
    /// 
    /// Defines object insertion event arguments for the Polaris Insert Object window. 
    /// </summary>
    public class ObjectInsertionEventArgs : EventArgs
    {
        /// <summary>
        /// The class name of the object to be inserted.
        /// </summary>
        public string ClassName { get; set; }
    }
}
