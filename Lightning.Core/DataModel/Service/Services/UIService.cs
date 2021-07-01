using Lightning.Core.SDL2;
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
        internal override string ClassName => base.ClassName;
        internal override ServiceImportance Importance => ServiceImportance.Low;
        public List<Font> Fonts { get; set; }
        public override ServiceStartResult OnStart()
        {
            ServiceStartResult SSR = new ServiceStartResult();

            Logging.Log("Initialising SDL2_ttf...", ClassName);

            if (SDL_ttf.TTF_Init() < 0)
            {
                SSR.FailureReason = $"Failed to initialise SDL2_ttf: {SDL.SDL_GetError()}";
            }
            else
            {
                SSR.Successful = true;
                return SSR; 
            }

            return SSR; 
        }
        public override ServiceShutdownResult OnShutdown() => new ServiceShutdownResult { Successful = true };

        private static bool UISERVICE_INITIALISED { get; set; }

        public override void Poll()
        {
            if (!UISERVICE_INITIALISED)
            {
                Init();
            }
            //main loop goes here
            //else 
            //{
            //
            //}

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
