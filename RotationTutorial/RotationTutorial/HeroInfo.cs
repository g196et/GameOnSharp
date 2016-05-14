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
        public Hero Hero { get { return hero; } set { hero = value; } }
        Button addStrength;
        Button addStamina;
        Button addIntellect;
        Texture2D buttonTexture;
        public HeroInfo()
        {
            addIntellect = new Button(new Rectangle(225, 125, 20, 20), "+");
            addIntellect.Action += () =>
            {
                if (hero.StatPoints > 0)
                {
                    hero.StatPoints -= 1;
                    hero.Intellect += 1;
                }
            };
            addStamina = new Button(new Rectangle(225, 75, 20, 20), "+");
            addStamina.Action += () =>
            {
                if (hero.StatPoints > 0)
                {
                    hero.StatPoints -= 1;
                    hero.Stamina += 1;
                }
            };
            addStrength = new Button(new Rectangle(225, 25, 20, 20), "+");
            addStrength.Action += () =>
            {
                if (hero.StatPoints > 0)
                {
                    hero.StatPoints -= 1;
                    hero.Strength += 1;
                }
            };
        }
        public void Initialize(Game game)
        {}
        public void LoadContent(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("ButtonTexture1");
        }
        public void UnloadContent()
        { }
        public int Update(GameTime gameTime)
        {
            addIntellect.Update(gameTime);
            addStamina.Update(gameTime);
            addStrength.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                return 1;
            return 3;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.spriteFront, "Strength = " + hero.Strength,
                new Vector2(50, 25), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "Stamina = " + hero.Stamina,
                new Vector2(50, 75), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "Intellect = " + hero.Intellect,
                new Vector2(50, 125), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "StatPoints = " + hero.StatPoints,
                new Vector2(50, 175), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "curEXP = " + hero.Level.CurrentExperience,
                new Vector2(50, 225), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "HP = " + hero.Health.Current+"/"+
                hero.Health.Max, new Vector2(50, 275), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "MP = " + hero.Mana.Current + "/"
                + hero.Mana.Max, new Vector2(50, 325), Color.White);
            spriteBatch.Draw(buttonTexture, addStrength.Rectangle, Color.White);
            spriteBatch.Draw(buttonTexture, addStamina.Rectangle, Color.White);
            spriteBatch.Draw(buttonTexture, addIntellect.Rectangle, Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "+", 
                new Vector2(addStrength.Rectangle.X,addStrength.Rectangle.Y), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "+",
                new Vector2(addStamina.Rectangle.X, addStamina.Rectangle.Y), Color.White);
            spriteBatch.DrawString(Game1.spriteFront, "+",
                new Vector2(addIntellect.Rectangle.X, addIntellect.Rectangle.Y), Color.White);
        }
    }
}
