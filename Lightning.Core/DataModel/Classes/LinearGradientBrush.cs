using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LinearGradientBrush (neé Gradient :P)
    /// 
    /// July 30, 2021 (modified August 7, 2021: Now a brush)
    /// 
    /// Defines a UI gradient.
    /// </summary>
    [TypeConverter(typeof(GradientConverter))]
    public class LinearGradientBrush : Brush
    {
        internal override string ClassName => "LinearGradientBrush";

       
        private bool GRADIENT_INITIALISED { get; set; }

        internal override InstanceTags Attributes => base.Attributes;

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

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
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

        internal override void Init()
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

        private void DoRender(Renderer SDL_Renderer, ImageBrush Tx)
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GradientStop");

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

                        if (!NotCameraAware)
                        {
                            CurPosition -= SDL_Renderer.CCameraPosition;
                            GStopPlusOnePos -= SDL_Renderer.CCameraPosition;
                        }

                        Vector2 Diff = GStopPlusOnePos - CurPosition;

                        Color4 C4A = GradientStop.Colour;
                        Color4 C4B = GradientStopPlusOne.Colour;

                        Color4 CDiff = C4B - C4A;

                        for (double j = CurPosition.X; j < GStopPlusOnePos.X; j++)
                        {
                            //double Percentage = (j - CurPosition.X) * (j - GStopPlusOnePos.X);
                            double Percentage = (j - CurPosition.X) / Math.Abs(j - GStopPlusOnePos.X);

                            Color4 FinalColour = C4A + (CDiff * Percentage);
                            SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, FinalColour.R, FinalColour.G, FinalColour.B, FinalColour.A);


                            SDL.SDL_RenderDrawPoint(SDL_Renderer.RendererPtr, (int)j, (int)CurPosition.Y);

                            for (double k = CurPosition.Y; k < GStopPlusOnePos.Y; k++)
                            {
                                //double YPercentage = (k - CurPosition.Y) * (k - GStopPlusOnePos.Y);
                                double YPercentage = (k - CurPosition.Y) / Math.Abs(k - GStopPlusOnePos.Y);
                                Color4 FinalColourY = C4A + (CDiff * YPercentage);

                                SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, FinalColourY.R, FinalColourY.G, FinalColourY.B, FinalColourY.A);


                                SDL.SDL_RenderDrawPoint(SDL_Renderer.RendererPtr, (int)CurPosition.X, (int)k);
                            }


                        }

                        SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, 0, 0, 0, 0);
                    }

                }
            }
        }
    }
}
