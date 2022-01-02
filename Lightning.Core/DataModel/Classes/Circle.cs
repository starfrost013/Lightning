using NuCore.Utilities;
using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Circle
    /// 
    /// April 12, 2021 (modified December 11, 2021: Initial NR port)
    /// 
    /// Renders a circle.
    /// </summary>
    public class Circle : Primitive
    {
        internal override string ClassName => "Circle";

        private bool Circle_Initialised { get; set; }

        private Ellipse NREllipse { get; set; }

        private void Circle_Init(Scene SDL_Renderer)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            Ellipse NewEllipse = (Ellipse)MainWindow.AddObject("Ellipse");

            if (Position != null) NewEllipse.Position = new Vector2Internal(Position.X, Position.Y);

            if (Size != null)  NewEllipse.Size = new Vector2Internal(Size.X, Size.Y);
            if (Colour != null) NewEllipse.Colour = new Color4Internal(Colour.A, Colour.R, Colour.G, Colour.B);
            NewEllipse.Antialiased = !NotAntialiased; // antialias default in lightning, not in nurender
            NewEllipse.Bordered = Bordered;
            // todo: nr bordercolour
            if (BorderSize != null) NewEllipse.BorderSize = new Vector2Internal(BorderSize.X, BorderSize.Y);
            NewEllipse.Filled = Fill;
            NREllipse = NewEllipse;
            Circle_Initialised = true; 
            // todo: nr zindex
        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            Brush CBrush = GetBrush(); 

            if (!Circle_Initialised)
            {
                Circle_Init(SDL_Renderer);
            }
            else
            {
                if (CBrush != null)
                {
                    CBrush.Render(SDL_Renderer, Tx);
                }
                else
                {
                    if (Position != null)
                    {
                        if (ForceToScreen)
                        {
                            NREllipse.Position = new Vector2Internal(Position.X, Position.Y);
                        }
                        else
                        {
                            NREllipse.Position = new Vector2Internal(Position.X - MainWindow.Settings.RenderingInformation.CCameraPosition.X,
                            Position.Y - MainWindow.Settings.RenderingInformation.CCameraPosition.Y);
                        }

                    }
                    return;

                }
  
            }
        }
    }
}
