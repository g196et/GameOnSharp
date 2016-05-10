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
        public MenuState()
        { }
        public void Initialize(Game game)
        { }
        public void LoadContent(ContentManager content)
        { }
        public void UnloadContent()
        { }
        public int Update(GameTime gameTime)
        { return 0; }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.spriteFront, "You lose!", new Vector2(0, 0), Color.White);
        }
    }
}
