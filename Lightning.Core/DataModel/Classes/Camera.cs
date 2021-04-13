using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Camera
    /// 
    /// April 13, 2021
    /// 
    /// Defines a Camera. A Camera is the viewport of a Lightning level. 
    /// </summary>
    public class Camera : ControllableObject
    {
        public override string ClassName => "Camera";

        public override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Destroyable | InstanceTags.Instantiable | InstanceTags.Serialisable | InstanceTags.ShownInIDE;
        /// <summary>
        /// Is this Camera active?
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (Active)
            {
                SDL_Renderer.CCameraPosition = new Vector2(Position.X, Position.Y);
            }
            else
            {
                return; 
            }
        }

        public override void OnKeyDown(Control Control)
        {
            // TEMPORARY CODE
            switch (Control.KeyCode.ToString())
            {
                case "LEFT":
                case "A":
                    Position.X += 10;
                    return;
                case "RIGHT":
                case "D":
                    Position.X -= 10;
                    return;
                case "UP":
                case "W":
                    Position.Y += 10;
                    return;
                case "DOWN":
                case "S":
                    Position.Y -= 10;
                    return; 
            }
            // END TEMPORARY CODE

        }

    }
}
