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
        IList<ISkill> listSkill = new List<ISkill>();
        PointClass health, mana, energy;
        int strength, stamina, intellect;
        Weapon weapon;

        Texture2D texture;
        Rectangle rectangle;

        Rectangle manaRectangle;
        Rectangle energyRectangle;
        Rectangle healthRectangle;

        Texture2D healthBarTexture;
        Texture2D manaBarTexture;
        Texture2D energyBarTexture;

        public PointClass Health
        {
            get { return health; }
            set { health = value; }
        }
        public PointClass Mana
        {
            get { return mana; }
            set { mana = value; }
        }
        public PointClass Energy
        {
            get { return energy; }
            set { energy = value; }
        }
        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
        public int Stamina
        {
            get { return stamina; }
            set { stamina = value; }
        }
        public int Intellect
        {
            get { return intellect; }
            set { intellect = value; }
        }

        public Hero(Texture2D heroTexture,Texture2D healthBarTexture, Texture2D manaBarTexture, Texture2D energyBarTexture)
        {
            strength = 10;
            stamina = 10;
            intellect = 10;
            health = new PointClass(10*strength, 10*strength);
            mana = new PointClass(10 * intellect, 10 * intellect);
            energy = new PointClass(10 * stamina, 10 * stamina);
            weapon = new Weapon(5);
            this.healthBarTexture = healthBarTexture;
            this.manaBarTexture = manaBarTexture;
            this.energyBarTexture = energyBarTexture;
            healthRectangle = new Rectangle(10, 10, 300,
                25);
            manaRectangle = new Rectangle(healthRectangle.X,
                healthRectangle.Y + healthRectangle.Height + 10, 300,
                25);
            energyRectangle = new Rectangle(manaRectangle.X, manaRectangle.Y
                + manaRectangle.Height + 10,
                300, 25);
            texture = heroTexture;
            rectangle = new Rectangle(200, 200, 250, 200);
            listSkill.Add(new SkillRegenHealth());
            listSkill.Add(new SkillFireBall());
        }

        public void Attack(IPerson person)
        {
            person.Health.Current = person.Health.Current-(strength + weapon.Damage);
            energy.Current -= 20;
        }
        public bool RegenHealth ()
        {
            if (mana.Current >= 30)
            {
                health.Current += 70;
                mana.Current -= 30;
                if (health.Current > health.Max)
                    health.Current = health.Max;
                return true;
            }
            return false;
        }

        public void Update()
        {
            healthRectangle.Width = healthBarTexture.Width * this.Health.Current / this.Health.Max;
            manaRectangle.Width = manaBarTexture.Width * this.Mana.Current / this.Mana.Max;
            energyRectangle.Width = energyBarTexture.Width * this.Energy.Current / this.Energy.Max;
        }

        public bool Input(IPerson enemy)
        {
            if (energy.Current <= 0 || Keyboard.GetState().IsKeyDown(Keys.F))
            {
                energy.Current = energy.Max;
                return false;
            }
            //Обычная атака
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Attack(enemy);
            }
            //Регенерация здоровья
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                listSkill[0].Effect(this, enemy);
            }
            //Fire ball
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                listSkill[1].Effect(this, enemy);
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarTexture, healthRectangle, Color.White);
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.Draw(manaBarTexture, manaRectangle, Color.White);
            spriteBatch.Draw(energyBarTexture, energyRectangle, Color.White);
        }

    }
}
