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
    public class Enemy:IPerson
    {
        const int consumptionEnergy = 50;
        const int minMyHelth = 40;
        const int minHisHelth = 35;
        const int constStats = 10;
        const int positionX = 900;
        const int positionY = 10;

        IList<ISkill> listSkill;
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

        Texture2D texture;
        Rectangle rectangle;

        Rectangle manaRectangle;
        Rectangle energyRectangle;
        Rectangle healthRectangle;

        Texture2D healthBarTexture;
        Texture2D manaBarTexture;
        Texture2D energyBarTexture;


        /// <summary>
        /// НЕ ЗНАЮ ЧТО ЗНАЧЯТ ВСЕ ЭТИ КОНСТАНТЫ, САМИ ИХ ВЫНОСИТЕ
        /// </summary>
        /// <param name="enemyTexture"></param>
        /// <param name="healthBarTexture"></param>
        /// <param name="manaBarTexture"></param>
        /// <param name="energyBarTexture"></param>
        public void LoadContent (Texture2D enemyTexture,Texture2D healthBarTexture,
            Texture2D manaBarTexture, Texture2D energyBarTexture)
        {
            this.healthBarTexture = healthBarTexture;
            this.manaBarTexture = manaBarTexture;
            this.energyBarTexture = energyBarTexture;
            healthRectangle = new Rectangle(positionX, positionY, 300,
                25);
            manaRectangle = new Rectangle(healthRectangle.X,
                healthRectangle.Y + healthRectangle.Height + positionY, 300,
                25);
            energyRectangle = new Rectangle(manaRectangle.X, manaRectangle.Y
                + manaRectangle.Height + positionY,
                300, 25);
            rectangle = new Rectangle(positionX, 200, 250, 200);
            texture = enemyTexture;
        }

        public Enemy()
        {
            Strength = 2 * constStats;
            Stamina = constStats;
            Intellect = constStats;
            Vitality = constStats;
            Health = new PointClass(constStats * Vitality, constStats * Vitality);
            Mana = new PointClass(constStats * Intellect, constStats * Intellect);
            Energy = new PointClass(constStats * Stamina, constStats * Stamina);
            listSkill = new List<ISkill>();
            listSkill.Add(new SkillRegenHealth());
            listSkill.Add(new SkillFireBall());
        }
        /// <summary>
        /// Выполняет дефолтную атаку
        /// </summary>
        /// <param name="person">противник</param>
        public bool Attack(IPerson person)
        {
            person.Health.Current -= Strength;
            Energy.Current -= consumptionEnergy;
            return true;
        }
        /// <summary>
        /// Обновляет длину баров
        /// </summary>
        public void Update()
        {
            healthRectangle.Width = healthBarTexture.Width * this.Health.Current / this.Health.Max;
            manaRectangle.Width = manaBarTexture.Width * this.Mana.Current / this.Mana.Max;
            energyRectangle.Width = energyBarTexture.Width * this.Energy.Current / this.Energy.Max;
        }

        /// <summary>
        /// ИИ
        /// </summary>
        /// <param name="person">противник</param>
        /// <returns>true, если энергия закончилась, false в противном случае</returns>
        public bool Input(IPerson person)
        {
            if (Energy.Current <= 0)
            {
                Energy.Current = Energy.Max;
                return true;
            }
            //Регенерация жизни
            if ((Health.Current <= minMyHelth) && 
                (Mana.Current >= listSkill[0].MP) && 
                (Energy.Current >= listSkill[0].SP))
            {
                listSkill[0].Effect(this, person);
                return false;
            }
            //Fire ball
            if (((Mana.Current / Mana.Max >= 0.6) || (person.Health.Current <= minHisHelth))
                && (Mana.Current >= listSkill[1].MP) && (Energy.Current >= listSkill[1].SP))
            {
                listSkill[1].Effect(this, person);
                return false;
            }
            Attack(person);
            return false;
        }

        /// <summary>
        /// отрисовывка
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(healthBarTexture, healthRectangle, Color.White); 
            spriteBatch.Draw(texture, rectangle, Color.White); 
            spriteBatch.Draw(manaBarTexture, manaRectangle, Color.White);
            spriteBatch.Draw(energyBarTexture, energyRectangle, Color.White);
            spriteBatch.DrawString(MapState.spriteFont, Health.Current + "/" + Health.Max, new Vector2(healthRectangle.Center.X-25, healthRectangle.Y),Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, Mana.Current + "/" + Mana.Max, new Vector2(manaRectangle.Center.X - 25, manaRectangle.Y), Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, Energy.Current + "/" + Energy.Max, new Vector2(energyRectangle.Center.X - 25, energyRectangle.Y), Color.Black);
        }
    }
}
