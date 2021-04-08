﻿using System;
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

        public GetGameSettingResult GetSetting(string Name)
        {
            GetGameSettingResult GGSR = new GetGameSettingResult();

            foreach (GameSetting Setting in Settings)
            {
                if (Setting.Name == Name)
                {
                    GGSR.Successful = true;
                    GGSR.Setting = Setting;
                    return GGSR;
                }    
            }

            GGSR.FailureReason = $"Cannot find the GameSetting with the name {Name}! in this game's GameSettings!";
            return GGSR; 
        }

    }
}
