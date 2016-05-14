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
    class MenuState : IGame
    {
        Button newGame;
        Button saveGame;
        Button loadGame;
        Button settings;
        Button quitGame;
        Texture2D buttonTexture;
        public MenuState()
        {
            newGame = new Button(new Rectangle(100,100,100,30), "New Game");
            saveGame = new Button(new Rectangle(100, 200, 100, 30), "Save Game");
            loadGame = new Button(new Rectangle(100, 300, 100, 30), "Load Game");
            settings = new Button(new Rectangle(100, 400, 100, 30), "Settings");
            quitGame = new Button(new Rectangle(100, 500, 100, 30), "Quit Game");
            
        }
        public void Initialize(Game game)
        {
            quitGame.Action += () =>
            {
                Environment.Exit(0);
            };
            newGame.Action += () =>
            {
                
            };
        }
        public void LoadContent(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("ButtonTexture1");
        }
        public void UnloadContent()
        { }
        public int Update(GameTime gameTime)
        {
            newGame.Update(gameTime);
            saveGame.Update(gameTime);
            loadGame.Update(gameTime);
            settings.Update(gameTime);
            quitGame.Update(gameTime);
            return 0;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MapState.spriteFont, "You lose!", new Vector2(0, 0), Color.White);
            spriteBatch.Draw(buttonTexture, newGame.Rectangle, Color.White);
            spriteBatch.Draw(buttonTexture, saveGame.Rectangle, Color.White);
            spriteBatch.Draw(buttonTexture, loadGame.Rectangle, Color.White);
            spriteBatch.Draw(buttonTexture, settings.Rectangle, Color.White);
            spriteBatch.Draw(buttonTexture, quitGame.Rectangle, Color.White);
        }
    }
}
