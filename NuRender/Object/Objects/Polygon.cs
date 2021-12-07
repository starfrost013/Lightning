using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Object
{ 
    /// <summary>
    /// Polygon
    /// 
    /// November 21, 2021
    /// 
    /// NuRender polygon object
    /// </summary>
    public class Polygon : Primitive
    {
        /// <summary>
        /// A list of <see cref="Vector2"/>s containing the points for this polygon. 
        /// </summary>
        public List<Vector2> Points { get; set; }

        public Polygon()
        {
            Points = new List<Vector2>();
        }

        public override void Start(WindowRenderingInformation RenderInfo)
        {
            base.Start(RenderInfo);
        }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
            List<int> X = new List<int>();
            List<int> Y = new List<int>();

            foreach (Vector2 Point in Points)
            {
                X.Add((int)Point.X);
                Y.Add((int)Point.Y);
            }

            int[] AX = X.ToArray();
            int[] AY = Y.ToArray(); 

            if (Filled)
            {
                ErrorManager.ThrowError(ClassName, "NRUnimplementedFeatureException", "Filled Polygons presently unimplemented");
            }
            else
            {
                if (Antialiased)
                {
                    SDL_gfx.aaPolygonRGBA(RenderInfo.RendererPtr, AX, AY, Colour.R, Colour.G, Colour.B, Colour.A);
                }
                else
                {
                    SDL_gfx.polygonRGBA(RenderInfo.RendererPtr, AX, AY, Colour.R, Colour.G, Colour.B, Colour.A);
                }
            }
        }
    }
}
