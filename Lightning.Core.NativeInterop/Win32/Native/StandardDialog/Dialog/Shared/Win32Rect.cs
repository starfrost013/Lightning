using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// Win32Rect
    /// 
    /// June 22, 2021 (modified January 16, 2022: Long => Int)
    /// 
    /// Defines the Win32 GDI RECT class in .NET.
    /// </summary>
    public class Win32Rect
    {
        /// <summary>
        /// X coordinate of the upper left point of the rectangle.
        /// </summary>
        int Left;

        /// <summary>
        /// X coordinate of the bottom right point of the rectangle.
        /// </summary>
        int Right;

        /// <summary>
        /// Y coordinate of the upper left point of the rectangle.
        /// </summary>
        int Top;

        /// <summary>
        /// Y coordinate of the bottom right point of the rectangle.
        /// </summary>
        int Bottom;

        public Win32Rect()
        {

        }

        public Win32Rect(int NLeft, int NRight, int NTop, int NBottom)
        {
            Left = NLeft;
            Right = NRight;
            Top = NTop;
            Bottom = NBottom;
        }

        // TODO: Convert from AABB/paired Vec2 in PhysicsService.
    }
}
