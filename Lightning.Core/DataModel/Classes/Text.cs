using NuCore.Utilities;
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

        public Text()
        {
            Position = new Vector2();
        }
        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (Content == null) Content = "";

            FindFontResult FFR = FindFont();

            if (!FFR.Successful)
            {
                return; 
            }
            else
            {
                Font TextFont = FFR.Font;

                // Set font style flags
                int FontStyleFlags = 0;
                
                if (Bold) FontStyleFlags += 1;
                if (Italic) FontStyleFlags += 2;
                if (Underline) FontStyleFlags += 4;
                if (Strikethrough) FontStyleFlags += 8;

                // Style the font if bold/italic/underline/strikethrough are set

                if (FontStyleFlags > 0) SDL_ttf.TTF_SetFontStyle(TextFont.FontPointer, FontStyleFlags);

                // Init surface to be used
                IntPtr SurfaceSDL = IntPtr.Zero;

                Vector2 FontSize = GetApproximateFontSize(TextFont);

                int FontWidth = (int)FontSize.X;
                int FontHeight = (int)FontSize.Y;

                SDL.SDL_Color SDLC = new SDL.SDL_Color();

                if (Colour != null)
                {
                    SDLC.r = Colour.R;
                    SDLC.g = Colour.G;
                    SDLC.b = Colour.B;
                    SDLC.a = Colour.A;

                    // Perform text rendering

                }
                else
                {
                    SDLC.r = 255;
                    SDLC.g = 255;
                    SDLC.b = 255;
                    SDLC.a = 255;
                }

                if (AntiAliasingDisabled)
                {
                    SurfaceSDL = SDL_ttf.TTF_RenderText_Solid(TextFont.FontPointer, Content, SDLC);
                }
                else
                {
                    SurfaceSDL = SDL_ttf.TTF_RenderText_Blended(TextFont.FontPointer, Content, SDLC);
                }

                // Convert to texture for hardware rendering
                IntPtr TextTexture = SDL.SDL_CreateTextureFromSurface(SDL_Renderer.RendererPtr, SurfaceSDL);

                // corre
                SDL.SDL_Rect SourceRect = new SDL.SDL_Rect
                {
                    x = 0,
                    y = 0,
                    w = FontWidth,
                    h = FontHeight,

                };

                SDL.SDL_Rect DestinationRect = new SDL.SDL_Rect();

                if (ForceToScreen) // dumb hack that is BAD and NOT RECOMMENDED!
                {
                    DestinationRect.x = (int)Position.X;
                    DestinationRect.y = (int)Position.Y;
                    DestinationRect.w = FontWidth;
                    DestinationRect.h = FontHeight;
                }
                else
                {
                    DestinationRect.x = (int)Position.X - (int)SDL_Renderer.CCameraPosition.X;
                    DestinationRect.y = (int)Position.Y - (int)SDL_Renderer.CCameraPosition.Y;
                    DestinationRect.w = FontWidth;
                    DestinationRect.h = FontHeight;
                }


                SDL.SDL_RenderCopy(SDL_Renderer.RendererPtr, TextTexture, ref SourceRect, ref DestinationRect);

                SDL.SDL_FreeSurface(SurfaceSDL);
                SDL.SDL_DestroyTexture(TextTexture);
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

        internal Vector2 GetApproximateFontSize(Font Fnt)
        {

            Font FX = Fnt;

            int FontWidth = 0;
            int FontHeight = 0;

            if (SDL_ttf.TTF_SizeText(FX.FontPointer, Content, out FontWidth, out FontHeight) < 0)
            {
                ErrorManager.ThrowError(ClassName, "FailedToRenderTextException", $"Failed to render text: Error sizing text: {SDL.SDL_GetError()}");
                return null;
            }
            else
            {
                Vector2 FinalVec2 = new Vector2();
                FinalVec2.X = FontWidth;
                FinalVec2.Y = FontHeight;

                return FinalVec2;
            }

        }
        
    }
}
