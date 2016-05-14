using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class SkillFireBall:ISkill
    {
        int mana = 10;
        int damage = 35;
        int energy = 50;
        public bool Effect(IPerson user,IPerson victim)
        {
            if ((user.Mana.Current >= mana)&&(user.Energy.Current >= energy))
            {
                user.Mana.Current -= mana;
                user.Energy.Current -= energy;
                victim.Health.Current -= damage;
                return true;
            }
            return false;
        }
    }
}
