using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CheckedEvent
    /// 
    /// August 9, 2021
    /// 
    /// Defines an event fired when a <see cref="CheckBox"/> is checked.
    /// </summary>
    public delegate void CheckedEvent
    (
        object Sender,
        CheckedEventArgs EventArgs


    );
}
