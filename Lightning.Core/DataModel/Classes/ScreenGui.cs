using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScreenGui
    /// 
    /// June 29, 2021
    /// 
    /// Defines a GUI that is placed on the screen. Its position is relative to the screen resolution.
    /// </summary>
    public class ScreenGui : GuiRoot
    {
        internal override string ClassName => "ScreenGui";


        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {

            // hack
            // need to find a better way to do this

            // move to oncreate?
            // force the position to a screen position
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GuiElement");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGuiRootsException");
                return; 
            }
            else
            {
                foreach (Instance Instance in GMIR.Instances)
                {
                    GuiElement GuiRoot = (GuiElement)Instance;
                    //todo: finish this

                    if (GuiRoot.Position != null)
                    {
                        GetInstanceResult GIR = DataModel.GetFirstChildOfType("GameSettings");

                        if (!GIR.Successful
                            || GIR.Instance == null)
                        {
                            ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException", "Failed to obtain GameSettings for UI...");
                        }
                        else
                        {
                            GameSettings GS = (GameSettings)GIR.Instance;

                            GetGameSettingResult WindowWidthSettingResult = GS.GetSetting("WindowWidth");
                            GetGameSettingResult WindowHeightSettingResult = GS.GetSetting("WindowHeight");
                            
                            if (!WindowHeightSettingResult.Successful
                                || !WindowWidthSettingResult.Successful)
                            {
                                ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "Failed to obtain WindowWidth or WindowHeight setting!");
                                return; // will never run
                            }
                            else
                            {
                                GameSetting WindowWidthSetting = WindowWidthSettingResult.Setting;
                                GameSetting WindowHeightSetting = WindowHeightSettingResult.Setting;

                                int WindowWidth = (int)WindowWidthSetting.SettingValue;
                                int WindowHeight = (int)WindowHeightSetting.SettingValue;

                                if (GuiRoot.Position.X > WindowWidth) GuiRoot.Position.X = WindowWidth;
                                if (GuiRoot.Position.X < 0) GuiRoot.Position.X = 0;
                                if (GuiRoot.Position.Y > WindowHeight) GuiRoot.Position.Y = WindowHeight;
                                if (GuiRoot.Position.Y < 0) GuiRoot.Position.Y = 0;

                                GuiRoot.ForceToScreen = true; 
                            }
                        }
                    }
                
                }
            }

            // render all children
            base.Render(SDL_Renderer, Tx);
        }
    }
}
