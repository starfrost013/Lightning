using Lightning.Utilities;
using System; 
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Schema; // need to create our own xmlseveritytype equivalent
using System.Xml.Serialization;

namespace Lightning.Core
{
    /// <summary>
    /// Non-DataModel (Engine Core)
    /// 
    /// GlobalSettings
    /// 
    /// 2021-04-05
    /// 
    /// Holds global settings for the engine [DataModel] 
    /// 
    /// </summary>
    [XmlRoot("GlobalSettings")]
    public class GlobalSettings // not static for reasons
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

        [XmlElement("StartupServices")]
        /// <summary>
        /// A list of services to be started by the Service Control Manager at init time. Contains an optional startup order and list of strings. 
        /// </summary>
        public ServiceStartupCommandCollection ServiceStartupCommands { get; set; }

        /// <summary>
        /// Serialises \EngineContent\GlobalSettings.xml to an instance of GlobalSettings. 
        /// </summary>
        /// <returns></returns>
        public GlobalSettingsResult SerialiseGlobalSettings()
        {
            GlobalSettingsResult GSR = new GlobalSettingsResult();

            XmlSchemaResult XSR1 = SerialiseGlobalSettings_Validate();

            if (!XSR1.Successful)
            {
                switch (XSR1.RSeverity)
                {
                    // Throw a warning or fatalerror.
                    case XmlSeverityType.Warning:
                        string FailureReason = $"An error occurred during serialisation of settings. Some or all global engine settings may fail to load: {XSR1.FailureReason}";
                        ErrorManager.ThrowError("GlobalSettings Serialiser", "GlobalSettingsValidationWarningException", FailureReason);
                        GSR.FailureReason = FailureReason;

                        return GSR; 
                    case XmlSeverityType.Error:
                        string FailureReason2 = $"Failed to serialise GlobalSettings: {XSR1.FailureReason}!";
                        ErrorManager.ThrowError("GlobalSettings Serialiser", "GlobalSettingsValidationFailureException", FailureReason2);

                        GSR.FailureReason = FailureReason2;
                        return GSR; // this is a fatal error
                }

               
                
                return GSR; 
            }
            else
            {
                GlobalSettingsResult SSR = SerialiseGlobalSettings_Serialise(); 

                if (!SSR.Successful)
                {
                    ErrorManager.ThrowError("GlobalSettings Serialiser", "FailedToSerialiseGlobalSettingsException", $"Failed to serialise GlobalSettings: {SSR.FailureReason}");
                    return SSR;
                }
                else
                {
                    return SSR; 
                }
            }
        }

        private static XmlSchemaResult SerialiseGlobalSettings_Validate()
        {
            LightningXMLSchema LXMLS = new LightningXMLSchema();
            LXMLS.XSI.XmlPath = GLOBALSETTINGS_XML_PATH;
            LXMLS.XSI.SchemaPath = GLOBALSETTINGS_XSD_PATH;

            XmlSchemaResult XSR = LXMLS.Validate();

            return XSR;
        }

        /// <summary>
        /// Serialises GlobalSettings. 
        /// </summary>
        /// <returns>A <see cref="GlobalSettingsResult"/> object. The serialised GlobalSettings object is located within </returns>
        private static GlobalSettingsResult SerialiseGlobalSettings_Serialise() // genericresult for now?
        {
            GlobalSettingsResult GSR = new GlobalSettingsResult(); 

            XmlSerializer XS = new XmlSerializer(typeof(GlobalSettings));

            XmlReader XR = XmlReader.Create(GLOBALSETTINGS_XML_PATH);

            try
            {
                // we are creating it as globalsettings so we can easily convert
                GSR.Settings = (GlobalSettings)XS.Deserialize(XR);
                GSR.Successful = true;
                return GSR;
            }
            catch (InvalidOperationException err)
            {
                GSR.FailureReason = $"Failed to serialise: {err}.\n\nInner exception (this may provide more information surrounding the issue): {err.InnerException}";
                return GSR; 
            }

        }

    }
}
