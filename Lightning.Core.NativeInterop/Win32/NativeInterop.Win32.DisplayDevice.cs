#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// Win32 display device.
    /// 
    /// March 5, 2021   Move from Emerald to Lightning.
    /// </summary>
    public class DISPLAY_DEVICE
    {
        /// <summary>
        /// The size of this structure.
        /// </summary>
        public int cb { get; set; }

        /// <summary>
        /// The name of the display device.
        /// </summary>
        public char[] DeviceName { get; set; }

        /// <summary>
        /// The description of the display device.
        /// </summary>
        public char[] DeviceString { get; set; }

        /// <summary>
        /// Display device flags. [Todo - make this an enum]
        /// </summary>
        public int DeviceFlags { get; set; }

        /// <summary>
        /// "Not used". ID of the device, one supposes
        /// </summary>
        public char[] DeviceID { get; set; }

        /// <summary>
        /// "Reserved". Thank you MSDN.
        /// </summary>
        public char[] DeviceKey { get; set; }

        public DISPLAY_DEVICE()
        {
            DeviceKey = new char[128];
            DeviceID = new char[128];
            DeviceName = new char[32];
            DeviceString = new char[128];
            cb = 424; // remove if failing

        }
    }
}
#endif
