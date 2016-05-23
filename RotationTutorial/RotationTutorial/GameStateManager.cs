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
    public class GameStateManager:Game
    {
        const int width = 1280;
        const int height = 800;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //ContentManager contentHelp;

        Camera camera;
        enum State : int { MapState = 1, FightState, HeroInfoState, MenuState };
        public IGame CurrentState { get; set; }
        public MapState MapState { get; set; }
        public FightState FightState { get; set; }
        public HeroInfo HeroInfo { get; set; }
        public MenuState MenuState { get; set; }

        public GameStateManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            MapState = new MapState(this);
            FightState = new FightState(Content, graphics);
            CurrentState = MapState;
            MenuState = new MenuState(this);
            HeroInfo = new HeroInfo();
            IsMouseVisible = true;
            
        }
        protected override void Initialize()
        {
            MapState.Initialize(this);
            camera = new Camera(this.GraphicsDevice.Viewport);
            FightState.Initialize(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);           
            MapState.LoadContent(Content);
            HeroInfo.LoadContent(Content);
            MenuState.LoadContent(Content);

        }
        protected override void UnloadContent()
        {
            CurrentState.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            int state = CurrentState.Update(gameTime);
            if (state == (int)State.MapState)
                CurrentState = MapState;
            else if (state == (int)State.FightState)
            {
                if (FightState.Enemy != MapState.CurrentBot.Enemy)
                {
                    FightState.Enemy = MapState.CurrentBot.Enemy;
                    FightState.LoadContent(Content);
                }
                CurrentState = FightState;

            }
            else if (state == (int)State.HeroInfoState)
            {
                HeroInfo.Hero = FightState.Hero;
                CurrentState = HeroInfo;
                FightState.Hero = HeroInfo.Hero;
            }
            else if (state == (int)State.MenuState)
            {
                CurrentState = MenuState;
            }
                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null/*,
                camera.transform*/); 
            CurrentState.Draw(gameTime,spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void Save(StreamWriter writer)
        {
            MapState.Save(writer);
            HeroInfo.Save(writer);
        }
        public void Load(StreamReader reader)
        {
            MapState.Load(reader);
            HeroInfo.Load(reader);
        }
        public void NewMapState(int currentMap)
        {
            MapState = new MapState(this, currentMap);
            MapState.Initialize(this);
            MapState.LoadContent(Content);
            CurrentState = MapState;
        }

    }
}
