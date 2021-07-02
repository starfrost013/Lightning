using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ServiceNotificationType
    /// 
    /// April 10, 2021
    /// 
    /// Defines the valid types of service notifications that can be passed by services to the Service Control Manager.
    /// </summary>
    public enum ServiceNotificationType
    {
        /// <summary>
        /// The service is shutting down.
        /// </summary>
        Shutdown = 0,

        /// <summary>
        /// The service is shutting down and the user has requested the engine to also shut down.
        /// </summary>
        Shutdown_ShutDownEngine = 1,

        /// <summary>
        /// The service is shutting down due to an unrecoverable error.
        /// </summary>
        Crash = 2,

        /// <summary>
        /// The service is shutting down due to a crash that cannot be recovered in any circumstances.
        /// </summary>
        UnrecoverableCrash = 3

    }
}
