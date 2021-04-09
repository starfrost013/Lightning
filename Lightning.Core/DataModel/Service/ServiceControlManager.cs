﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Timers; 
namespace Lightning.Core
{
    /// <summary>
    /// Controls services. 2021-03-10
    /// </summary>
    public class ServiceControlManager : Instance
    {
        public override string ClassName => "ServiceControlManager";
        public override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Instantiable | InstanceTags.ParentLocked | InstanceTags.Serialisable;

        /// <summary>
        /// A list of the currently running services. Each object is a reference to an object directly under the Workspace. 
        /// </summary>
        public List<Service> RunningServices { get; set; }

        public ServiceControlManager()
        {
            RunningServices = new List<Service>();
        }

        public void InitStartupServices(ServiceStartupCommandCollection StartupServices)
        {
            Logging.Log("Initialising startup services...", ClassName);

            List<ServiceStartupCommand> SSCList = StartupServices.Commands;
            
            // Sort by the StartOrder. 
            SSCList = SSCList.OrderBy(SSCList => SSCList.StartOrder).ToList(); 

            foreach (ServiceStartupCommand SSC in StartupServices)
            {
                Logging.Log($"Initialising startup service with name {SSC.ServiceName}, startup priority {SSC.StartOrder}", ClassName);
                StartService(SSC.ServiceName);
            } 
        }

        /// <summary>
        /// Starts the service of type Type. The type must inherit from <see cref="Service"/> in the DataModel. change to classname?
        /// </summary>
        /// <param name="TypeOfService"></param>
        /// <returns>A <see cref="ServiceStartResult"/>containing the success code and - if it has failed - the failure reason; if it succeeds it will include the service.</returns>
        public ServiceStartResult StartService(string ClassName)
        {
            ServiceStartResult SSR = new ServiceStartResult();

            //todo: check for duplicate 
            try
            {
                Logging.Log($"Starting Service {ClassName}", ClassName);
                object ObjX = DataModel.CreateInstance(ClassName);

                Service Svc = (Service)ObjX;

                // Check if another instance of this service is already running.
                if (!StartService_CheckForDuplicateServiceRunning(ClassName))
                {
                    RunningServices.Add(Svc);
                    return Svc.OnStart();
                }
                else
                {
                    SSR.Information = $"Attempted to initiate {ClassName} when it is already running!";
                    return SSR;
                }

                
            }
            catch (ArgumentException err)
            {
                ServiceStartResult SvcSR = new ServiceStartResult();
#if DEBUG
                SvcSR.Information = $"Attempted to instantiate an invalid service\n\n{err}";
#else
                SvcSR.Information = "Attempted to instantiate an invalid service";
#endif

                return SvcSR; 
            }

        }

        // not bool?
        private bool StartService_CheckForDuplicateServiceRunning(string ClassName)
        {
            foreach (Service Svc in RunningServices)
            {
                if (ClassName == Svc.ClassName)
                {
                    return true;
                }
                
            }

            return false;
        }

        /// <summary>
        /// Get the service with name <paramref name="ServiceName"/>
        /// </summary>
        /// <param name="ServiceName">The name of the service you wish to acquire. It must be running - this is signified by a reference being within the <see cref="ServiceControlManager.RunningServices"/> list. </param>
        /// <returns>The service object, or null if it does not exist [TEMP]</returns>
        public Service GetService(string ServiceName)
        {
            foreach (Service Svc in RunningServices)
            {
                if (Svc.ClassName == ServiceName)
                {
                    return Svc;
                }
            }

            return null; // TEMP 
        }

        /// <summary>
        /// Kills the service with class name <paramref name="ServiceName"/>.
        /// </summary>
        /// <param name="ServiceName">The class name of the service to kill. Must inherit from <see cref="Service"/>.</param>
        /// <returns>A <see cref="ServiceShutdownResult"/> object. Success is determined by the <see cref="ServiceShutdownResult.Successful"/> property. For further information, see the documentation for <see cref="ServiceShutdownResult"/>.</returns>
        public ServiceShutdownResult KillService(string ServiceName)
        {
            Logging.Log($"Attempting to kill service with classname {ServiceName}");

            ServiceShutdownResult SSR = new ServiceShutdownResult(); 

            foreach (Service Svc in RunningServices)
            {
                if (Svc.ClassName == ServiceName)
                {
                    ServiceShutdownResult SSR_Svc = Svc.OnShutdown();

                    if (!SSR_Svc.Successful)
                    {
                        SSR.FailureReason = "Service shutdown failure.";
                        return SSR; 
                    }
                    else
                    {
                        RunningServices.Remove(Svc);
                        SSR.Successful = true;
                        return SSR; 
                    }
                    
                    
                }
            }

            SSR.FailureReason = "Attempted to kill a service that does not exist or is not running!";
            return SSR; 
        }

        /// <summary>
        /// Shutdown all services. Used at the killing of the SCM itself during engine shutdown.
        /// </summary>
        /// <returns></returns>
        public ServiceShutdownResult ShutdownAllServices()
        {
            Logging.Log("Shutting down all services...", ClassName);

            ServiceShutdownResult SSR = new ServiceShutdownResult();

            foreach (Service Svc in RunningServices)
            {
                string XClassName = Svc.ClassName;

                ServiceShutdownResult SSR_KillSvc = KillService(XClassName);

                if (!SSR_KillSvc.Successful)
                {
                    SSR.FailureReason = $"SCM: Service shutdown failure: The service {XClassName} failed to shut down: {SSR.FailureReason}";

                }
                else
                {
                    
                    // no error occurred, continue.
                    continue; 
                }
            }

            // No errors have occurred

            SSR.Successful = true;
            return SSR; 

        }

        public void PollServices()
        {
            foreach (Service Svc in RunningServices)
            {
                Svc.Poll();
            }
        }

        public void UpdateServices()
        {
            
        }
    }
}
