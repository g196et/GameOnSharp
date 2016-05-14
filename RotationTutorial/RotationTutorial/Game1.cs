﻿using System;
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
    public class Game1 : IGame
    {
        SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }
        public Rectangle SpriteRectangle { get { return spriteRectangle; } }
        public Vector2 SpritePosition { get { return spritePosition; } }

        Vector2 spritePosition;
        Rectangle spriteRectangle;
        MapHero mapHero;
        IList<MapBot> listBot;
        public IList<MapBot> ListBot { get { return listBot; } set { listBot = value; } }
        MapBot currentBot;
        public MapBot CurrentBot { get { return currentBot; } }
        Camera camera;

        //Background
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;

        public static SpriteFont spriteFront;

        Map map;
        //Vector2 distance;

        public Game1(SpriteBatch spriteBatch)
        {
            listBot = new List<MapBot>();
            this.spriteBatch = spriteBatch;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize(Game game)
        {
            mapHero = new MapHero(game.Content.Load<Texture2D>("гг3"), new Vector2(75, 75));
            listBot.Add(new MapBot(game.Content.Load<Texture2D>("зай1"), new Vector2(150, 150), new Enemy()));
            listBot.Add(new MapBot(game.Content.Load<Texture2D>("зай2"), new Vector2(225, 225), new Enemy()));
            camera = new Camera(game.GraphicsDevice.Viewport);
            map = new Map();

            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
           
            Tiles.Content = Content;
            map.Generate(new int[,]{
                {2,2,2,2,2,2,2,},
                {2,1,1,1,1,1,2,},
                {2,1,1,1,2,1,2,},
                {2,1,2,1,1,1,2,},
                {2,1,2,1,1,1,2,},
                {2,1,1,1,1,1,2,},
                {2,2,2,2,2,2,2,},
            }, 75);
            foreach(MapBot bot in listBot)
            {
                bot.AddMobMap(map);
            }
            backgroundTexture = Content.Load<Texture2D>("Back1");
            spriteFront = Content.Load<SpriteFont>("SpriteFont1");
            backgroundPosition = new Vector2(-950, -500);
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
        public int Update(GameTime gameTime)
        {
            
            int smth=1;
            spritePosition = mapHero.Position;
            spriteRectangle = mapHero.Rectangle1;
            camera.Update(gameTime, this);
            mapHero.Update(gameTime, map);
            foreach(MapBot bot in listBot)
            {
                mapHero.CheckAtack(bot);
                       
                if (mapHero.CheckAtackField)
                {
                    mapHero.CheckAtackField = false;
                    map.GetRectangle(new Point((int)mapHero.Rectangle1.Center.X, 
                        (int)mapHero.Rectangle1.Center.Y)).Mob = false;
                    bot.Position = new Vector2(-75, -75);
                    currentBot = bot;
                    smth = 2;
                    return smth;
                }
            }
            //Заход в инвентарь
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                smth = 3;
            }
            return smth;
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch) 
        {
            map.Draw(spriteBatch);
            foreach (MapBot bot in listBot)
            {
                if (map.GetRectangle(bot.Rectangle.Center).Mob)
                    bot.Draw(spriteBatch);
            }
            mapHero.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFront, mapHero.Counter.ToString(), new Vector2(0, -150), Color.White);           
        }
    }
}
