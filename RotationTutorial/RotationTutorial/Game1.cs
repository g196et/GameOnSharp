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

namespace RotationTutorial
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public Vector2 spritePosition;
        public Rectangle spriteRectangle;
        MapHero mapHero;
        Camera camera;

        //Background
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;


        Map map;
        //Vector2 distance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            mapHero = new MapHero(Content.Load<Texture2D>("man1"), new Vector2(0, 0));
            camera = new Camera(GraphicsDevice.Viewport);
            map = new Map();

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
            Tiles.Content = Content;
            map.Generate(new int[,]{
                {1,1,1,1,1,},
                {1,1,1,2,1,},
                {1,2,1,1,1,},
                {1,2,1,1,1,},
                {1,1,1,1,1,},
            }, 80);

            backgroundTexture = Content.Load<Texture2D>("Back1");
            backgroundPosition = new Vector2(-950, -500);
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

            spritePosition = mapHero.Position;
            spriteRectangle = mapHero.Rectangle1;
            camera.Update(gameTime, this);
            mapHero.Update(gameTime);
            foreach (Tiles tile in map.MapTiles)
                if (!tile.Passability)
                    mapHero.Collision(tile.Rectangle, map.Width, map.Height);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null,
                camera.transform);
            //spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            map.Draw(spriteBatch);
            mapHero.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
