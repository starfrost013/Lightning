﻿using Lightning.Core;
using Lightning.Core.API;
using System;
using System.Xml;

namespace Polaris.Core
{
    /// <summary>
    /// PolarisState
    /// 
    /// May 13, 2021 (modified May 17, 2021)
    /// 
    /// Defines the global state for Polaris.
    /// </summary>
    public class PolarisState
    {
        /// <summary>
        /// The Lightning DataModel.
        /// </summary>
        public DataModel DataModel { get; set; }

        /// <summary>
        /// The XML document that will be used as a tree for saving.
        /// </summary>
        public XmlDocument XmlDocument { get; set; }

        /// <summary>
        /// The loaded UI tabs.
        /// </summary>
        public TabCollection Tabs { get; set; }


        /// <summary>
        /// Currently loaded file name. (Redundant - DataModel.DATAMODEL_LASTXML_FILENAME)
        /// </summary>
        ///public string FileName { get; set; }
        ///
        public PolarisState()
        {
            XmlDocument = new XmlDocument();
            Tabs = new TabCollection();
        }

        public void Init()
        {
            Init_InitDataModel();
            Init_SerialiseTabs();
        }

        private void Init_InitDataModel()
        {
            LaunchArgs LA = new LaunchArgs();

            // Do not initialise services or game xml

            LA.AppName = "Polaris";

            DataModel = new DataModel();

            DataModel.Init();
        }

        private void Init_SerialiseTabs()
        {
            Init_SerialiseTabs_Validate();
            Init_SerialiseTabs_Serialise(); 
        }

        private void Init_SerialiseTabs_Validate()
        {
            // Do not add to datamodel -- we are not going to pollute the datamodel
            LightningXMLSchema LXMLS = new LightningXMLSchema();

            LXMLS.XSI.SchemaPath = @"Content\Polaris\Schema\TabCollection.xsd";
            LXMLS.XSI.XmlPath = @"Content\Polaris\Tabs.xml";

            LXMLS.Validate();
        }

        private void Init_SerialiseTabs_Serialise()
        {

        }


    }
}
