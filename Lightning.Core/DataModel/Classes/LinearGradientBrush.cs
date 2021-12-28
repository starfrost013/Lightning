using NuCore.Utilities;
using NuRender;
using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LinearGradientBrush (neé Gradient :P)
    /// 
    /// July 30, 2021 (modified August 17, 2021: Made it work, added direction)
    /// 
    /// Defines a UI gradient.
    /// </summary>
    [TypeConverter(typeof(GradientConverter))]
    public class LinearGradientBrush : Brush
    {
        internal override string ClassName => "LinearGradientBrush";

       
        private bool GRADIENT_INITIALISED { get; set; }

        internal override InstanceTags Attributes => base.Attributes;

        /// <summary>
        /// The direction of this gradient - see <see cref="GradientDirection"/>.
        /// </summary>
        public GradientDirection Direction { get; set; }

        public override void OnCreate()
        {
            Type ParentType = Parent.GetType(); // parent cannot be null as parentcanbenull is not set

            if (ParentType != typeof(PhysicalObject)
            && !ParentType.IsSubclassOf(typeof(PhysicalObject)))
            {
                ErrorManager.ThrowError(ClassName, "BrushMustHavePhysicalObjectParentException");
                Parent.RemoveChild(this);
                return;
            }

            PhysicalObject ParentPE = (PhysicalObject)Parent;

            if (ParentPE.Position == null
            || ParentPE.Size == null)
            {
                ErrorManager.ThrowError(ClassName, "GradientParentMustHavePositionException");
                Parent.RemoveChild(this);
                return;
            }

        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            if (!GRADIENT_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }
        }

        internal void Init()
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GradientStop");
            
            if (!GMIR.Successful
            || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGradientStopsException");

            }
            else 
            {
                List<Instance> Instances = GMIR.Instances;

                if (Instances.Count == 0)
                {
                    ErrorManager.ThrowError(ClassName, "GradientMustHaveAboveZeroStopsException");
                    Parent.RemoveChild(this);
                }
                else
                {
                    foreach (Instance Ins in Instances)
                    {
                        GradientStop GS = (GradientStop)Ins;

                        double MaxGradientWidth = 0.0;

                        if (GS.StopPoint > MaxGradientWidth) MaxGradientWidth = GS.StopPoint;

                        if (GS.StopPoint < MaxGradientWidth)
                        {
                            ErrorManager.ThrowError(ClassName, "InvalidGradientException", "Gradient cannot go backwards! All GradientStops must have sequential StopPoints!");
                            Parent.RemoveChild(this);
                        }

                        if (GS.Colour == null)
                        {
                            ErrorManager.ThrowError(ClassName, "InvalidGradientException", "Gradient cannot go backwards! All GradientStops must have valid Colours!");
                            Parent.RemoveChild(this);
                        }

                    }
                }

                GRADIENT_INITIALISED = true;


            }
        }

        private void DoRender(Scene SDL_Renderer, ImageBrush Tx)
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GradientStop");

            Window MainWindow = SDL_Renderer.GetMainWindow(); 

            if (!GMIR.Successful
            || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToGetListOfGradientStopsException");
            }
            else
            {
                List<Instance> GStops = GMIR.Instances;

                for (int i = 0; i < GStops.Count; i++)
                {
                    Instance GStop = GStops[i]; 

                    GradientStop GradientStop = (GradientStop)GStop;

                    PhysicalObject PEParent = (PhysicalObject)Parent;

                    Vector2 PEParentPosition = PEParent.Position;

                    Vector2 CurPosition = PEParentPosition + (PEParent.Size * GradientStop.StopPoint);

                    if (GStops.Count - i > 1)
                    {
                        Instance GStopPlusOne = GStops[i + 1];

                        GradientStop GradientStopPlusOne = (GradientStop)GStopPlusOne;

                        Vector2 GStopPlusOnePos = PEParentPosition + (PEParent.Size * GradientStopPlusOne.StopPoint);

                        if (!ForceToScreen)
                        {
                            // temp until converter
                            CurPosition -= new Vector2(MainWindow.Settings.RenderingInformation.CCameraPosition.X, MainWindow.Settings.RenderingInformation.CCameraPosition.Y);
                            GStopPlusOnePos -= new Vector2(MainWindow.Settings.RenderingInformation.CCameraPosition.X, MainWindow.Settings.RenderingInformation.CCameraPosition.Y);
                        }

                        SetUpGradientDirection(CurPosition, GStopPlusOnePos);

                        Vector2 Diff = GStopPlusOnePos - CurPosition;

                        Color4 C4A = GradientStop.Colour;
                        Color4 C4B = GradientStopPlusOne.Colour;

                        Color4 CDiff = C4B - C4A;

                        double j = CurPosition.X;
                        double k = CurPosition.Y;

                        for (j = CurPosition.X; j <= GStopPlusOnePos.X; j++)
                        {

                            double Percentage = (j - CurPosition.X) / Math.Abs(GStopPlusOnePos.X - CurPosition.X);

                            if (Percentage > 1) break; 

                            Color4 FinalColour = C4A + (CDiff * Percentage);
                            SDL.SDL_SetRenderDrawColor(MainWindow.Settings.RenderingInformation.RendererPtr, FinalColour.R, FinalColour.G, FinalColour.B, FinalColour.A);

                            for (k = CurPosition.Y; k <= GStopPlusOnePos.Y; k++)
                            {

                                double YPercentage = (k - CurPosition.Y) / Math.Abs(GStopPlusOnePos.Y - CurPosition.Y);

                                if (YPercentage > 1) break;

                                Color4 FinalColourY = C4A + (CDiff * YPercentage);

                                SDL.SDL_SetRenderDrawColor(MainWindow.Settings.RenderingInformation.RendererPtr, FinalColourY.R, FinalColourY.G, FinalColourY.B, FinalColourY.A);


                                SDL.SDL_RenderDrawPoint(MainWindow.Settings.RenderingInformation.RendererPtr, (int)j, (int)k);
                            }

                        }

                        SDL.SDL_SetRenderDrawColor(MainWindow.Settings.RenderingInformation.RendererPtr, 0, 0, 0, 0);
                    }

                }
            }

        }

        private void SetUpGradientDirection(Vector2 Pos1, Vector2 Pos2)
        {
            switch (Direction)
            {
                case GradientDirection.Left:
                    return;
                case GradientDirection.Right:
                    Pos2.X = Pos1.X;
                    Pos1.X = Pos2.X;
                    return;
                case GradientDirection.Up:
                    if (Pos1.Y < Pos2.Y)
                    {
                        Pos2.Y = Pos1.Y; // GO UP
                    }
                    return;
                case GradientDirection.Down:
                    if (Pos1.Y > Pos2.Y)
                    {
                        Pos2.Y = Pos1.Y;
                    }
                    return;
                case GradientDirection.TopLeft: // should work - pos1/2 will always become smaller
                    Vector2 Smallest = Vector2.Min(Pos1, Pos2);

                    Vector2 LargestTL = new Vector2();
                    
                    if (Smallest == Pos1)
                    {
                        Pos1 = Smallest;

                    }
                    else
                    {
                        LargestTL = Pos1;
                        Pos2 = LargestTL;
                        Pos1 = Smallest;
                    }

                    return;
                case GradientDirection.TopRight:
                    Vector2 LargestTR = Vector2.Max(Pos1, Pos2);

                    if (LargestTR == Pos2)
                    {
                        Vector2 NPos1 = Pos1;
                        Pos1 = LargestTR;
                        Pos2 = NPos1;
                    }

                    return;
                case GradientDirection.BottomLeft:
                    Vector2 LargestBL = new Vector2();

                    if (Pos2.X > Pos1.X)
                    {
                        LargestBL = Pos2;
                        Pos1.X = Pos2.X;
                        Pos2.X = LargestBL.X;
                    }

                    return;
                case GradientDirection.BottomRight:
                    if (Pos2.X > Pos1.X)
                    {
                        LargestBL = Pos2;
                        Pos1.X = Pos2.X;
                        Pos2.X = LargestBL.X;
                    }

                    if (Pos2.Y > Pos1.Y)
                    {
                        LargestBL = Pos2;
                        Pos2 = Pos1;
                        Pos1 = LargestBL;
                    }

                    return; 
            }


        }
    }
}
