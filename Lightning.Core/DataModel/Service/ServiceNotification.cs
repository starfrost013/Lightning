using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// ServiceNotification
    /// 
    /// April 10, 2021
    /// 
    /// Defines a notification that the service is about to perform an action 
    /// </summary>
    public class ServiceNotification
    {
        public string ServiceClassName { get; set; }
        public ServiceNotificationType NotificationType { get; set; }
    }
}
