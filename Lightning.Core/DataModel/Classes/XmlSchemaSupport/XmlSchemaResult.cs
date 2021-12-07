using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace Lightning.Core.API
{
    public class XmlSchemaResult : IResult
    {
        public string FailureReason { get; set; }
        public XmlSeverityType RSeverity { get; set; }
        public bool Successful { get; set; }
    }
}
