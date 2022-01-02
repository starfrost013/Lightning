using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.Utilities
{
    /// <summary>
    /// NuCore
    /// 
    /// MessageSeverity
    /// 
    /// An enum listing severities of messages that can be output.
    /// </summary>
    public enum MessageSeverity
    {
        /// <summary>
        /// A purely informational message.
        /// </summary>
        Message = 0,

        /// <summary>
        /// A warning that does not prompt a message box to the user.
        /// </summary>
        Warning_NoPrompt = 1,

        /// <summary>
        /// A warning that does prompt a message box to the user.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// An error.
        /// </summary>
        Error = 3,
        
        /// <summary>
        /// A fatal error that causes the program to exit.
        /// </summary>
        FatalError = 4
    }
}
