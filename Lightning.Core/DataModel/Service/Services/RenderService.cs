﻿using System;
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
        public override string SName { get => "RenderService"; }

        public override ServiceStartResult OnStart()
        {
            throw new NotImplementedException(); 
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
