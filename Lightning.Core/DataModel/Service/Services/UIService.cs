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

#if DEBUG

        /// <summary>
        /// DEBUG only - funny string to be displayed with debug text
        /// </summary>
        private string FunnyHahaDebugString { get; set; }

#endif
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
#if DEBUG

            FunnyHahaDebugString = DebugStrings.GetDebugString();
            RenderDebugText(); // call before font loading so we don't have to load the font again
#endif

            LoadAllFonts();


            UISERVICE_INITIALISED = true; 
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

        /// <summary>
        /// DEBUG ONLY: Renders engine debugging information.
        /// </summary>
        private void RenderDebugText()
        {
#if DEBUG
            // Because Text by definition must be within a GUI,
            // we can't just add text.
            //
            // As this is a debug feature, we are just going to create a new screengui object
            // and put it in the workspace.
            // 
            // This is better than adding tons of (redundant...) code in order to write a debug feature.

            GuiRoot GR = (GuiRoot)DataModel.CreateInstance("GuiRoot"); // will enter workspace

            ScreenGui SG = (ScreenGui)DataModel.CreateInstance("ScreenGui", GR);

            Text DebugText = (Text)DataModel.CreateInstance("Text", SG);

            string DoNotUse = "Debug GUI Component - Do not use!";

            GR.Name = DoNotUse;
            SG.Name = DoNotUse;
            DebugText.Name = DoNotUse;

            DebugText.Content =
                $"Lightning Engine version {LVersion.GetVersionString()}\n (Debug)" +
                $"{FunnyHahaDebugString}" +
                $"DataModel version {DataModel.DATAMODEL_API_VERSION_MAJOR}.{DataModel.DATAMODEL_API_VERSION_MINOR}.{DataModel.DATAMODEL_API_VERSION_REVISION}";

            DebugText.Colour = new Color4 { A = 255, R = 255, G = 255, B = 255 };
            DebugText.BackgroundColour = new Color4 { A = 0, R = 0, G = 0, B = 0 };

            // TODO: default positioning stuff so we don't have to do this
            
            Vector2 Position = new Vector2(0, 0);

            GR.Position = Position;
            SG.Position = Position;
            DebugText.Position = Position;

            string FontName = "Arial.18pt for DEBUG";
            DebugText.FontFamily = FontName;

            // setup font

            Font Fnt = (Font)DataModel.CreateInstance("Font");

            GlobalSettings GS = DataModel.GetGlobalSettings();

            if (GS.DebugDefaultFontPath == null)
            {
                // destroy everything
                DataModel.RemoveInstance(GR);
                return; 
            }
            else
            {
                Fnt.FontPath = GS.DebugDefaultFontPath;
                Fnt.FontSize = 18;
                Fnt.Name = FontName;
                return; // font will be loaded setc
            }
#else
            throw new Exception("Do not call on Release builds ever!");
#endif
        }


    }
}
