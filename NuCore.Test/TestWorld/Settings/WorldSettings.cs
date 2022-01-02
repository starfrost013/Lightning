using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Test
{
    /// <summary>
    /// WorldSettings
    /// 
    /// Holds test world settings.
    /// </summary>
    public class WorldSettings
    {
        /// <summary>
        /// TestTime (milliseconds). The time that each test will run for.
        /// </summary>
        public int TestTime { get; set; }

        public WorldSettings(int NTestTime = 2000)
        {
            TestTime = NTestTime; 
        }
    }
}
