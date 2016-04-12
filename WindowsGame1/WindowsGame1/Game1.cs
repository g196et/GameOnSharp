using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Game World
        Texture2D ballTexture;
        Rectangle ballRectangle;

        Texture2D batTexture;
        Rectangle batRectangle;

        Vector2 velocity;

        Random myRandom = new Random();

        //Screen Parameters
        int screenWidth;
        int screenHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("ball");
            ballRectangle = new Rectangle(300, 350, 32, 32);

            batTexture = Content.Load<Texture2D>("ground");
            batRectangle = new Rectangle(250, 350, 100, 20);


            velocity.X = 3f;
            velocity.Y = 3f;

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ballRectangle.X = ballRectangle.X + (int)velocity.X;
            ballRectangle.Y = ballRectangle.Y - (int)velocity.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                batRectangle.X += 6;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                batRectangle.X -= 6;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ballRectangle.X = (screenWidth / 2) - (ballTexture.Width / 2);
                ballRectangle.Y = (screenHeight / 2) - (ballRectangle.Height / 2);
                RandomLoad();
            }

            if (ballRectangle.Intersects(batRectangle))
                velocity.Y = -velocity.Y;

            if (ballRectangle.X <= 0)
                velocity.X = -velocity.X;
            if (ballRectangle.X + ballTexture.Width >= screenWidth)
                velocity.X = -velocity.X;

            if (ballRectangle.Y <= 0)
                velocity.Y = -velocity.Y;
            if (ballRectangle.Y + ballTexture.Height >= screenHeight)
                velocity.Y = -velocity.Y;

            base.Update(gameTime);
        }

        void RandomLoad()
        {
            int random = myRandom.Next(0, 4);
            if (random == 0)
            {
                velocity.X = 3f;
                velocity.Y = 3f;
            }
            if (random == 1)
            {
                velocity.X = -3f;
                velocity.Y = 3f;
            }
            if (random == 2)
            {
                velocity.X = -3f;
                velocity.Y = -3f;
            }
            if (random == 3)
            {
                velocity.X = 3f;
                velocity.Y = -3f;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(ballTexture, ballRectangle, Color.White);
            spriteBatch.Draw(batTexture, batRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
