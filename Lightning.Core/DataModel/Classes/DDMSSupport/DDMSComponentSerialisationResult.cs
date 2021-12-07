using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Lightning.Core.API
{
    public class DDMSComponentSerialisationResult : IResult
    {
        public XmlDocument XmlDocument { get; set; }
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
    }
}
