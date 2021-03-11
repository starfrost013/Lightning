using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lightning.Core
{
    public class DataModelSerialiser : Instance
    {
        /// <summary>
        /// Transform an XML document to a DataModel.
        /// 
        /// 2021-03-11
        /// </summary>
        /// <param name="Schema">The schema to utilise for serialisation.</param>
        /// <param name="Path">The path to the XML document to serialise. </param>
        /// <returns></returns>
        public DataModel Serialise_DataModel(LightningXMLSchema Schema, string Path)
        {
            string XScPath = Schema.Path;

            throw new NotImplementedException(); 

            
        }
    }
}
