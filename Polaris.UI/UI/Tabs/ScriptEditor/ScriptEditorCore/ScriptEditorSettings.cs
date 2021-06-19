using Lightning.Core.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polaris.UI
{
    /// <summary>
    /// ScriptEditorSettings
    /// 
    /// June 16, 2021
    /// 
    /// Script editor settings.
    /// </summary>
    public class ScriptEditorSettings
    {


        /// <summary>
        /// The font family to use for the Script Editor.
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// The font size to use for the Script Editor.
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Backing field for <see cref="ZoomPercent"/>.
        /// </summary>
        private int _zoompercent { get; set; }

        /// <summary>
        /// Zoom percentage.
        /// </summary>
        public int ZoomPercent
        {
            get
            {
                return _zoompercent;
            }
            set
            {
                if (value > 5 || value < 0.2) // TEMP until settings 
                {
                    _zoompercent = 1;
                }
                else
                {
                    _zoompercent = value;
                }
            }
        }

        public Color3 CursorStrokeColour { get; set; }
        public Color3 HighlightColour { get; set; }
        public Color3 KeywordColour { get; set; }
        public Color3 StatementColour { get; set; }
        public Color3 VariableColour { get; set; }


        
    }
}
