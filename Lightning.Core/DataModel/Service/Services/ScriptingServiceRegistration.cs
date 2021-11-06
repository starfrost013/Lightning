using NLua; 
using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace Lightning.Core.API
{
    public partial class ScriptingService
    {
        public void LoadAPI()
        {
            Logging.Log("Loading Lightning.Core for Lua use...", ClassName); 
            ScriptGlobals.LuaState.DoString("luanet.load_assembly('Lightning.Core')", "LuaNET Lightning Namespace Loader");
        }

        public void RegisterAPI()
        {
            RegisterGlobals();
            RegisterClasses(); 
        }

        private void RegisterGlobals()
        {
            Logging.Log("Registering global methods for Lua scripting...", ClassName);

            RegisterMethod("Lightning.Core.API;ScriptingService;Wait");
        }

        /// <summary>
        /// Registers classes for use with Lua.
        /// </summary>
        private void RegisterClasses()
        {
            Logging.Log("Registering classes for Lua scripting...", ClassName);

            // core stuff
            RegisterClass("Lightning.Core.API.DataModel");
            RegisterClass("Lightning.Core.API.Instance");
            RegisterClass("Lightning.Core.API.Service");
            RegisterClass("Lightning.Core.API.ScriptingTest");

            // register the actual api

            RegisterClass("Lightning.Core.API.AABB");
            RegisterClass("Lightning.Core.API.AnimatedImageBrush");
            RegisterClass("Lightning.Core.API.Animation");
            RegisterClass("Lightning.Core.API.AnimationFrame");
            RegisterClass("Lightning.Core.API.Brush");
            RegisterClass("Lightning.Core.API.Button");
            RegisterClass("Lightning.Core.API.Camera");
            RegisterClass("Lightning.Core.API.CheckBox");
            RegisterClass("Lightning.Core.API.Circle");
            RegisterClass("Lightning.Core.API.Color3");
            RegisterClass("Lightning.Core.API.Color4");
            RegisterClass("Lightning.Core.API.Container");
            RegisterClass("Lightning.Core.API.Control");
            RegisterClass("Lightning.Core.API.ControllableObject");
            //we won't expose ddms directly, helper class will be used
            RegisterClass("Lightning.Core.API.DefaultPhysicsController");
            RegisterClass("Lightning.Core.API.Font");
            RegisterClass("Lightning.Core.API.GameMetadata");
            RegisterClass("Lightning.Core.API.GradientStop");
            RegisterClass("Lightning.Core.API.Gui");
            RegisterClass("Lightning.Core.API.GuiElement");
            RegisterClass("Lightning.Core.API.GuiRoot");
            RegisterClass("Lightning.Core.API.Humanoid");
            RegisterClass("Lightning.Core.API.ImageBrush");
            RegisterClass("Lightning.Core.API.Line");
            RegisterClass("Lightning.Core.API.LinearGradientBrush");
            // luaglobalmethods will not be exposed
            RegisterClass("Lightning.Core.API.Menu");
            RegisterClass("Lightning.Core.API.MenuItem");
            RegisterClass("Lightning.Core.API.PhysicalObject");
            RegisterClass("Lightning.Core.API.PhysicsController");
            RegisterClass("Lightning.Core.API.Rectangle");
            RegisterClass("Lightning.Core.API.ScreenGui");
            RegisterClass("Lightning.Core.API.Sky");
            RegisterClass("Lightning.Core.API.SolidColourBrush");
            RegisterClass("Lightning.Core.API.Sound");
            RegisterClass("Lightning.Core.API.SurfaceGui");
            RegisterClass("Lightning.Core.API.Text");
            RegisterClass("Lightning.Core.API.TextBox");
            RegisterClass("Lightning.Core.API.Vector2");
            RegisterClass("Lightning.Core.API.Workspace");
            RegisterClass("Lightning.Core.API.WorldGui");
        }


    }
}
