using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Polaris.UI
{
    /// <summary>
    /// ScriptEditorCore
    /// 
    /// June 13, 2021
    /// 
    /// Implements a Lua script editor for Polaris. 
    /// </summary>
    public class ScriptEditorCore : Control
    {
        public ScriptEditorHighlight Highlight { get; set; }
        public ScriptEditorSettings Settings { get; set; }
        public TextChunkCollection Text { get; set; }
        
        public ScriptEditorCore()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptEditorCore), new FrameworkPropertyMetadata(typeof(ScriptEditorCore)));

        }


        private void Init()
        {
            base.MouseDown += OnMouseDown;
            
            Highlight = new ScriptEditorHighlight();
            Text = new TextChunkCollection();
            Settings = new ScriptEditorSettings(); 
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Sets the highlightposition and snaps it to the nearest character.
            string ConcatenatedString = Text.Concatenate();

            int NewlineCountY = ConcatenatedString.CountNewlines();

            double ConvertedSingleCharacterPixelCount = ScreenUtil.WPFToPixels(Settings.FontSize);
            
            double MaxPosY = ConvertedSingleCharacterPixelCount * NewlineCountY;

            
            // PROBABLY easier to indirectly determine the line ID

            Point MousePos = e.GetPosition(this);

            int LineID = Convert.ToInt32(NewlineCountY * (MaxPosY / MousePos.Y));

            string TheLine = ConcatenatedString.GetLineWithId(LineID);


            if (TheLine == null)
            {
                return; // do something else?
            }
            else
            {

                double YPos  = ConvertedSingleCharacterPixelCount * LineID;

                double LinePixelLength = ConvertedSingleCharacterPixelCount * TheLine.Length;

                // no percentage required, this is easier
                double XPos = LinePixelLength * (LinePixelLength / ConvertedSingleCharacterPixelCount);

                Point LinePos = new Point(XPos, YPos);

                Line Indicator = new Line();
                Indicator.X1 = XPos;
                Indicator.X2 = XPos;
                Indicator.Y1 = YPos;
                Indicator.Y2 = YPos;
                Indicator.StrokeThickness = 1;
                Indicator.Stroke
                // all positions relative to this

                this.AddVisualChild(Indicator);

            }

            
        }
    }
}
