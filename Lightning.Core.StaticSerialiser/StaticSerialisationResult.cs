using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightning.Core.StaticSerialiser
{
    /// <summary>
    /// 2021-01-21  20:00 (starfrost)
    /// 
    /// Enhanced from the original sample to have enhanced return code functionality, to not use winforms, and to refactor into multiple files.
    /// 
    /// 2021-04-04  00:10 (starfrost)
    /// 
    /// Moved from TrackMaker Iris to Lightning. AWAITING REWRITE.
    /// </summary>
    public class StaticSerialisationResult
    {
        public string Message { get; set; }

        public bool Successful { get; set; }


    }
}
