﻿using System;
using System.Collections.Generic;
using System.Diagnostics; 
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

        /// <summary>
        /// A timer used to update each service. 
        /// </summary>
        private ServiceGlobalData SvcGlobalData { get; set; }

        public ServiceControlManager()
        {
            RunningServices = new List<Service>();
            SvcGlobalData = new ServiceGlobalData(); 
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
        /// Initialises the main timer used for updating services.
        /// 
        /// April 9, 2021
        /// </summary>
        public void InitServiceUpdates()
        {

            
            Logging.Log("Entering main loop...", ClassName);

            Workspace WS = DataModel.GetWorkspace();

            GetInstanceResult GIR = WS.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful || GIR.Instance == null)
            {
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException", $"Failed to load GameSettings or it was somehow unloaded: {GIR.FailureReason}");
                return; 
            }
            else
            {
                GameSettings GS = (GameSettings)GIR.Instance;

                string SettingName = "MaxFPS";

                GetGameSettingResult MaxFPS_Result = GS.GetSetting(SettingName);
                
                if (!MaxFPS_Result.Successful)
                {
                    ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", $"Failed to load a setting that is required for the game to start: {SettingName}.");
                    return; 
                }
                else
                {
                    GameSetting MaxFPS_Setting = MaxFPS_Result.Setting;

                    SvcGlobalData.ServiceUpdateTimer = new Stopwatch();

                    int MaxFPS = (int)MaxFPS_Setting.SettingValue;
                    
                    SvcGlobalData.ServiceUpdateTimer.Start();
                    UpdateGame(MaxFPS); 

                }

            }

        }

        public void UpdateGame(int MaxFPS)
        {
            // slightly less temporary code
            while (true)
            {
                // The 
                long ElapsedMillisecondsSinceStart = SvcGlobalData.ServiceUpdateTimer.ElapsedMilliseconds;

                int TargetFrameTimeMS = 1000 / MaxFPS;

                if (ElapsedMillisecondsSinceStart % TargetFrameTimeMS == 0)
                {
                    UpdateServices();
                }
            }
        }

        /// <summary>
        /// Starts the service of type Type. The type must inherit from <see cref="Service"/> in the DataModel. change to classname?
        /// </summary>
        /// <param name="TypeOfService"></param>
        /// <returns>A <see cref="ServiceStartResult"/> containing the success code and - if it has failed - the failure reason; if it succeeds the Service object will be added to <see cref="RunningServices"/>.</returns>
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
                    SSR.FailureReason = $"Attempted to initiate {ClassName} when it is already running!";
                    return SSR;
                }

                
            }
            catch (ArgumentException err)
            {
                ServiceStartResult SvcSR = new ServiceStartResult();
#if DEBUG
                SvcSR.FailureReason = $"Attempted to instantiate an invalid service\n\n{err}";
#else
                SvcSR.FailureReason = "Attempted to instantiate an invalid service";
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
        private ServiceShutdownResult KillService(string ServiceName, bool ForceShutdown = false)
        {
            Logging.Log($"Attempting to kill the service with type {ServiceName}");

            ServiceShutdownResult SSR = new ServiceShutdownResult(); 

            foreach (Service Svc in RunningServices)
            {
                if (Svc.ClassName == ServiceName)
                {
                    switch (ForceShutdown)
                    {
                        case false:
                            ServiceShutdownResult SSR_Svc = Svc.OnShutdown();

                            if (!SSR_Svc.Successful)
                            {
                                SSR.FailureReason = $"Service shutdown failure: {SSR_Svc.FailureReason}";
                                return SSR;
                            }
                            else
                            {
                                RunningServices.Remove(Svc);
                                SSR.Successful = true;
                                return SSR;
                            }
                        case true:
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
        private ServiceShutdownResult KillAllServices(bool ForceKillAll = false)
        {
            Logging.Log("Shutting down all services...", ClassName);

            ServiceShutdownResult SSR = new ServiceShutdownResult();

            foreach (Service Svc in RunningServices)
            {
                string XClassName = Svc.ClassName;

                ServiceShutdownResult SSR_KillSvc = KillService(XClassName);

                if (!ForceKillAll)
                {
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
                else
                {
                    continue; 
                }
            }

            // No errors have occurred

            SSR.Successful = true;
            return SSR; 

        }

        /// <summary>
        /// Main loop: Polls all services
        /// </summary>
        private void UpdateServices()
        {
            foreach (Service Svc in RunningServices)
            {
                Svc.Poll();
            }

        }

        /// <summary>
        /// Checks if the srevce
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public bool IsServiceRunning(string ClassName)
        {
            foreach (Service Svc in RunningServices)
            {
                if (Svc.ClassName == ClassName)
                {
                    return true;
                }
            }

            return false; 
        }

        /// <summary>
        /// Handles a service notification. 
        /// </summary>
        /// <param name="SvcNotification"></param>
        public void NotifyServiceEvent(ServiceNotification SvcNotification)
        {
            if (SvcNotification == null)
            {
                ErrorManager.ThrowError(ClassName, "AttemptedToPassInvalidServiceNotificationException");
                return;
            }
            else
            {

                // If the ServiceNotification exists, check it
                if (SvcNotification.ServiceClassName == null)
                {
                    ErrorManager.ThrowError(ClassName, "AttemptedToPassInvalidServiceNotificationException");
                    return; 
                }
                else
                {
                    if (SvcNotification.ServiceClassName.Length == 0)
                    {
                        ErrorManager.ThrowError(ClassName, "AttemptedToPassInvalidServiceNotificationException");
                        return; 
                    }
                }

                try
                {
                    Type ServiceType = Type.GetType(SvcNotification.ServiceClassName);

                    if (!ServiceType.IsSubclassOf(typeof(Service)))
                    {
                        ErrorManager.ThrowError(ClassName, "AttemptedToHandleServiceNotificationAboutANonServiceException", $"Attempted to handle a ServiceNotification about {ClassName}, which is not a Service!");
                        return; 
                    }
                    else // If we have a valid notification, handle it 
                    {
                        switch (SvcNotification.NotificationType)
                        {
                            case ServiceNotificationType.Shutdown:
                                Logging.Log(ClassName, $"The {SvcNotification.ServiceClassName} has notified the Service Control Manager that it wishes to shut down. Killing it...");
                                KillService(ClassName);
                                return;
                            case ServiceNotificationType.Shutdown_ShutDownEngine:
                                Logging.Log(ClassName, $"The {SvcNotification.ServiceClassName} has notified the Service Control Manager that it is shutting down as the user has requested an engine shutdown. Shutting down...");
                                ShutdownEngine();
                                return;
                            case ServiceNotificationType.Crash:
                                Logging.Log(ClassName, $"The {SvcNotification.ServiceClassName} has notified the Service Control Manager that it has crashed. Attempting to recover the service...");
                                HandleCrashedService(SvcNotification.ServiceClassName);
                                return;
                        }
                    }
                }
                catch (ArgumentException err)
                {
#if DEBUG
                    ErrorManager.ThrowError(ClassName, "AttemptedToHandleServiceNotificationAboutANonServiceException", $"Attempted to handle a ServiceNotification about {ClassName}, which is not a Service!\n\n{err}");
#else
                    ErrorManager.ThrowError(ClassName, "AttemptedToHandleServiceNotificationAboutANonServiceException", $"Attempted to handle a ServiceNotification about {ClassName}, which is not a Service!");
#endif
                }
            }

            
        }

        /// <summary>
        /// Shuts down the engine. 
        /// </summary>
        private void ShutdownEngine()
        {
            Logging.Log(ClassName, $"The engine is shutting down...");
            // Shuts down the engine by first killing all services, then clearing the DataModel
            // and finally exiting the process.
            KillAllServices();
            DataModel.Clear();

            Logging.Log("The engine has shut down. Exiting.");
            Environment.Exit(0);
        }

        private void HandleCrashedService(string ClassName)
        {
            // Get the service before we kill it
            Service Svc = GetService(ClassName);

            // Check the importance of the service.
            switch (Svc.Importance)
            {
                case ServiceImportance.Low:
                    // Forcibly kill the service and reboot it
                    KillService(ClassName, true);
                    StartService(ClassName);
                    return; 
                case ServiceImportance.High:
                    ErrorManager.ThrowError(ClassName, "CriticalServiceFailureException", $"The critical service {ClassName} has failed - the engine cannot continue to run.");
                    return; 
            }
        }
    }
}
