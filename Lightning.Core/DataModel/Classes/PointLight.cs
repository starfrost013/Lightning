using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class PointLight : Light
    {
        public override void Render(Scene SDL_Renderer, ImageBrush Tx, IntPtr RenderTarget)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            int Min = (int)-(Range / 2);
            int Max = (int)(Range / 2);

            for (int y = (int)Position.X + Min; y < (int)Position.X + Max; y++)
            {
                for (int x = (int)Position.Y + Min; x < (int)Position.Y + Max; x++)
                {
                    Color4 NColour = Colour;

                    int AbsX = (int)(x - Position.X);
                    int AbsY = (int)(y - Position.Y);

                    // get the distance and then draw it

                    int Distance = (int)Math.Abs(Math.Sqrt((AbsX * AbsX) + (AbsY * AbsY)));

                    NColour.A = (byte)(Intensity * (1 - (Distance / Range))); 
                    

                    SDL_gfx.pixelRGBA(MainWindow.Settings.RenderingInformation.RendererPtr, x, y, NColour.R, NColour.G, NColour.B, NColour.A);
                }
            }
        }
    }
}
