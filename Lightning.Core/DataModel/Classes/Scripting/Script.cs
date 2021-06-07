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
        /// Is this Lua script paused?
        /// 
        /// As we currently use a single thread, every script has to yield on its own,
        /// as if we were using an old-school cooperative multitasking operating system.
        /// 
        /// As a result of this, each script will need to pause if it uses an infinite loop. The maximum execution time also needs to be low in order to prevent poorly written scripts
        /// lagging the game
        /// </summary>
        public bool IsPaused { get; set; }

        /// <summary>
        /// Is this Lua script running?
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Stopwatch for the current script.
        /// </summary>
        public Stopwatch CurrentScriptRunningStopwatch { get; set; }

        /// <summary>
        /// Stopwatch used for wait time.
        /// </summary>
        public Stopwatch WaitCountdownStopwatch { get; set; }

        public override void OnCreate()
        {
            ScriptContent = new List<string>();
            CurrentScriptRunningStopwatch = new Stopwatch();
            WaitCountdownStopwatch = new Stopwatch(); 
        }

        /// <summary>
        /// The currently executing line.
        /// </summary>
        internal int CurrentlyExecutingLine { get
            {
                return _currentlyexecutingline;
            }
            set
            {
                if (_currentlyexecutingline < 0
                    || _currentlyexecutingline > ScriptContent.Count)
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
                else
                {
                    _currentlyexecutingline = value; 
                }
            }

        }

        /// <summary>
        /// A list of the script's lines.
        /// </summary>
        internal List<string> ScriptContent { get; set; }
        

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
                string[] SplitX = value.Split('\n');

                if (SplitX.Length != 0)
                {
                    ScriptContent = SplitX.ToList();
                }

                _content = value; 
            }
        }

    }
}
