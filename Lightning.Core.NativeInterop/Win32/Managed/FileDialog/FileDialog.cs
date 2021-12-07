#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    public class FileDialog
    {
        /// <summary>
        /// The filename. Automatically filled in by the API.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Filter to use
        /// </summary>
        public OpenFileDialogFilter Filter { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determine if a
        /// </summary>
        public bool ReadOnly { get; set; }
        /// <summary>
        /// Gets or sets a boolean that determines if the user will be prompted on overwrite. Alternative to <see cref="Flags"/>.  Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool UseOverwritePrompt { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determinesif multi-selection functionality will be available. Alternative to <see cref="Flags"/>. Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool Multiselect { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determines if the file to be saved must exist. Alternative to <see cref="Flags"/>. Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool FileMustExist { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determines if the path to be saved must exist. Alternative to <see cref="Flags"/>. Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool PathMustExist { get; set; }

        /// <summary>
        /// Gets or sets the Window title of the Open File Dialog. 
        /// </summary>
        public string WindowTitle { get; set; }

        /// <summary>
        /// The Win32 API flags for this dialog. See <see cref="OpenFileDialogFlags"/>. Used for advanced configuration.
        /// </summary>
        public OpenFileDialogFlags Flags { get; set; }

        /// <summary>
        /// The Win32 Extended API flags for this dialog. See <see cref="OpenFileDialogFlags"/>. Used for advanced configuration.
        /// </summary>
        public OpenFileDialogFlagsEx FlagsEx { get; set; }

        /// <summary>
        /// Defines the starting filter index for the filter. Default = 0
        /// </summary>
        public int StartFilterIndex { get; set; }

        /// <summary>
        /// The initial directory for the 
        /// </summary>
        public string InitialDirectory { get; set; }
    }
}
#endif