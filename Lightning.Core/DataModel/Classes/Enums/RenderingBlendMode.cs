using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{ 
    /// <summary>
    /// RenderingBlendMode
    /// 
    /// July 13, 2021
    /// 
    /// Defines the blending mode to use for rendering. Equivalent to SDL.SDL_BLENDMODE.
    /// </summary>
    public enum RenderingBlendMode
    {
        /// <summary>
        /// No blending:
        /// 
        /// dstRGBA = srcRGBA
        /// </summary>
        None = 0,

        /// <summary>
        /// Alpha blending:
        /// 
        /// dstRGB = (srcRGB * srcA) + (dstRGB * (1-srcA))
        /// dstA = srcA + (dstA * (1-srcA))
        /// 
        /// dstRGBA = new Color4(dstRGB.R, dstRGB.G, dstRGB.B, dstA.A);
        /// </summary>
        AlphaBlending = 1,

        /// <summary>
        /// Additive blending:
        /// 
        /// dstRGB = (srcRGB * srcA) + dstRGB
        /// </summary>
        AdditiveBlending = 2,

        /// <summary>
        /// Colour modulation:
        /// 
        /// dstRGB = srcRGB * dstRGB
        /// dstA = srcA;
        ///
        /// dstRGBA = new Color4(dstRGB.R, dstRGB.G, dstRGB.B, dstA.A);
        /// </summary>
        ColourModulation = 3,


        /// <summary>
        /// Colour multiplication:
        /// 
        /// dstRGB = (srcRGB * dstRGB) + (dstRGB * (1-srcA))
        /// dstA = (srcA * dstA) + (dstA * (1 - srcA))
        /// </summary>
        ColourMultiplication = 4
    }
}
