using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// A setting that can be created for a game.
    /// </summary>
    public class GameSettings : SerialisableObject
    {
        public List<GameSetting> Settings { get; set; }

        public GameSettings()
        {
            Settings = new List<GameSetting>();
        }


    }
}
