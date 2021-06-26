using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Script
    /// 
    /// April 16, 2021 (modified April 28, 2021) 
    /// 
    /// Defines a LightningScript script. 
    /// </summary>
    public class Script : SerialisableObject
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override string ClassName => "Script";

        private int _currentlyexecutingline { get; set; }

        /// <summary>
        /// The current execution time of this Lua script.
        /// </summary>
        public int ExecutionTime { get; set; }

        /// <summary>
        /// Is this Lua script running?
        /// </summary>
        public bool IsPaused { get; set; }

        /// <summary>
        /// Stopwatch for the current script.
        /// </summary>
        public Stopwatch CurrentScriptRunningStopwatch { get; set; }

        /// <summary>
        /// Stopwatch used for wait time.
        /// </summary>
        public Stopwatch WaitCountdownStopwatch { get; set; }

        /// <summary>
        /// Is this script the Lua Sandbox?
        /// </summary>
        internal virtual bool IsSandbox { get; }

        public override void OnCreate()
        {
            CurrentScriptRunningStopwatch = new Stopwatch();
            WaitCountdownStopwatch = new Stopwatch(); 
        }

        /// <summary>
        /// The currently executing line.
        /// </summary>
        internal int CurrentlyExecutingLine
        {
            get
            {
                return _currentlyexecutingline;
            }
            set
            {
                if (_currentlyexecutingline >= 0)
                {
                    _currentlyexecutingline = value;
                }
                else
                {
                    if (Name != null)
                    {
                        ErrorManager.ThrowError(ClassName, "ErrorAcquiredInvalidLineException", $"Attempted to acquire invalid line {value} for the script with name {Name}!");
                    }
                    else
                    {
                        ErrorManager.ThrowError(ClassName, "ErrorAcquiredInvalidLineException", $"Attempted to acquire invalid line {value} for a script!");
                    }
                }
            }

        }

        private string _content { get; set; }
        /// <summary>
        /// Used for seamless DataModel serialisation. 
        /// </summary>
        public string Content
        {
            get
            {
                return _content;
            }

            set 
            {

                _content = value; 
            }
        }

    }
}
