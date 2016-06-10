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
    public class MenuState : IGame
    {
        enum State {MenuState=0,MapState=1,SettingsState=5}
        const int size1 = 100;
        const int size2 = 30;
        Button newGame;
        Button saveGame;
        Button loadGame;
        Button settings;
        Button quitGame;
        int state;
        public string Text { get; set; }
        Texture2D buttonTexture;
        GameStateManager manager;
        public Song Song { get; set; }
        public MenuState(GameStateManager manager)
        {
            state = (int)State.MenuState;
            Text = string.Empty;
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
                StreamWriter writer;
                if (!file.Exists)
                {
                    writer = file.CreateText();
                }
                else
                {
                    writer = new StreamWriter("SaveFile1.txt");
                }
                
                manager.Save(writer);   
                writer.Close(); 
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
                    state = (int)State.MapState;
                }
            };
            newGame.Action += () =>
            {
                manager.NewMapState(1);
                manager.Hero = new Hero();
                manager.Hero.LoadItems(manager.Content);
                manager.MapState.Hero.LoadItems(manager.Content);
                Text = string.Empty;
            };
            settings.Action += () =>
            {
                state = (int)State.SettingsState;
            };
            
        }
        public void Initialize(Game game)
        { }
        public void LoadContent(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("ButtonTexture1");
            newGame.LoadContent(content);
            saveGame.LoadContent(content);
            loadGame.LoadContent(content);
            settings.LoadContent(content);
            quitGame.LoadContent(content);
            Song = content.Load<Song>("song");
            
        }
        public void UnloadContent()
        { }
        public int Update(GameTime gameTime)
        {
            state = (int)State.MenuState;
            newGame.Update(gameTime);
            saveGame.Update(gameTime);
            loadGame.Update(gameTime);
            settings.Update(gameTime);
            quitGame.Update(gameTime);
            if(Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                state = (int)State.MapState;
            }
            return state;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MapState.spriteFont, Text, new Vector2(0, 0), Color.White);
            newGame.Draw(spriteBatch);
            saveGame.Draw(spriteBatch);
            loadGame.Draw(spriteBatch);
            settings.Draw(spriteBatch);
            quitGame.Draw(spriteBatch);
        }
    }
}
