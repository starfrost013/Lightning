using NuCore.Utilities;
using NuRender;
using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Text
    /// 
    /// June 30, 2021 (modified December 8, 2021: NuRender integration, phase 2)
    /// 
    /// Defines text. :O
    /// </summary>
    public class Text : GuiElement
    {
        internal override string ClassName => "Text";

        /// <summary>
        /// Toggles anti-aliasing of this text. 
        /// </summary>
        public bool AntiAliasingDisabled { get; set; }

        /// <summary>
        /// Content of this Text.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The font family of this Text.
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// The font size (int) of this font. RECOMMENDED
        /// </summary>
        public int FontSize { get; set; }
        
        /// <summary>
        /// If this property is set to true, this text will be rendered bold.
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// If this property is set to true, this text will be rendered italic.
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// If this property is set to true, this text will be rendered underline.
        /// </summary>
        public bool Underline { get; set; }

        /// <summary>
        /// If this property is set to true, this text will be rendered struck through.
        /// </summary>
        public bool Strikethrough { get; set; }

        /// <summary>
        /// The number of pixels of a potential outline
        /// </summary>
        public int OutlinePixels { get; set; }

        /// <summary>
        /// If true, SDL2_ttf fonts will not be used - instead the basic text rendering of SDL2_gfx will be used.
        /// </summary>
        public bool DisableTTF { get; set; }

        /// <summary>
        /// Toggles word-wrapping.
        /// </summary>
        public bool WordWrap { get; set; }

        private NuRender.Text NRText { get; set; }

        private bool Text_Initialised { get; set; }
        public Text()
        {
            Position = new Vector2();
        }

        internal void Text_Init(Scene SDL_Renderer)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();
            NRText = (NuRender.Text)MainWindow.AddObject("Text");

            if (Colour != null)
            {
                NRText.Colour = (Color4Internal)Colour;
            }
            else
            {
                NRText.Colour = NuRender.NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR;
            }
            
            if (BackgroundColour != null) NRText.BackgroundColour = (Color4Internal)BackgroundColour;
            if (Position != null) NRText.Position = (Vector2Internal)Position;

            if (Bold) NRText.Style += (int)TextStyle.Bold;
            if (Italic) NRText.Style += (int)TextStyle.Italic;
            if (Underline) NRText.Style += (int)TextStyle.Underline;
            if (Strikethrough) NRText.Style += (int)TextStyle.Strikethrough;

            NRText.WordWrap = WordWrap;
            NRText.DisableTTF = DisableTTF;
            NRText.Content = Content;
            
            // TEMP
            
            if (AntiAliasingDisabled)
            {
                NRText.RenderingMode = TextRenderingMode.NoAntialias;
            }
            else
            {
                NRText.RenderingMode = TextRenderingMode.Normal; 
            }

            if (!DisableTTF)
            {
                FindFontResult FFR = FindFont();

                if (FFR.Successful)
                {
                    NRText.Font = FFR.Font.Name;
                }
                else
                {
                    ErrorManager.ThrowError(ClassName, "NRCannotFindFontException", $"Failed to find font {NRText.Font}! Fonts must be loaded before text containing them is used.");
                    // delete this text
                    Parent.RemoveChild(this);
                }

                Text_Initialised = true;
            }
            else
            {
                Text_Initialised = true;
                return; 
            }



        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx, IntPtr RenderTarget)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();
            
            if (!Text_Initialised)
            {
                Text_Init(SDL_Renderer);
            }
            else
            {
                if (ForceToScreen)
                {
                    NRText.Position = (Vector2Internal)Position;
                }
                else
                {
                    NRText.Position = (Vector2Internal)Position - MainWindow.Settings.RenderingInformation.CCameraPosition;
                }
            }

        }


        internal FindFontResult FindFont()
        {
            // this is basically the only SOLUTION to this until we figure out how to PASS SHIT from the FUCKING SERVICE

            FindFontResult FFR = new FindFontResult();

            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Font");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfFontsException");
                FFR.FailureReason = "Failed to obtain list of fonts";
                return FFR; // will never run 
            }
            else
            {
                List<Instance> Instances = GMIR.Instances;

                if (Instances.Count == 0)
                {
                    ErrorManager.ThrowError(ClassName, "NoFontsInstalledCannotUseUIException");
                    FFR.FailureReason = "here are no fonts installed - cannot use UI!";
                    return FFR; 
                }
                else
                {
                    foreach (Instance Instance in Instances)
                    {
                        Font Fnt = (Font)Instance;

                        if (Fnt.Name == FontFamily)
                        {
                            FFR.Successful = true;
                            FFR.Font = Fnt;
                            return FFR;
                        }
                    }
                }

            }

            FFR.FailureReason = "Unknown error";
            return FFR; 
        }

        /// <summary>
        /// Deprecated - NR 
        /// </summary>
        /// <param name="Fnt"></param>
        /// <returns></returns>
        internal Vector2 GetApproximateFontSize(Font Fnt)
        {
            // temporary workaround until TextBox ported to NR
            if (!Fnt.FONT_LOADED) return new Vector2(10, 10);

            Vector2 FontSize = Fnt.GetFontSize(Content);

            if (FontSize.X == 0
            || FontSize.Y == 0) 
            {
                ErrorManager.ThrowError(ClassName, "FailedToRenderTextException", $"Failed to render text: Error sizing text: {SDL.SDL_GetError()}");
                return null;
            }
            else
            {
                return FontSize;
            }

        }
    }
}
