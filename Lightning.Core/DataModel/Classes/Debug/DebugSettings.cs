#if DEBUG
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// DebugSettings
    /// 
    /// November 13, 2021
    /// 
    /// Defines settings for the IGDService.
    /// </summary>
    public class DebugSettings
    {
        /// <summary>
        /// Hitbox display debug
        /// </summary>
        public bool DisplayHitboxes { get; set; }

        /// <summary>
        /// Collision display debug
        /// </summary>
        public bool DisplayCollision { get; set; }

        /// <summary>
        /// The current debug string.
        /// </summary>
        internal string CurrentDebugString { get; set; }
        internal int _windowwidth { get; set; }

        /// <summary>
        /// The current window width.
        /// </summary>
        internal int WindowWidth
        {
            get
            {
                return _windowwidth;
            }
            set
            {
                _windowwidth = value;

                if (WindowHeight > 0) WindowSize = new Vector2(WindowWidth, WindowHeight);
            }
        }

        /// <summary>
        /// Backing field for <see cref="WindowHeight"/>.
        /// </summary>
        private int _windowheight { get; set; }

        /// <summary>
        /// The current window height.
        /// </summary>
        internal int WindowHeight
        {
            get
            {
                return _windowheight;
            }
            set
            {
                _windowheight = value;

                if (WindowWidth > 0) WindowSize = new Vector2(WindowWidth, WindowHeight);
            }
        }

        internal Vector2 WindowSize { get; set; } // bad, remove at some point
    }
}
#endif