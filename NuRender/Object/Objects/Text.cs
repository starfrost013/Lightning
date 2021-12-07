using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    public class Text : NRObject
    {

        /// <summary>
        /// Backing field for <see cref="_content"/>
        /// </summary>
        private string _content { get; set; }

        /// <summary>
        /// If true, SDL2_ttf fonts will not be used - instead the basic text rendering of SDL2_gfx will be used.
        /// </summary>
        public bool DisableTTF { get; set; }

        /// <summary>
        /// The content of this text.
        /// </summary>
        public string Content
        {
            get
            {
                return _content; 
            }
            set
            {
                _content = value;

                if ((_content != null)
                && _content.Length > 0)
                {
                    InternalContent = _content.Split('\n'); 
                }
            }
        }

        /// <summary>
        /// Internal content 
        /// </summary>
        private string[] InternalContent { get; set; }

        /// <summary>
        /// The font of this text. Must be a valid loaded font!
        /// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Determines if smoothing is disabled for this text.
        /// </summary>
        public TextRenderingMode RenderingMode { get; set; }

        /// <summary>
        /// Determines if word wrapping is enabled for this text.
        /// </summary>
        public bool WordWrap { get; set;  }

        /// <summary>
        /// Determines the line spacing for this text. If 0, default line spacing is used.
        /// </summary>
        public int LineSpacing { get; set; }

        /// <summary>
        /// DIf <see cref="RenderingMode"/> is set to <see cref="TextRenderingMode.Shaded"/>, the background colour of the text. If it is not, ignored. If it is null and the <see cref="RenderingMode"/>
        /// is <see cref="TextRenderingMode.Shaded"/>, it will be treated as if <see cref="RenderingMode"/> was set to <see cref="TextRenderingMode.Normal"/>.
        /// </summary>
        public Color4 BackgroundColour { get; set; }

        public override void Start(WindowRenderingInformation RenderingInformation)
        {
            if (!Start_EnsureFontLoaded(RenderingInformation))
            {
                return; // todo: delete self
            }

            return;
        }

        private bool Start_EnsureFontLoaded(WindowRenderingInformation RenderingInformation)
        {
            if (DisableTTF) return true; 
            
            foreach (Font Fnt in RenderingInformation.Fonts)
            {
                if (Fnt.FontName == Font)
                {
                    return false;
                }
            }

            return true;
        }

        private Font GetFont(WindowRenderingInformation RenderingInformation)
        {
            if (DisableTTF) return null; // using SDL2_gfx here, so no font

            foreach (Font Fnt in RenderingInformation.Fonts)
            {
                if (Fnt.FontName == Font)
                {
                    return Fnt; 
                }
            }

            return null;
        }

        public override void Render(WindowRenderingInformation RenderingInformation)
        {
            // Renders text.

            if (DisableTTF)
            {
                Render_NoTTF(RenderingInformation);
            }
            else
            {
                Render_TTF(RenderingInformation);
            }

            return;
        }

        private void Render_NoTTF(WindowRenderingInformation RenderingInformation)
        {
            if (WordWrap)
            {
                int CurY = (int)Position.Y;

                foreach (string Line in InternalContent)
                {
                    SDL_gfx.stringRGBA(RenderingInformation.RendererPtr, (int)Position.X, CurY, Line, Colour.R, Colour.G, Colour.B, Colour.A);

                    // implement custom line spacing
                    if (LineSpacing != 0)
                    {
                        CurY += 12; // STATIC for debug font (todo: custom debug font?)
                    }
                    else
                    {
                        CurY += LineSpacing;
                    }
                }
                
            }
            else
            {
                SDL_gfx.stringRGBA(RenderingInformation.RendererPtr, (int)Position.X, (int)Position.Y, Content, Colour.R, Colour.G, Colour.B, Colour.A);
            }

        }

        private void Render_TTF(WindowRenderingInformation RenderingInformation)
        {
            int FontX = 0; // cannot use a property for out/ref
            int FontY = 0; 

            Font Fnt = GetFont(RenderingInformation);

            if (SDL_ttf.TTF_SizeText(Fnt.Pointer, Content, out FontX, out FontY) < 0) // use the first line for content
            {
                // size to one line
                ErrorManager.ThrowError(ClassName, "NRFailedToSizeTTFTextException", $"Failed to size TTF Text for rendering: {SDL.SDL_GetError()}");
                return; 

                //todo: delete self
            }

            Vector2 Position = new Vector2(FontX, FontY);

            if (WordWrap)
            {
                int CurY = (int)Position.Y;
                
                foreach (string Line in InternalContent)
                {
                    Render_TTF_DoRender(Fnt, Line); 

                    // implement custom line spacing
                    if (LineSpacing != 0)
                    {
                        CurY += FontY;
                    }
                    else
                    {
                        CurY += LineSpacing; 
                    }
                    
                }
            }
            else
            {
                Render_TTF_DoRender(Fnt, Content); // no word wrap
            }   
            
        }

        private void Render_TTF_DoRender(Font Fnt, string Text)
        {

            SDL.SDL_Color RenderColour = new SDL.SDL_Color // set up render colour
            {
                r = Colour.R,
                g = Colour.G,
                b = Colour.B,
                a = Colour.A
            };

            switch (RenderingMode)
            {
                case TextRenderingMode.NoAntialias: // non-antialiased text specified
                    SDL_ttf.TTF_RenderText_Solid(Fnt.Pointer, Text, RenderColour);
                    return; 
                case TextRenderingMode.Normal: // blended text specified
                    SDL_ttf.TTF_RenderText_Blended(Fnt.Pointer, Text, RenderColour);
                    return; 
                case TextRenderingMode.Shaded: // shaded text specified 
                    if (BackgroundColour != null)
                    {
                        SDL.SDL_Color BGColour = new SDL.SDL_Color // set up render colour
                        {
                            r = BackgroundColour.R,
                            g = BackgroundColour.G,
                            b = BackgroundColour.B,
                            a = BackgroundColour.A
                        };

                        SDL_ttf.TTF_RenderText_Shaded(Fnt.Pointer, Text, RenderColour, BGColour);
                        return; 
                    }
                    else
                    {
                        SDL_ttf.TTF_RenderText_Blended(Fnt.Pointer, Text, RenderColour); // treat as if blended as its null
                        return;
                    }

            }
        }

    }
}




