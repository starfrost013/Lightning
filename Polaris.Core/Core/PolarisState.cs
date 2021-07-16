using Lightning.Core;
using Lightning.Core.API;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Polaris.Core
{
    /// <summary>
    /// PolarisState
    /// 
    /// May 13, 2021 (modified May 22, 2021)
    /// 
    /// Defines the global state for Polaris.
    /// </summary>
    public class PolarisState
    {

        /// <summary>
        /// The Lightning DataModel.
        /// </summary>
        public static DataModel DataModel { get; set; }

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

        public void Init(LaunchArgs DataModelLaunchArgs = null)
        {
            Init_InitDataModel(DataModelLaunchArgs);
            Init_SerialiseTabs();
        }

        private void Init_InitDataModel(LaunchArgs DataModelLaunchArgs = null)
        {

            LaunchArgs LA = null; 

            // Do not initialise services or game xml

            if (DataModelLaunchArgs != null)
            {
                LA = DataModelLaunchArgs;
            }
            else
            {
                LA = new LaunchArgs();
            }

            // override some (services will not be initialised)
            LA.AppName = "Polaris";
            LA.InitServices = false;

            // temp
            DataModel = new DataModel();
            DataModel.Init(LA);
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
            XmlSerializer XS = new XmlSerializer(typeof(TabCollection));

            XmlReader XRA = XmlReader.Create(@"Content\Polaris\Tabs.xml");

            Tabs = (TabCollection)XS.Deserialize(XRA);
        }

        public void Shutdown()
        {
            DataModel.Shutdown();
        }

        public void Clear() => DataModel.Clear();

        public bool LoadFile(string FileName) => DataModel.LoadFile(FileName);
    }
}
