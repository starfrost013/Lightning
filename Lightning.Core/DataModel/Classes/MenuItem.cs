using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// MenuItem
    /// 
    /// July 5, 2021
    /// 
    /// Defines a menu item for UI.
    /// </summary>
    public class MenuItem : GuiElement
    {

        internal override string ClassName => "MenuItem";

        /// <summary>
        /// The Content of this MenuItem
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The font family of this MenuItem
        /// </summary>
        public string FontFamily { get; set; }

        private bool MENUITEM_INITIALISED { get; set; } 

        private Rectangle ItemRectangle { get; set; }
        private Text ItemText { get; set;  }

        /// <summary>
        /// See <see cref="Text.Bold"/>.
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// See <see cref="Text.Italic"/>.
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// See <see cref="Text.Underline"/>.
        /// </summary>
        public bool Underline { get; set; }

        /// <summary>
        /// See <see cref="Text.Strikethrough"/>.
        /// </summary>
        public bool Strikethrough { get; set; }

        public MenuState State { get; set; }
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            // rendered by menu
            if (!MENUITEM_INITIALISED)
            {
                Init();
            }
            else
            {
                RenderChildren(SDL_Renderer, Tx);
            }
            
        }

        private void Init()
        {
            if (Size == null)
            {
                Size = new Vector2();
            }

            // Set default values if the user has not specified default values.
            if (Size.X == 0) Size.X = 50;
            if (Size.Y == 0) Size.Y = 20;


            if (Position == null) Position = new Vector2(0, 0);

            if (BackgroundColour == null)
            {
                if (Colour == null) // BG and FG colour are not set
                {
                    BackgroundColour = new Color4 { A = 255, R = 255, G = 255, B = 255 };
                    Colour = new Color4 { A = 0, R = 0, G = 0, B = 0 };
                }
                else // BG colour is set, but FG colour isn't - set BG to the opposite of FG with A=255
                {
                    byte DefaultBackgroundColourR = (byte)(255 - Colour.R);
                    byte DefaultBackgroundColourG = (byte)(255 - Colour.G);
                    byte DefaultBackgroundColourB = (byte)(255 - Colour.B);

                    BackgroundColour = new Color4 { A = 255, R = DefaultBackgroundColourR, G = DefaultBackgroundColourG, B = DefaultBackgroundColourB };
                }
            }

            if (Colour == null) Colour = new Color4 { A = 255, R = 255, G = 255, B = 255 }; // if backgroundcolour is set but not colour

            // size disregarded for text


            ItemRectangle = new Rectangle(); // todo: figure out if renderable is a hack

            if (ItemRectangle.Position == null) ItemRectangle.Position = new Vector2();
            if (ItemRectangle.Size == null) ItemRectangle.Size = new Vector2();

            
            ItemRectangle.Size = Size;
            ItemRectangle.Position = Position;

            ItemRectangle.Colour = BackgroundColour;

            ItemRectangle.Fill = true; // allow changing?


            ItemText = (Text)DataModel.CreateInstance("Text", this);

            if (ItemText.Position == null) Position = new Vector2();

            ItemText.Content = Content;
            ItemText.Colour = Colour;
            ItemText.FontFamily = FontFamily;
            ItemText.Position = Position;

            ItemText.Bold = Bold;
            ItemText.Italic = Italic;
            ItemText.Underline = Underline;
            ItemText.Strikethrough = Strikethrough;

            MENUITEM_INITIALISED = true;
        }


        private void RenderChildren(Renderer SDL_Renderer, Texture Tx)
        { 
            // render second and lower level hierarchy
            foreach (Instance Ins in Children)
            {
                Type InsType = Ins.GetType();

                if (InsType == typeof(MenuItem))
                {
                    MenuItem MenuItem = (MenuItem)Ins;

                    // menu options are automatically rendered?

                    ItemRectangle.Render(SDL_Renderer, Tx); 
                    ItemText.Render(SDL_Renderer, Tx);
                    
                    if (MenuItem.Children.Count > 0) MenuItem.Render(SDL_Renderer, Tx);


                }
            }
        }
    }
}
