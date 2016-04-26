using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class Enemy
    {
        PointClass health, mana, stamina;
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

        public Enemy()
        {
            health = new PointClass(100, 100);
            mana = new PointClass(100, 100);
            stamina = new PointClass(100, 100);
        }

        public void Update()
        {

        }

        public void Input()
        {

        }

        public void Draw()
        {

        }
    }
}
