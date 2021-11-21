using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core.SDL2
{
    /// <summary>
    /// P/Invoke definitions for SDL2_gfx 
    /// </summary>
    public static partial class SDL_gfx
    {
        #region SDL2_gfxPrimitives.c

        /// <summary>
        /// Draw pixel with blending enabled if a<255.
        /// </summary>
        /// <param name="Renderer"> The renderer to draw on.</param>
        /// <param name="X">The horizontal coordinate of the pixel.</param>
        /// <param name="Y">The vertical coordinate of the pixel.</param>
        /// <param name="R">The red component of the pixel's colour.</param>
        /// <param name="G">The green component of the pixel's colour.</param>
        /// <param name="B">The blue component of the pixel's colour.</param>
        /// <param name="A">The alpha component of the pixel's colour.</param>
        /// <returns></returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pixelRGBA(IntPtr Renderer, int X, int Y, byte R, byte G, byte B, byte A);

        #region TODO - DOCUMENTATION

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int hlineRGBA(IntPtr Renderer, int X1, int X2, int Y1, int Y2, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int vlineRGBA(IntPtr Renderer, int X1, int X2, int Y1, int Y2, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int bezierRGBA(IntPtr Renderer,
        [In] int[] VX,
        [In] int[] VY,
        int N, int S, byte R, byte G, byte B, byte A);


        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int thicklineRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, byte Width, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rectangleRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int roundedRectangleRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, int Radius, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int boxRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int roundedBoxRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, int Radius, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lineRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int aalineRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int arcRGBA(IntPtr Renderer, int X, int Y, int Radius, int Start, int End, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ellipseRGBA(IntPtr Renderer, int X, int Y, int RadX, int RadY, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int aaellipseRGBA(IntPtr Renderer, int X, int Y, int RadX, int RadY, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int filledEllipseRGBA(IntPtr Renderer, int X, int Y, int RX, int RY, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pieRGBA(IntPtr Renderer, int X, int Y, int Radius, int Start, int End, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int filledPieRGBA(IntPtr Renderer, int X, int Y, int Radius, int Start, int End, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int trigonRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, int X3, int Y3, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int aatrigonRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, int X3, int Y3, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int filledTrigonRGBA(IntPtr Renderer, int X1, int Y1, int X2, int Y2, int X3, int Y3, byte R, byte G, byte B, byte A);

        //todo: aafilledTrigonRGBA

        //todo: aafilledTrigonRGBA


        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int polygonRGBA(IntPtr Renderer,
        ref int[] VX,
        ref int[] VY,
        int N, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int aapolygonRGBA(IntPtr Renderer,
        ref int[] VX,
        ref int[] VY,
        int N, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int filledPolygonRGBA(IntPtr Renderer,
        [In] int[] VX,
        [In] int[] VY,
        int N, byte R, byte G, byte B, byte A);


        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int texturedPolygon(IntPtr Renderer,
        [In] int[] VX,
        [In] int[] VY, int N, IntPtr Surface, int TextureDX, int TextureDY, byte R, byte G, byte B, byte A);

        #region GFX Primitives Core 

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gfxPrimitivesSetFont(IntPtr FontArray, uint CW, uint CH);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gfxPrimitivesSetFontRotation(uint Rotation);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int characterRGBA(IntPtr Renderer, byte X, byte Y, char C, byte R, byte G, byte B, byte A);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int stringRGBA(IntPtr Renderer, byte X, byte Y, string C, byte R, byte G, byte B, byte A);


        #endregion

        #endregion
        #endregion
    }
}
