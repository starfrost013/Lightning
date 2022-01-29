using Lightning.Core.API; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Pong
{
    /// <summary>
    /// GPlayer
    /// 
    /// January 23, 2022
    /// 
    /// Defines a Pong player.
    /// </summary>
    public class GPlayer
    {
        /// <summary>
        /// The mode of this player - see <see cref="GPlayerMode"/>
        /// </summary>
        public GPlayerMode Mode { get; set; }
        
        /// <summary>
        /// The score of this player.
        /// </summary>
        public int Score { get; set; }


        /// <summary>
        /// The rectangle for this player.
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// The velocity of this object.
        /// </summary>
        public Vector2 Velocity { get; set; }

        public GPlayer()
        {
            Velocity = new Vector2();
        }

        public GPlayer(GPlayerMode NMode)
        {
            Mode = NMode;
            Velocity = new Vector2();
        }


        public GPlayer(GPlayerMode NMode, Rectangle Rect)
        {
            Mode = NMode;
            Rectangle = Rect;
            Velocity = new Vector2();
        }

        public GPlayer(GPlayerMode NMode, Rectangle Rect, int InitialScore)
        {
            Mode = NMode;
            Rectangle = Rect; 
            Score = InitialScore;
            Velocity = new Vector2();
        }
    }
}
