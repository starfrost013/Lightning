using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Lightning.Core
{

    /// <summary>
    /// This is a custom error handler that can be used by an error.
    /// 
    /// It currently takes an Error object as its sole parameter and returns nothing (void).
    /// 
    /// THIS WILL OVERRIDE ALL BEHAVIOUR! 
    /// </summary>
    /// <param name="err"></param>
    /// <returns></returns>
    public delegate void CustomErrorHandler(Error err);
    /// <summary>
    /// Lightning
    /// 
    /// Error
    /// 
    /// Non-instanceable object (not part of the DataModel)
    /// </summary>
    [XmlRoot("Error")]
    public class Error
    {

        [XmlIgnore]
        public CustomErrorHandler CustomErrHandler { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Id")]
        public uint Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Severity")]
        public MessageSeverity Severity { get; set; }


    }
}
