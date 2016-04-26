using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    /// <summary>
    /// Вспомогательный класс для ЖМЭ
    /// </summary>
    class PointClass
    {
        public PointClass(int max, int current)
        {
            this.max = max;
            this.current = current;
        }
        int max; public int Max { get { return max; } set { max = value; } }
        int current; public int Current { get { return current; } set { current = value; } }
    };

    class Hero
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

        public Hero()
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
