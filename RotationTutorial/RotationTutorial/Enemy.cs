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
    public class Enemy:IPerson
    {
        IList<ISkill> listSkill;
        PointClass health, mana, energy;
        int strength, stamina, intellect, vitality;

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

        public void LoadContent (Texture2D enemyTexture,Texture2D healthBarTexture,
            Texture2D manaBarTexture, Texture2D energyBarTexture)
        {
            this.healthBarTexture = healthBarTexture;
            this.manaBarTexture = manaBarTexture;
            this.energyBarTexture = energyBarTexture;
            healthRectangle = new Rectangle(900, 10, 300,
                25);
            manaRectangle = new Rectangle(healthRectangle.X,
                healthRectangle.Y + healthRectangle.Height + 10, 300,
                25);
            energyRectangle = new Rectangle(manaRectangle.X, manaRectangle.Y
                + manaRectangle.Height + 10,
                300, 25);
            rectangle = new Rectangle(900, 200, 250, 200);
            texture = enemyTexture;
        }

        public Enemy()
        {
            strength = 20;
            stamina = 10;
            intellect = 10;
            vitality = 10;
            health = new PointClass(10 * vitality, 10 * vitality);
            mana = new PointClass(10 * intellect, 10 * intellect);
            energy = new PointClass(10 * stamina, 10 * stamina);
            listSkill = new List<ISkill>();
            listSkill.Add(new SkillRegenHealth());
            listSkill.Add(new SkillFireBall());
        }
        public bool Attack(IPerson person)
        {
            person.Health.Current = person.Health.Current - strength;
            energy.Current -= 50;
            return true;
        }

        public void Update()
        {
            healthRectangle.Width = healthBarTexture.Width * this.Health.Current / this.Health.Max;
            manaRectangle.Width = manaBarTexture.Width * this.Mana.Current / this.Mana.Max;
            energyRectangle.Width = energyBarTexture.Width * this.Energy.Current / this.Energy.Max;
        }

        public bool Input(IPerson person)
        {
            if (energy.Current <= 19)
            {
                energy.Current = energy.Max;
                return true;
            }
            //Регенерация жизни
            if ((health.Current <= 35) && (mana.Current >= 30)&& (energy.Current>=50))
            {
                listSkill[0].Effect(this, person);
                return false;
            }
            //Fire ball
            if (((mana.Current / mana.Max >= 0.6) || (person.Health.Current <= 35))&&(Mana.Current>=10)&&(energy.Current>=75))
            {
                listSkill[1].Effect(this, person);
                return false;
            }
            Attack(person);
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(healthBarTexture, healthRectangle, Color.White); 
            spriteBatch.Draw(texture, rectangle, Color.White); 
            spriteBatch.Draw(manaBarTexture, manaRectangle, Color.White);
            spriteBatch.Draw(energyBarTexture, energyRectangle, Color.White);
            spriteBatch.DrawString(MapState.spriteFont, health.Current + "/" + health.Max, new Vector2(healthRectangle.Center.X-25, healthRectangle.Y),Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, mana.Current + "/" + mana.Max, new Vector2(manaRectangle.Center.X - 25, manaRectangle.Y), Color.Black);
            spriteBatch.DrawString(MapState.spriteFont, energy.Current + "/" + energy.Max, new Vector2(energyRectangle.Center.X - 25, energyRectangle.Y), Color.Black);
        }
    }
}
