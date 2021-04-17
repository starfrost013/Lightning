using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ServiceNotification
    /// 
    /// April 10, 2021 (modified April 13, 2021: Introduce Reason)
    /// 
    /// Defines a notification that the service is about to perform an action 
    /// </summary>
    public class ServiceNotification
    {
        /// <summary>
        /// The <see cref="ServiceNotificationType"/> of this notification.
        /// </summary>
        public ServiceNotificationType NotificationType { get; set; }

        /// <summary>
        /// The <see cref="Instance.ClassName"/> of the calling service.
        /// </summary>
        public string ServiceClassName { get; set; }

        /// <summary>
        /// An optional reason for the notification.
        /// </summary>
        public string Reason { get; set; }

    }
}
