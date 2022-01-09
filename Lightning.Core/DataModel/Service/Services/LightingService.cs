using NuRender; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LightingService
    /// 
    /// January 9, 2022 
    /// 
    /// Implements an accelerated rendering pipeline for lighting.
    /// Builds a screen-space lightmap and renders it to a NuRender Image for rendering. 
    /// </summary>
    public class LightingService : Service
    {
        internal override ServiceImportance Importance => ServiceImportance.Low;

        public override void OnDataSent(ServiceMessage Data)
        {
            return; 
        }

        public override ServiceStartResult OnStart()
        {
            // Subscribe to the pre-rendering event.
            PreRender += DoPreRender; 
            return new ServiceStartResult { Successful = true };
        }

        public override ServiceShutdownResult OnShutdown()
        {
            return new ServiceShutdownResult { Successful = true };
        }

        private void DoPreRender(object Sender, PreRenderEventArgs PEAA)
        {
            return;
        }

        public override void Poll()
        {
            return;
        }
    }
}
