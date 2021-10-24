using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptTimer [Non-Datamodel]
    /// 
    /// Holds a timer designed for scripting.
    /// </summary>
    public class ScriptTimer : Stopwatch
    {

        public Script Script { get; set; }

        /// <summary>
        /// The time to countdown
        /// 
        /// [DEPRECATED - Oct 7, 2021]
        /// </summary>
        internal int CountdownTime { get; set; }

        public new void Start()
        {
            base.Start();
        }

        public void Tick()
        {
            if (Elapsed.TotalMilliseconds > CountdownTime) Stop(); 
        
        }

        public void Pause() => Stop(); 

        public new void Stop() => base.Stop();

        
    }
}
