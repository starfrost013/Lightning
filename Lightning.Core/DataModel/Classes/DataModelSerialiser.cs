using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; 
using System.Text;
using System.Xml;

namespace Lightning.Core
{
    /// <summary>
    /// Dynamic DataModel Serialiser 0.x/1.x - DataModelSerialiser (DataModelDeserialiser class)
    /// 
    /// April 14, 2021
    /// 
    /// Implements the serialisation (saving) side of DDMS.
    /// </summary>
    public partial class DataModelDeserialiser
    {
        /// <summary>
        /// Saves a DDMS file using XML Serialisation.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public GenericResult DDMS_Serialise(string Path)
        {
            Logging.Log($"Saving DataModel to {Path}.", ClassName); 
            GenericResult GR = new GenericResult();

            if (!DDMS_Serialise_FindRequiredComponents())
            {
                // create a new XmlDocument and add a new node
                // not using XDocument for writing as the api is fucked up the ass.
                XmlDocument XD = new XmlDocument();

                XmlNode XNRoot = XD.CreateElement("Lightning");


                foreach (string FName in Enum.GetNames(typeof(DDMSComponents)))
                {
                    XmlNode XComponentNode = XD.CreateElement(FName);

                    XNRoot.AppendChild(XComponentNode);
                }

                Debug.Assert(XNRoot.HasChildNodes);

                foreach (XmlNode XComponentNode in XNRoot.ChildNodes)
                {
                    switch (XComponentNode.Name)
                    {
                        case "Metadata":
                            DDMSComponentSerialisationResult DDCSR_Metadata = DDMS_Serialise_SerialiseMetadataComponent(XD, XComponentNode);

                            if (!DDCSR_Metadata.Successful)
                            {
                                ErrorManager.ThrowError(ClassName, "FailedToSaveLgxException", $"Failed to save LGX file: Error parsing Metadata component: {DDCSR_Metadata.FailureReason}");
                            }
                            else
                            {
                                XD = DDCSR_Metadata.XmlDocument;
                            }

                            continue;
                        case "Settings":
                            DDMSComponentSerialisationResult DDCSR_Settings = DDMS_Serialise_SerialiseSettingsComponent(XD, XComponentNode);

                            if (!DDCSR_Settings.Successful)
                            {
                                ErrorManager.ThrowError(ClassName, "FailedToSaveLgxException", $"Failed to save LGX file: Error parsing Settings component: {DDCSR_Settings.FailureReason}");
                            }
                            else
                            {
                                XD = DDCSR_Settings.XmlDocument;
                            }

                            continue;
                        case "Workspace":
                            DDMSComponentSerialisationResult DDCSR_Workspace = DDMS_Serialise_SerialiseWorkspaceComponent(XD, XComponentNode);

                            if (!DDCSR_Workspace.Successful)
                            {
                                ErrorManager.ThrowError(ClassName, "FailedToSaveLgxException", $"Failed to save LGX file: Error parsing Workspace component: {DDCSR_Workspace.FailureReason}");
                            }
                            else
                            {
                                XD = DDCSR_Workspace.XmlDocument;
                            }

                            continue;
                    }
                }

                GR.Successful = true;
                return GR; 
            }
            else
            {
                string ErrorString = "Failed to save LGX file: Invalid DataModel - must exit!";
                ErrorManager.ThrowError(ClassName, "InvalidDataModelCannotSaveLgxException", ErrorString);

                GR.FailureReason = ErrorString; 
                return GR; // will not run
            }

        }

        private bool DDMS_Serialise_FindRequiredComponents()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR1 = Ws.GetFirstChildOfType("GameMetadata");
            GetInstanceResult GIR2 = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR1.Successful || !GIR2.Successful
                || GIR1.Instance == null || GIR2.Instance == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private DDMSComponentSerialisationResult DDMS_Serialise_SerialiseMetadataComponent(XmlDocument XD, XmlNode XMetadataNode)
        {
            DDMSComponentSerialisationResult DDCSR = new DDMSComponentSerialisationResult();

            
            // we already checked
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameMetadata");

            GameMetadata GMA = (GameMetadata)GIR.Instance;

            // Create the Metadata XML elements.
            XmlNode XDMSchemaVersion = XD.CreateElement("DMSchemaVersion");
            XmlNode XAuthor = XD.CreateElement("Author");
            XmlNode XDescription = XD.CreateElement("Description");
            XmlNode XGameName = XD.CreateElement("GameName"); 
            XmlNode XCreationDate = XD.CreateElement("CreationDate");
            XmlNode XLastModifiedDate = XD.CreateElement("LastModifiedDate");
            XmlNode XRevisionID = XD.CreateElement("RevisionID");
            XmlNode XVersion = XD.CreateElement("Version");

            XDMSchemaVersion.Value = XMLSCHEMA_VERSION;
            if (GMA.Author != null) XAuthor.Value = GMA.Author;
            if (GMA.Description != null) XDescription.Value = GMA.Description;

            if (GMA.Name == null || GMA.Name == "")
            {
                DDCSR.FailureReason = "Please name this Game!";
                return DDCSR; 
            }

            string CurrentTime = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");

            if (GMA.RevisionNumber == 0)
            {
                XCreationDate.Value = CurrentTime;
            }
            else
            {
                XCreationDate.Value = GMA.CreationDate.ToString("yyyy-MM-dd HH:mm:ss");
            }

            XLastModifiedDate.Value = CurrentTime;

            XRevisionID.Value = GMA.RevisionNumber + 1.ToString();

            // Version is optional
            if (GMA.Version != null) XVersion.Value = GMA.Version;

            XMetadataNode.AppendChild(XDMSchemaVersion);
            if (XAuthor.Value != null) XMetadataNode.AppendChild(XAuthor);
            if (XDescription.Value != null) XMetadataNode.AppendChild(XDescription);
            XMetadataNode.AppendChild(XCreationDate);
            XMetadataNode.AppendChild(XGameName); 
            XMetadataNode.AppendChild(XLastModifiedDate);
            XMetadataNode.AppendChild(XRevisionID);
            if (XVersion.Value != null) XMetadataNode.AppendChild(XVersion); // only append if used.

            XD.AppendChild(XMetadataNode);

            DDCSR.Successful = true;
            return DDCSR; 
        }

        private DDMSComponentSerialisationResult DDMS_Serialise_SerialiseSettingsComponent(XmlDocument XD, XmlNode XSettingsNode)
        {

            DDMSComponentSerialisationResult DDCSR = new DDMSComponentSerialisationResult();

            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            // check already made
            GameSettings GS = (GameSettings)GIR.Instance;

            foreach (Instance Setting in GS.Children)
            {
                Type SettingType = Setting.GetType();

                // continue if not a gamesetting
                if (Setting.ClassName != "GameSetting"
                    || SettingType == typeof(GameSetting))
                {
                    continue; 
                }
                else
                {
                    GameSetting SettingToAdd = (GameSetting)Setting;

                    XmlNode XSettingNode = XD.CreateElement("Setting");

                    XmlNode XNameNode = XD.CreateElement("Name");
                    XmlNode XTypeNode = XD.CreateElement("Type");
                    XmlNode XValueNode = XD.CreateElement("Value");

                    XNameNode.Value = SettingToAdd.SettingValue.ToString();
                    string XTypeNodeValueName = SettingToAdd.SettingType.FullName;

                    if (DDMS_ParseSettings_CheckIfValidTypeForInstantiation(XTypeNodeValueName))
                    {
                        if (XTypeNodeValueName.ContainsCaseInsensitive("Lightning."))
                        {
                            XTypeNodeValueName.Replace("Lightning.", "");
                        }

                        XTypeNode.Value = XTypeNodeValueName;
                        
                    }
                    else
                    {
                        DDCSR.FailureReason = "This Setting cannot be saved - the Type must be in the System or Lightning.* namespaces!";
                        return DDCSR;
                    }

                    XSettingNode.AppendChild(XNameNode);
                    XSettingNode.AppendChild(XTypeNode);
                    XSettingNode.AppendChild(XValueNode);

                    XSettingsNode.AppendChild(XSettingNode);

                }
            }

            DDCSR.Successful = true;
            return DDCSR;
        }

        private DDMSComponentSerialisationResult DDMS_Serialise_SerialiseWorkspaceComponent(XmlDocument XD, XmlNode XWorkspaceNode)
        {
            DDMSComponentSerialisationResult DDCSR = new DDMSComponentSerialisationResult();

            Workspace Ws = DataModel.GetWorkspace();

            foreach (Instance Ins in Ws.Children)
            {
                DDMS_Serialise_SerialiseDMObjectToElement(XD, XWorkspaceNode, Ins);
            }

            DDCSR.Successful = true;
            return DDCSR; 
        }

        private DDMSComponentSerialisationResult DDMS_Serialise_SerialiseDMObjectToElement(XmlDocument XD, XmlNode XWorkspaceNode, Instance Ins)
        {
            DDMSComponentSerialisationResult DDCSR = new DDMSComponentSerialisationResult();

            XmlNode XInstanceNode = XD.CreateElement(Ins.ClassName);

            List<InstanceInfoProperty> IIP = Ins.Info.Properties;

            foreach (InstanceInfoProperty IIPItem in IIP)
            {
                XmlAttribute XPropertyAttribute = XD.CreateAttribute(IIPItem.Name);

                XPropertyAttribute.Value = Ins.Info.GetValue(XPropertyAttribute.Name, Ins).ToString();
                XInstanceNode.Attributes.Append(XPropertyAttribute);
            }

            XInstanceNode.AppendChild(XWorkspaceNode);

            if (Ins.Children.Count != 0)
            {
                foreach (Instance InsChild in Ins.Children)
                {
                    DDMS_Serialise_SerialiseDMObjectToElement(XD, XInstanceNode, InsChild);
                }
            }

            DDCSR.Successful = true;
            DDCSR.XmlDocument = XD;
            return DDCSR; 
        }


#if DEBUG
        private void ATest() => DDMS_Serialise("WritingTest.xml");

#endif
    }

}
