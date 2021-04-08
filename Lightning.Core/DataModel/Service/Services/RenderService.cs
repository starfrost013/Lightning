using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// RenderService
    /// 
    /// Handles rendering of all PhysicalInstances for Lightning using SDL2. 
    ///
    /// 2021-03-14: Created
    /// 2021-04-07: Added first functionality
    /// 2021-04-08: Added test code.
    /// </summary>
    public class RenderService : Service
    {
        public override string ClassName => "RenderService";

        public override ServiceStartResult OnStart()
        {
            // TEST code
            ServiceStartResult SSR = new ServiceStartResult();

            Logging.Log("RenderService Init", ClassName);
            SSR.Successful = true;
            return SSR; 
            
        }

#if DEBUG
        public static void ATest_RenderServiceQuerySettings()
        {
            Logging.Log("Query GameSettings Test:", "Automated Testing");

            GetInstanceResult GS = DataModel.GetFirstChildOfType("GameSettings");

            // It should always be loaded at this point.
            Debug.Assert(GS.Successful && GS.Instance != null);

            GameSettings Settings = (GameSettings)GS.Instance;

            GetGameSettingResult GGSR = Settings.GetSetting("MaxFPS");

            // assert - this means the setting failed to load previously.
            Debug.Assert(GGSR.Successful && GGSR.Setting != null);

            GameSetting Setting = GGSR.Setting;

            Logging.Log($"MaxFPS: {Setting.SettingValue}");


        }
#endif
        public override ServiceShutdownResult OnShutdown()
        {
            throw new NotImplementedException();
        }

        public override void Poll()
        {
            throw new NotImplementedException();
        }

        public override ServiceShutdownResult OnUnexpectedShutdown()
        {
            throw new NotImplementedException();
        }
    }
}
