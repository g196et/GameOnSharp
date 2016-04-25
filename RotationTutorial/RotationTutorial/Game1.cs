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
using System.IO;

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
        List <MapBot> mapBot; public List<MapBot> mapBot1 { get { return mapBot; } }
        Camera camera;
        int[,] mapArray; public int[,] MapArray { get{ return mapArray;} }

        //Background
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;

        public static SpriteFont spriteFront;

        Map map;
        //Vector2 distance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 725;
            graphics.PreferredBackBufferHeight = 600;
        }

        public void AddMapBot(MapBot mapBot)
        {
            this.mapBot.Add(mapBot);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            mapHero = new MapHero(Content.Load<Texture2D>("man1"), new Vector2(75, 75));
            mapBot = new List<MapBot>();
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
            //Загрузка карты из файла
            using (StreamReader streamReader = new StreamReader(@"c:/c#/Map.txt")) 
            {
                string[] temp;
                int x = Int32.Parse(streamReader.ReadLine());
                int y = Int32.Parse(streamReader.ReadLine());
                mapArray = new int[x,y];
                for (int j = 0; j < y; j++ )
                {
                    temp = streamReader.ReadLine().Split((new char[] { ' ' }),
                        StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < x; i++)
                    {
                        mapArray[j, i] = Int32.Parse(temp[i]);
                    }
                }
            }
            map.Generate(mapArray, 75);
            using (StreamReader streamReader = new StreamReader(@"c:/c#/BotPosition.txt"))
            {
                string nameTexture = streamReader.ReadLine();
                int countBot = Int32.Parse(streamReader.ReadLine());
                string[] temp = streamReader.ReadLine().Split(new char[] { ',', ' ' },
                    StringSplitOptions.RemoveEmptyEntries); 
                Vector2 position;
                Texture2D botTexture = Content.Load<Texture2D>(nameTexture);
                for (int i = 0; i < countBot; i++)
                {
                    position = new Vector2(Int32.Parse(temp[i * 2]), Int32.Parse(temp[i * 2 + 1]));
                    mapBot.Add(new MapBot(botTexture, position));
                    map.GetTile(new Point((int)(position.X + mapBot[i].Rectangle.Width/2),
                        (int)(position.Y + mapBot[i].Rectangle.Width/2))).Mob = true;
                }
            }
            backgroundTexture = Content.Load<Texture2D>("Back1");
            spriteFront = Content.Load<SpriteFont>("SpriteFont1");
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
            //if (mapHero.CheckAtackField)
                //this.Exit();

            spritePosition = mapHero.Position;
            spriteRectangle = mapHero.Rectangle1;
            camera.Update(gameTime, this);
            mapHero.Update(gameTime, map);
            //Переделать
            mapHero.CheckAtack(mapBot[0]);
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
            foreach (MapBot bot in mapBot)
                if (map.GetTile(bot.Rectangle.Center).Mob)
                    bot.Draw(spriteBatch);
            mapHero.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFront, mapHero.Counter.ToString(), new Vector2(0, -150), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
