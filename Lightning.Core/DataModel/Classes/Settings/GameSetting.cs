using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class GameSetting : GameSettings // hack 
    {
        public string SettingName { get; set; }
        public Type SettingType { get; set; }
        public object SettingValue { get; set; }
    }
}
