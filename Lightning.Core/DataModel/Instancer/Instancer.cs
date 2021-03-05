using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{ 
    /// <summary>
    /// Instancer.cs
    /// 
    /// 2021-03-03
    /// 
    /// Provides auxillary support for the DataModel. Creates an instance of the class specified to its CreateInstance method.
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
        public static object CreateInstance(Type Typ) 
        {
            Type InstanceType = typeof(Instance);

            if (!Typ.IsSubclassOf(InstanceType))
            {
                //todo: throw errpr
                return null; 
            }
            else
            {
                // may need more code here
                object NewT = Activator.CreateInstance(Typ);

                return NewT;
            }

        } // END SHOULD BE GENERIC TYPE PARAMETER

    }
}
