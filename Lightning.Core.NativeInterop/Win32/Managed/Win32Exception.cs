#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// Win32Exception
    /// 
    /// July 15, 2021
    /// 
    /// Defines a Win32-related exception for calling Win32 NativeInterop APIs.
    /// </summary>
    public class Win32Exception : Exception
    {
        /// <summary>
        /// <inheritdoc/> Prepends <c>"Win32 Exception: "</c> and the last Win32 error code that occurred.
        /// </summary>
        public override string Message => $"Win32 Exception: {Marshal.GetLastWin32Error()}: {Message}";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Win32Exception()
        {

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ExceptionMessage"><inheritdoc/></param>
        public Win32Exception(string ExceptionMessage) : base(ExceptionMessage)
        {

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ExceptionMessage"><inheritdoc/></param>
        /// <param name="InnerException"><inheritdoc/></param>
        public Win32Exception(string ExceptionMessage, Exception InnerException) : base(ExceptionMessage, InnerException)
        {

        }
    }
}
#endif