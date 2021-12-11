using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Pixel
    /// 
    /// December 11, 2021
    /// 
    /// Simple pixel primitive class
    /// </summary>
    public class Pixel : NRObject
    {
        public override string ClassName => "Pixel";

        public override void Start(WindowRenderingInformation RenderingInformation)
        {
            return; // do nothing
        }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
            SDL_gfx.pixelRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)Position.Y, Colour.R, Colour.G, Colour.B, Colour.A);
        }
    }
}
