using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lightning.Core
{
    public class XMLSchema : SerialisableObject
    {
        public XmlSchema _schema { get; set; }
        public string SchemaName { get; set; }
    }
}
