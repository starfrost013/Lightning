using Lightning.Utilities; 
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

        /// <summary>
        /// TEMP: Defines init failure
        /// 
        /// TODO: Result class
        /// </summary>
        private bool BUTTON_INITIALISATION_FAILED { get; set; }

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

        /// <summary>
        /// Padding around the text. 
        /// </summary>
        public Vector2 Padding { get; set; }

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (BUTTON_INITIALISATION_FAILED) return; 
            
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
            if (Size == null) Size = new Vector2(50, 25);

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
            
            ItemText = (Text)DataModel.CreateInstance("Text", this);

            ItemText.Content = Content;
            ItemText.FontFamily = FontFamily;
            

            if (ItemRectangle.Size == null)
            {
                FindFontResult FFR = ItemText.FindFont();

                if (!FFR.Successful
                    || FFR.Font == null)
                {
                    BUTTON_INITIALISATION_FAILED = true;
                    return; 
                }
                else
                {
                    Font FontOfText = FFR.Font; 
                    Vector2 FontSize = ItemText.GetApproximateFontSize(FontOfText);

                    if (FontSize == null)
                    {
                        ItemRectangle.Size = Size; 
                    }
                    else
                    {
                        if (Padding == null)
                        {
                            ItemRectangle.Size = FontSize + new Vector2(10, 10); 
                        }
                        else
                        {
                            ItemRectangle.Size = FontSize + Padding;
                        }
                        
                    }
                }
                
            }

            if (HorizontalAlignment == Alignment.None
                || HorizontalAlignment == Alignment.Left)
            {
                ItemText.Position.X = Position.X;
            }
            else
            {
                if (HorizontalAlignment == Alignment.Centre)
                {
                    // Try to approximate centering.
                    ItemText.Position = new Vector2((ItemRectangle.Position.X) - (ItemRectangle.Position.X / 2) - ((ItemRectangle.Size.X / Content.Length) ), ItemRectangle.Position.Y);
                }
                else if (HorizontalAlignment == Alignment.Right) // ew elseif
                {
                    ItemText.Position = new Vector2(ItemRectangle.Position.X + ItemRectangle.Size.X, ItemRectangle.Position.Y);
                }

            }

            if (VerticalAlignment == Alignment.None
                || VerticalAlignment == Alignment.Left
                || VerticalAlignment == Alignment.Top)
            {
                ItemText.Position.Y = Position.Y;
            }
            else
            {
                if (VerticalAlignment == Alignment.Centre)
                {
                    ItemText.Position.Y = ItemRectangle.Position.Y - (ItemRectangle.Position.Y / 2) - (ItemRectangle.Size.Y / Content.Length); 
                }
                else if (VerticalAlignment == Alignment.Right
                    || VerticalAlignment == Alignment.Bottom) // ew elseif
                {
                    ItemText.Position.Y = ItemRectangle.Position.Y + ItemRectangle.Size.Y; 
                }
            }
            
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
