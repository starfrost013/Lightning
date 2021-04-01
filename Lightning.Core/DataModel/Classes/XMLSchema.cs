using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lightning.Core
{
    public class LightningXMLSchema : Instance
    {
        public override string ClassName => "XMLSchema";
        public XmlSchema Schema { get; set; }
        public XmlSchemaData XSI { get; set; }

        /// <summary>
        /// Get around the requirement for validationeventhandler to return void (The API had a spaz. Hold out! API!)
        /// </summary>
        private XmlSchemaResult __dumbhack { get; set; }
        public LightningXMLSchema()
        {
            XSI = new XmlSchemaData();

        }

        public XmlSchemaResult Validate()
        {
            XmlSchemaResult XSR = new XmlSchemaResult();
            
            if (XSI.SchemaPath == null
                || XSI.XmlPath == null)
            {
                XSR.FailureReason = "Invalid XmlReaderSettings!";
                XSR.Severity = XmlSeverityType.Error;
                return XSR;
            }
            else
            {
                XmlReaderSettings XRS = new XmlReaderSettings();
                XRS.ValidationType = ValidationType.Schema;

                XRS.IgnoreComments = true;
                XRS.IgnoreWhitespace = true;
                XRS.ValidationEventHandler += Validate_OnFail;

                XmlReader XR = XmlReader.Create(XSI.XmlPath, XRS);

                // yes we have to do this.
                while (XR.Read())
                {

                }
            }

            XSR.Successful = true;
            return XSR; 
        }

        private void Validate_OnFail(object sender, ValidationEventArgs EventArgs)
        {
            switch (EventArgs.Severity)
            {
                case XmlSeverityType.Warning:
                    Logging.Log($"XML Validation Warning: {EventArgs.Exception}", ClassName, MessageSeverity.Warning);
                    return;
                case XmlSeverityType.Error:
                    Logging.Log($"XML Validation Error: {EventArgs.Exception}", ClassName, MessageSeverity.Error);
                    return;
            }
        }

    }
}
