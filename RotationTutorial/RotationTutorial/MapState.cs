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
        const int tileSize = 75;
        const int mapCount = 2;
        const string mapName = "map";
        const string botVector = "botVector";
        const string botName = "botName";
        int currenMap;
        Vector2 spritePosition;
        Rectangle spriteRectangle;
        MapHero mapHero;
        MapBot currentBot;
        Map map;
        Game game;
        Camera camera;
        IList<MapBot> listBot;
        List<string> listMapFileName;
        public static SpriteFont spriteFont;
        public static string mapFileName;
        Hero hero;
        public Hero Hero { get { return hero; } set { hero = value; } }
        enum State : int { MapState = 1, FightState, HeroInfo, MenuState }
        State state = new State();

        GameStateManager GSM;

        public Rectangle SpriteRectangle { get { return spriteRectangle; } }
        public Vector2 SpritePosition { get { return spritePosition; } }

        public IList<MapBot> ListBot { get { return listBot; } set { listBot = value; } }

        public MapBot CurrentBot { get { return currentBot; } }


        public MapState(GameStateManager GSM,Hero hero,int currentMap = 1)
        {
            this.hero = hero;
            this.GSM = GSM;
            this.currenMap = currentMap;
            listMapFileName = new List<string>();
            for (int i = 1; i <= mapCount; i++)
            {
                listMapFileName.Add(mapName + i.ToString() + ".txt");
            }
            listBot = new List<MapBot>();
            map = new Map();
            hero = new Hero();
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
            using (StreamReader reader = new StreamReader(botVector + currenMap + ".txt"))
            {
                while (!reader.EndOfStream)
                {
                    string[] str = reader.ReadLine().Split(' ');
                    listBot.Add(new MapBot(new Vector2(int.Parse(str[0]) * tileSize, int.Parse(str[1]) * tileSize),
                        int.Parse(str[2]), int.Parse(str[3]), int.Parse(str[4]), int.Parse(str[5])));
                }
            }
            camera = new Camera(game.GraphicsDevice.Viewport);
            using (StreamReader stream = new StreamReader(listMapFileName[currenMap - 1]))
            {
                map.LoadMap(stream, tileSize);
            }            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            mapHero.LoadContent(Content, "гг3");
            using (StreamReader reader = new StreamReader(botName + currenMap + ".txt"))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    listBot[i].LoadContent(Content, reader.ReadLine());
                    i++;
                }
            }
            Tiles.Content = Content;
            using (StreamReader stream = new StreamReader(listMapFileName[currenMap-1]))
            {
                map.LoadMap(stream, tileSize);
            }
            map.LoadContent(Content);
            foreach(MapBot bot in listBot)
            {
                bot.AddMobMap(map);
            }
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
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
            if (map.GetRectangle(new Point((int)mapHero.Rectangle1.Center.X,
                        (int)mapHero.Rectangle1.Center.Y)).Portal)
            {
                if (currenMap < mapCount)
                {
                    GSM.NewMapState(currenMap + 1);
                }
                else
                {
                    return (int)State.MenuState;
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
            writer.WriteLine(currenMap);
            mapHero.Save(writer);
            hero.Save(writer);
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
            //ПРОВЕРИТЬ!!!!
            currenMap = int.Parse(reader.ReadLine());
            this.LoadContent(game.Content);
            mapHero.Load(reader);
            hero.Load(reader);
            int botNum = int.Parse(reader.ReadLine());
            //listBot = new List<MapBot>();
            //for(int i=0;i<botNum;i++)
            //{
            //    MapBot bot = new MapBot(new Vector2(0, 0));
            //    bot.Load(reader);
            //    bot.LoadContent(game.Content, reader.ReadLine());
            //    listBot.Add(bot);
            //    bot.AddMobMap(map);
            //    map.GetRectangle(bot.Rectangle.Center).Mob = true;
            //}
            string[] line=reader.ReadLine().Split(new string[]{"#",":","{","}","X","Y","Width","Height"},StringSplitOptions.RemoveEmptyEntries);
            this.spritePosition.X = int.Parse(line[0]);
            this.spritePosition.Y = int.Parse(line[1]);
            this.spriteRectangle.X = int.Parse(line[3]);
            this.spriteRectangle.Width = int.Parse(line[4]);
            this.spriteRectangle.Height = int.Parse(line[5]);
        }
    }
}
