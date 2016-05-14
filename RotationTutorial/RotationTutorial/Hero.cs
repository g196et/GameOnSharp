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
        /// НЕ ЗНАЮ ЧТО ЗНАЧЯТ ВСЕ ЭТИ КОНСТАНТЫ, САМИ ИХ ВЫНОСИТЕ
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
            healthRectangle = new Rectangle(position, position, 300,
                25);
            manaRectangle = new Rectangle(healthRectangle.X,
                healthRectangle.Y + healthRectangle.Height + position, 300,
                25);
            energyRectangle = new Rectangle(manaRectangle.X, manaRectangle.Y
                + manaRectangle.Height + position,
                300, 25);
            texture = heroTexture;
            rectangle = new Rectangle(200, 200, 250, 200);
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

    }
}
