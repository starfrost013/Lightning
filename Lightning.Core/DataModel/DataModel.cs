using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning
    /// 
    /// DataModel
    /// 
    /// Provides a unified object system for Lightning.
    /// All objects inherit from the Instance class, which this class manages. 
    /// </summary>
    public class DataModel
    {
        public static int DATAMODEL_VERSION_MAJOR = 0;
        public static int DATAMODEL_VERSION_MINOR = 1;
        public static int DATAMODEL_VERSION_REVISION = 2;

        public List<Instance> State { get; set; }

        public DataModel()
        {
            string DataModel_String = $"{DATAMODEL_VERSION_MAJOR}.{DATAMODEL_VERSION_MINOR}.{DATAMODEL_VERSION_REVISION}";
            Console.WriteLine($"DataModel Init\nDataModel Version {DataModel_String} now initialising...");
            State = new List<Instance>();
        }

        /// <summary>
        /// Create a new Instance.
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public object CreateInstance(string ClassName)
        {
            try
            {

                // Do some kinda weird kludge to get the generic type we want
                Type WantedType = Type.GetType(ClassName);

                InstantiationResult IX = Instancer.CreateInstance(WantedType);

                // Throw an error if not successful 
                // todo: add more functionality to the error system
                if (IX.Successful)
                {
                    return IX; //TEMP
                }
                else
                {
                    return null; //TODO: throw error 
                }
                
            }
            catch (Exception) //TODO: HANDLE VARIOUS TYPES OF EXCEPTION
            {
                return null; // TEMP
            }
        }

        public InstanceInfo GetInstanceInfo(string ClassName)
        {
            throw new NotImplementedException();
        }
    }
}
