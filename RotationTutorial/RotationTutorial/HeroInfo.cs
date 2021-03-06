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
using System.IO;

namespace RotationTutorial
{
    public class HeroInfo: IGame
    {
        enum State : int { MapState = 1, FightState, HeroInfo, MenuState }
        State state = new State();
        const int constStats = 10;
        const int x = 50;
        const int y = 25;
        const int x2 = 225;
        Hero hero;
        public Hero Hero { get { return hero; } set { hero = value; } }
        Button addStrength;
        Button addStamina;
        Button addIntellect;
        Button addVitality;
        Texture2D buttonTexture;
        public Song Song { get; set; }
        public HeroInfo(Hero hero)
        {
            this.Hero = hero;
            addIntellect = new Button(new Rectangle(x2, y*5, 20, 20), "+");
            
            addStamina = new Button(new Rectangle(x2, y*3, 20, 20), "+");
            
            addStrength = new Button(new Rectangle(x2, y, 20, 20), "+");
            
            addVitality = new Button(new Rectangle(x2, y * 7, 20, 20), "+");
            
        }
        public void Initialize(Game game)
        {
            addIntellect.Action += () =>
            {
                if (hero.StatPoints > 0)
                {
                    hero.StatPoints -= 1;
                    hero.Intellect += 1;
                    hero.Mana.Max = hero.Intellect * constStats;
                    hero.Mana.Current = Hero.Mana.Max;
                }
            };
            addStamina.Action += () =>
            {
                if (hero.StatPoints > 0)
                {
                    hero.StatPoints -= 1;
                    hero.Stamina += 1;
                    hero.Energy.Max = hero.Stamina * constStats;
                    hero.Energy.Current = Hero.Energy.Max;
                }
            }; 
            addStrength.Action += () =>
            {
                if (hero.StatPoints > 0)
                {
                    hero.StatPoints -= 1;
                    hero.Strength += 1;
                }
            }; 
            addVitality.Action += () =>
            {
                if (hero.StatPoints > 0)
                {
                    hero.StatPoints -= 1;
                    hero.Vitality += 1;
                    hero.Health.Max = hero.Vitality * constStats;
                    hero.Health.Current += constStats;
                }
            };
        }
        public void LoadContent(ContentManager content)
        {
            hero.LoadItems(content);
            buttonTexture = content.Load<Texture2D>("ButtonTexture0");
            Song = content.Load<Song>("song");
        }
        public void UnloadContent()
        { }
        public int Update(GameTime gameTime)
        {
            addIntellect.Update(gameTime);
            addStamina.Update(gameTime);
            addStrength.Update(gameTime);
            addVitality.Update(gameTime);
            hero.Inventory.Update();
            hero.Armor = hero.Inventory.CurrentArmor;
            hero.Weapon = hero.Inventory.CurrentWeapon;
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                state = State.MapState;
                return (int)state;
            }
            state = State.HeroInfo;
            return (int)state;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MapState.spriteFont, "Vitality = " + hero.Vitality,
                new Vector2(x, y*7), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, "Strength = " + hero.Strength,
                new Vector2(x, y), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, "Stamina = " + hero.Stamina,
                new Vector2(x, 3*y), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, "Intellect = " + hero.Intellect,
                new Vector2(x, 5*y), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, "StatPoints = " + hero.StatPoints,
                new Vector2(x, 9*y), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, "curEXP = " + hero.Level.CurrentExperience,
                new Vector2(x, 11*y), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, "HP = " + hero.Health.Current+"/"+
                hero.Health.Max, new Vector2(x, 13*y), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, "MP = " + hero.Mana.Current + "/"
                + hero.Mana.Max, new Vector2(x, 15*y), Color.White);
            if (hero.StatPoints > 0)
            {
                spriteBatch.Draw(buttonTexture, addStrength.Rectangle, Color.White);
                spriteBatch.Draw(buttonTexture, addStamina.Rectangle, Color.White);
                spriteBatch.Draw(buttonTexture, addIntellect.Rectangle, Color.White);
                spriteBatch.Draw(buttonTexture, addVitality.Rectangle, Color.White);
            }
            spriteBatch.DrawString(MapState.spriteFont,"Energy = "+hero.Energy.Max,
                new Vector2(x, 17*y), Color.White);
            int Int = 0;
            if(hero.Weapon!=null)
            {
                Int = hero.Weapon.Damage;
            }
            spriteBatch.DrawString(MapState.spriteFont, "Damage = " + Int,
                new Vector2(x, 19 * y), Color.White);
            if (hero.Armor != null)
            {
                Int = hero.Armor.Defense;
            }
            else
            {
                Int = 0;
            }
            spriteBatch.DrawString(MapState.spriteFont, "Defense = " + Int,
                new Vector2(x, 21 * y), Color.White);
            hero.Inventory.Draw(spriteBatch);
        }
        public void Save(StreamWriter writer)
        {
            hero.Save(writer);
        }
        public void Load(StreamReader reader)
        {
            hero.Load(reader);
        }
    }
}
