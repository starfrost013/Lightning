using System;
using System.Collections.Generic;
using System.Linq; 
using System.Reflection;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Lightning
    /// 
    /// DataModel (API Version 0.18.0) 
    /// 
    /// Provides a unified object system for Lightning.
    /// All objects inherit from the Instance class, which this class manages. 
    /// </summary>
    public class DataModel
    {
        public static int DATAMODEL_API_VERSION_MAJOR = 0;
        public static int DATAMODEL_API_VERSION_MINOR = 18;
        public static int DATAMODEL_API_VERSION_REVISION = 0;

        // shouldn't be static? idk

        /// <summary>
        /// The last loaded XML file using DDMS.
        /// </summary>
        public static string DATAMODEL_LASTXML_PATH { get; set; }

        /// <summary>
        /// Path to the namespace that contains DataModel objects
        /// </summary>
        public static string DATAMODEL_NAMESPACE_PATH = "Lightning.Core.API";

        /// <summary>
        /// The global engine settings for this DataModel. 
        /// </summary>
        public static GlobalSettings Settings { get; set; }

        /// <summary>
        /// Contains a list of the first-level instances
        /// todo: make non-static
        /// </summary>
        private static InstanceCollection State;

        public DataModel()
        {

        }

        /// <summary>
        /// Initialises the DataModel 
        /// </summary>
        /// <param name="Args"></param>
        public static void Init(LaunchArgs Args = null, bool Reinitialising = false)
        {
            if (!Init_VerifyCompatibleSystem())
            {
                // Error Maanger isn't initialised so just throw a messagebox and exit
                MessageBox.Show("Your system is not compatible with Lightning.\nLightning requires a system with more than one hardware thread (i.e. a dual core processor or a single-core processor with hyperthreading or SMT).", "Fatal Error 0000", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0x0001DEAD);
            }
            else
            {

                string DataModel_String = $"{DATAMODEL_API_VERSION_MAJOR}.{DATAMODEL_API_VERSION_MINOR}.{DATAMODEL_API_VERSION_REVISION}";
                Logging.Log($"DataModel\nAPI Version {DataModel_String}\nNow Initialising...", "DataModel");

                if (!Reinitialising)
                {
                    State = new InstanceCollection();
                }
                


                if (!ErrorManager.ERRORMANAGER_LOADED)
                {
                    ErrorManager.Init();
                }

                Workspace WorkSvc = null;
                ServiceControlManager SCM = null;


                if (!Reinitialising)
                {
                    // init the SCM
                    WorkSvc = (Workspace)CreateInstance("Workspace");

                    SCM = (ServiceControlManager)CreateInstance("ServiceControlManager", WorkSvc);
                }
                else
                {
                    WorkSvc = DataModel.GetWorkspace();

                    GetInstanceResult GIR = WorkSvc.GetFirstChildOfType("ServiceControlManager");

                    if (!GIR.Successful
                        && GIR.Instance == null)
                    {
                        ErrorManager.ThrowError("DataModel", "ReinitialisingBeforeInitialisingDataModelException");
                    }
                    else
                    {
                        SCM = (ServiceControlManager)GIR.Instance; 
                    }
                }

                if (WorkSvc == null
                    || SCM == null)
                {
                    ErrorManager.ThrowError("DataModel", "ReinitialisingBeforeInitialisingDataModelException");
                }

                if (!GlobalSettings.GLOBALSETTINGS_LOADED)
                {
                    GlobalSettingsResult GSR = GlobalSettings.SerialiseGlobalSettings();

                    if (GSR.Successful)
                    {
                        // set the globalsettings if successful 
                        Settings = GSR.Settings;
#if DEBUG
                        Settings.ATest();
#endif
                    }
                    else
                    {
                        return; // this should not really be running rn 
                    }
                }


#if DEBUG_ATEST_DATAMODEL //todo: unit testing
            ATest();
#endif

                if (Args != null)
                {

                    if (Args.AppName != null)
                    {
                        // Allow for SDK-specific behaviour.
                        if (Args.AppName.Contains("Polaris")
                            || Args.AppName.Contains("LightningSDK"))
                        {
                            Logging.Log($"SDK Launching...", "DataModel");
                        }
                    }

                    if (Args.InitServices)
                    {
                        // assume normal init 
                        SCM.InitStartupServices(Settings.ServiceStartupCommands);

                        if (Args.GameXMLPath != null)
                        {
                            DataModelDeserialiser DMS = (DataModelDeserialiser)CreateInstance("DataModelDeserialiser");

                            DataModel DM = DMS.DDMS_Deserialise(Args.GameXMLPath);

                            // Check for a failure
                            if (DM == null)
                            {
                                HandleFailureToOpenDocument();
                            }
                            else
                            {
                                // Enter the main loop.

                                SCM.InitServiceUpdates();
                            }
                        }
                        else
                        {
                            // failsafe
                            ErrorManager.ThrowError("DataModel", "FailureToOpenLgxException", "No LGX file was supplied and the condition was not handled.");
                        }
                    }
                    else
                    {
                        Logging.Log("Skipping service initialisation: NoInitServices supplied", "DataModel");
                        Logging.Log("Initialisation completed", "DataModel");
                        return;
                    }

                }
            }
            
            
        }

        private static bool Init_VerifyCompatibleSystem()
        {
            if (Environment.ProcessorCount == 1)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }

        private static void HandleFailureToOpenDocument()
        {
            ErrorManager.ThrowError("DataModel", "FailedToOpenLgxException");
            return; 
        }

        /// <summary>
        /// Create a new Instance. Optionally set the parent of this object.
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static object CreateInstance(string ClassName, Instance Parent = null)
        {
            try
            {

                // Do some kinda weird kludge to get the generic type we want
                Type WantedType = Type.GetType($"{DATAMODEL_NAMESPACE_PATH}.{ClassName}");

                InstantiationResult IX = Instancer.CreateInstance(WantedType);

                // Throw an error if not successful 
                // todo: add more functionality to the error system
                if (IX.Successful)
                {
                    //runtime type only
                    Instance NewInstance = (Instance)IX.Instance;

                    NewInstance.GenerateInstanceInfo();

                    // Return the object in the parent tree if not null 
                    if (Parent == null)
                    {
                        State.Add(NewInstance, Parent);

                        // default to the workspace if it is not null; otherwise get datamodel root

                        if (NewInstance.Attributes.HasFlag(InstanceTags.ParentCanBeNull))
                        {
                            return State.Instances[State.Count - 1];
                        }
                        else
                        {

                            Type InsType = NewInstance.GetType();

                            GetInstanceResult GIR = GetFirstChildOfType("Workspace");
                            Workspace TheWorkspace = (Workspace)GIR.Instance;

                            if (!GIR.Successful
                                || GIR.Instance == null)
                            {
                                // The workspace is trashed, die
                                ErrorManager.ThrowError("DataModel", "TheWorkspaceHasBeenDestroyedException");
                            }

                            // Force Services into the SCM
                            if (InsType.IsSubclassOf(typeof(Service)))
                            {
                                GetInstanceResult SGIR = TheWorkspace.GetFirstChildOfType("ServiceControlManager");

                                if (!SGIR.Successful
                                    || SGIR.Instance == null)
                                {
                                    ErrorManager.ThrowError("DataModel", "ServiceControlManagerFailureException");
                                }

                                ServiceControlManager SCM = (ServiceControlManager)SGIR.Instance;
                                return SCM.Children[SCM.Children.Count - 1];
                            
                            }
                            else
                            {

                                return TheWorkspace.Children[TheWorkspace.Children.Count - 1];
                            }
                        }
                    }
                    else
                    {
                        // DO!!!! NOT!!!! CALL!!!! PARENT.CHILDREN.INSTANCES.ADD!!!
                        // I REPEAT, DO!!!! NOT!!!! CALL!!!! THAT until we can get this bullshit in order 
                        Parent.Children.Add(NewInstance, Parent);

                        int ParentChildrenInstanceCount = Parent.Children.Instances.Count;

                        if (ParentChildrenInstanceCount == 0)
                        {
                            return Parent.Children.Instances[Parent.Children.Instances.Count];
                        }
                        else
                        {
                            return Parent.Children.Instances[Parent.Children.Instances.Count - 1];
                        }
                    }
                }
                else
                {
                    ErrorManager.ThrowError(ClassName, "DataModelInstanceCreationFailedException", IX.FailureReason);
                    return null;
                }
                
            }
            catch (IndexOutOfRangeException err)
            {
                ErrorManager.ThrowError(ClassName, "InternalInstanceAdditionErrorException", err);
                return null; 
            }
            catch (Exception err) //TODO: HANDLE VARIOUS TYPES OF EXCEPTION
            {
                ErrorManager.ThrowError(ClassName, "DataModelInstanceCreationUnknownErrorException", err); 
                return null; 
            }
        }

        public static void RemoveInstance(Instance Ins, Instance Parent = null)
        {
            if (Parent == null)
            {
                State.Remove(Ins);
            }
            else
            {
                State.Remove(Ins, Parent); 
            }


            return; 
        }

        public static void Clear()
        {
            // we will need to do a lot more than this
            State.Clear();
            // Reinitialise
            Init(null, true);
        }
        public static void Shutdown()
        {
            Logging.Log("The DataModel is shutting down. Clearing it...");
            State.Clear();
        }
        

#if DEBUG
        private void ATest()
        {
            Console.WriteLine("V0.1.27 2021-03-13 Testing DataModel...");

            CreateInstance("Color3");
            CreateInstance("Color4");
            CreateInstance("Vector2");

        }

        /// <summary>
        /// ATest #2 for DataModel - DDMS Serialisation
        /// </summary>
        public void ATest_Serialise()
        {

            DataModelDeserialiser DDX = (DataModelDeserialiser)CreateInstance("DataModelSerialiser");
            DDX.DDMS_Deserialise(@"Content\Test\Test.xml");

        }

        

        /// <summary>
        /// Dump the current DataModel instance to console.
        /// </summary>
        public void InstanceDump(bool FilterAccessors = true)
        {
            // implement: 2021-03-09

            Console.WriteLine("DataModel dump:\n");

            foreach (Instance II in State)
            {
                InstanceDump_DumpInstance(II, FilterAccessors);
            }

            return; 
        }

        private void InstanceDump_DumpInstance(Instance II, bool FilterAccessors = true)
        {
            Console.WriteLine($"Instance: {II.ClassName}:");

            if (II.Name != null) Console.WriteLine($"Instance: {II.ClassName} ({II.Name})");

            Console.WriteLine($"Tags: {II.Attributes}");

            InstanceInfo IIF = II.Info;

            foreach (InstanceInfoMethod IIM in IIF.Methods)
            {
                if (FilterAccessors)
                {
                    if (IIM.MethodName.Contains("get_")
                        || IIM.MethodName.Contains("set_"))
                    {
                        continue;
                    }
                }

                Console.Write("Method: ");
                Console.Write($"{IIM.MethodName}\n");

                if (IIM.Parameters.Count == 0)
                {
                    continue;
                }
                else
                {
                    foreach (InstanceInfoMethodParameter IIMP in IIM.Parameters)
                    {

                        Console.Write("Parameter: ");
                        Console.Write($"Name: {IIMP.ParamName} ");
                        Console.Write($"Type: {IIMP.ParamType.Name}\n");
                    }
                }
            }

            // don't bother going any further than one level deep
            foreach (InstanceInfoProperty IIP in IIF.Properties)
            {
                Console.Write("Property:");
                Console.Write($"Name: {IIP.Name} ");
                Console.Write($"Type: {IIP.Type}\n");
            }

            if (II.Children.Count > 0)
            {
                foreach (Instance IChild in II.Children)
                {
                    // check that this does not cause problems.s
                    InstanceDump_DumpInstance(IChild, true);
                }
            }
        }

#endif


        /// <summary>
        /// Gets the first child of this Instance with ClassName <see cref="ClassName"/>
        /// </summary>
        /// <returns>A <see cref="GetInstanceResult"/> object. The Instance is <see cref="GetInstanceResult.Instance"/>.</returns>
        public static GetInstanceResult GetFirstChildOfType(string ClassName) => State.GetFirstChildOfType(ClassName);

        /// <summary>
        /// Gets the last child of this Instance with Class Name <see cref="ClassName"/>
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static GetInstanceResult GetLastChildOfType(string ClassName) => State.GetLastChildOfType(ClassName);

        /// <summary>
        /// Acquires the <see cref="GlobalSettings"/> for the current DataModel. 
        /// </summary>
        /// <returns>The current <see cref="GlobalSettings"/> object if it is loaded (<see cref="GlobalSettings.GLOBALSETTINGS_LOADED"/> is true. Otherwise, it will return <code>null</code>.</returns>
        public static GlobalSettings GetGlobalSettings()
        {
            if (!GlobalSettings.GLOBALSETTINGS_LOADED)
            {
                ErrorManager.ThrowError("GlobalSettings", "CannotAcquireUnloadedGlobalSettingsException");
                return null;
            }
            else
            {
                return Settings;
            }
        }

        /// <summary>
        /// Helper method to retrieve the Workspace from the DataModel. 
        /// </summary>
        /// <returns></returns>
        public static Workspace GetWorkspace()
        {
            GetInstanceResult GIR = DataModel.GetFirstChildOfType("Workspace");

            if (!GIR.Successful || GIR.Instance == null)
            {
                ErrorManager.ThrowError("DataModel", "WorkspaceHasBeenDestroyedException");
                return null;
            }

            return (Workspace)GIR.Instance;

        }

        /// <summary>
        /// Check if the root of the DataModel contains <paramref name="Obj"/>Obj.
        /// </summary>
        /// <param name="Obj"></param>
        public static bool Contains(Instance Obj) => State.Contains(Obj);

        /// <summary>
        /// I don't really like this.
        /// 
        /// This gets the DataModel root.. It's the best option available.
        /// </summary>
        /// <returns></returns>
        internal static InstanceCollection GetState() => State; 
    }
}
