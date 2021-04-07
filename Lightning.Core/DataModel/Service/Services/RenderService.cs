using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// RenderService
    /// 
    /// Handles rendering of all PhysicalInstances for Lightning using SDL2. 
    ///
    /// 2021-03-14: Created
    /// </summary>
    public class RenderService : Service
    {
        public override string ClassName => "RenderService";

        public override ServiceStartResult OnStart()
        {
            // TEST code
            ServiceStartResult SSR = new ServiceStartResult();
            

            Logging.Log("yes I am renderservice", ClassName);
            SSR.Successful = true;
            return SSR; 
            
        }

        public override ServiceShutdownResult OnShutdown()
        {
            throw new NotImplementedException();
        }

        public override ServiceShutdownResult OnUnexpectedShutdown()
        {
            throw new NotImplementedException();
        }
    }
}
