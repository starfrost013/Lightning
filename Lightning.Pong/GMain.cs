using Lightning.Core.API;
using System;
using System.Collections.Generic;

/// <summary>
/// Lightning Pong
/// 
/// ©2022 starfrost
/// </summary>
namespace Lightning.Pong
{
    public class GMain : App
    {
        /// <summary>
        /// Defines the players
        /// </summary>
        public List<GPlayer> Players { get; set; }

        /// <summary>
        /// Defines the ball
        /// </summary>
        public GBall Ball { get; set; }

        /// <summary>
        /// The window size (acquired from gamesettings)
        /// </summary>
        private Vector2 WindowSize { get; set; }

        private int AIError { get; set; }
        /// <summary>
        /// Run after the game has been loaded.
        /// </summary>
        public override void Start()
        {
            Players = new List<GPlayer>();
            Ball = new GBall();

            Workspace ws = DataModel.GetWorkspace();

            GetInstanceResult gir1 = ws.GetChild("Paddle1");
            GetInstanceResult gir2 = ws.GetChild("Paddle2");
            GetInstanceResult ballresult = ws.GetChild("Ball");

            // set up a 2 player game.
            // error checking would usually? be here
            Players.Add(new GPlayer(GPlayerMode.Player, (Rectangle)gir1.Instance));
            Players.Add(new GPlayer(GPlayerMode.AI, (Rectangle)gir2.Instance)); // horror
            Ball.Circle = (Circle)ballresult.Instance;

            Ball.Velocity.X = -4; // start it off going straight
            Ball.Velocity.Y = 1;

            Start_GetPosition();

            OnKeyDownHandler += OnKeyDown;
            
        }

        private void Start_GetPosition()
        {
            Workspace ws = DataModel.GetWorkspace();

            // error checking has already been done here

            GetInstanceResult gsresult = ws.GetFirstChildOfType("GameSettings");

            GameSettings gs = (GameSettings)gsresult.Instance;

            GetGameSettingResult ggsr1 = gs.GetSetting("WindowWidth");
            GetGameSettingResult ggsr2 = gs.GetSetting("WindowHeight");
            GetGameSettingResult ggsr3 = gs.GetSetting("Pong_AIError");

            GameSetting gswidth = ggsr1.Setting;
            GameSetting gsheight = ggsr2.Setting;
            GameSetting gsaierror = ggsr3.Setting;

            WindowSize = new Vector2((int)gswidth.SettingValue, (int)gsheight.SettingValue);
            AIError = (int)gsaierror.SettingValue;
        }

        /// <summary>
        /// Run each frame. Subscribe to the OnRender event to get the current rendering context.
        /// </summary>
        public override void Render()
        {
            // If I defined a custom class inheriting from rectangle etc etc I could
            // register Lightning events to this

            // but i am too lazy and this is a quick project as an example
            // so don't do this

            Ball.Circle.Position += Ball.Velocity;

            if (Ball.Circle.Position.X < 0
            || Ball.Circle.Position.X > WindowSize.X - Ball.Circle.Size.X) Ball.Velocity.X = -Ball.Velocity.X;

            if (Ball.Circle.Position.Y < 0
            || Ball.Circle.Position.Y > WindowSize.Y - Ball.Circle.Size.Y) Ball.Velocity.Y = -Ball.Velocity.Y;

            if (Ball.Circle.Position.X < 0) Ball.Circle.Position.X = Ball.Circle.Size.X;
            if (Ball.Circle.Position.Y < 0) Ball.Circle.Position.Y = Ball.Circle.Size.Y;

            if (Ball.Circle.Position.X > WindowSize.X - Ball.Circle.Size.X) Ball.Circle.Position.X = WindowSize.X - Ball.Circle.Size.X;

            if (Ball.Circle.Position.Y > WindowSize.Y - Ball.Circle.Size.Y) Ball.Circle.Position.Y = WindowSize.Y - Ball.Circle.Size.Y;
            
            Ball_CheckCollisionWithPaddles();
            Render_DoAI();

        }

        private void Ball_CheckCollisionWithPaddles()
        {
            // check for collision
            CollisionResult crpaddle0 = AABB.IsColliding(Ball.Circle, Players[0].Rectangle);
            CollisionResult crpaddle1 = AABB.IsColliding(Ball.Circle, Players[1].Rectangle);

            // flip ball velocity if colliding
            if (crpaddle0.Successful
            || crpaddle1.Successful)
            {
                Ball.Velocity = -Ball.Velocity;
            }
        }

        private void Render_DoAI()
        {
            if (Ball.Velocity.X < 0) return;

            GPlayer aiplayer = Players[1];
            aiplayer.Rectangle.Position += aiplayer.Velocity;

            
            if (aiplayer.Rectangle.Position.Y > Ball.Circle.Position.Y
            && Ball.Velocity.Y > 0)
            {
                aiplayer.Velocity.Y = -Ball.Velocity.Y;
            }
            else if (aiplayer.Rectangle.Position.Y < Ball.Circle.Position.Y
            && Ball.Velocity.Y > 0)
            {
                aiplayer.Velocity.Y = Ball.Velocity.Y;
            }

            if (aiplayer.Rectangle.Position.Y > Ball.Circle.Position.Y
            && Ball.Velocity.Y < 0)
            {
                aiplayer.Velocity.Y = Ball.Velocity.Y;
            }
            else if (aiplayer.Rectangle.Position.Y < Ball.Circle.Position.Y
            && Ball.Velocity.Y < 0)
            {
                aiplayer.Velocity.Y = -Ball.Velocity.Y;
            }

            if (aiplayer.Rectangle.Position.Y < 0)
            {
                aiplayer.Rectangle.Position.Y = aiplayer.Rectangle.Size.Y;
                aiplayer.Velocity = -aiplayer.Velocity;
            }

            if (aiplayer.Rectangle.Position.Y > WindowSize.Y - aiplayer.Rectangle.Size.Y)
            {
                aiplayer.Rectangle.Position.Y = WindowSize.Y - aiplayer.Rectangle.Size.Y;
                aiplayer.Velocity = -aiplayer.Velocity;
            }

        }
            /// <summary>
            /// Acquires the current player - see <see cref="GPlayer"/>.
            /// </summary>
            /// <returns></returns>
            private GPlayer GetPlayer()
            {
            foreach (GPlayer Player in Players)
            {
                if (Player.Mode == GPlayerMode.Player)
                {
                    return Player;
                }
            }

            return null;
        }

        private void OnKeyDown(object Sender, KeyEventArgs EventArgs)
        {
            GPlayer player = GetPlayer();

            switch (EventArgs.Key.KeySym)
            {
                case "UP":
                case "W":
                    player.Rectangle.Position.Y -= 7;
                    if (player.Rectangle.Position.Y < 0) player.Rectangle.Position.Y = player.Rectangle.Size.Y;
                    if (player.Rectangle.Position.Y > WindowSize.Y - player.Rectangle.Size.Y) player.Rectangle.Position.Y = WindowSize.Y - player.Rectangle.Size.Y;
                    return;
                case "DOWN":
                case "S":
                    player.Rectangle.Position.Y += 7;

                    if (player.Rectangle.Position.Y < 0) player.Rectangle.Position.Y = player.Rectangle.Size.Y;
                    if (player.Rectangle.Position.Y > WindowSize.Y - player.Rectangle.Size.Y) player.Rectangle.Position.Y = WindowSize.Y - player.Rectangle.Size.Y;
                    return; 
            }
        }

    }
}
