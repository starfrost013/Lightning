namespace Lightning.Core.API
{
    /// <summary>
    /// PhysicsState
    /// 
    /// July 23, 2021
    /// 
    /// Defines the physics state.
    /// </summary>
    public class PhysicsState
    {
        /// <summary>
        /// The current gravitational pull
        /// </summary>
        public Vector2 Gravity { get; set; }

        /// <summary>
        /// The current physics mode - see <see cref="GravityState"/>.
        /// </summary>
        public GravityState GravityState { get; set; }

        internal static readonly Vector2 GravityDefaultValue = new Vector2(0, -0.01);

        /// <summary>
        /// The boundary under which any object will be destroyed by the PhysicsService.
        /// </summary>
        public Vector2 ObjectKillBoundary { get; set; }

        internal static readonly Vector2 ObjectKillBoundaryDefaultValue = new Vector2(2147483647, -1000);

        /// <summary>
        /// The terminal velocity of this physics engine.
        /// </summary>
        public Vector2 TerminalVelocity { get; set; }

        internal static readonly Vector2 TerminalVelocityDefaultValue = new Vector2(5, -1.6);

        /// <summary>
        /// Percentage to use for positionally correcting in the physics engine.
        /// </summary>
        public double PositionalCorrectionPercentage { get; set; }

        internal static readonly double PositionalCorrectionPercentageDefaultValue = 0.2;

        /// <summary>
        /// Slop to use for positionally corrcting in the physics engine.
        /// </summary>
        public double PositionalCorrectionSlop { get; set; }

        internal static readonly double PositionalCorrectionSlopDefaultValue = 0.01; 
    }
}
