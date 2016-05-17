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
    public class MapState : IGame
    {
        enum State : int { MapState = 1, FightState, HeroInfo, MenuState }
        State state = new State();
        const int tileSize = 75;
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

        public static SpriteFont spriteFont;
        public static string mapFileName;

        Map map;
        Game game;

        public MapState(SpriteBatch spriteBatch)
        {
            listBot = new List<MapBot>();
            this.spriteBatch = spriteBatch;
            mapFileName = "map.txt";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize(Game game)
        {
            this.game = game;
            mapHero = new MapHero(new Vector2(tileSize, tileSize));
            listBot.Add(new MapBot(new Vector2(2* tileSize, 2* tileSize)));
            listBot.Add(new MapBot(new Vector2(3* tileSize, 3* tileSize)));
            camera = new Camera(game.GraphicsDevice.Viewport);
            map = new Map();

            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            mapHero.LoadContent(Content, "гг3");
            listBot[0].LoadContent(Content, "зай1");
            listBot[1].LoadContent(Content, "зай2");
            Tiles.Content = Content;
            using (StreamReader stream = new StreamReader(mapFileName))
            {
                map.LoadMap(stream, tileSize);
            }
            foreach(MapBot bot in listBot)
            {
                bot.AddMobMap(map);
            }
            backgroundTexture = Content.Load<Texture2D>("Back1");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
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
                    bot.Position = new Vector2(-tileSize, -tileSize);
                    currentBot = bot;
                    state = State.FightState;
                    return (int)state;
                }
            }
            //Заход в инвентарь
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                state = State.HeroInfo;
                return (int)state;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.M))
            {
                state = State.MenuState;
                return (int)state;
            }
            state = State.MapState;
            return (int)state;
           
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
            spriteBatch.DrawString(spriteFont, mapHero.Counter.ToString(), 
                new Vector2(0, -2*tileSize), Color.White);           
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(mapFileName);
            mapHero.Save(writer);
            writer.WriteLine(listBot.Count.ToString());
            foreach(MapBot bot in listBot)
            {
                bot.Save(writer);
                writer.WriteLine(bot.Texture.Name);
            }
            writer.WriteLine(this.spritePosition.X + "#" + this.spritePosition.Y + "#" + 
                this.spriteRectangle);
        }

        public void Load(StreamReader reader)
        {
            mapFileName = reader.ReadLine();
            mapHero.Load(reader);
            int botNum = int.Parse(reader.ReadLine());
            listBot = new List<MapBot>();
            for(int i=0;i<botNum;i++)
            {
                MapBot bot = new MapBot(new Vector2(0, 0));
                bot.Load(reader);
                bot.LoadContent(game.Content, reader.ReadLine());
                listBot.Add(bot);
                bot.AddMobMap(map);
                map.GetRectangle(bot.Rectangle.Center).Mob = true;
            }
            string[] line=reader.ReadLine().Split(new string[]{"#",":","{","}","X","Y","Width","Height"},StringSplitOptions.RemoveEmptyEntries);
            this.spritePosition.X = int.Parse(line[0]);
            this.spritePosition.Y = int.Parse(line[1]);
            this.spriteRectangle.X = int.Parse(line[3]);
            this.spriteRectangle.Width = int.Parse(line[4]);
            this.spriteRectangle.Height = int.Parse(line[5]);
        }
    }
}
