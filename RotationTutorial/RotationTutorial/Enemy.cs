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
    public class Enemy:IPerson
    {
        enum Skill : int { Punch = -1, FireBall = 0, Regeneration = 1 }
        const int consumptionEnergy = 50;
        const int minMyHelth = 40;
        const int minHisHelth = 35;
        const int constStats = 10;
        const int positionX = 900;
        const int positionY = 10;
        const int barWidth = 300;
        const int barHeight = 25;
        const int enemyPositionX = 800;
        const int enemyPositionY = 200;
        const int enemyWidth = 250;
        const int enemyHeight = 400;
        const int skip = 150;
        public Armor Armor { get; set; }

        IList<ISkill> listSkill;
        public IList<ISkill> ListSkill { get { return listSkill; } }
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
        public Enemy(int strength, int stamina, int intellect, int vitality)
        {
            Strength = strength;
            Stamina = stamina;
            Intellect = intellect;
            Vitality = vitality;
            Health = new PointClass(constStats * Vitality, constStats * Vitality);
            Mana = new PointClass(constStats * Intellect, constStats * Intellect);
            Energy = new PointClass(constStats * Stamina, constStats * Stamina);
            listSkill = new List<ISkill>();
            listSkill.Add(new SkillRegenHealth());
            listSkill.Add(new SkillFireBall());
            Armor = new Armor("Shit armor", 0);
        }

        /// <summary>
        /// 
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
            healthRectangle = new Rectangle(positionX, positionY, barWidth,
                barHeight);
            manaRectangle = new Rectangle(positionX,
                healthRectangle.Y + healthRectangle.Height + positionY, barWidth,
                barHeight);
            energyRectangle = new Rectangle(positionX, manaRectangle.Y
                + manaRectangle.Height + positionY,
                barWidth, barHeight);
            rectangle = new Rectangle(enemyPositionX, enemyPositionY, enemyWidth, enemyHeight);
            texture = enemyTexture;
        }

        
        /// <summary>
        /// Выполняет дефолтную атаку
        /// </summary>
        /// <param name="person">противник</param>
        public bool Attack(IPerson person)
        {
            if(person.Armor!=null)
            {
                person.Health.Current -= Strength - person.Armor.Defense;
            }
            else
            {
                person.Health.Current -= Strength;
            }
            
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
        public int? Input(IPerson person)
        {
            if (Energy.Current <= 0)
            {
                Energy.Current = Energy.Max;
                return null;
            }
            //Регенерация жизни
            if ((Health.Current <= minMyHelth) &&
                (Mana.Current >= listSkill[(int)Skill.Regeneration].MP) &&
                (Energy.Current >= listSkill[(int)Skill.Regeneration].SP))
            {
                listSkill[(int)Skill.Regeneration].Effect(this, person);
                return (int)Skill.Regeneration;
            }
            //Fire ball
            if (((Mana.Current / Mana.Max >= 0.6) || (person.Health.Current <= minHisHelth))
                && (Mana.Current >= listSkill[(int)Skill.FireBall].MP)
                && (Energy.Current >= listSkill[(int)Skill.FireBall].SP))
            {
                listSkill[(int)Skill.FireBall].Effect(this, person);
                return (int)Skill.FireBall;
            }
            if (Attack(person))
            {
                return (int)Skill.Punch;
            }
            return skip;
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
            spriteBatch.DrawString(MapState.spriteFont, Health.ToString(),
                new Vector2(healthRectangle.Center.X-25, healthRectangle.Y),Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, Mana.ToString(), 
                new Vector2(manaRectangle.Center.X - 25, manaRectangle.Y), Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, Energy.ToString(),
                new Vector2(energyRectangle.Center.X - 25, energyRectangle.Y), Color.Black);
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(this.Health + "#" + this.Mana + "#");
            writer.WriteLine(this.Strength + "#" + this.Stamina + "#" + this.Intellect + "#" + 
                this.Vitality);
            //writer.WriteLine(listSkill.Count);

        }
        public void Load(StreamReader reader)
        {
            string[] line = reader.ReadLine().Split(new char[] { '#', '/' },
                StringSplitOptions.RemoveEmptyEntries);
            this.Health.Current = int.Parse(line[0]);
            this.Health.Max = int.Parse(line[1]);
            this.Mana.Current = int.Parse(line[2]);
            this.Mana.Max = int.Parse(line[3]);
            line = reader.ReadLine().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            this.Strength = int.Parse(line[0]);
            this.Stamina = int.Parse(line[1]);
            this.Intellect = int.Parse(line[2]);
            this.Vitality = int.Parse(line[3]);
        }
    }
}
