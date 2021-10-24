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
        /// The current state of the Lua script.
        /// </summary>
        public ScriptState State { get; set; }

        /// <summary>
        /// Stopwatch for the current script.
        /// 
        /// [DEPRECATED - Oct 7, 2021] 
        /// </summary>
        public Stopwatch CurrentScriptRunningStopwatch { get; set; }

        /// <summary>
        /// Stopwatch used for wait time.
        /// 
        /// [DEPRECATED - Oct 7, 2021]
        /// </summary>
        public Stopwatch WaitCountdownStopwatch { get; set; }


        /// <summary>
        /// The timer that manages the script running.
        /// </summary>
        public ScriptTimer Timer { get; set; }

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
                        ErrorManager.ThrowError(ClassName, "ErrorAcquiredInvalidLineException", $"Attempted to set current script line to invalid line {value} for the script with name {Name}!");
                    }
                    else
                    {
                        ErrorManager.ThrowError(ClassName, "ErrorAcquiredInvalidLineException", $"Attempted to set current script line to invalid line {value} for a script!");
                    }
                }
            }

        }

        /// <summary>
        /// Backing field for <see cref="_content"></see>
        /// </summary>
        private string _content { get; set; }

        /// <summary>
        /// Used for seamless DataModel serialisation. 
        /// </summary>
        public virtual string Content
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

        /// <summary>
        /// Coroutine name. Set by ScriptingService.
        /// </summary>
        internal string CoroutineName { get; set; }

        /// <summary>
        /// Pauses or unpauses the script.
        /// </summary>
        public void Pause()
        {
            State = ScriptState.Paused;
            Timer.Pause(); 
        }

        /// <summary>
        /// Stops the script.
        /// </summary>
        public void Stop()
        {
            State = ScriptState.Completed;
            Timer.Stop(); 
        }

        /// <summary>
        /// Starts the script.
        /// </summary>
        public void Start()
        {
            State = ScriptState.Running;
            Timer.Start();
        }

        /// <summary>
        /// Ticks the script. 
        /// </summary>
        public void Tick() => Timer.Tick(); 

        public Script()
        {
            Timer = new ScriptTimer(); 
        }

    }
}
