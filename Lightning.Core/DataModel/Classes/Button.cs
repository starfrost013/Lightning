using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Button
    /// 
    /// July 12, 2021 (modified August 1, 2021: textbox refactoring)
    /// 
    /// Defines a button
    /// </summary>
    public class Button : TextBox
    {
        internal override string ClassName => "Button";
    }
}
