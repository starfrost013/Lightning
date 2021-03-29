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
        public XmlSchemaInfo XSI { get; set; }

        public LightningXMLSchema()
        {

        }

        public XmlSchemaResult Validate()
        {
            XmlReaderSettings XRS = new XmlReaderSettings();
            throw new NotImplementedException();
        }

        private void Validate_OnFail(object ValidationObject)
        {
            throw new NotImplementedException();
        }

    }
}
