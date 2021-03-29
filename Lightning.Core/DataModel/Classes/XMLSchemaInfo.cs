using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// 2021-03-06
    /// 
    /// Non-instantiable
    /// 
    /// XMLSchemaInfo
    /// 
    /// Holds information about an XML schema.
    /// 
    /// 2021-03-06: Created.
    /// 2021-03-09: Added properties.
    /// 
    /// </summary>
    public class XMLSchemaInfo : Instance
    {
        public override InstanceTags Attributes { get => InstanceTags.Instantiable; }
        public override string ClassName { get => "XMLSchemaInfo"; }
        public string SchemaPath { get; set; }
        public string XmlPath { get; set; }
    }
}
