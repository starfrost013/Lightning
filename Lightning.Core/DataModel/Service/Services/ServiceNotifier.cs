using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ServiceNotifier
    /// 
    /// April 10, 2021
    /// 
    /// Notifies the Service Control Manager that a service notification event has occurred. Does the heavy lifting for the services.
    /// </summary>
    public static class ServiceNotifier
    {
        public static void NotifySCM(ServiceNotification Notification)
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("ServiceControlManager");

            if (!GIR.Successful || GIR.Instance == null)
            {
                ErrorManager.ThrowError("ServiceNotifier", "ServiceControlManagerFailureException");
            }
            else
            {
                ServiceControlManager TheSCM = (ServiceControlManager)GIR.Instance;
                TheSCM.OnNotifyServiceEvent(Notification);
            }
        }
    }
}
