using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;  

namespace Polaris.UI
{
    public partial class ScriptEditorCore : Control
    {
        public TextBlock GetTextForDisplay()
        {
            TextBlock TB = new TextBlock();

            string TBText = Text.Concatenate();

            string[] TextArray = TBText.Split(' ');

            foreach (string TextLine in TextArray)
            {
                TB.Inlines.Add(new Run(TextLine));
            }

            // TEST
            TB.Width = 60;
            TB.Height = 60;
            
            
            AddVisualChild(TB);

            return TB;
        }
    }
}
