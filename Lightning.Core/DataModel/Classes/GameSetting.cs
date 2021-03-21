using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class GameSetting : SerialisableObject
    {
        public string SettingName { get; set; }
        public Type SettingType { get; set; }

    }
}
