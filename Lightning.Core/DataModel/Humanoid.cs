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

        public void Render()
        {
            // Some rendering temp stuff for humanoids

            if (Health < 0) Kill(); 
        }


        private void Kill()
        {
            Position = RespawnPoint;
            Health = MaxHealth; 
        }
    }
}
