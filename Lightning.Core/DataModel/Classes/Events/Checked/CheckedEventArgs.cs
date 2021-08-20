using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CheckedEventArgs
    /// 
    /// August 9, 2021
    /// 
    /// Defines event arguments for the <see cref="CheckedEvent"/> event.
    /// </summary>
    public class CheckedEventArgs : EventArgs
    {
        /// <summary>
        /// Determines if the checkbox is checked.
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Optional <see cref="MouseEventArgs"/> for extra handling of event arguments.
        /// </summary>
        public MouseEventArgs InnerEventArgs { get; set; }
    }
}
