using NuCore.Utilities;
using NuRender;
using NuRender.SDL2;
using System;

namespace NuRender.Test
{
    class Program
    {
        /// <summary>
        /// My A-Level computer science teacher was banging on at me about how while (true) is bad lol
        /// 
        /// Tile engine for NuRender ver.0.2.0
        /// </summary>
        private static bool Running { get; set; }

        private static TestWorld Tests { get; set; }

        private static Scene CurScene { get; set; }

        static void Main(string[] args)
        {
            Logging.Log("NuRender Test Application\n©2021,2022 starfrost");
            CurScene = Init();
            RunTests(CurScene);

        }

        private static Scene Init()
        {
            NuRender.NuRender_Init();
            Scene Sc = new Scene();
            Sc.AddWindow(new WindowSettings());
            Running = true;
            Tests = new TestWorld();

            return Sc;
        }

        private static void RunTests(Scene Sc)
        {

            // TEMP 
            Line Li = (Line)Sc.Windows[0].AddObject("Line");
            Li.Thickness = 35;
            Li.LineStart = new Vector2Internal(0, 0);
            Li.LineEnd = new Vector2Internal(500, 500);
            Li.Colour = new Color4Internal(255, 255, 255, 255);

            // END TEMP - WILL BE REMOVED
            RegisterTests();
            Tests.RunTests(Sc); 

            while (Running) // TEMP: TODO: MOVE RUNNING TO SCENE
            {
                //Sc.Main(); // enter the NR window

            }
        }

        private static void RegisterTests()
        {
            //todo: move to testregister
            Tests.Tests.Add(new Test { Name = "Pixels", Method = NRTests.Test_SDL2GFX_PixelRGBA });
            Tests.Tests.Add(new Test { Name = "HLine", Method = NRTests.Test_SDL2GFX_HLine });
            Tests.Tests.Add(new Test { Name = "VLine", Method = NRTests.Test_SDL2GFX_VLine });
            Tests.Tests.Add(new Test { Name = "Line", Method = NRTests.Test_SDL2GFX_Line });
            Tests.Tests.Add(new Test { Name = "Line (Anti-Aliased)", Method = NRTests.Test_SDL2GFX_AALine });
            Tests.Tests.Add(new Test { Name = "Rectangle", Method = NRTests.Test_SDL2GFX_Rectangle });
            Tests.Tests.Add(new Test { Name = "Rounded Rectangle", Method = NRTests.Test_SDL2GFX_RoundedRectangle });
            Tests.Tests.Add(new Test { Name = "Filled Rectangle", Method = NRTests.Test_SDL2GFX_FilledRectangle });
            Tests.Tests.Add(new Test { Name = "Filled Rounded Rectangle", Method = NRTests.Test_SDL2GFX_FilledRoundedRectangle });
            Tests.Tests.Add(new Test { Name = "Arc", Method = NRTests.Test_SDL2GFX_Arc });
            Tests.Tests.Add(new Test { Name = "Ellipse", Method = NRTests.Test_SDL2GFX_Ellipse });
            Tests.Tests.Add(new Test { Name = "Ellipse (Anti-Aliased)", Method = NRTests.Test_SDL2GFX_AAEllipse });
            Tests.Tests.Add(new Test { Name = "Filled Ellipse", Method = NRTests.Test_SDL2GFX_FilledEllipse });
            Tests.Tests.Add(new Test { Name = "Pie", Method = NRTests.Test_SDL2GFX_Pie });
            Tests.Tests.Add(new Test { Name = "Filled Pie", Method = NRTests.Test_SDL2GFX_FilledPie });
            Tests.Tests.Add(new Test { Name = "Triangle", Method = NRTests.Test_SDL2GFX_Trigon });
            Tests.Tests.Add(new Test { Name = "Triangle (Anti-Aliased)", Method = NRTests.Test_SDL2GFX_AATrigon });
            Tests.Tests.Add(new Test { Name = "Filled Triangle", Method = NRTests.Test_SDL2GFX_FilledTrigon });
            Tests.Tests.Add(new Test { Name = "Polygon", Method = NRTests.Test_SDL2GFX_Polygon });
            Tests.Tests.Add(new Test { Name = "Polygon (Anti-Aliased)", Method = NRTests.Test_SDL2GFX_AAPolygon });
            Tests.Tests.Add(new Test { Name = "Single Character", Method = NRTests.Test_SDL2GFX_Character });
            Tests.Tests.Add(new Test { Name = "String", Method = NRTests.Test_SDL2GFX_String });

            //todo: move to testregister
        }

    }
}
