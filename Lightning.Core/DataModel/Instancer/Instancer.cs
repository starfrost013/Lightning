﻿using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace Lightning.Core.API
{ 
    /// <summary>
    /// Instancer.cs
    /// 
    /// 2021-03-03
    /// 
    /// Provides auxillary support for the DataModel. Creates an instance of the class specified to its CreateInstance method.
    /// 
    /// Modified 2021-03-06 to facilitate attributing.
    /// Modified 2021-03-07 for result classes.
    /// </summary>
    public static class Instancer //SHOULD BE GENERIC TYPE PARAMETER!!!
    {
        /// <summary>
        /// Creates an instance of a DataModel instance and sets its parent to Parent if it is null. 
        /// 
        /// I wanted to use generic type parameters,
        /// but the fucking compiler is SHIT
        /// </summary>
        /// <returns></returns>
        public static InstantiationResult CreateInstance(Type Typ) 
        {
            InstantiationResult IR = new InstantiationResult(); 


            if (!CreateInstance_CheckIfInstanceTypeIsInDataModel(Typ))
            {
                //todo: throw errpr

                IR.FailureReason = "DataModel: Error instancing Instance: Class is not in the DataModel!";
                return IR;
 
            }
            else
            {

                if (!CreateInstance_CheckIfClassIsInstantiable(Typ))
                {

                    IR.FailureReason = "DataModel: Class is not instantiable!";
                    return IR; 
                }
                else
                {

                    Logging.Log($"Instantiating Instance with type: {Typ}", "Instancer");

                    // may need more code here
                    object NewT = Activator.CreateInstance(Typ);
                    
                    // by default it's set to false, which is why we are doing it
                    IR.Successful = true;
                    IR.Instance = NewT;
                    return IR;
                }

            }

        } // END SHOULD BE GENERIC TYPE PARAMETER

        /// <summary>
        /// pre-result class; will write result classes in Lightning.Utilities tomorrow
        /// </summary>
        private static bool CreateInstance_CheckIfClassIsInstantiable(Type Typ)
        {
            PropertyInfo[] PropInfoList = Typ.GetProperties();

            object TestObject = Activator.CreateInstance(Typ);

            // Parse the instance's class information to check for validity.
            // in the future we will check for parent locking etc
            // we don't use instanceinfo because it hasn't been initialised yet to prevent a stack overflow in old builds (2021-03-21)
            foreach (PropertyInfo PropInfo in PropInfoList)
            {
                switch (PropInfo.Name)
                {
                    case "ClassName":

                        string ClassName = (string)PropInfo.GetValue(TestObject);

                        if (ClassName == "Instance") // todo: result classing
                        {
                            return false;
                        }
                        else
                        {
                            // parsing successful, continue...
                            continue; 
                        }
                    case "Attributes":

                        InstanceTags IT = (InstanceTags)PropInfo.GetValue(TestObject);

                        if (!IT.HasFlag(InstanceTags.Instantiable))
                        {
                            return false;
                        }
                        else
                        {
                            continue;
                        }
                    case "Deprecated":
                        bool Deprecated = (bool)PropInfo.GetValue(TestObject);

                        if (Deprecated)
                        {
                            Logging.Log($"{Typ.Name} is deprecated. It may be removed at any time in the future. Using this in new projects is not recommended.", "DataModel", MessageSeverity.Warning);
                        }

                        return true;
                    case "Experimental":
                        bool Experimental = (bool)PropInfo.GetValue(TestObject);

                        if (Experimental)
                        {
                            Logging.Log($"{Typ.Name} is experimental. It may be removed at any time in the future. Using this in projects you intend to release is not recommended.", "DataModel", MessageSeverity.Warning);
                        }

                        return true;

                }
            }

            
            return true; 
        }


        /// <summary>
        /// Check if a class is in the DataModel. 
        /// </summary>
        /// <param name="InstanceType"></param>
        /// <returns></returns>
        private static bool CreateInstance_CheckIfInstanceTypeIsInDataModel(Type InstanceType)
        {
            Type Root = typeof(Instance);

            return InstanceType.IsSubclassOf(Root);

        }

    }

    
}
