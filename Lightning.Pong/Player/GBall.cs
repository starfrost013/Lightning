using Lightning.Core.API; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Pong
{
    public class GBall
    {
        /// <summary>
        /// The circle of this ball.
        /// </summary>
        public Circle Circle { get; set; }
        
        /// <summary>
        /// The velocity of the ball.
        /// </summary>
        public Vector2 Velocity { get; set; }   
        
        public GBall()
        {
            Velocity = new Vector2();
        }
    }
}
