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

            Logging.Log("Registering methods for Lua scripting...", ClassName);
            
#if DEBUG
            // Test method registration
            //RegisterMethod("Lightning.Core.API;ScriptingTest;ScTest");
#endif
            //RegisterMethod("Lightning.Core.API;Instance;AddChild");

        }

        
    }
}
