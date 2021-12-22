using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.Packaging
{
    public enum PackageFileCompressionMode
    {
        /// <summary>
        /// Store flag
        /// </summary>
        Store = 0,

        /// <summary>
        /// Files will be re-encoded into a six-bit encoding scheme.
        /// </summary>
        SixBit = 1,

        /// <summary>
        /// LZMA will be used to compress the files.
        /// </summary>
        LZMA = 2,

        /// <summary>
        /// SixBit + LZMA
        /// </summary>
        Default = 3
    }
}
