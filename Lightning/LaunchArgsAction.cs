using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning
{
    /// <summary>
    /// Launcher: Actions to perform 
    /// </summary>
    public enum LaunchArgsAction
    {
        /// <summary>
        /// Launch a GameXML.
        /// </summary>
        LaunchGameXML = 0,

        /// <summary>
        /// Do nothing.
        /// </summary>
        DoNothing = 1,

        /// <summary>
        /// Initialise, but do not start any services. [Polaris / LightningSDK only]
        /// </summary>
        InitNoServices = 2
    }
}
