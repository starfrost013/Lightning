#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ColorRef = System.UInt32;
using LParam = System.IntPtr; 

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// ChooseColor
    /// 
    /// June 20, 2021
    /// 
    /// Win32 choose colour class
    /// </summary>
    public class ChooseColor
    {
        /// <summary>
        /// INTERNAL: size of structure
        /// </summary>
        public uint Size;

        /// <summary>
        /// HWND of an owner window for the colour dialog - NULL if the colour dialog has no window
        /// </summary>
        public IntPtr HwndOwner;

        /// <summary>
        /// Dependent on Flags: If <see cref="Flags"/>.CC_ENABLETEMPLATEHANDLE  is set in the Flags member, hInstance is a handle to a memory object containing a dialog box template. If the CC_ENABLETEMPLATE flag is set, 
        /// hInstance is a handle to a module that contains a dialog box template named by the lpTemplateName member. 
        /// If neither <see cref="Flags"/>.CC_ENABLETEMPLATEHANDLE nor <see cref="Flags"/>.CC_ENABLETEMPLATE is set, this member is ignored.
        /// </summary>
        public IntPtr HInstance;

        /// <summary>
        /// A U32 format colour that is returned when the dialog box exits
        /// 
        /// If see cref="Flags"/>.CC_RGBINIT is set, this colour will also be used as the default colour.
        /// </summary>
        public ColorRef RGBResult;

        /// <summary>
        /// An array of U32 format colours (MUST BE 16 IN LENGTH) that are used for setting custom colours.
        /// </summary>
        public ColorRef[] LPCustomColors;

        /// <summary>
        /// Flags that can be used to modify the behaviour of the dialog - see <see cref="ChooseColorFlags"/>.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public ChooseColorFlags Flags;

        /// <summary>
        /// Application-defined data that the system passes to the delegate identified by the <see cref="LPCCHookCallback"/> delegate. When the system sends the WM_INITDIALOG message to the delegate / hook procedure, 
        /// the message's lParam parameter is a pointer to the CHOOSECOLOR structure specified when the dialog was created. 
        /// The hook procedure can use this pointer to get the <see cref="LCustomData"/> value.
        /// </summary>
        public LParam LCustomData;

        /// <summary>
        /// Delegate used for callback hooks to override the default window message handling of the Colour dialog.
        /// </summary>
        public ChooseColorHookCallback LPCCHookCallback;

        /// <summary>
        /// The name of the dialog box template resource in the module identified by the <see cref="HInstance"/>member. This template is substituted for the standard dialog box template. 
        /// For numbered dialog box resources, lpTemplateName can be a value returned by the MAKEINTRESOURCE macro. 
        /// This member is ignored unless <see cref="Flags"/>.CC_ENABLETEMPLATE is set.
        /// </summary>
        public string LPTemplateName;

        /// <summary>
        /// Reserved (internal)
        /// </summary>
        public HEditMenu LPEditInfo;

    }
}
#endif