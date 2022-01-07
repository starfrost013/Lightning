using NuRender.SDL2; 
using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Window
    /// 
    /// August 31, 2021 (modified September 6, 2021) 
    /// 
    /// Defines a window.
    /// </summary>
    public class Window
    {   
        /// <summary>
        /// Fake ClassName used for throwing errors etc
        /// </summary>
        internal string ClassName => "Window";

        /// <summary>
        /// The <see cref="NRObject"/>s for this window.
        /// 
        /// TODO: NROBJECTCOLLECTION
        /// </summary>
        public List<NRObject> NRObjects { get; set; }

        /// <summary>
        /// The settings of this Window - see <see cref="WindowSettings"/>.
        /// </summary>
        public WindowSettings Settings { get; set; }

        
        public Window()
        {
            NRObjects = new List<NRObject>(); 
        }

        /// <summary>
        /// Initialises this window.
        /// </summary>
        internal bool Init()
        {
            Logging.Log($"Creating SDL window with title {Settings.ApplicationName}, ID {Settings.WindowID}...", ClassName);

            // we don't support combining windowmodes but we do support custom windowflags

            if (Settings.WindowMode == WindowMode.Fullscreen) Settings.WindowFlags |= SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
            else if (Settings.WindowMode == WindowMode.WindowedBorderless) Settings.WindowFlags |= SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS;

            Settings.RenderingInformation.WindowPtr = SDL.SDL_CreateWindow(Settings.ApplicationName, (int)Settings.WindowPosition.X, (int)Settings.WindowPosition.Y, (int)Settings.WindowSize.X, (int)Settings.WindowSize.Y, Settings.WindowFlags);

            if (Settings.RenderingInformation.WindowPtr == IntPtr.Zero)
            {
                ErrorManager.ThrowError(ClassName, "NRErrorCreatingNRWindowException", $"Error creating NuRender Window {Settings.WindowID}, title {Settings.ApplicationName}: {SDL.SDL_GetError()}");
                return false; 
            }
            else
            {
                Logging.Log($"Creating SDL renderer for window with title {Settings.ApplicationName}, ID {Settings.WindowID}...", ClassName);

                Settings.RenderingInformation.RendererPtr = SDL.SDL_CreateRenderer(Settings.RenderingInformation.WindowPtr, (int)Settings.WindowID, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
                
                if (Settings.RenderingInformation.RendererPtr == IntPtr.Zero)
                {
                    ErrorManager.ThrowError(ClassName, "NRErrorCreatingNRRendererException", $"Error creating NuRender Renderer for Window {Settings.WindowID}, title {Settings.ApplicationName}: {SDL.SDL_GetError()}");
                    return false;     
                }

                return true;
            }
          
        }

        /// <summary>
        /// Main window method
        /// </summary>
        /// <param name="Present">If false, the rendering will not be cleared. Use this if you need to do other rendering work. YOU MUST CALL SDL.SDL_RENDERCLEAR BEFORE YOUR RENDERING WORK IS DONE!</param>
        internal void Main(bool Clear = true)
        {
            SDL.SDL_Event IncomingEvent = new SDL.SDL_Event();

            int IsEventIncoming = SDL.SDL_PollEvent(out IncomingEvent);

            if (IsEventIncoming != 0)
            {
                if (IncomingEvent.window.windowID == Settings.WindowID)
                {
                    switch (IncomingEvent.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            if (Settings.Unquittable) return;

                            Logging.Log($"Window {Settings.WindowID}, title {Settings.ApplicationName} is exiting (user requested quit)...", ClassName);

                            RaiseOnExitEventToAllObjects(new NREventArgs());
                            // todo: raise event
                            Shutdown(false);
                            return;
                    }
                }
            }
            else
            {
                Render(Settings.RenderingInformation, Clear) ; 
                return; 
            }

        }

        public void Shutdown(bool ShutdownSDL = false)
        {
            Logging.Log("Shutting down SDL...", ClassName);

            // clear all objects
            // todo: Shutdown() method for NR Objects.
            NRObjects.Clear(); 

            if (Settings.RenderingInformation.RendererPtr != null) SDL.SDL_DestroyRenderer(Settings.RenderingInformation.RendererPtr);
            if (Settings.RenderingInformation.WindowPtr != null) SDL.SDL_DestroyWindow(Settings.RenderingInformation.WindowPtr);

            if (ShutdownSDL) SDL.SDL_Quit();

        }

        internal void RaiseOnExitEventToAllObjects(NREventArgs NREventArgs)
        {
            NREvent NRE = new NREvent();

            NRE.EventArgs = NREventArgs;

            foreach (NRObject NRO in NRObjects)
            {
                if (NRO.OnExit != null)
                {
                    NRO.OnExit(this, NRE);
                }
            }
        }


        internal void Render(WindowRenderingInformation RenderInfo, bool Clear = true)
        {

            if (Clear) SDL.SDL_RenderClear(RenderInfo.RendererPtr);

            SDL.SDL_SetRenderDrawBlendMode(RenderInfo.RendererPtr, RenderInfo.BlendingMode);

            List<NRObject> ObjList = NRObjects.OrderBy(Obj => Obj.ZIndex).ToList();

            foreach (NRObject NRO in ObjList)
            {
                if (NRO.Colour != null) SDL.SDL_SetRenderDrawColor(RenderInfo.RendererPtr, NRO.Colour.R, NRO.Colour.G, NRO.Colour.B, NRO.Colour.A);

                NRO.Render(RenderInfo); 

                SDL.SDL_SetRenderDrawColor(RenderInfo.RendererPtr, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.R, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.G, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.B, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.A);
            }

            SDL.SDL_RenderPresent(RenderInfo.RendererPtr);
        }

        /// <summary>
        /// Adds a NuRender object with type <see cref="ClassName"/>
        /// </summary>
        public NRObject AddObject(string ClassName)
        {
            NRObject NRO = (NRObject)NRActivator.NRActivate(ClassName);

            if (NRO == null)
            {
                // error message here perhaps?
                return null;
            }
            else
            {
                NRObjects.Add(NRO);
                return NRO; 
            }
        }
    }   
}
