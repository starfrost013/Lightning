using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace Lightning.Core
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
        /// Creates an instance of a DataModel instance
        /// 
        /// I wanted to use generic type parameters,
        /// but the fucking compiler is SHIT
        /// </summary>
        /// <returns></returns>
        public static InstantiationResult CreateInstance(Type Typ) 
        {
            InstantiationResult IR = new InstantiationResult(); 
            Type InstanceType = typeof(Instance);

            if (!Typ.IsSubclassOf(InstanceType))
            {
                //todo: throw errpr
                
                return null; 
            }
            else
            {

                if (!CreateInstance_CheckIfClassIsInstantiable(Typ))
                {
                    
                    return null;
                }
                else
                {
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
            // Parse the instance class to check for validity.
            // in the future we will check for parent locking etc
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
                         
                }
            }

            return true; 
        }
    }

    
}
