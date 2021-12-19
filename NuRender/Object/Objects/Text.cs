using NuCore.Utilities;
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
        /// Determines the line spacing for this text. If 0, default line spacing is used.
        /// </summary>
        public int LineSpacing { get; set; }

        /// <summary>
        /// Determines if word wrapping is enabled for this text.
        /// </summary>
        public bool WordWrap { get; set; }

        /// <summary>
        /// The style of this text. Has no effect if <see cref="DisableTTF"/> is true.
        /// </summary>
        public TextStyle Style { get; set; }

        /// <summary>
        /// DIf <see cref="RenderingMode"/> is set to <see cref="TextRenderingMode.Shaded"/>, the background colour of the text. If it is not, ignored. If it is null and the <see cref="RenderingMode"/>
        /// is <see cref="TextRenderingMode.Shaded"/>, it will be treated as if <see cref="RenderingMode"/> was set to <see cref="TextRenderingMode.Normal"/>.
        /// </summary>
        public Color4Internal BackgroundColour { get; set; }

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
                if (Fnt.Name == Font)
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
                if (Fnt.Name == Font)
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
            if (Content == null
            || Content.Length == 0)
            {
                return;
            }

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

            Vector2Internal Size = new Vector2Internal(FontX, FontY);

            if (Style > 0) SDL_ttf.TTF_SetFontStyle(Fnt.Pointer, (int)Style); // Dec 19, 2021

            if (WordWrap)
            {
                int CurY = (int)Position.Y;
                
                foreach (string Line in InternalContent)
                {
                    Render_TTF_DoRender(Fnt, Line, Size, RenderingInformation); 

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
                Render_TTF_DoRender(Fnt, Content, Size, RenderingInformation); // no word wrap
            }   
            
        }

        private void Render_TTF_DoRender(Font Fnt, string Text, Vector2Internal Size, WindowRenderingInformation RenderInfo)
        {
            SDL.SDL_Color RenderColour = new SDL.SDL_Color // set up render colour
            {
                r = Colour.R,
                g = Colour.G,
                b = Colour.B,
                a = Colour.A
            };

            SDL.SDL_Rect SourceRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = (int)Size.X,
                h = (int)Size.Y
            };


            SDL.SDL_Rect DestinationRect = new SDL.SDL_Rect
            {
                x = (int)Position.X,
                y = (int)Position.Y,
                w = (int)Size.X,
                h = (int)Size.Y
            };

            switch (RenderingMode)
            {
                case TextRenderingMode.NoAntialias: // non-antialiased text specified
                    IntPtr Surface = SDL_ttf.TTF_RenderText_Solid(Fnt.Pointer, Text, RenderColour);
                    IntPtr Texture = SDL.SDL_CreateTextureFromSurface(RenderInfo.RendererPtr, Surface);

                    SDL.SDL_RenderCopy(RenderInfo.RendererPtr, Texture, ref SourceRect, ref DestinationRect);
                    return; 
                case TextRenderingMode.Normal: // blended text specified
                    IntPtr NSurface = SDL_ttf.TTF_RenderText_Blended(Fnt.Pointer, Text, RenderColour);
                    IntPtr NTexture = SDL.SDL_CreateTextureFromSurface(RenderInfo.RendererPtr, NSurface);

                    SDL.SDL_RenderCopy(RenderInfo.RendererPtr, NTexture, ref SourceRect, ref DestinationRect);
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

                        IntPtr SSurface = SDL_ttf.TTF_RenderText_Shaded(Fnt.Pointer, Text, RenderColour, BGColour);
                        IntPtr STexture = SDL.SDL_CreateTextureFromSurface(RenderInfo.RendererPtr, SSurface);

                        SDL.SDL_RenderCopy(RenderInfo.RendererPtr, STexture, ref SourceRect, ref DestinationRect);
                        return; 
                    }
                    else
                    {
                        IntPtr BSurface = SDL_ttf.TTF_RenderText_Blended(Fnt.Pointer, Text, RenderColour); // treat as if blended as its null
                        IntPtr BTexture = SDL.SDL_CreateTextureFromSurface(RenderInfo.RendererPtr, BSurface);

                        SDL.SDL_RenderCopy(RenderInfo.RendererPtr, BTexture, ref SourceRect, ref DestinationRect);
                        return;
                    }

            }
        }

    }
}