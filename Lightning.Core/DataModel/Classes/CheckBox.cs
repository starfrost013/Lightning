using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CheckBox
    /// 
    /// August 7, 2021 (modified December 11, 2021: Initial NR port)
    /// 
    /// Defines a check box. 
    /// </summary>
    public class CheckBox : TextBox
    {
        internal override string ClassName => "CheckBox";

        /// <summary>
        /// Determines if this CheckBox is checked.
        /// </summary>
        public bool Checked { get; set; }


        private bool CHECKBOX_INITIALISED { get; set; }

        private Line L1 { get; set; }
        private Line L2 { get; set; }

        /// <summary>
        /// CheckedEvent: Defines the method to be called when this checkbox is checked - see <see cref="CheckedEvent"/>
        /// </summary>
        /// 
        public CheckedEvent CheckedEventHandler { get; set; }
        internal void Init()
        {
            if (Size == null) Size = new Vector2(50, 50);
            L1 = (Line)DataModel.CreateInstance("Line");
            L2 = (Line)DataModel.CreateInstance("Line");


            L1.Colour = Colour;
            L2.Colour = Colour;

            Click += OnClicked; 

            CHECKBOX_INITIALISED = true; 
        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx, IntPtr RenderTarget)
        {
            if (!CHECKBOX_INITIALISED)
            {
                Init();
            }
            else
            {
                PerformRender(SDL_Renderer, Tx);
            }


        }

        private void PerformRender(Scene SDL_Renderer, ImageBrush Tx)
        {
            base.Render(SDL_Renderer, Tx, IntPtr.Zero);
            L1.Invisible = !Checked;
            L2.Invisible = !Checked;

            L1.Begin = new Vector2(Position.X, Position.Y + (Size.Y / 1.5));
            L1.End = new Vector2(Position.X + (Size.X / 2), Position.Y + Size.Y);

            L2.Begin = L1.End;
            L2.End = new Vector2(Position.X + Size.X, Position.Y);
        }

        public void OnClicked(object Sender, MouseEventArgs MEA)
        {
            Checked = !Checked;

            if (CheckedEventHandler != null)
            {
                CheckedEventArgs CEA = new CheckedEventArgs();
                CEA.IsChecked = Checked;
                CEA.InnerEventArgs = MEA;

                CheckedEventHandler(this, CEA);

            }
        }
    }
}
