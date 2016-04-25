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
    class Game2:IGame
    {
        SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }
        public Rectangle SpriteRectangle { get { return spriteRectangle; } }
        public Vector2 SpritePosition { get { return spritePosition; } }
        ContentManager Content;
        Vector2 spritePosition;
        Rectangle spriteRectangle;
        MapBot mapBot; public MapBot mapBot1 { get { return mapBot; } set { mapBot = value; } }

        //Background
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;

        public static SpriteFont spriteFront;

        //Vector2 distance;
        Texture2D texture;
        public Game2(ContentManager Content)
        {
            this.Content = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize(Game game)
        {


            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
           texture = Content.Load<Texture2D>("Back1");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public bool Update(GameTime gameTime)
        {
            spriteRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                return true;
            else
                return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch) 
        {
            texture = Content.Load<Texture2D>("Back1");
            spriteBatch.Draw(texture, spriteRectangle, Color.AliceBlue);
            
        }
    }
}
