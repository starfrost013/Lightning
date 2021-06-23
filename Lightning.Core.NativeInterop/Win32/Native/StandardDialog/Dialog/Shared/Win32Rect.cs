using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// Win32Rect
    /// 
    /// June 22, 2021 
    /// 
    /// Defines the Win32 GDI RECT class in .NET.
    /// </summary>
    public class Win32Rect
    {
        /// <summary>
        /// X coordinate of the upper left point of the rectangle.
        /// </summary>
        long Left;

        /// <summary>
        /// X coordinate of the bottom right point of the rectangle.
        /// </summary>
        long Right;

        /// <summary>
        /// Y coordinate of the upper left point of the rectangle.
        /// </summary>
        long Top;

        /// <summary>
        /// Y coordinate of the bottom right point of the rectangle.
        /// </summary>
        long Bottom;

        // TODO: Convert from AABB/paired Vec2 in PhysicsService...?
    }
}
