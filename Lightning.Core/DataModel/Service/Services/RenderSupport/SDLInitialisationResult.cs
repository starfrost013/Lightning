using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class SDLInitialisationResult : IResult
    {

        public string FailureReason { get; set; }
        public Renderer Renderer { get; set; }
        public int SDLErrorCode { get; set; }
        public bool Successful { get; set; }

    }
}
