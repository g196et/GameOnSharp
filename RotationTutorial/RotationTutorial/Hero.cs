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
    class Hero:IPerson
    {
        const int consumptionEnergy = 50;
        const int position = 10;
        const int constStats = 10;
        IList<ISkill> listSkill = new List<ISkill>();
        Weapon weapon;
        public bool Check { get; set; }

        Texture2D texture;
        Rectangle rectangle;

        Rectangle manaRectangle;
        Rectangle energyRectangle;
        Rectangle healthRectangle;

        const int barWidth = 300;
        const int barHeight = 25;
        const int heroPositionX = 200;
        const int heroPositionY = 200;
        const int heroWidth = 250;
        const int heroHeight=400;

        Texture2D healthBarTexture;
        Texture2D manaBarTexture;
        Texture2D energyBarTexture;

        public Level Level { get; set; }
        /// <summary>
        /// отвечает за очки жизней
        /// </summary>
        public PointClass Health { get; set; }
        /// <summary>
        /// отвечает за очки маны
        /// </summary>
        public PointClass Mana { get; set; }
        /// <summary>
        /// отвечает за очки энергии
        /// </summary>
        public PointClass Energy { get; set; }
        /// <summary>
        /// стат силы
        /// </summary>
        public int Strength { get; set; }
        /// <summary>
        /// стат выносливости
        /// </summary>
        public int Stamina { get; set; }
        /// <summary>
        /// стат интелекта
        /// </summary>
        public int Intellect { get; set; }
        /// <summary>
        /// стат живучести
        /// </summary>
        public int Vitality { get; set; }
        /// <summary>
        /// нераспределённые очки статов
        /// </summary>
        public int StatPoints { get; set; }

        public Hero()
        {
            Level = new Level(new int[] { 50,150 });
            Strength = 2*constStats;
            Stamina = constStats;
            Intellect = constStats;
            Vitality = constStats;
            Health = new PointClass(constStats * Vitality, constStats * Vitality);
            Mana = new PointClass(constStats * Intellect, constStats * Intellect);
            Energy = new PointClass(constStats * Stamina, constStats * Stamina);
            weapon = new Weapon(5);
            listSkill.Add(new SkillRegenHealth());
            listSkill.Add(new SkillFireBall());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemyTexture"></param>
        /// <param name="healthBarTexture"></param>
        /// <param name="manaBarTexture"></param>
        /// <param name="energyBarTexture"></param>
        public void LoadContent(Texture2D heroTexture,Texture2D healthBarTexture, Texture2D manaBarTexture, Texture2D energyBarTexture)
        {
            this.healthBarTexture = healthBarTexture;
            this.manaBarTexture = manaBarTexture;
            this.energyBarTexture = energyBarTexture;
            healthRectangle = new Rectangle(position, position, barWidth,
                barHeight);
            manaRectangle = new Rectangle(healthRectangle.X,
                healthRectangle.Y + healthRectangle.Height + position, barWidth,
                barHeight);
            energyRectangle = new Rectangle(manaRectangle.X, manaRectangle.Y
                + manaRectangle.Height + position,
                barWidth, barHeight);
            texture = heroTexture;
            rectangle = new Rectangle(heroPositionX, heroPositionY, heroWidth, heroHeight);
        }

        /// <summary>
        /// дефолтная атака
        /// </summary>
        /// <param name="person">враг</param>
        /// <returns>true, если получилось, false в противном случае</returns>
        public bool Attack(IPerson person)
        {
            if (Energy.Current >= consumptionEnergy)
            {
                person.Health.Current = person.Health.Current - (Strength + weapon.Damage);
                Energy.Current -= consumptionEnergy;
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Изменяет длину баров
        /// </summary>
        public void Update()
        {
            healthRectangle.Width = healthBarTexture.Width * this.Health.Current / this.Health.Max;
            manaRectangle.Width = manaBarTexture.Width * this.Mana.Current / this.Mana.Max;
            energyRectangle.Width = energyBarTexture.Width * this.Energy.Current / this.Energy.Max;    
        }

        /// <summary>
        /// Обработка управления героем
        /// </summary>
        /// <param name="enemy">враг</param>
        /// <returns>true, если энергия осталась,false - иначе</returns>
        public bool Input(IPerson enemy)
        {
            if ((Energy.Current == 0) || (Keyboard.GetState().IsKeyDown(Keys.F)))
            {
                Energy.Current = Energy.Max;
                return false;
            }
            //Обычная атака
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (Attack(enemy))
                {
                    Check = true;
                    return true;
                }
            }
            //Регенерация здоровья
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if(listSkill[0].Effect(this, enemy))
                {
                    Check = true;
                    return true;
                }
            }
            //Fire ball
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                if(listSkill[1].Effect(this, enemy))
                {
                    Check = true;
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// отрисовка
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarTexture, healthRectangle, Color.White);
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.Draw(manaBarTexture, manaRectangle, Color.White);
            spriteBatch.Draw(energyBarTexture, energyRectangle, Color.White);
            spriteBatch.DrawString(MapState.spriteFont, Health.Current + "/" + Health.Max, new Vector2(healthRectangle.Center.X - 25, healthRectangle.Y), Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, Mana.Current + "/" + Mana.Max, new Vector2(manaRectangle.Center.X - 25, manaRectangle.Y), Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, Energy.Current + "/" + Energy.Max, new Vector2(energyRectangle.Center.X - 25, energyRectangle.Y), Color.Black);
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(this.Health + "#" + this.Mana + "#");
            writer.WriteLine(this.Strength+"#"+this.Stamina+"#"+this.Intellect+"#"+this.Vitality+"#"+
                this.StatPoints);
            this.Level.Save(writer);
            writer.WriteLine(listSkill.Count);

        }
        public void Load(StreamReader reader)
        {
            string[] line=reader.ReadLine().Split(new char[]{'#','/'},
                StringSplitOptions.RemoveEmptyEntries);
            this.Health.Current=int.Parse(line[0]);
            this.Health.Max = int.Parse(line[1]);
            this.Mana.Current = int.Parse(line[2]);
            this.Mana.Max = int.Parse(line[3]);
            line=reader.ReadLine().Split(new char[]{'#'},StringSplitOptions.RemoveEmptyEntries);
            this.Strength=int.Parse(line[0]);
            this.Stamina=int.Parse(line[1]);
            this.Intellect=int.Parse(line[2]);
            this.Vitality=int.Parse(line[3]);
            this.StatPoints = int.Parse(line[4]);
            this.Level.Load(reader);
        }
    }
}
