using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lightning.Core
{
    public class XMLSchema : Instance
    {
        public override string ClassName => "XMLSchema";
        public XmlSchema _schema { get; set; }
        public string SchemaName { get; set; }
        public string Path { get; set; }
        public Type ClassToSerialise { get; set; }
        public bool SerialiseAllInheritedClasses { get; set; }
    }
}
