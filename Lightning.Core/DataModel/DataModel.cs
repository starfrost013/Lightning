using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lightning.Core
{
    public class DataModel
    {
        public static int DATAMODEL_VERSION_MAJOR = 0;
        public static int DATAMODEL_VERSION_MINOR = 1;
        public static int DATAMODEL_VERSION_REVISION = 0;

        public List<Instance> Level { get; set; }

        public DataModel()
        {
            string DataModel_String = $"{DATAMODEL_VERSION_MAJOR}.{DATAMODEL_VERSION_MINOR}.{DATAMODEL_VERSION_REVISION}";
            Console.WriteLine($"DataModel Init\nDataModel Version {DataModel_String}");
            Level = new List<Instance>();
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

                object IX = Instancer.CreateInstance(WantedType);

                return IX; //TEMP
            }
            catch (Exception) //TODO: HANDLE VARIOUS TYPES OF EXCEPTION
            {
                return null; // TEMP
            }
        }
    }
}
