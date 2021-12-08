using NuRender.SDL2;
using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Line
    /// 
    /// A NuRender line.
    /// 
    /// Repeatedly draws SDL lines. Probably shouldn't do that lol.
    /// </summary>
    public class Line : Primitive // for now
    {
        public override string ClassName => "Line";

        /// <summary>
        /// Defines the start of this line - see <see cref="LineStart"/>.
        /// </summary>
        public Vector2Internal LineStart { get; set; }

        /// <summary>
        /// Defines the end of this line - see <see cref="LineStart"/>.
        /// </summary>
        public Vector2Internal LineEnd { get; set; }

        /// <summary>
        /// Backing field for <see cref="Thickness"/>
        /// </summary>
        private int _thickness { get; set; }

        /// <summary>
        /// The thickness of each line.
        /// </summary>
        public int Thickness
        {
            get
            {
                if (_thickness < 1) 
                {
                    return 1;
                }
                else
                {
                    return _thickness; 
                }
            }
            set
            {
                if (value < 1)
                {
                    _thickness = 1;
                }
                else
                {
                    _thickness = value; 
                }
            }

        }
        public override void Start(WindowRenderingInformation RenderingInformation)
        {
            
        }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
            if (LineStart == null
            || LineEnd == null)
            {
                return; // TEMP
            }
            else
            {

                // force a new object to be created (HACK)
                Vector2Internal CurLineStart = new Vector2Internal(LineStart.X, LineStart.Y);
                Vector2Internal CurLineEnd = new Vector2Internal(LineEnd.X, LineEnd.Y);

                for (int i = 0; i < Thickness; i++)
                {
                    // fun test
                    CurLineStart.X++;
                    CurLineEnd.X++;

                    if (Antialiased)
                    {
                        SDL_gfx.aalineRGBA(RenderInfo.RendererPtr, (int)CurLineStart.X, (int)CurLineStart.Y, (int)CurLineEnd.X, (int)CurLineEnd.Y, Colour.R, Colour.G, Colour.B, Colour.A);
                    }
                    else
                    {
                        SDL_gfx.lineRGBA(RenderInfo.RendererPtr, (int)CurLineStart.X, (int)CurLineStart.Y, (int)CurLineEnd.X, (int)CurLineEnd.Y, Colour.R, Colour.G, Colour.B, Colour.A);
                    }
                        

                }

            }
        }
    }
}
