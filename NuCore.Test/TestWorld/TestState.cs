using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Test
{
    /// <summary>
    /// TestState
    /// 
    /// November 13, 2021
    /// 
    /// Defines the current test state. This is to allow tests to take any amount of time and not be skipped.
    /// </summary>
    public enum TestState
    {
        /// <summary>
        /// This test has not started.
        /// </summary>
        ToRun = 0,

        /// <summary>
        /// This test is currently running. 
        /// </summary>
        Running = 1,

        /// <summary>
        /// This test has successfully completed.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// This test has unsuccessfully completed.
        /// </summary>
        Error = 3
    }
}
