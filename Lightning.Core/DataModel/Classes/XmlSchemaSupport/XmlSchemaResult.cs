using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace Lightning.Core
{
    public class XmlSchemaResult : IResult
    {
        public string FailureReason { get; set; }
        public XmlSeverityType Severity { get; set; }
        public bool Successful { get; set; }
    }
}
