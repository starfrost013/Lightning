#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// Lightning Native Interop (Win32)
    /// 
    /// Unmanaged DEVMODE structure for managed code
    /// 
    /// 2021-03-05  Move from Emerald to Lightning (v1.0.10)
    /// </summary>
    public class DEVMODE
    {
        public byte dmDeviceName { get; set; }
        public ushort dmSpecVersion { get; set; }
        public ushort dmDriverVersion { get; set; }
        public ushort dmSize { get; set; }
        public ushort dmDriverExtra { get; set; }
        public DEVMODE_PRINTINFORMATION dmPrintingInformation { get; set; }
        public DEVMODE_POINT dmPoint { get; set; }
        public short dmColor { get; set; }
        public short dmDuplex { get; set; }
        public short dmYResolution { get; set; }
        public short dmTTOption { get; set; }
        public short dmCollate { get; set; }
        public byte dmFormName { get; set; }
        public ushort dmLogPixels { get; set; }
        public uint dmBitsPerPel { get; set; }
        public uint dmPelsWidth { get; set; }
        public uint dmPelsHeight { get; set; }
        public uint dmDisplayFlags { get; set; }
        public uint dmNup { get; set; }
        public uint dmDisplayFrequency { get; set; }
        public uint dmICMMethod { get; set; }
        public uint dmICMIntent { get; set; }
        public uint dmMediaType { get; set; }
        public uint dmDitherType { get; set; }
        public uint dmReserved1 { get; set; }
        public uint dmReserved2 { get; set; }
        public uint dmPanningWidth { get; set; }
        public uint dmPanningHeight { get; set; }
        public DEVMODE()
        {
            dmPrintingInformation = new DEVMODE_PRINTINFORMATION();
            dmPoint = new DEVMODE_POINT();

        }
    }

    public struct DEVMODE_PRINTINFORMATION
    {
        public short dmOrientation { get; set; }
        public short dmPaperSize { get; set; }
        public short dmPaperLength { get; set; }
        public short dmPaperWidth { get; set; }
        public short dmScale { get; set; }
        public short dmCopies { get; set; }
        public short dmDefaultSource { get; set; }
        public short dmPrintQuality { get; set; }
    }

    /// <summary>
    /// Oh noes
    /// </summary>
    public struct DEVMODE_POINT
    {
        public double X { get; set; }

        /// <summary>
        /// 2021-03-05 - LIGHTNING MOVE ONLY
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// 2021-03-05 - LIGHTNING MOVE ONLY
        /// </summary>
    }
}
#ENDIF
