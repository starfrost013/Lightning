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
    }
}
