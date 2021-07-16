#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// OpenFileDialogFilter
    /// 
    /// July 15, 2021
    /// 
    /// Implements an easier to use file filter string API for Win32 common dialogs.
    /// </summary>
    public class OpenFileDialogFilter
    {
        public List<OpenFileDialogFilterItem> Items { get; set; }

        public OpenFileDialogFilter()
        {
            Items = new List<OpenFileDialogFilterItem>();
        }

        public void AddItem(string Extension, string Description = null)
        {
            OpenFileDialogFilterItem OFDFI = new OpenFileDialogFilterItem();

            OFDFI.Extension = Extension;
            OFDFI.Description = Description;

            Items.Add(OFDFI);
        }

        public override string ToString()
        {
            StringBuilder SB = new StringBuilder();
            
            for (int i = 0; i < Items.Count; i++) // for loop for checking last id
            {
                OpenFileDialogFilterItem FilterItem = Items[i];

                SB.Append(FilterItem.ToString()); // add each filter
                if (i < (Items.Count - 1))
                {
                    // terminated by two 0x00s
                    SB.Append("\0");
                    SB.Append("\0");
                }
            }

            return SB.ToString(); 
        }
    }
}
#endif