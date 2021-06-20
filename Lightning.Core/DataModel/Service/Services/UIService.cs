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
        internal override ServiceImportance Importance => ServiceImportance.Low;
        public override ServiceStartResult OnStart() => throw new NotImplementedException();
        public override ServiceShutdownResult OnShutdown() => throw new NotImplementedException();

        public override void Poll() => throw new NotImplementedException();
    }
}
