using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// ScriptState
    /// 
    /// October 10, 2021 (modified October 11, 2021)
    /// 
    /// Defines the state of a script.
    /// </summary>
    public enum ScriptState
    {
        /// <summary>
        /// The script is ready to start but has not run. 
        /// </summary>
        NotStarted = 0,

        /// <summary>
        /// Running - the script is running normally. Events are handled and each line is run.
        /// </summary>
        Running = 1,

        /// <summary>
        /// Paused - the script has yielded or some other script is running.
        /// </summary>
        Paused = 2,

        /// <summary>
        /// The script has been completed, probably because it completed its taks.
        /// </summary>
        Completed = 3,

        /// <summary>
        /// The script has been terminated, either due to a fatal error or due to running above the global execution time limit.
        /// </summary>
        Terminated = 4,

        /// <summary>
        /// This is the Lua sandbox and cannot be terminated. May just use IsSandbox.
        /// </summary>
        Sandbox = 5
    }
}
