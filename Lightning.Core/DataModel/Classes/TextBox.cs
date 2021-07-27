using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TextBox
    /// 
    /// July 19, 2021
    /// 
    /// Defines a text box.
    /// </summary>
    public class TextBox : GuiElement
    {
        internal override string ClassName => "TextBox";
        public bool AntiAliasingDisabled { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Strikethrough { get; set; }

        public string FontFamily { get; set; }

        public Alignment Alignment { get; set; }
    }
}
