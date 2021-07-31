using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Gradient
    /// 
    /// July 30, 2021
    /// 
    /// Defines a UI gradient.
    /// </summary>
    public class Gradient : GuiElement
    {
        internal override string ClassName => "Gradient";

       
        private bool GRADIENT_INITIALISED { get; set; }

        internal override InstanceTags Attributes => base.Attributes;

        public override void OnCreate()
        {
            Type ParentType = Parent.GetType(); // parent cannot be null as parentcanbenull is not set

            if (!ParentType.IsSubclassOf(typeof(GuiElement)))
            {
                ErrorManager.ThrowError(ClassName, "GradientMustHaveGuiElementParentException");
                Parent.RemoveChild(this);
                return; // hopefully not required
            }
            else
            {
                GuiElement ParentGE = (GuiElement)Parent;

                if (ParentGE.Position == null
                || ParentGE.Size == null)
                {
                    ErrorManager.ThrowError(ClassName, "GradientParentMustHavePositionException");
                    Parent.RemoveChild(this);
                    return; 
                }
                
            }
        }

        public override void Render(Renderer SDL_Renderer, Texture Tx)
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

        private void Init()
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

        private void DoRender(Renderer SDL_Renderer, Texture Tx)
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

                    GuiElement GEParent = (GuiElement)Parent;

                    Vector2 GEParentPosition = GEParent.Position;

                    Vector2 CurPosition = GEParentPosition * GradientStop.StopPoint;

                    if (GStops.Count - i >= 1)
                    {
                        Instance GStopPlusOne = GStops[i + 1];

                        GradientStop GradientStopPlusOne = (GradientStop)GStopPlusOne;

                        Vector2 GStopPlusOnePos = GEParentPosition * GradientStopPlusOne.StopPoint;

                        Vector2 Diff = GStopPlusOnePos - CurPosition;

                        for (double j = CurPosition.X; j < GStopPlusOnePos.X; j++)
                        {
                            double Percentage = (j - CurPosition.X) * (j - GStopPlusOnePos.X);

                            Color4 C4A = GradientStop.Colour;
                            Color4 C4B = GradientStopPlusOne.Colour;

                            Color4 CDiff = C4B - C4A;

                            Color4 FinalColour = C4A + (CDiff * Percentage);

                            double FinalX = CurPosition.X + ((GStopPlusOnePos.X - CurPosition.X) * Percentage);

                            SDL.SDL_RenderDrawPoint(SDL_Renderer.RendererPtr, (int)FinalX, (int)CurPosition.Y);

                            for (double k = CurPosition.Y; k < GStopPlusOnePos.Y; k++)
                            {
                                double YPercentage = (k - CurPosition.Y) * (k - GStopPlusOnePos.Y);

                                Color4 FinalColourY = (CDiff * YPercentage);

                                double FinalY = CurPosition.Y + ((GStopPlusOnePos.Y - CurPosition.y) * Percentage);

                                SDL.SDL_RenderDrawPoint(SDL_Renderer.RendererPtr, (int)CurPosition.X, (int)FinalY);
                            }


                        }
                    }

                }
            }
        }
    }
}
