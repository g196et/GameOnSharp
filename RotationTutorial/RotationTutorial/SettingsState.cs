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
    public class SettingsState:IGame
    {
        enum State : int {  MenuState=4, SettingsState=5 };
        const string yes = "yes";
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;
        SettingsButtons resolutionButton;
        Button accept;
        Button back;
        SettingsButtons fullscreen;
        TrackBar volumeBar;
        int state;
        public Song Song { get; set; }

        public SettingsState(GraphicsDeviceManager graphics)
        {
            state = (int)State.SettingsState;
            List<string> list = new List<string>();
            list=new string[]{"1366:768","1280:800","640:480","1920:1080"}.ToList<string>();
            resolutionButton = new SettingsButtons(list, new Rectangle(350, 300, 250, 250));
            accept = new Button(new Rectangle(100, 100, 50, 50), "Accept");
            back = new Button(new Rectangle(100, 200, 50, 50), "Back");
            list=new string[] {"no","yes"}.ToList<string>();
            fullscreen = new SettingsButtons(list,new Rectangle(350,400,250,250));
            this.graphics = graphics;
            volumeBar = new TrackBar(400, 100, 100, 25);
        }
        
        public void Initialize(Game game)
        {
            
            this.graphicsDevice = game.GraphicsDevice;
            accept.Action += () =>
            {
                string[] resolution=resolutionButton.CurValue.Split(
                    new char[]{' ',':'},StringSplitOptions.RemoveEmptyEntries);
                graphics.PreferredBackBufferWidth=int.Parse(resolution[0]);
                graphics.PreferredBackBufferHeight = int.Parse(resolution[1]);
                if(fullscreen.CurValue==yes)
                {
                    graphics.IsFullScreen = true;
                }
                else
                {
                    graphics.IsFullScreen = false;
                }
                graphics.ApplyChanges();
                
            };
            back.Action += () =>
            {
                state = (int)State.MenuState;
            };
            resolutionButton.Initialize(game);
            fullscreen.Initialize(game);
        }

        public void LoadContent(ContentManager content)
        {
            accept.LoadContent(content);
            back.LoadContent(content);
            resolutionButton.LoadContent(content);
            fullscreen.LoadContent(content);
            Song = content.Load<Song>("song");
            volumeBar.LoadContent(content);
        }

        public void UnloadContent()
        {
            
        }

        public int Update(GameTime gameTime)
        {
            state = (int)State.SettingsState;
            resolutionButton.Update(gameTime);
            accept.Update(gameTime);
            back.Update(gameTime);
            fullscreen.Update(gameTime);
            volumeBar.Update();
            return state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            resolutionButton.Draw(spriteBatch);
            accept.Draw(spriteBatch);
            back.Draw(spriteBatch);
            fullscreen.Draw(spriteBatch);
            volumeBar.Draw(spriteBatch);
            
        }
    }
}
