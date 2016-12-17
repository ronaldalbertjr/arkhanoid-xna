using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arkhanoid.Classes;

namespace Arkhanoid
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        Player player;
        Ball ball;

        const int PLAYER_OFFSET = 70;
        const float PLAYER_SPEED = 10f;
        const float BALL_SPEED = 4f;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            player = new Player();
            ball = new Ball();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.Texture = Content.Load<Texture2D>("playerImage");
            player.Position = new Vector2(ScreenWidth / 2 - player.Texture.Width / 2, ScreenHeight - PLAYER_OFFSET);

            ball.Texture = Content.Load<Texture2D>("ballImage");
            ball.Launch(BALL_SPEED);
            
        }
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            player.Velocity = Input.GetKeyboardInputDirection(PlayerIndex.One) * PLAYER_SPEED;
            player.Move(player.Velocity);

            ball.Move(ball.Velocity);

            if(GameObject.CheckCollision(player, ball))
            {
                ball.Velocity.Y = -Math.Abs(ball.Velocity.Y);
            }
            else if (ball.Position.Y < 0)
            {
                ball.Launch(BALL_SPEED);
            }
            else if (ball.Position.Y + ball.Texture.Height > ScreenHeight)
            {
                ball.Launch(BALL_SPEED);
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
