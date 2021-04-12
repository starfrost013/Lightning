using Lightning.Core.SDL2;
using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Circle
    /// 
    /// April 12, 2021
    /// 
    /// Renders a circle.
    /// </summary>
    public class Circle : Primitive
    {
        /// <summary>
        /// The diameter of the circle.
        /// </summary>
        private int Diameter { get; set;  }

        /// <summary>
        /// backing field for radius
        /// </summary>
        private int _radius { get; set; }

        /// <summary>
        /// The radius of the circle. 
        /// </summary>
        public int Radius { get
            {
                return _radius;
            }
            set
            {
                Diameter = value * 2; // r = 2d
                _radius = value; 
            }

        }

        public new void Render(IntPtr SDLRenderer, Texture Tx)
        {
            SDL.SDL_SetRenderDrawBlendMode(SDLRenderer, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            // This isn't particularly efficient.
            // There's better ways to do this but this is the simplest for now. 
            for (int i = 0; i < 360; i++)
            {
                double X = Radius * Math.Cos(MathUtil.DegreesToRadians(i));
                double Y = Radius * Math.Sin(MathUtil.DegreesToRadians(i));

                // draw a point. 
                SDL.SDL_RenderDrawPointF(SDLRenderer, (float)X, (float)Y);
                
            }
        }


    }
}
