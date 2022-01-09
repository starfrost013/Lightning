using NuCore.Utilities;
using NuRender; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// DebugPage (Lightning IGD Services)
    /// 
    /// August 20, 2021 (modified December 11, 2021)
    /// 
    /// Defines a special type of GUI used for debugging.
    /// </summary>
    public class DebugGui : Gui
    {
        internal override string ClassName => "DebugGui";

        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable | InstanceTags.ParentCanBeNull;
        /// <summary>
        /// The header of this debug page.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Internal: determines if the debug page is initialised
        /// </summary>
        internal bool DEBUGPAGE_INITIALISED { get; set; }

        /// <summary>
        /// Determines if this debug page is active.
        /// </summary>
        private bool Active { get; set; }

        /// <summary>
        /// Debug settings. Passed from IGDService.
        /// </summary>
        internal DebugSettings Settings { get; set; }

        public override void OnCreate()
        {
            // TODO: List building methods in RenderService, etc, need to be recursive
            OnKeyDownHandler += OnKeyDown;
            Settings = new DebugSettings();
        }

        internal void DP_Init()
        {

            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful
            || GIR.Instance == null)
            {
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException");
            }
            else
            {
                GameSettings Settings = (GameSettings)GIR.Instance;
                GetGameSettingResult GGSR_WindowWidth = Settings.GetSetting("WindowWidth");
                GetGameSettingResult GGSR_WindowHeight = Settings.GetSetting("WindowHeight");

                if (!GGSR_WindowWidth.Successful
                || !GGSR_WindowHeight.Successful
                || GGSR_WindowWidth.Setting == null
                || GGSR_WindowHeight.Setting == null)
                {
                    ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "Failed to obtain WindowWidth and WindowHeight GameSettings for in-game debugging services!");
                    return;
                }
                else
                {

                    GameSetting WindowWidth_Setting = GGSR_WindowWidth.Setting;
                    GameSetting WindowHeight_Setting = GGSR_WindowHeight.Setting;

                    try
                    {
                        int WindowWidth = (int)WindowWidth_Setting.SettingValue;
                        int WindowHeight = (int)WindowHeight_Setting.SettingValue;

                        Vector2 DbgPageBegin = new Vector2(WindowWidth * 0.3, WindowHeight * 0.3); // todo: gamesetting for this
                        Vector2 DbgPageEnd = new Vector2(WindowWidth * 0.7, WindowHeight * 0.7);

                        Init_ForceToScreen();
                        
                        
                    }
                    catch (Exception err)
                    {
#if DEBUG
                        ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", $"WindowWidth and WindowHeight have an invalid value!\n\n{err}");
#else
                        ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "WindowWidth and WindowHeight have an invalid value!");
#endif
                        return; 
                    }

                    DEBUGPAGE_INITIALISED = true;

                    // by this point we have already verified that debug is enable
                    Active = true; 
                    return;

                }
            }
        }

        private void Init_ForceToScreen()
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GuiElement");

            if (!GMIR.Successful
            || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGuiElementsException");
                return; 
            }
            else
            {
                List<Instance> Instances = (List<Instance>)GMIR.Instances;

                foreach (Instance Instance in Instances)
                {
                    GuiElement GR = (GuiElement)Instance;

                    GR.ForceToScreen = true; 
                }
            }

        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            if (!DEBUGPAGE_INITIALISED)
            {
                DP_Init();
            }
            else
            {
                if (Active) DoRender(SDL_Renderer, Tx);
            }
        }


        private void DoRender(Scene SDL_Renderer, ImageBrush Tx)
        {
            List<DebugPage> DebugPages = GetDebugPages();

            foreach (DebugPage DP in DebugPages)
            {
                if (Settings.DisplayHitboxes) DoRender_RenderHitboxes(SDL_Renderer, Tx);

                if (DP.IsOpen)
                {
                    DP.Render(SDL_Renderer, Tx);
                    base.Render(SDL_Renderer, Tx); // render all elements

                    // todo: remove this idiot hack 
                    break; // only render one page.
                    // end todo: remove this idiot hack 
                }

                
           }
        }

        private void DoRender_RenderHitboxes(Scene SDL_Renderer, ImageBrush Tx)
        {
            Workspace Ws = DataModel.GetWorkspace();

            Window MainWindow = SDL_Renderer.GetMainWindow();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("PhysicalInstance");

            if (GMIR.Successful)
            {
                List<Instance> Instances = GMIR.Instances;

                foreach (PhysicalInstance Instance in Instances)
                {
                    if (Instance.PhysicsEnabled)
                    {
                        if (Instance.AABB != null)
                        {
                            // this is a stupid fucking hack
                            NuRender.Rectangle Rectangle = new NuRender.Rectangle(); // hack to make it get GC'd

                            Rectangle.Size = (Vector2Internal)Instance.AABB.Size;

                            if (Instance.ForceToScreen)
                            {
                                Rectangle.Position = (Vector2Internal)Instance.Position;
                            }
                            else
                            {
                                Rectangle.Position = (Vector2Internal)Instance.Position - MainWindow.Settings.RenderingInformation.CCameraPosition;
                            }

                            Rectangle.Colour = (Color4Internal)new Color4(255, 255, 255, 0);
                            Rectangle.Filled = true;
                            //todo: borders, backgrounds
                            Rectangle.Render(MainWindow.Settings.RenderingInformation);
                            // end this is a stupid fucking hack
                        }

                    }
                }
            }
            else
            {
                ErrorManager.ThrowError(ClassName, "ErrorObtainingListOfPhysicalInstancesToRenderException", "Error obtaining list of physical objects for debug rendering!");
                return;
            }
        }

        internal List<DebugPage> GetDebugPages()
        {
            // TEMP CODE UNTIL WE CAN FIX THIS SHIT 
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("DebugPage");

            if (!GMIR.Successful
            || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "UnableToAcquireDebugGuiException");
                return null; 
            }
            else
            {
                List<Instance> Instances = GMIR.Instances;
                List<DebugPage> DebugPages = ListTransfer<Instance, DebugPage>.TransferBetweenTypes(Instances);

                
                return DebugPages; 
                
            }
        }

        private void OnKeyDown(object Sender, KeyEventArgs EventArgs)
        {
            string TheKey = EventArgs.Key.KeyCode.ToString();

            switch (TheKey)
            {
                // make these configurable
                case "ESCAPE":
                    Active = !Active;
                    return;
                case "F7":
                    Settings.DisplayHitboxes = !Settings.DisplayHitboxes;
                    return;
                case "F8":
                    Settings.DisplayCollision = !Settings.DisplayCollision;
                    return;
            }

        }
    }
}
