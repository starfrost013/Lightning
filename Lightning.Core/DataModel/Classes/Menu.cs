using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Menu
    /// 
    /// July 5, 2021
    /// 
    /// Defines a menu for UI.
    /// </summary>
    public class Menu : GuiElement
    {
        internal override string ClassName => "Menu";

        /// <summary>
        /// If true, this menu will be forced to the top.
        /// </summary>
        public bool ForceToTop { get; set; }

        private bool MENU_INITIALISED { get; set; }

        private Rectangle ItemRectangle { get; set; }
        
        private MenuState State { get; set; }
        public override void Render(Scene SDL_Renderer, ImageBrush Tx, IntPtr RenderTarget)
        {
            if (!MENU_INITIALISED)
            {
                Menu_Init();
            }
            else
            {
                Menu_RenderMenu(SDL_Renderer, Tx); 
            }
        }

        private void Menu_RenderMenu(Scene SDL_Renderer, ImageBrush Tx)
        {
            ItemRectangle.Render(SDL_Renderer, Tx, IntPtr.Zero);

            // if the menu is open
            if (State.Open)
            {
                // render the menu items
                for (int i = 0; i < Children.Count; i++)
                {
                    Instance Ins = Children[i];

                    Type InsT = Ins.GetType();

                    if (InsT == typeof(MenuItem))
                    {
                        // force first level menu items to render in a specific way
                        MenuItem MenuItem = (MenuItem)Ins;

                        Vector2 Pos = new Vector2(); // DO NOT ADD TO DATAMODEL!

                        Pos = Position;

                        for (int j = 0; j < i; j++)
                        {
                            Instance NewIns = Children[j];

                            MenuItem Mn = (MenuItem)NewIns;

                            Pos += Mn.Position;
                            Mn.Position = Pos;
                        }

                        MenuItem.Render(SDL_Renderer, Tx, IntPtr.Zero);
                    }

                }
            }
            else
            {
                return; 
            }

            
        }

        private void Menu_Init()
        {
            State = new MenuState();

            if (ForceToTop) ZIndex = -2147483647;

            // initialises the menu

            if (Position == null) Position = new Vector2();
            if (Size == null) Size = new Vector2();

            ItemRectangle = (Rectangle)DataModel.CreateInstance("Rectangle"); // just put it in the workspace as a workaround - don't want to force parentcanbenull on everything

            if (ItemRectangle.Position == null) ItemRectangle.Position = new Vector2();
            if (ItemRectangle.Size == null) ItemRectangle.Size = new Vector2();

            ItemRectangle.Position.X = Position.X;
            ItemRectangle.Position.Y = Position.Y;
            ItemRectangle.Size.X = Size.X;
            ItemRectangle.Size.Y = Size.Y;

            ItemRectangle.Colour = BackgroundColour;

            ItemRectangle.Fill = true; // allow changing?

            MENU_INITIALISED = true; 

        }

        public override void OnClick(object Sender, MouseEventArgs EventArgs)
        {
            if (EventArgs.RelativePosition.X > Position.X
                && EventArgs.RelativePosition.X < (Position.X + Size.X)
                && EventArgs.RelativePosition.Y > Position.Y
                && EventArgs.RelativePosition.Y < (Position.Y + Size.Y) // change this code when we get AABBs.
                )
            {
                State.Open = !State.Open; 
            }
                
        }

    }
}
