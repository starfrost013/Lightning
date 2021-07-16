using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Button
    /// 
    /// July 12, 2021
    /// 
    /// Defines a button
    /// </summary>
    public class Button : GuiElement
    {
        public string Content { get; set; } 

        /// <summary>
        /// Gets or sets the Font Family to use for the text of this Button.
        /// </summary>
        public string FontFamily { get; set; }
        
        private bool BUTTON_INITIALISED { get; set; }

        private Rectangle ItemRectangle { get; set; }

        private Text ItemText { get; set; }

        /// <summary>
        /// Will this Button be filled?
        /// </summary>
        public bool Fill { get; set; }

        /// <summary>
        /// If true, the text of this Button will be bold.
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// If true, the text of this Button will be italic.
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// If true, the text of this Button will be underlined.
        /// </summary>
        public bool Underline { get; set; }
        
        /// <summary>
        /// If true, the text of this Button will be strikethrough.
        /// </summary>
        public bool Strikethrough { get; set; }


        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (!BUTTON_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }
            
        }

        private void Init()
        {
            ItemRectangle = new Rectangle(); // TODO: DATAMODEL (this works around a known bug, but is hacky)

            if (Position == null) Position = new Vector2(0, 0);
            if (Colour == null) Colour = new Color4(255, 255, 255, 255);
            if (BorderColour == null) BorderColour = new Color4(0, 0, 0, 0); // do not draw by default
            if (BackgroundColour == null) BackgroundColour = new Color4(255, 0, 0, 0);

            if (FontFamily == null
                || FontFamily == "")
            {
                ErrorManager.ThrowError(ClassName, "MustDefineFontForGuiElementException", "Buttons require their FontFamily property to be set.");
                
                return; 
            }

            ItemRectangle.Position = Position;
            ItemRectangle.Colour = Colour;
            ItemRectangle.Fill = Fill;
            ItemRectangle.BorderWidth = BorderWidth;
            ItemRectangle.BorderColour = BorderColour;
            ItemRectangle.Size = Size;
            
            ItemText = (Text)DataModel.CreateInstance("Text", this);

            ItemText.Content = Content;
            ItemText.FontFamily = FontFamily;
            ItemText.Position = Position;
            ItemText.Bold = Bold;
            ItemText.Italic = Italic;
            ItemText.Underline = Underline;
            ItemText.Strikethrough = Strikethrough;
            ItemText.BorderWidth = BorderWidth;
            ItemText.BorderColour = BorderColour;

            BUTTON_INITIALISED = true; 
        }

        private void DoRender(Renderer SDL_Renderer, Texture Tx)
        {
            ItemRectangle.Render(SDL_Renderer, Tx);

            ItemText.Render(SDL_Renderer, Tx);
        }
    }
}
