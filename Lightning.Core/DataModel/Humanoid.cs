using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Humanoid
    /// 
    /// May 24, 2021
    /// 
    /// Defines a Humanoid.
    /// </summary>
    public class Humanoid : ControllableObject
    {
        /// <summary>
        /// The name of the character.
        /// </summary>
        public string CharName { get; set; }

        /// <summary>
        /// The current health of the character. 
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// The maximum health of the character.
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Is the character invincible?
        /// </summary>
        public bool Invincible { get; set; }

        /// <summary>
        /// Kill plane: If the player's X & Y coordinates are larger than this properties' X & Y values, it will die
        /// </summary>
        public Vector2 KillPlane { get; set; }

        /// <summary>
        /// Respawn point: TODO: MAKE LIST
        /// </summary>
        public Vector2 RespawnPoint { get; set; }

        /// <summary>
        /// Low health colour.
        /// </summary>
        public Color3 LowHealthColour { get; set; }

        /// <summary>
        /// Medium health colour.
        /// </summary>
        public Color3 MediumHealthColour { get; set; }

        /// <summary>
        /// High health colour.
        /// </summary>
        public Color3 HighHealthColour { get; set; }

        /// <summary>
        /// Low health colour threshold. [0-1]
        /// </summary>
        public double LowHealthThreshold { get; set; }

        /// <summary>
        /// Medium health colour threshold. [0-1]
        /// </summary>
        public double MediumHealthThreshold { get; set; }

        /// <summary>
        /// High health colour threshold. [0-1]
        /// </summary>
        public double HighHealthThreshold { get; set; }

        /// <summary>
        /// Triggers the name being displayed or not.
        /// </summary>
        public bool DisplayName { get; set; }

        /// <summary>
        /// Displays the health bar if true.
        /// </summary>
        public bool DisplayHealthBar { get; set; }

        /// <summary>
        /// Length of this object's health bar
        /// </summary>
        public int HealthBarLength { get; set; }

        /// <summary>
        /// Health bar second colour - used for the areas of the health bar that don't have health
        /// </summary>
        public Color3 HealthBarColour2 { get; set; }

        public override void Render(Renderer SDL_Renderer, Texture Tx = null)
        {
            // Some rendering temp stuff for humanoids

            // Prevent health going above MaxHealth

            if (!Invincible)
            {
                if (MaxHealth == 0) MaxHealth = 100;
                
                if (Health <= 0
                || (Position.X > RespawnPoint.X
                && Position.Y > RespawnPoint.Y))
                {
                    // Kill the player if they aren't invincible
                    Kill();
                }

                if (DisplayHealthBar)
                {
                    // Set a default health bar length and some other values.
                    if (HealthBarLength == 0) HealthBarLength = 50;
                    if (LowHealthColour == null) LowHealthColour = new Color3 { R = 0, G = 0, B = 255 };
                    if (MediumHealthColour == null) MediumHealthColour = new Color3 { R = 255, G = 216, B = 0 };
                    if (HighHealthColour == null) HighHealthColour = new Color3 { R = 0, G = 85, B = 16 };
                    if (LowHealthThreshold == 0) LowHealthThreshold = 25;
                    if (MediumHealthThreshold == 0) MediumHealthThreshold = 50;
                    if (HighHealthThreshold == 0) HighHealthThreshold = 75;
                    if (HealthBarColour2 == null) HealthBarColour2 = new Color3 { R = 255, G = 255, B = 255 };

                    // Temp code until the Text system is implemented
                    double HBPositionX = Position.X;
                    double HBPositionY = Position.Y - 30;

                    int HealthPercent = (int)MaxHealth / Health;

                    int HealthBarLine1EndXPos = (int)Position.X + (HealthBarLength * HealthPercent);
                    int HealthBarLine2EndXPos = (int)Position.X + HealthBarLength;

                    int HealthBarLine2StartXPos = (int)HBPositionX + HealthBarLine1EndXPos;

                    // eww elseif
                    // but it is required :(
                    if (Health <= LowHealthThreshold)
                    {
                        SDL.SDL_SetRenderDrawColor(SDL_Renderer.SDLRenderer, LowHealthColour.R, LowHealthColour.G, LowHealthColour.B, 255);
                    }
                    else if (Health > LowHealthThreshold
                        && Health <= MediumHealthThreshold)
                    {
                        SDL.SDL_SetRenderDrawColor(SDL_Renderer.SDLRenderer, MediumHealthColour.R, MediumHealthColour.G, MediumHealthColour.B, 255);
                    }
                    else
                    {
                        SDL.SDL_SetRenderDrawColor(SDL_Renderer.SDLRenderer, HighHealthColour.R, HighHealthColour.G, HighHealthColour.B, 255);
                    }

                    SDL.SDL_RenderDrawLineF(SDL_Renderer.SDLRenderer, (float)HBPositionX, (float)HBPositionY, HealthBarLine1EndXPos, (float)HBPositionY);

                    SDL.SDL_SetRenderDrawColor(SDL_Renderer.SDLRenderer, HealthBarColour2.R, HealthBarColour2.G, HealthBarColour2.B, 255);

                    SDL.SDL_RenderDrawLineF(SDL_Renderer.SDLRenderer, (float)HealthBarLine2StartXPos, (float)HBPositionY, HealthBarLine2EndXPos, (float)HBPositionY);

                    // Reset draw colour. 
                    SDL.SDL_SetRenderDrawColor(SDL_Renderer.SDLRenderer, 255, 255, 255, 255);
                }
            }


            base.Render(SDL_Renderer, Tx);
        }


        private void Kill()
        {
            Position = RespawnPoint;
            Health = MaxHealth; 
        }
    }
}
