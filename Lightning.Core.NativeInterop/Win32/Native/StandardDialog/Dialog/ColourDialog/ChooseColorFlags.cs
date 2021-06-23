#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// ChooseColorFlags
    /// 
    /// June 20, 2021
    /// 
    /// Defines colour choice flags
    /// </summary>
    public enum ChooseColorFlags
    {
        /// <summary>
        /// Causes the <see cref="ChooseColor"/> dialog to use its <see cref="ChooseColor."/>
        /// </summary>
        CC_RGBINIT = 0x01,

        CC_FULLOPEN = 0x02,

        CC_PREVENTFULLOPEN = 0x04,

        CC_SHOWHELP = 0x08,

        CC_ENABLEHOOK = 0x10,

        CC_ENABLETEMPLATE = 0x20,

        CC_ENABLETEMPLATEHANDLE = 0x40,

        CC_SOLIDCOLOR = 0x80,

        CC_ANYCOLOR = 0x100


    }
}
#endif