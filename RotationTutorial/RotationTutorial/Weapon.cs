using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class Weapon
    {
        int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public Weapon(int damage)
        {
            this.damage = damage;
        }
    }
}
