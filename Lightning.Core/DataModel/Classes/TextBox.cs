using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TextBox
    /// 
    /// July 19, 2021 (modified August 1, 2021)
    /// 
    /// Defines a text box.
    /// </summary>
    public class TextBox : Text
    {
        public Rectangle ItemRectangle { get; set; }

        public bool Fill { get; set; }
        private bool TEXTBOX_INITIALISED { get; set; }
        private bool TEXTBOX_INITIALISATION_FAILED { get; set; }
        public Vector2 Padding { get; set; }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (TEXTBOX_INITIALISATION_FAILED) return;

            if (!TEXTBOX_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }

        }

        internal override void Init() // called by button
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
            ItemRectangle.BorderColour = BorderColour;
            ItemRectangle.Colour = BorderColour;
            ItemRectangle.BackgroundColour = BackgroundColour;
            ItemRectangle.Fill = Fill;

            Vector2 FontSize = null;

            if (ItemRectangle.Size == null)
            {
                FindFontResult FFR = FindFont();

                if (!FFR.Successful
                    || FFR.Font == null)
                {
                    TEXTBOX_INITIALISATION_FAILED = true;
                    return;
                }
                else
                {
                    Font FontOfText = FFR.Font;
                    FontSize = GetApproximateFontSize(FontOfText);

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

            if (HorizontalAlignment == Alignment.Centre)
            {
                if (FontSize != null)
                {
                    Position = new Vector2((ItemRectangle.Position.X) + (ItemRectangle.Size.X / 2) - (FontSize.X / 2), ItemRectangle.Position.Y);
                }
                else
                {
                    // Approximate centering
                    Position = new Vector2((ItemRectangle.Position.X) + (ItemRectangle.Size.X / 2), ItemRectangle.Position.Y);
                }

            }
            else if (HorizontalAlignment == Alignment.Right) // ew elseif
            {
                Position = new Vector2(ItemRectangle.Position.X + ItemRectangle.Size.X, ItemRectangle.Position.Y);
            }

            if (VerticalAlignment == Alignment.Centre)
            {
                if (FontSize != null)
                {
                    Position.Y = ItemRectangle.Position.Y + (ItemRectangle.Size.Y / 2) - (FontSize.Y / 2);
                }
                else
                {
                    Position.Y = ItemRectangle.Position.Y + (ItemRectangle.Size.Y / 2);
                }
            }
            else if (VerticalAlignment == Alignment.Right
                || VerticalAlignment == Alignment.Bottom) // ew elseif
            {
                Position.Y = ItemRectangle.Position.Y + ItemRectangle.Size.Y;
            }

            TEXTBOX_INITIALISED = true;
        }

        internal void DoRender(Renderer SDL_Renderer, ImageBrush Tx)
        {
            ItemRectangle.Render(SDL_Renderer, Tx);

            base.Render(SDL_Renderer, Tx);
        }
    }
}
