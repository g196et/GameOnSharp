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
    class Enemy:IPerson
    {
        IList<ISkill> listSkill;
        PointClass health, mana, stamina;
        int damage = 30;
        int defense = 10;

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
        public PointClass Stamina
        {
            get { return stamina; }
            set { stamina = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        public Enemy(Texture2D enemyTexture,Texture2D healthBarTexture, Texture2D manaBarTexture, Texture2D energyBarTexture)
        {
            health = new PointClass(100, 100);
            mana = new PointClass(100, 100);
            stamina = new PointClass(100, 100);
            this.healthBarTexture = healthBarTexture;
            this.manaBarTexture = manaBarTexture;
            this.energyBarTexture = energyBarTexture;
            healthRectangle = new Rectangle(900, 10, healthBarTexture.Width,
                healthBarTexture.Height);
            manaRectangle = new Rectangle(healthRectangle.X,
                healthRectangle.Y + healthRectangle.Height + 10, manaBarTexture.Width,
                manaBarTexture.Height);
            energyRectangle = new Rectangle(manaRectangle.X, manaRectangle.Y
                + manaRectangle.Height + 10,
                healthRectangle.Width, healthRectangle.Height);
            rectangle = new Rectangle(900, 200, enemyTexture.Width, enemyTexture.Height);
            texture = enemyTexture;
            listSkill = new List<ISkill>();
            listSkill.Add(new SkillRegenHealth());
            listSkill.Add(new SkillFireBall());
        }
        public void Attack(IPerson person)
        {
            person.Health.Current = person.Health.Current - (damage - person.Defense);
        }

        public void Update()
        {
            healthRectangle.Width = healthBarTexture.Width * this.Health.Current / this.Health.Max;
            manaRectangle.Width = manaBarTexture.Width * this.Mana.Current / this.Mana.Max;
        }

        public bool Input(IPerson person)
        {
            if ((health.Current <= 35) && (mana.Current >= 30))
            {
                listSkill[0].Effect(this, person);
                return true;
            }
            if (((mana.Current / mana.Max >= 0.6) || (person.Health.Current <= 35))&&(Mana.Current>=10))
            {
                listSkill[1].Effect(this, person);
                return true;
            }
            Attack(person);
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
