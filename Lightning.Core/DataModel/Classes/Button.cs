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

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (!BUTTON_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender();
            }
            
        }

        private void Init()
        {
            ItemRectangle = new Rectangle(); // TODO: DATAMODEL (this works around a known bug, but is hacky)

            if (Position == null) Position = new Vector2(0, 0);
            if (Colour == null) Colour = new Color4(255, 255, 255, 255);
            if (BorderColour == null) BorderColour = new Color4(0, 0, 0, 0); // do not draw by default

            ItemRectangle.Position = Position;
            ItemRectangle.Colour = Colour;
            ItemRectangle.BorderColour = BorderColour;
            ItemRectangle.Fill = Fill;
            ItemRectangle.BorderWidth = BorderWidth;

            ItemText = (Text)DataModel.CreateInstance("Text", this);

            ItemText.FontFamily = FontFamily;
            
            BUTTON_INITIALISED = true; 
        }

        private void DoRender()
        {

        }
    }
}
