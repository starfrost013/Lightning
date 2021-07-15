using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// OpenFileDialogFilterItem
    /// 
    /// July 15, 2021
    /// 
    /// Defines a filter item for <see cref="OpenFileDialogFilter"/>.
    /// </summary>
    public class OpenFileDialogFilterItem
    {

        /// <summary>
        /// The description of this filter item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The extension of this filter item. Required - a <see cref="ArgumentNullException"> will be thrown if ToString(); is called when this property is set to null.</see> THE DOT MUST NOT BE ADDED!
        /// </summary>
        public string Extension { get; set; }

        public override string ToString()
        {
            StringBuilder SB = new StringBuilder();

            if (Extension == null)
            {
                throw new ArgumentNullException("Extension");
            }
            else
            {

                // use a default descritpion - do not require one
                if (Description == null)
                {
                    SB.Append($"{Extension} files");
                }
                else
                {
                    SB.Append($"{Description}");
                }

                SB.Append("|"); // pipe is used to separate items

                SB.Append($"*.{Extension}");

                return SB.ToString();

            }
        }
    }
}
