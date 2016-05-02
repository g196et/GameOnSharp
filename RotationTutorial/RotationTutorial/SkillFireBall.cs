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
        public bool Effect(IPerson user,IPerson victim)
        {
            if (user.Mana.Current >= mana)
            {
                user.Mana.Current -= mana;
                victim.Health.Current -= damage;
                return true;
            }
            return false;
        }
    }
}
