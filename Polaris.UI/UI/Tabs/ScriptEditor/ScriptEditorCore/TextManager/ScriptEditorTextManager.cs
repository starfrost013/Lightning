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

            string[] TextArray = TBText.Split('\n');

            

            foreach (string TextLine in TextArray)
            {

                Paragraph Paragraph = new Paragraph();
                TB.Inlines.Add(new Run(TextLine));
                
            }

            return TB;
        }
    }
}
