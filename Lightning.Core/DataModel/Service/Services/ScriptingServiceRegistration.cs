using NLua; 
using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace Lightning.Core.API
{
    public partial class ScriptingService
    {
        public void RegisterAPI(bool IsLua = false)
        {

            if (!IsLua)
            {
                Logging.Log("Registering methods for scripting...", ClassName);
            }
            else
            {
                Logging.Log("Registering methods for Lua scripting...", ClassName);
            }
            
#if DEBUG
            // Test method registration
            RegisterMethod("Lightning.Core.API;ScriptingTest;ScTest", IsLua);
#endif
            //RegisterMethod("Lightning.Core.API;Instance;AddChild");

        }

        
    }
}
