﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{

    /// <summary>
    /// Lightning
    /// 
    /// Copyright © 2021 starfrost
    /// </summary>
    public class SerialisableObject : Instance
    {
        public override string ClassName => "SerialisableObject";
        public static string SchemaName { get; set; }
    }
}
