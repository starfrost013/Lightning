using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Tools.ErrorConvert
{
    /// <summary>
    /// Strings
    /// 
    /// July 2, 2021
    /// 
    /// Defines static strings for the Lightning ErrorConvert program/
    /// </summary>
    public static class Strings
    {
        public const string STRING_HELPMSG = "Lightning.Tools.ErrorConvert.exe <error xml file> <error registration file> [options...]\n" +
            "Converts an old-style error registration file (Errors.xml) to a new-style one as of July 2, 2021 (ErrorCollection.cs)\n\n" +
            "Options:\n" +
            "-lightning-namespace: Lightning Namespace to use";

        public const string STRING_ERROR_NOT_ENOUGH_ARGUMENTS = "Not enough arguments supplied - must supply at least a old and new filename!";

        public const string STRING_ERROR_PATH_TOO_LONG = "Old or new file path too long (longer than MAX_CHARS)";

        public const string STRING_ERROR_GENERIC_ERROR_WRITING_NEW_FILE = "Error writing new file";

        public const string STRING_ERROR_OLD_FILE_NOT_FOUND = "The old file must exist to be converted!";

        public const string STRING_ERROR_ERROR_VALIDATING_OLD_XML = "An error occurred when validating the old XML against the old XML schema";

        public const string STRING_ERROR_ERROR_SERIALISING_OLD_XML = "An error occurred when serialising the old XML for conversion";

        public const string STRING_ERROR_INDEX_OUT_OF_RANGE = "Index out of range exception occurred (are you skipping IDs?)";
    }
}
