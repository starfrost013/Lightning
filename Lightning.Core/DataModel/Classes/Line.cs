using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Line
    /// 
    /// April 12, 2021
    /// 
    /// Implements...a line. Drawn using the SDL2 line API methods. Woo!
    /// </summary>
    public class Line : Primitive
    {
        internal override string ClassName => "Line";

        internal override InstanceTags Attributes => base.Attributes; // TEMP

        /// <summary>
        /// The point that this vector begins. 
        /// </summary>
        public Vector2 Begin { get; set; }

        /// <summary>
        /// The point that this vector ends
        /// </summary>
        public Vector2 End { get; set; }

        private NuRender.Line NRLine { get; set; }
        private bool Line_Initialised { get; set; }

        private void Line_Init(Scene SDL_Renderer)
        {
            //todo: perhaps use get/set accessors?
            Window MainWindow = SDL_Renderer.GetMainWindow();
            NRLine = (NuRender.Line)MainWindow.AddObject("Line");

            if (Begin != null) NRLine.LineStart = new Vector2Internal(Begin.X, Begin.Y);
            if (End != null) NRLine.LineEnd = new Vector2Internal(End.X, End.Y);
            if (Colour != null) NRLine.Colour = new Color4Internal(Colour.A, Colour.R, Colour.G, Colour.B);
            if (Position != null) NRLine.Position = new Vector2Internal(Position.X, Position.Y);
            NRLine.Bordered = Bordered;
            NRLine.Filled = Fill;
            NRLine.Antialiased = !NotAntialiased; // nr uses invert
            Line_Initialised = true;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Render"><inheritdoc/></param>
        /// <param name="Tx"><inheritdoc/></param>
        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            if (!Line_Initialised)
            {
                Line_Init(SDL_Renderer);
            }
            else
            {
                if (!ForceToScreen)
                {
                    // force down
                    NRLine.LineStart = new Vector2Internal(Begin.X - MainWindow.Settings.RenderingInformation.CCameraPosition.X
                    ,Begin.Y - MainWindow.Settings.RenderingInformation.CCameraPosition.Y);

                    NRLine.LineEnd = new Vector2Internal(End.X - MainWindow.Settings.RenderingInformation.CCameraPosition.X
, End.Y - MainWindow.Settings.RenderingInformation.CCameraPosition.Y);
                }
                else
                {
                    NRLine.LineStart = new Vector2Internal(Begin.X, Begin.Y);
                    NRLine.LineEnd = new Vector2Internal(End.X, End.Y);
                }
            }
        }
    }
}
