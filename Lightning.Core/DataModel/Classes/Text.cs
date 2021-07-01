using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Text
    /// 
    /// June 30, 2021
    /// 
    /// Defines text. :O
    /// </summary>
    public class Text : GuiElement
    {
        /// <summary>
        /// Content of this Text.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The font family of this Text.
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// The font size (int) of this font.
        /// </summary>
        public int FontSize { get; set; }
        
        /// <summary>
        /// Is this text bold?
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// Is this text italic?
        /// </summary>
        public bool Italic { get; set; }
        
        /// <summary>
        /// Is this text underline?
        /// </summary>
        public bool Underline { get; set; }

        /// <summary>
        /// Is this text strikethrough?
        /// </summary>
        public bool Strikethrough { get; set; }
    }
}
