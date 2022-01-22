using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GameService
    /// 
    /// November 13, 2021
    /// 
    /// User-facing game save/load API (TODO: support loading & saving from the Internet)
    /// </summary>
    public class GameService : Service
    {
        internal override string ClassName => "GameService";
        internal override ServiceImportance Importance => ServiceImportance.Low; 

        public override ServiceStartResult OnStart()
        {
            Logging.Log("GameService Init", ClassName);
            return new ServiceStartResult { Successful = true };
        }

        public override ServiceShutdownResult OnShutdown()
        {
            return new ServiceShutdownResult { Successful = true };
        }

        public override void OnDataSent(ServiceMessage Data)
        {
            return; // do nothing
        }

        public override void OnCreate()
        {
            return;
        }

        public override void Poll()
        {
            return;
        }

        public void LoadGame(string XMLFile)
        {
            // todo: .lwpak
            DataModel.LoadFile(XMLFile);
        }

        public void SaveCurrentGame(string XMLFile = null)
        {
            if (XMLFile == null) XMLFile = DataModel.DATAMODEL_LASTXML_PATH;

            // check again after setting to datamodel_lastxml_path
            if (XMLFile == null)
            {
                ErrorManager.ThrowError(ClassName, "CannotSaveWhenNoFileSpecifiedException");
            }
            else
            {
                DataModelDeserialiser DDMS = (DataModelDeserialiser)DataModel.CreateInstance("DataModelDeserialiser");
                DDMS.DDMS_Serialise(XMLFile);
            }
        }
    }
}
