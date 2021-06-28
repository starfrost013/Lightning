using Lightning.Core;
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
    public partial class ScriptEditorCore : Control
    {
        public ScriptEditorHighlight Highlight { get; set; }
        public ScriptEditorSettings Settings { get; set; }
        public ScriptEditorState State { get; set; }
        public TextChunkCollection Text { get; set; }
        
        private TextChunk CurrentTextChunk { get; set; }
        public ScriptEditorCore()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptEditorCore), new FrameworkPropertyMetadata(typeof(ScriptEditorCore)));
            Init(); 
        }


        private void Init()
        {
            base.MouseDown += OnMouseDown;
            base.KeyDown += OnKeyDown;

            State = new ScriptEditorState();
            Highlight = new ScriptEditorHighlight();
            Text = new TextChunkCollection();
            Settings = new ScriptEditorSettings();

            // Set up the first text chunk. 
            CurrentTextChunk = new TextChunk();
            Text.Add(CurrentTextChunk);

        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            State.IsSelected = true; 

            // Sets the highlightposition and snaps it to the nearest character.
            string ConcatenatedString = Text.Concatenate();

            int NewlineCountY = ConcatenatedString.CountNewlines();

            if (NewlineCountY == 0) 
            {
                return;
            }
            else
            {

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

                    double YPos = ConvertedSingleCharacterPixelCount * LineID;

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

                    //TODO: SET COLOUR
                    // all positions relative to this

                    this.AddVisualChild(Indicator);

                }

            }

        }

        public void OnMouseUp(object sender, MouseEventArgs e) => State.IsSelected = false;

        public void OnKeyDown(object sender, KeyEventArgs e)
        {

            if (State.IsSelected)
            {
                switch (e.Key)
                {
                    case Key.LWin:
                    case Key.RWin:
                    case Key.Apps:
                    case Key.Sleep:
                        return; // exempt keys
                    case Key.Back:
                        int TextLength = CurrentTextChunk.Length;

                        if (TextLength != 0)
                        {
                            CurrentTextChunk.Text = CurrentTextChunk.Text.Remove(TextLength - 1);
                        }
                        else
                        {
                            if (Text.Count > 1)
                            {
                                Text.Chunks.Remove(CurrentTextChunk);

                                CurrentTextChunk = Text[Text.Count - 2];
                            }
                            else
                            {
                                return; 
                            }

                        }

                        return;
                    case Key.Space:
                        TextChunk TC = new TextChunk();

                        Text.Add(TC); 

                        CurrentTextChunk = TC;

                        return;
                    default:
                        CurrentTextChunk.Text = CurrentTextChunk.Text.Append<string>(e.Key.ToString());

                        TextBlock TB = GetTextForDisplay();

                        foreach (Inline IL in TB.Inlines)
                        {
                            // ONLY runs are used
                            Run RL = (Run)IL;

                            string ComponentName = "Polaris Script Editor";

                            Logging.Log($"{RL.Text} ", ComponentName);
                             
                        }
                        
                        return;
                }

            }
            else
            {
                return; 
            }


        }
    }
}
