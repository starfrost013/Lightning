﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Reflection;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning
    /// 
    /// DataModel v0.4.0 
    /// 
    /// Provides a unified object system for Lightning.
    /// All objects inherit from the Instance class, which this class manages. 
    /// </summary>
    public class DataModel
    {
        public static int DATAMODEL_VERSION_MAJOR = 0;
        public static int DATAMODEL_VERSION_MINOR = 4;
        public static int DATAMODEL_VERSION_REVISION = 1;

        // shouldn't be static? idk

        /// <summary>
        /// The global engine settings for this DataModel. 
        /// </summary>
        public static GlobalSettings Settings { get; set; }

        /// <summary>
        /// Contains a list of the first-level instances
        /// todo: make non-static
        /// </summary>
        private static InstanceCollection State;

        /// <summary>
        /// Path to the namespace that contains DataModel objects
        /// </summary>
        public static string DATAMODEL_NAMESPACE_PATH = "Lightning.Core";

        
        public DataModel()
        {
            string DataModel_String = $"{DATAMODEL_VERSION_MAJOR}.{DATAMODEL_VERSION_MINOR}.{DATAMODEL_VERSION_REVISION}";
            Console.WriteLine($"DataModel Init\nDataModel Version {DataModel_String} now initialising...");
            

            State = new InstanceCollection();

        }

        public static void Init()
        {
            if (!ErrorManager.ERRORMANAGER_LOADED)
            {
                ErrorManager.Init();
            }


            // init the SCM
            Workspace WorkSvc = (Workspace)CreateInstance("Workspace");

            ServiceControlManager SCM = (ServiceControlManager)CreateInstance("ServiceControlManager", WorkSvc);
            
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
                    SCM.InitStartupServices(GSR.Settings.ServiceStartupCommands); 
                }
                else
                {
                    return; // this should not really be running rn 
                }
            }


#if DEBUG_ATEST_DATAMODEL //todo: unit testing
            ATest();
#endif
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
                        State.Add(NewInstance);

                        // default to the workspace if it is not null; otherwise get datamodel root

                        if (NewInstance.Attributes.HasFlag(InstanceTags.ParentCanBeNull))
                        {
                            return State.Instances[State.Count - 1];
                        }
                        else
                        {
                            GetInstanceResult GIR = GetFirstChildOfType("Workspace");
                            
                            if (GIR.Successful)
                            {
                                Workspace TheWorkspace = (Workspace)GIR.Instance;
                                return TheWorkspace.Children.Instances[TheWorkspace.Children.Instances.Count - 1];
                            }
                            else
                            {
                                // The workspace is trashed, die
                                ErrorManager.ThrowError("DataModel", "TheWorkspaceHasBeenDestroyedException");
                            }

                            return null;
                        }
                    }
                    else
                    {
                        // DO!!!! NOT!!!! CALL!!!! PARENT.CHILDREN.INSTANCES.ADD!!!
                        // I REPEAT, DO!!!! NOT!!!! CALL!!!! THAT until we can get this bullshit in order 
                        Parent.Children.Add(NewInstance);

                        int ParentChildrenInstanceCount = 0;

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
            catch (Exception) //TODO: HANDLE VARIOUS TYPES OF EXCEPTION
            {
                ErrorManager.ThrowError(ClassName, "DataModelInstanceCreationUnknownErrorException"); 
                return null; 
            }
        }

       
        public static void Clear()
        {
            // we will need to do a lot more than this
            State.Clear();
            // Reinitialise
            Init();
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
            // need to fix this weird api 
            LightningXMLSchema LXMLS = new LightningXMLSchema(); // has been removed from the datamodel
            LXMLS.XSI.XmlPath = @"Content\Schema\Lightning.xsd";

            DataModelSerialiser DDX = (DataModelSerialiser)CreateInstance("DataModelSerialiser");
            DDX.DDMS_Serialise(LXMLS, @"Content\Test\Test.xml");

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

    }
}
