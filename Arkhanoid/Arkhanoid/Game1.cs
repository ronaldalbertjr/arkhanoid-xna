using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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
        List<Block> blocks;

        const int PLAYER_OFFSET = 70;
        const float PLAYER_SPEED = 10f;
        const float BALL_SPEED = 4f;

        bool lost = false;
        bool win = false;
        bool game = false;
        bool menu = true;
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
            blocks = new List<Block>();
            lost = false;
            win = false;
            game = false;
            menu = true;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            player.Texture = Content.Load<Texture2D>("playerImage");
            player.Position = new Vector2(ScreenWidth / 2 - player.Texture.Width / 2, ScreenHeight - PLAYER_OFFSET);

            ball.Texture = Content.Load<Texture2D>("ballImage.png");
            ball.Launch(BALL_SPEED);

            for (int i = 0; i <= ScreenHeight / 2 ; i += Content.Load<Texture2D>("blockImage").Height + 30)
            {
                for (int j = 0; j <= ScreenWidth; j += Content.Load<Texture2D>("blockImage").Width + 20)
                {
                    blocks.Add(new Block());
                    blocks[blocks.Count - 1].Position = new Vector2(j, i);
                    blocks[blocks.Count - 1].Texture = Content.Load<Texture2D>("blockImage");
                }
            }

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
            if(menu)
            {
                if (Input.GetAnyKeyInput(PlayerIndex.One))
                {
                    menu = false;
                    game = true;
                }
            }
            else if (game)
            {
                player.Velocity = Input.GetKeyboardInputDirection(PlayerIndex.One) * PLAYER_SPEED;
                player.Move(player.Velocity);

                ball.Move(ball.Velocity);

                if (GameObject.CheckCollision(player, ball))
                {
                    ball.Velocity.Y = -Math.Abs(ball.Velocity.Y);
                }
                else if (ball.Position.Y < 0)
                {
                    ball.Velocity.Y = Math.Abs(ball.Velocity.Y);
                }
                else if (ball.Position.Y + ball.Texture.Height > ScreenHeight)
                {
                    lost = true;
                    game = false;
                    ball.Launch(BALL_SPEED);
                }
                foreach (Block b in blocks)
                {
                    if (GameObject.CheckCollision(b, ball) && !b.is_Hitten)
                    {
                        b.is_Hitten = true;
                        ball.Velocity.Y = Math.Abs(ball.Velocity.Y);
                        player.blocksDestroyed++;
                    }
                }
                if(player.blocksDestroyed >= blocks.Count)
                {
                    game = false;
                    win = true;
                }
            }
            else if (win)
            {
                if (Input.GetBackspaceInput(PlayerIndex.One))
                {
                    win = false;
                    menu = true;
                    blocks.RemoveAll(item => item == new Block());
                    for (int i = 0; i <= ScreenHeight / 2; i += Content.Load<Texture2D>("blockImage").Height + 30)
                    {
                        for (int j = 0; j <= ScreenWidth; j += Content.Load<Texture2D>("blockImage").Width + 20)
                        {
                            blocks.Add(new Block());
                            blocks[blocks.Count - 1].Position = new Vector2(j, i);
                            blocks[blocks.Count - 1].Texture = Content.Load<Texture2D>("blockImage");
                        }
                    }
                    ball.Launch(BALL_SPEED);
                }
            }
            else if(lost)
            {
                if (Input.GetBackspaceInput(PlayerIndex.One))
                {
                    lost = false;
                    menu = true;
                    blocks.RemoveAll(item => item == new Block());
                    for (int i = 0; i <= ScreenHeight / 2; i += Content.Load<Texture2D>("blockImage").Height + 30)
                    {
                        for (int j = 0; j <= ScreenWidth; j += Content.Load<Texture2D>("blockImage").Width + 20)
                        {
                            blocks.Add(new Block());
                            blocks[blocks.Count - 1].Position = new Vector2(j, i);
                            blocks[blocks.Count - 1].Texture = Content.Load<Texture2D>("blockImage");
                        }
                    }
                    ball.Launch(BALL_SPEED);
                }
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            if(menu)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("menuImage"), new Vector2(0, 0), Color.White);
            }
            else if (game)
            {
                player.Draw(spriteBatch);
                ball.Draw(spriteBatch);
                foreach (Block b in blocks)
                {
                    if (!b.is_Hitten)
                    {
                        b.Draw(spriteBatch);
                    }
                }
            }
            else if(win)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("winImage"), new Vector2(0, 0), Color.White);
            }
            else if(lost)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("lostImage"), new Vector2(0, 0), Color.White);
            }
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
