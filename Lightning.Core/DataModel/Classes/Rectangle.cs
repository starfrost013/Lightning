using NuCore.Utilities;
using NuRender;
using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Rectangle
    /// 
    /// April 12, 2021 (modified December 12, 2021: NR rewrite)
    /// 
    /// Defines a rectangle.
    /// </summary>
    public class Rectangle : Line
    {
        internal override string ClassName => "Rectangle";

        private NuRender.Rectangle NRRectangle { get; set; }

        private bool Rectangle_Initialised { get; set; }

        /// <summary>
        /// The radius of the border of this rectangle. If above 0, this rectangle will be rendered as a rounded rectangle. 
        /// </summary>
        public int BorderRadius { get; set; }
        private void Rectangle_Init(Scene SDL_Renderer)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();
            NRRectangle = (NuRender.Rectangle)MainWindow.AddObject("Rectangle");

            if (Size != null) NRRectangle.Size = new Vector2Internal(Size.X, Size.Y);
            if (Colour != null) NRRectangle.Colour = new Color4Internal(Colour.A, Colour.R, Colour.G, Colour.B);
            NRRectangle.Antialiased = !NotAntialiased; // antialias default in lightning, not in nurender
            NRRectangle.Bordered = Bordered;
            // todo: nr bordercolour
            if (BorderSize != null) NRRectangle.BorderSize = new Vector2Internal(BorderSize.X, BorderSize.Y);
            NRRectangle.BorderRadius = BorderRadius;
            NRRectangle.Filled = Fill;

            Rectangle_Initialised = true; 
        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            Brush Brush = GetBrush();

            Window MainWindow = SDL_Renderer.GetMainWindow();

            if (Brush != null)
            {
                Brush.Render(SDL_Renderer, Tx); 
            }
            else
            {
                if (Brush != null)
                {
                    Brush.Render(SDL_Renderer, Tx);
                }
                else
                {
                    if (Position != null)
                    {
                        if (ForceToScreen)
                        {
                            NRRectangle.Position = new Vector2Internal(Position.X, Position.Y);
                        }
                        else
                        {
                            NRRectangle.Position = new Vector2Internal(Position.X - MainWindow.Settings.RenderingInformation.CCameraPosition.X,
                            Position.Y - MainWindow.Settings.RenderingInformation.CCameraPosition.Y);
                        }

                    }
                    return;

                }
            }
            
        }

     
    }
}
