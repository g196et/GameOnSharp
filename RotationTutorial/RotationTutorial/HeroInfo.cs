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
    class HeroInfo: IGame
    {
        Hero hero;
        public HeroInfo(Hero hero)
        {
            this.hero = hero;
        }
        public void Initialize(Game game)
        {}
        public void LoadContent(ContentManager content)
        { }
        public void UnloadContent()
        { }
        public int Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                return 1;
            return 3;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.spriteFront, "Strength = " + hero.Strength, new Vector2(50, 25), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "Stamina = " + hero.Stamina, new Vector2(50, 75), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "Intellect = " + hero.Intellect, new Vector2(50, 125), Color.White);
        }
    }
}
