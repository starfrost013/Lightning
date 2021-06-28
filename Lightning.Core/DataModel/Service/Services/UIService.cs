using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// UIService
    /// 
    /// June 20, 2021
    /// 
    /// Implements a 2D UI system for Lightning.
    /// </summary>
    public class UIService : Service
    {
        internal override ServiceImportance Importance => ServiceImportance.Low;
        public override ServiceStartResult OnStart() => new ServiceStartResult { Successful = true };
        public override ServiceShutdownResult OnShutdown() => new ServiceShutdownResult { Successful = true };

        private static bool UISERVICE_INITIALISED { get; set; }

        public override void Poll()
        {
            if (!UISERVICE_INITIALISED)
            {
                Init();
            }
            else
            {

            }

            return; 
        }

        private void Init()
        {
            UISERVICE_INITIALISED = true; 
        }

        private List<GuiRoot> GetAllUIElements()
        {
            Workspace WS = DataModel.GetWorkspace();

            GetMultiInstanceResult IC = WS.GetAllChildrenOfType("GuiRoot"); 

            if (IC.Instances == null
                || !IC.Successful)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGuiRootsException");
                ServiceNotification SN = new ServiceNotification(ServiceNotificationType.Crash, ClassName, "Failed to obtain list of GUIRoots");
                ServiceNotifier.NotifySCM(SN);
                return null; // probably doesn't actually run
            }
            else
            {
                List<Instance> GuiObjects = IC.Instances;

                List<GuiRoot> GuiRootList = ListTransfer<Instance, GuiRoot>.TransferBetweenTypes(GuiObjects, true);

                return GuiRootList; 
            }

             
        }
    }
}
