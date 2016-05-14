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
    class GameStateManager:Game
    {
        const int width = 1280;
        const int height = 800;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //ContentManager contentHelp;

        Camera camera;

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
            MapState = new MapState(spriteBatch);
            FightState = new FightState(Content, graphics);
            CurrentState = MapState;
            MenuState = new MenuState();
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
            if (state == 1)
                CurrentState = MapState;
            else if (state == 2)
            {
                if (FightState.Enemy != MapState.CurrentBot.Enemy)
                {
                    FightState.Enemy = MapState.CurrentBot.Enemy;
                    FightState.LoadContent(Content);
                }
                CurrentState = FightState;

            }
            else if (state == 3)
            {
                HeroInfo.Hero = FightState.Hero;
                CurrentState = HeroInfo;
            }
            else if (state == 4)
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

    }
}
