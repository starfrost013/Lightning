﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ValueToken
    /// 
    /// April 16, 2021 (modified April 22, 2021: add comments)
    /// 
    /// Defines a value token. A value token is a token that defines an individual value - it may be of any type. Used for variables.
    /// </summary>
    public class ValueToken : Token 
    {
        public object Value { get; set; }
        public Type ValueType { get; set; }
        public string ValueString { get; set; }
    }
}