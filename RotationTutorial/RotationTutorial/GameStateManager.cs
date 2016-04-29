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
    class GameStateManager:Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;

        IGame CurrentState;
        Game1 mapState;
        Game2 fightState;
        public GameStateManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight =800;
            mapState = new Game1(spriteBatch);
            fightState = new Game2(Content, graphics);
            CurrentState = mapState;
            
        }
        protected override void Initialize()
        {
            CurrentState.Initialize(this);
            camera = new Camera(this.GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);           
            CurrentState.LoadContent(Content);
            fightState.LoadContent(Content);
        }
        protected override void UnloadContent()
        {
            CurrentState.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (CurrentState.Update(gameTime))
                if (CurrentState == mapState)
                {
                    CurrentState = fightState;
                    fightState.Initialize(this);
                }
                else
                    CurrentState = mapState;
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
