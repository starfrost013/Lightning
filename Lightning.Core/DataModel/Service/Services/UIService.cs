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
        internal override string ClassName => "UIService";
        internal override ServiceImportance Importance => ServiceImportance.Low;

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

        private static bool UISERVICE_INITIALISED { get; set; }

        private void LoadAllFonts()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Font");

            Logging.Log("Loading all fonts...", ClassName); 

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfFontsException");
                return; 
            }
            else
            {
                for (int i = 0; i < GMIR.Instances.Count; i++)// collection is being modified
                {
                    Instance Instance = GMIR.Instances[i];

                    Font Fnt = (Font)Instance;

                    if (Fnt.Name != null)
                    {
                        Logging.Log($"Loading the font {Fnt.Name}...", ClassName);

                        Fnt.Load();

                        if (!Fnt.FONT_LOADED)
                        {
                            // font failed to load
                            GMIR.Instances.Remove(Instance);
                        }
                    }
                    else
                    {
                        ErrorManager.ThrowError(ClassName, "FontMustDeclareNameException");
                        GMIR.Instances.Remove(Instance); 
                    }

                }
            }
        }

        private void UnloadAllFonts()
        {

            Logging.Log($"Unloading all fonts...", ClassName);

            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Font");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfFontsException");
                return;
            }
            else
            {
                foreach (Instance Ins in GMIR.Instances)
                {
                    Font Fnt = (Font)Ins;

                    Logging.Log($"Unloading the font {Fnt.Name}...", ClassName);
                    Fnt.Unload(); 
                }
            }
        }



        private void Init()
        {
            LoadAllFonts();
            UISERVICE_INITIALISED = true; 
        }

        private List<GuiElement> GetAllUIElements()
        {
            Workspace WS = DataModel.GetWorkspace();

            GetMultiInstanceResult IC = WS.GetAllChildrenOfType("GuiElement"); 

            if (IC.Instances == null
                || !IC.Successful)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGuiRootsException");
                ServiceNotification SN = new ServiceNotification(ServiceNotificationType.Crash, ClassName, "Failed to obtain list of GUIElements");
                ServiceNotifier.NotifySCM(SN);
                return null; // probably doesn't actually run
            }
            else
            {
                List<Instance> GuiObjects = IC.Instances;

                List<GuiElement> GuiRootList = ListTransfer<Instance, GuiElement>.TransferBetweenTypes(GuiObjects, true);

                return GuiRootList; 
            }

             
        }

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

        public override ServiceShutdownResult OnShutdown()
        {
            ServiceShutdownResult SSR = new ServiceShutdownResult();

            UnloadAllFonts();
            ShutdownSDL2TTF();

            SSR.Successful = true;

            return SSR;
        }

        private void ShutdownSDL2TTF()
        {
            Logging.Log("Shutting down SDL2_ttf...", ClassName);
            SDL_ttf.TTF_Quit();
            return; 
        }
    }
}
