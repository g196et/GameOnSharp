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
    class MenuState : IGame
    {
        const int size1 = 100;
        const int size2 = 30;
        Button newGame;
        Button saveGame;
        Button loadGame;
        Button settings;
        Button quitGame;
        int state = 0;
        Texture2D buttonTexture;
        GameStateManager manager;
        public MenuState(GameStateManager manager)
        {
            this.manager = manager;
            newGame = new Button(new Rectangle(size1,size1,size1,size2), "New Game");
            saveGame = new Button(new Rectangle(size1,2*size1, size1, size2), "Save Game");
            loadGame = new Button(new Rectangle(size1, 3*size1, size1, size2), "Load Game");
            settings = new Button(new Rectangle(size1, 4*size1, size1, size2), "Settings");
            quitGame = new Button(new Rectangle(size1, 5*size1, size1, size2), "Quit Game");
            quitGame.Action += () =>
            {
                Environment.Exit(0);
            };
            saveGame.Action += () =>
            {
                FileInfo file = new FileInfo("SaveFile1.txt");
                if (!file.Exists)
                {
                    file.Create();
                }
                file.OpenWrite().Close();
                using (StreamWriter writer = new StreamWriter("SaveFile1.txt"))
                {
                    manager.Save(writer);                    
                }
            };
            loadGame.Action += () =>
            {
                FileInfo file = new FileInfo("SaveFile1.txt");
                if (!file.Exists)
                {
                    return;
                }
                file.OpenWrite().Close();
                using (StreamReader reader = new StreamReader("SaveFile1.txt"))
                {
                    manager.Load(reader);
                    state = 1;
                }
            };
            
        }
        public void Initialize(Game game)
        { }
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
            return state;
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
