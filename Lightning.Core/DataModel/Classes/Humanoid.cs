using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Humanoid
    /// 
    /// May 24, 2021 (modified August 1, 2021)
    /// 
    /// Defines a Humanoid.
    /// </summary>
    public class Humanoid : ControllableObject
    {
        internal override string ClassName => "Humanoid";

        /// <summary>
        /// The name of the character. Will be implemented when UI is implemented.
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
        /// Low health colour threshold. 
        /// </summary>
        public double LowHealthThreshold { get; set; }

        /// <summary>
        /// Medium health colour threshold. 
        /// </summary>
        public double MediumHealthThreshold { get; set; }

        /// <summary>
        /// High health colour threshold. 
        /// </summary>
        public double HighHealthThreshold { get; set; }

        /// <summary>
        /// Triggers the <see cref="Name"/> of this object being displayed or not.
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

        /// <summary>
        /// Key used for moving left.
        /// </summary>
        public ConvertableStringList KeyLeft { get; set; }

        /// <summary>
        /// Key used for moving right
        /// </summary>
        public ConvertableStringList KeyRight { get; set; }

        /// <summary>
        /// Key used for jumping
        /// </summary>
        public ConvertableStringList KeyJump { get; set; }

        private bool HUMANOID_INITIALISED { get; set; }
        public override void OnCreate()
        {
            base.OnCreate();
            OnKeyDownHandler += OnKeyDown;
        }

        internal override void Init()
        {
            if (KeyLeft == null) KeyLeft = new ConvertableStringList { "LEFT", "A" };
            if (KeyRight == null) KeyRight = new ConvertableStringList { "RIGHT", "D" };
            if (KeyJump == null) KeyJump = new ConvertableStringList { "SPACE" };

            if (HealthBarLength == 0) HealthBarLength = 25;
            if (LowHealthColour == null) LowHealthColour = new Color3 { R = 0, G = 0, B = 255 };
            if (MediumHealthColour == null) MediumHealthColour = new Color3 { R = 255, G = 216, B = 0 };
            if (HighHealthColour == null) HighHealthColour = new Color3 { R = 0, G = 85, B = 16 };
            if (LowHealthThreshold == 0) LowHealthThreshold = Health / 4;
            if (MediumHealthThreshold == 0) MediumHealthThreshold = Health / 2;
            if (HighHealthThreshold == 0) HighHealthThreshold = Health / 1.5;
            if (HealthBarColour2 == null) HealthBarColour2 = new Color3 { R = 255, G = 255, B = 255 };
            if (LowHealthThreshold > MaxHealth) LowHealthThreshold = Health / 4;
            if (MediumHealthThreshold > MaxHealth) MediumHealthThreshold = Health / 2;
            if (HighHealthThreshold > MaxHealth) HighHealthThreshold = Health / 1.5;

            HUMANOID_INITIALISED = true;
            base.Init(); 

            
        }

        public void OnKeyDown(object Sender, KeyEventArgs KeyArgs)
        {

            Control ControlHit = KeyArgs.Key;

            string KeyCode = KeyArgs.Key.KeyCode.ToString();

            if (KeyLeft.Contains(KeyCode))
            {
                ApplyImpulse(new Vector2(-2, 0)); // move to the left
            }
            else if (KeyRight.Contains(KeyCode))
            {
                ApplyImpulse(new Vector2(2, 0)); // move to the left
            }
            else if (KeyJump.Contains(KeyCode))
            {
                // TODO: buffer stuff so holding it for longer is equal to a different jump height
                if (!KeyArgs.Repeat && IsColliding) ApplyImpulse(new Vector2(0, -2)); // move to the left
            }
           

        }

        public override void Render(Renderer SDL_Renderer, Texture Tx = null)
        {
            // Some rendering temp stuff for humanoids

            // Prevent health going above MaxHealth

            if (!HUMANOID_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }
            
        }

        private void DoRender(Renderer SDL_Renderer, Texture Tx = null)
        {
            if (!Invincible)
            {
                if (MaxHealth == 0) MaxHealth = 100;

                // Default
                if (RespawnPoint == null) RespawnPoint = new Vector2(999999999, 999999999);

                if (Health <= 0
                || (Position.X > RespawnPoint.X
                && Position.Y > RespawnPoint.Y))
                {
                    // Kill the player if they aren't invincible
                    Kill();
                }
            }

            if (DisplayHealthBar)
            {
                // Set a default health bar length and some other values.


                // Temp code until the Text system is implemented
                double HBPositionX = Position.X;
                double HBPositionY = Position.Y - 3;

                double HealthPercent = (double)Health / (double)MaxHealth;

                int HealthBarLine1EndXPos = (int)(Position.X + (HealthBarLength * HealthPercent));
                int HealthBarLine2EndXPos = (int)Position.X + HealthBarLength;

                int HealthBarLine2StartXPos = (int)(HBPositionX + (HealthBarLine1EndXPos - Position.X));

                // eww elseif
                // but it is required :(
                if (Health <= LowHealthThreshold)
                {
                    SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, LowHealthColour.R, LowHealthColour.G, LowHealthColour.B, 255);
                }
                else if (Health > LowHealthThreshold
                    && Health <= MediumHealthThreshold)
                {
                    SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, MediumHealthColour.R, MediumHealthColour.G, MediumHealthColour.B, 255);
                }
                else
                {
                    SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, HighHealthColour.R, HighHealthColour.G, HighHealthColour.B, 255);
                }

                SDL.SDL_RenderDrawLineF(SDL_Renderer.RendererPtr, (float)HBPositionX - (float)SDL_Renderer.CCameraPosition.X, (float)HBPositionY - (float)SDL_Renderer.CCameraPosition.Y, HealthBarLine1EndXPos - (float)SDL_Renderer.CCameraPosition.X, (float)HBPositionY - (float)SDL_Renderer.CCameraPosition.Y);

                SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, HealthBarColour2.R, HealthBarColour2.G, HealthBarColour2.B, 255);

                SDL.SDL_RenderDrawLineF(SDL_Renderer.RendererPtr, (float)HealthBarLine2StartXPos - (float)SDL_Renderer.CCameraPosition.X, (float)HBPositionY - (float)SDL_Renderer.CCameraPosition.Y, HealthBarLine2EndXPos - (float)SDL_Renderer.CCameraPosition.X, (float)HBPositionY - (float)SDL_Renderer.CCameraPosition.Y);

                // Reset draw colour. 
                SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, 0, 0, 0, 255);
            }

            base.Render(SDL_Renderer, Tx);
        }

        private void Kill()
        {
            // There are no respawn points
            if (RespawnPoint != null)
            {
                Position = RespawnPoint;
            }

            Health = MaxHealth; 
        }
    }
}
