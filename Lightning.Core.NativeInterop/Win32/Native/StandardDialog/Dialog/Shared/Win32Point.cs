using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// Win32Point
    /// 
    /// June 22, 2021
    /// 
    /// Defines a Win32 point class in .NET
    /// </summary>
    public class Win32Point
    {
        public int X;
        public int Y;

        public Win32Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        // TODO: Lightning V2 to Win32Point conversion (NOT HERE - IN LIGHTNING.CORE)
        // TODO: WPF Point to Win32 Point conversion (NOT HERE - IN POLARIS)
    }
}
