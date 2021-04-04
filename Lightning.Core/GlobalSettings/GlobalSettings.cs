using Lightning.Core.StaticSerialiser; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{ 
    /// <summary>
    /// Non-DataModel (Engine Core)
    /// 
    /// GlobalSettings
    /// 
    /// 2021-04-03
    /// 
    /// Holds global settings for the engine.
    /// 
    /// uses Lightning.Core.StaticSerialiser.dll
    /// </summary>
    public static class GlobalSettings
    {
        /// <summary>
        /// The path to the GlobalSettings XML.
        /// </summary>
        public static string GLOBALSETTINGS_XML_PATH = @"Content\EngineContent\GlobalSettings.xml";

        /// <summary>
        /// The path to the GlobalSettings XML schema.
        /// </summary>
        public static string GLOBALSETTINGS_XSD_PATH = @"Content\Schema\GlobalSettings.xsd";

        /// <summary>
        /// The schema version
        /// </summary>
        public static string GLOBALSETTINGS_XSD_SCHEMAVERSION = "0.1.0.0001";

        /// <summary>
        /// A list of services to be started by the Service Control Manager at init time. Contains an optional startup order and list of strings. 
        /// </summary>
        public static List<ServiceStartupCommand> ServiceStartupCommands { get; set; }

        public static void SerialiseGlobalSettings()
        {
            
        }

        public static void SerialiseGlobalSettings_Validate()
        {

        }

        public static StaticSerialisationResult SerialiseGlobalSettings_Serialise()
        {

        }

    }
}
