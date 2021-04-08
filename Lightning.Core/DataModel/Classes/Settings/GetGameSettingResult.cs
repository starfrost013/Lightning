﻿using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class GetGameSettingResult : IResult
    {
        public GameSetting Setting { get; set; }
        public string FailureReason { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }
    }
}
