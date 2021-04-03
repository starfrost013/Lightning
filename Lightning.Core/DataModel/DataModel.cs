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
    /// DataModel v0.2.3
    /// 
    /// Provides a unified object system for Lightning.
    /// All objects inherit from the Instance class, which this class manages. 
    /// </summary>
    public class DataModel
    {
        public static int DATAMODEL_VERSION_MAJOR = 0;
        public static int DATAMODEL_VERSION_MINOR = 2;
        public static int DATAMODEL_VERSION_REVISION = 3;

        // shouldn't be static? idk

        /// <summary>
        /// Contains a list of the first-level instances
        /// todo: make list
        /// </summary>
        private static List<Instance> State;

        public DataModel()
        {
            string DataModel_String = $"{DATAMODEL_VERSION_MAJOR}.{DATAMODEL_VERSION_MINOR}.{DATAMODEL_VERSION_REVISION}";
            Console.WriteLine($"DataModel Init\nDataModel Version {DataModel_String} now initialising...");
            

            State = new List<Instance>();
            ErrorManager.Init();

            // init the SCM
            CreateInstance("ServiceControlManager");
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
                Type WantedType = Type.GetType($"Lightning.Core.{ClassName}");

                InstantiationResult IX = Instancer.CreateInstance(WantedType);

                // Throw an error if not successful 
                // todo: add more functionality to the error system
                if (IX.Successful)
                {
                    //runtime type only
                    Instance NewInstance = (Instance)IX.Instance;

                    NewInstance.GenerateInstanceInfo();

                    if (Parent == null)
                    {
                        State.Add(NewInstance);
                    }
                    else
                    {
                        Parent.Children.Instances.Add(NewInstance); 
                    }

                    // Hack to get around ref limitations ig?
                    // fix this if it doesn't work

                    return State[State.Count - 1]; //TEMP
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
            LightningXMLSchema LXMLS = (LightningXMLSchema)CreateInstance("LightningXMLSchema");
            LXMLS.XSI.XmlPath = @"Content\Schema\Lightning.xsd";

            DataModelSerialiser DDX = (DataModelSerialiser)CreateInstance("DataModelSerialiser");
            DDX.DDMS_Serialise(LXMLS, @"Content\Test\Test.xml");

        }
#endif
        

        /// <summary>
        /// Dump the current DataModel instance to console.
        /// </summary>
        public void InstanceDump(bool FilterAccessors = true)
        {
            // implement: 2021-03-09

            Console.WriteLine("DataModel dump:\n");

            foreach (Instance II in State)
            {
                Console.WriteLine($"Instance: {II.ClassName}:");

                if (II.Name != null) Console.WriteLine($"Instance: {II.ClassName} ({II.Name})");

                Console.WriteLine($"Tags: {II.Attributes.ToString()}");

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
            }

            return; 
        }
    }
}
