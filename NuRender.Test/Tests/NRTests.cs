using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Test
{
    public static class NRTests
    {
        #region Pixel Plotting
        internal static TestResult Test_SDL2GFX_PixelRGBA(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();


            int PixelsToDraw = 100000;

            for (int i = 0; i < PixelsToDraw; i++)
            {
                int X = Rnd.Next(0, 1151);
                int Y = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);


                SDL_gfx.pixelRGBA(Renderer, X, Y, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        #endregion

        #region Line Tests
        internal static TestResult Test_SDL2GFX_HLine(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.hlineRGBA(Renderer, X1, X2, Y1, Y2, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_VLine(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.vlineRGBA(Renderer, X1, X2, Y1, Y2, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_Line(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.lineRGBA(Renderer, X1, X2, Y1, Y2, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code


        }

        internal static TestResult Test_SDL2GFX_AALine(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.aalineRGBA(Renderer, X1, X2, Y1, Y2, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code


        }

        internal static TestResult Test_SDL2GFX_Arc(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X = Rnd.Next(0, 1151);
                int Y = Rnd.Next(0, 863);
                int Radius = Rnd.Next(0, 250);
                int Start = Rnd.Next(0, 200);
                int End = Rnd.Next(0, 200);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.arcRGBA(Renderer, X, Y, Radius, Start, End, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code


        }

        #endregion

        #region Rectangle tests 
        internal static TestResult Test_SDL2GFX_Rectangle(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.rectangleRGBA(Renderer, X1, X2, Y1, Y2, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_RoundedRectangle(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                int Radius = Rnd.Next(0, 20);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.roundedRectangleRGBA(Renderer, X1, X2, Y1, Y2, Radius, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_FilledRectangle(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.boxRGBA(Renderer, X1, X2, Y1, Y2, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_FilledRoundedRectangle(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                int Radius = Rnd.Next(0, 10);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.roundedBoxRGBA(Renderer, X1, X2, Y1, Y2, Radius, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        #endregion

        #region Circle, Ellipse, and Pie tests

        internal static TestResult Test_SDL2GFX_Ellipse(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X = Rnd.Next(0, 1151);
                int Y = Rnd.Next(0, 863);
                int RadX = Rnd.Next(0, 100);
                int RadY = Rnd.Next(0, 100);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.ellipseRGBA(Renderer, X, Y, RadX, RadY, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_AAEllipse(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X = Rnd.Next(0, 1151);
                int Y = Rnd.Next(0, 863);
                int RadX = Rnd.Next(0, 100);
                int RadY = Rnd.Next(0, 100);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.aaellipseRGBA(Renderer, X, Y, RadX, RadY, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }


        internal static TestResult Test_SDL2GFX_FilledEllipse(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X = Rnd.Next(0, 1151);
                int Y = Rnd.Next(0, 863);
                int RadX = Rnd.Next(0, 100);
                int RadY = Rnd.Next(0, 100);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.filledEllipseRGBA(Renderer, X, Y, RadX, RadY, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_Pie(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X = Rnd.Next(0, 1151);
                int Y = Rnd.Next(0, 863);
                int Radius = Rnd.Next(0, 100);
                int Start = Rnd.Next(0, 100);
                int End = Rnd.Next(0, 100);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.pieRGBA(Renderer, X, Y, Radius, Start, End, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        internal static TestResult Test_SDL2GFX_FilledPie(Scene Sc)
        {

            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X = Rnd.Next(0, 1151);
                int Y = Rnd.Next(0, 863);
                int Radius = Rnd.Next(0, 100);
                int Start = Rnd.Next(0, 100);
                int End = Rnd.Next(0, 100);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.filledPieRGBA(Renderer, X, Y, Radius, Start, End, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }
        #endregion

        #region Triangle tests

        internal static TestResult Test_SDL2GFX_Trigon(Scene Sc)
        {
            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int X3 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                int Y3 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.trigonRGBA(Renderer, X1, Y1, X2, Y2, X3, Y3, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code
        }

        internal static TestResult Test_SDL2GFX_AATrigon(Scene Sc)
        {
            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int X3 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                int Y3 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.aatrigonRGBA(Renderer, X1, Y1, X2, Y2, X3, Y3, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code
        }

        internal static TestResult Test_SDL2GFX_FilledTrigon(Scene Sc)
        {
            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {
                int X1 = Rnd.Next(0, 1151);
                int X2 = Rnd.Next(0, 1151);
                int X3 = Rnd.Next(0, 1151);
                int Y1 = Rnd.Next(0, 863);
                int Y2 = Rnd.Next(0, 863);
                int Y3 = Rnd.Next(0, 863);
                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                SDL_gfx.filledTrigonRGBA(Renderer, X1, Y1, X2, Y2, X3, Y3, R, G, B, A); // todo: store renderers perwindow
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code
        }

        #endregion

        #region Polygon tests
        internal static TestResult Test_SDL2GFX_Polygon(Scene Sc)
        {
            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {

                int PolygonSizeMultiplier = Rnd.Next(1, 10);

                int X1 = 0 * PolygonSizeMultiplier;
                int X2 = 6 * PolygonSizeMultiplier;
                int X3 = 6 * PolygonSizeMultiplier;
                int X4 = -2 * PolygonSizeMultiplier;
                int X5 = -5 * PolygonSizeMultiplier;
                int X6 = X1; 
                int Y1 = 8 * PolygonSizeMultiplier;
                int Y2 = 5 * PolygonSizeMultiplier;
                int Y3 = -1 * PolygonSizeMultiplier;
                int Y4 = -4 * PolygonSizeMultiplier;
                int Y5 = 3 * PolygonSizeMultiplier;
                int Y6 = Y1;

                int ModifierX = Rnd.Next(100, 700);
                int ModifierY = Rnd.Next(100, 700);

                // create tuple
                (int, int, int, int, int, int, int, int, int, int, int, int) Ints = (
                X1 += ModifierX, X2 += ModifierX,
                X3 += ModifierX, X4 += ModifierX, 
                X5 += ModifierX, X6 += ModifierX,
                Y1 += ModifierY, Y2 += ModifierY, 
                Y3 += ModifierY, Y4 += ModifierY, 
                Y5 += ModifierY, Y6 += ModifierY); /// increment all 


                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                // use tuple items

                int[] X = new int[6] { Ints.Item1, Ints.Item2, Ints.Item3, Ints.Item4, Ints.Item5, Ints.Item6 };
                int[] Y = new int[6] { Ints.Item7, Ints.Item8, Ints.Item9, Ints.Item10, Ints.Item11, Ints.Item12 };

                // ref not required?
                SDL_gfx.polygonRGBA(Renderer, X, Y, R, G, B, A);
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code
        }


        internal static TestResult Test_SDL2GFX_AAPolygon(Scene Sc)
        {
            // dumb test code

            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            Random Rnd = new Random();

            int LinesToDraw = 100;

            for (int i = 0; i < LinesToDraw; i++)
            {

                int PolygonSizeMultiplier = Rnd.Next(1, 10);

                int X1 = 0 * PolygonSizeMultiplier;
                int X2 = 6 * PolygonSizeMultiplier;
                int X3 = 6 * PolygonSizeMultiplier;
                int X4 = -2 * PolygonSizeMultiplier;
                int X5 = -5 * PolygonSizeMultiplier;
                int X6 = X1;
                int Y1 = 8 * PolygonSizeMultiplier;
                int Y2 = 5 * PolygonSizeMultiplier;
                int Y3 = -1 * PolygonSizeMultiplier;
                int Y4 = -4 * PolygonSizeMultiplier;
                int Y5 = 3 * PolygonSizeMultiplier;
                int Y6 = Y1;

                int ModifierX = Rnd.Next(100, 700);
                int ModifierY = Rnd.Next(100, 700);

                // create tuple
                (int, int, int, int, int, int, int, int, int, int, int, int) Ints = (
                X1 += ModifierX, X2 += ModifierX,
                X3 += ModifierX, X4 += ModifierX,
                X5 += ModifierX, X6 += ModifierX,
                Y1 += ModifierY, Y2 += ModifierY,
                Y3 += ModifierY, Y4 += ModifierY,
                Y5 += ModifierY, Y6 += ModifierY); /// increment all 


                byte R = (byte)Rnd.Next(0, 255);
                byte G = (byte)Rnd.Next(0, 255);
                byte B = (byte)Rnd.Next(0, 255);
                byte A = (byte)Rnd.Next(0, 255);

                // use tuple items

                int[] X = new int[6] { Ints.Item1, Ints.Item2, Ints.Item3, Ints.Item4, Ints.Item5, Ints.Item6 };
                int[] Y = new int[6] { Ints.Item7, Ints.Item8, Ints.Item9, Ints.Item10, Ints.Item11, Ints.Item12 };
                // ref not required?
                SDL_gfx.aaPolygonRGBA(Renderer, X, Y, R, G, B, A);
            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
            // END DUMB test code

        }

        #endregion

        #region Text tests (SDL2_gfx)

        internal static TestResult Test_SDL2GFX_Character(Scene Sc)
        {
            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            byte TheChar = (byte)'A';

            int CurX = 0;
            int CurY = 0;

            int NumOfChars = 40;
            int NumOfLines = 24;

            for (int i = 0; i < NumOfLines; i++)
            {
                
                for (int j = 0; j < NumOfChars; j++)
                {
                    CurX += 10;

                    SDL_gfx.characterRGBA(Renderer, CurX, CurY, (char)TheChar, 255, 255, 255, 255);

                    TheChar++;

                }

                CurX = 0;
                CurY += 10;
                TheChar = (byte)'A';
            }

            SDL.SDL_RenderPresent(Renderer); 

            return new TestResult { Successful = true };
        }

        internal static TestResult Test_SDL2GFX_String(Scene Sc)
        {
            IntPtr Renderer = Sc.Windows[0].Settings.RenderingInformation.RendererPtr;
            SDL.SDL_RenderClear(Renderer);

            string TestString = "I am the ball";

            int CurX = 0;
            int CurY = 0;

            int NumOfLines = 35;

            for (int i = 0; i < NumOfLines; i++)
            {
                CurX += 10;

                SDL_gfx.stringRGBA(Renderer, CurX, CurY, TestString, 255, 255, 255, 255);

                CurX = 0;
                CurY += 10;

            }

            SDL.SDL_RenderPresent(Renderer);

            return new TestResult { Successful = true };
        }

        #endregion
    }
}
