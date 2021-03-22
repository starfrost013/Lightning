using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lightning.Core
{
    public class DataModelSerialiser : Instance
    {

        /// <summary>
        /// An incredibly dumb hack to get around the compiler
        /// </summary>
        private static bool IsSuccessful { get; set; }

        /// <summary>
        /// Change?
        /// 
        /// The XML schema version. 
        /// </summary>
        public static string XMLSCHEMA_VERSION = "0.2.2.0004";

        /// <summary>
        /// DDMS (Dynamic DataModel Serialiser)
        /// 
        /// Transform an XML document to a DataModel.
        /// 
        /// 2021-03-11
        /// 
        /// make ddmsserialisationresult?
        /// 
        /// TODO: THROW ERROR
        /// </summary>
        /// <param name="Schema">The schema to utilise for serialisation.</param>
        /// <param name="Path">The path to the XML document to serialise. </param>
        /// <returns></returns>
        public DataModel DDMS_Serialise(LightningXMLSchema Schema, string Path)
        {
            Logging.Log($"DDMS: Reading {Path} and transforming to DataModel...");

            string ScPath = Schema.Path;

            XmlReaderSettings XRS = new XmlReaderSettings(); 

            XmlReader XR = XmlReader.Create(Path, XRS);

            // Create the datamodel that we will be returning.
            DataModel DM = new DataModel();
            DDMS_Validate(Schema, Path);
            
            while (XR.Read())
            {
                switch (XR.NodeType)
                {
                    case XmlNodeType.Whitespace:
                    case XmlNodeType.SignificantWhitespace:
                    case XmlNodeType.Comment:
                        // continue and skip useless nodes
                        continue;
                    case XmlNodeType.Element:

                        List<string> ValidComponents = new List<string>();

                        foreach (int EVal in Enum.GetValues(typeof(DDMSComponents)))
                        {
                            // Death to Dumb Hacks (especially political ones)!
                            string EvalString = Enum.GetName(typeof(DDMSComponents), EVal);

                            if (EvalString == null)
                            {
                                // TEMP - SERIALISATION - ERRORS.XML
                                return null;
                                // VTEMP - SERIALISATION - ERRORS.XML
                            }
                            else
                            {
                                ValidComponents.Add(EvalString);
                            }

                        }

                        // We have already determined the valid components,
                        // so no error checking
                        foreach (string ValidComponent in ValidComponents)
                        {
                            if (XR.Name == ValidComponent)
                            {
                                DDMSComponents DDMSComp = (DDMSComponents)Enum.Parse(typeof(DDMSComponents), ValidComponent);
                                DDMSSerialisationResult DDSRMS = DDMS_SerialiseFileComponent(XR, DM, DDMSComp);

                                if (DDSRMS.Successful)
                                {
                                    DM = DDSRMS.DataModel;
                                }
                                else
                                {
                                    // TEMP UNTIL SERIALISATION ERRORS.XML
                                    Logging.Log($"Failed to serialise DDMS metadata component - {DDSRMS.FailureReason}", "DDMS Serialiser - metadata", MessageSeverity.Error);
                                }
                            }
                        }

                        continue; 
                        
                }

            }

            return DM; 
            
        }

        private DDMSValidateResult DDMS_Validate(LightningXMLSchema Schema, string Path)
        {

            DDMSValidateResult DDMSVR = new DDMSValidateResult();

            XmlReaderSettings XRS = new XmlReaderSettings();
            XRS.ValidationType = ValidationType.Schema;
            XRS.Schemas.Add(null, Schema.Path);
            XRS.ValidationEventHandler += DDMS_Validate_OnFail;
            XmlReader XR = XmlReader.Create(Path, XRS);
            
            // Yep, we have to do this crap.
            while (XR.Read())
            {
                
            }
            
            if (!IsSuccessful)
            {
                DDMSVR.FailureMessage = "XML schema validation failure";
                return DDMSVR;
            }
            else
            {
                DDMSVR.Successful = true;
                return DDMSVR; 
            }
            
        }

        private void DDMS_Validate_OnFail(object sender, ValidationEventArgs e)
        {
            IsSuccessful = false; 
            switch (e.Severity)
            {
                case XmlSeverityType.Warning:
                    Logging.Log($"DDMS Serialisation Warning!{e.Message}\n\n{e.Exception}");
                    return;
                case XmlSeverityType.Error:
                    // TODO - SERIALISATION -ERRORS.XML
                    Logging.Log($"DDMS Serialisation Error!{e.Message}\n\n{e.Exception}");
                    return;
                    // TODO - SERIALISATION - ERRORS.XML
            }
        }

        /// <summary>
        /// Parses DDMS file components.
        /// </summary>
        /// <param name="XM">The XmlReader to pass</param>
        /// <param name="DM"></param>
        /// <param name="Component"></param>
        /// <returns></returns>
        private DDMSSerialisationResult DDMS_SerialiseFileComponent(XmlReader XM, DataModel DM, DDMSComponents Component)
        {
            DDMSSerialisationResult DDSR = new DDMSSerialisationResult();

            
            switch (Component)
            {
                case DDMSComponents.Metadata:
                    DDSR = DDMS_ParseMetadataComponent(XM, DM);

                    return DDSR; 
                case DDMSComponents.Settings:
                    DDSR = DDMS_ParseSettingsComponent(XM, DM);
                    DDSR.DataModel = DM;

                    return DDSR;
                case DDMSComponents.InstanceTree:
                    DDSR = DDMS_ParseInstanceTreeComponent(XM, DM);
                    return DDSR;
            }

            return DDSR;


        }

        /// <summary>
        /// Parses the metadata component of a DDSR-compliant file.
        /// 
        /// March 20, 2021
        /// </summary>
        /// <param name="XM"></param>
        /// <param name="DM"></param>
        /// <returns></returns>
        private DDMSSerialisationResult DDMS_ParseMetadataComponent(XmlReader XM, DataModel DM)
        {

            // Clear global datamodel state
            // Might need to make it non-static
            DataModel.Clear();

            DDMSSerialisationResult DDSR = new DDMSSerialisationResult();

            GameMetadata GM = (GameMetadata)DataModel.CreateInstance("GameMetadata");

            while (XM.Read())
            {
                switch (XM.NodeType)
                {
                    // blame the hp stream for my unproductive day 
                    // also i had to go to my dads
                    // 2021-03-20
                    case XmlNodeType.Element:

                        try
                        {
                            switch (XM.Name)
                            {
                                case "Author":
                                    GM.Author = XM.Value;
                                    continue;
                                case "CreationDate":
                                    GM.CreationDate = DateTime.Parse(XM.Value);
                                    continue;
                                case "DMSchemaVersion":
                                    if (XM.Value != XMLSCHEMA_VERSION)
                                    {
                                        DDSR.FailureReason = $"Invalid version! Using version {XM.Value}, expected {XMLSCHEMA_VERSION}!";
                                    }
                                    continue;
                                case "LastModifiedDate":
                                    GM.LastModifiedDate = DateTime.Parse(XM.Value);
                                    continue;
                                case "RevisionID":
                                    GM.RevisionNumber = Convert.ToInt32(XM.Value);
                                    continue; 

                            }
                        }
                        catch (FormatException err)
                        {
                            // TODO - SERIALISATION - ERRORS.XML
                            // TEMP
                            Logging.Log(err.Message, "DDMS Serialiser - Metadata", MessageSeverity.Error);
                            // END TEMP
                            // TODO - SERIALISATION - ERRORS.XML
                            DDSR.FailureReason = $"Attempted to serialise invalid XML/n{err}";
                        }

                        continue; 
                    case XmlNodeType.EndElement:
                        if (XM.Name.ContainsCaseInsensitive("Metadata"))
                        {
                            // we would have returned earlier in the case of an error

                            DDSR.Successful = true;
                            DDSR.DataModel = DM;

                            return DDSR; 
                        }
                        continue;
                }
            }


            return DDSR;
        }

        private DDMSSerialisationResult DDMS_ParseSettingsComponent(XmlReader XM, DataModel DM)
        {
            throw new NotImplementedException();
        }

        private DDMSSerialisationResult DDMS_ParseInstanceTreeComponent(XmlReader XM, DataModel DM)
        {

            DDMSSerialisationResult DDSR = new DDMSSerialisationResult();

            while (XM.Read())
            {
                switch (XM.NodeType)
                {
                    case XmlNodeType.Element:
                        string XDataModelName = XM.Name;

                        try
                        {

                            // Namespace path to DataModel
                            string DATAMODELPATH = "Lightning.Core.";
                            // Todo: EngineGlobal?
                            Type XDR = Type.GetType($"{DATAMODELPATH}{XDataModelName}");

                            //bug: no checks
                            //see: polymorphism
                            //THIS IS NOT AN INSTANCE THIS IS WHATEVER CLASS WE HAVE JUST CREATED IT IS COMPILE-TIME AN INSTANCE BUT AT RUNTIME IT IS THE RESULT OF DATAMODEL.CREATEINSTANCE
                            Instance XDRInstance = (Instance)DataModel.CreateInstance(XDataModelName);

                            // TODO: instantiationresult from datamodel.createinstance
                            if (XDRInstance == null)
                            {
                                // successful false by default
                                DDSR.FailureReason = "Object is not in the datamodel or the object is non-instantiable.";
                                return DDSR; 
                            }
                            for (int i = 0; i < XM.AttributeCount; i++)
                            {
                                // meh
                                // better ways to do this - fix this
                                XM.MoveToAttribute(i);

                                // perform a kind of wizardry with InstanceInfo and classes
                                
                                foreach (InstanceInfoProperty IIP in XDRInstance.Info.Properties)
                                {
                                    // We have found the instance property that we want
                                    if (XM.Name == IIP.Name)
                                    {
                                        // WIZARD TIME
                                        // Convert from string to arbitrary type! :D 

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
                                        object? CConvertedObject = Convert.ChangeType(XM.Value, IIP.Type);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

                                        if (CConvertedObject != null)
                                        {
                                            PropertyInfo PI = XDR.GetProperty(XM.Name);


                                            //todo: handle lists...they will have subnodes
                                            PI.SetValue(XDRInstance, CConvertedObject);
                                        }
                                        else
                                        {
                                            DDSR.FailureReason = "DDMS: Conversion error: Unknown error";
                                        }
                                    }
                                } 
                            }

                        }
                        catch (ArgumentNullException err)
                        {
#if DEBUG
                            DDSR.FailureReason = $"DDMS: Conversion error: Data not found!\n\n{err}";
#else
                            DDSR.FailureReason = $"DDMS: Conversion error: Data not found!";                 
#endif
                            return DDSR; 
                        }
                        catch (FormatException err)
                        {
#if DEBUG
                            DDSR.FailureReason = $"DDMS: Conversion error: Format of data incorrect!\n\n{err}";
#else
                            DDSR.FailureReason = $"DDMS: Conversion error: Format of data incorrect!";
#endif
                            return DDSR;
                        }
                        catch (InvalidCastException err)
                        {
#if DEBUG
                            DDSR.FailureReason = $"DDMS: Conversion error: Invalid data in XML!\n\n{err}";
#else
                            DDSR.FailureReason = $"DDMS: Conversion error: Invalid data in XML!";
#endif
                            return DDSR;
                        }
                        catch (OverflowException err)
                        {
#if DEBUG
                            DDSR.FailureReason = $"DDMS: Conversion error: Integer overflow!\n\n{err}";
#else
                            DDSR.FailureReason = $"DDMS: Conversion error: Integer overflow!";
#endif
                            return DDSR;
                        }
                        catch (TargetInvocationException err)
                        {
#if DEBUG
                            DDSR.FailureReason = $"DDMS: Conversion error: Error instantiating type!\n\n{err}";
#else
                            DDSR.FailureReason = $"DDMS: Conversion error: Error instantiating type!";
#endif
                            return DDSR;
                        }
                        catch (TypeLoadException err)
                        {
#if DEBUG
                            DDSR.FailureReason = $"DDMS: Conversion error: Attempted to instantiate invalid type!\n\n{err}";
#else
                            DDSR.FailureReason = $"DDMS: Conversion error: Attempted to instantiate invalid type!";
#endif
                            return DDSR;
                        }
                        catch (Exception err)
                        {
#if DEBUG
                            DDSR.FailureReason = $"DDMS: Unknown error parsing InstanceTree!\n\n{err}";
#else
                            DDSR.FailureReason = $"DDMS: Unknown error parsing InstanceTre!e";
#endif
                            return DDSR;
                        }

                        continue;

                    case XmlNodeType.EndElement:

                        if (XM.Name.ContainsCaseInsensitive("InstanceTree"))
                        {
                            DDSR.Successful = true;
                            return DDSR;
                        }
                        else
                        {
                            continue; 
                        }

                        
                
                }
            }

            return DDSR; 
        }

         
    }
}
