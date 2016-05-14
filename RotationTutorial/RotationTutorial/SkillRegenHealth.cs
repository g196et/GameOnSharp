using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class SkillRegenHealth:ISkill
    {
        int mana = 30;
        int energy = 50;
        public bool Effect(IPerson user,IPerson victim=null)
        {
            if ((user.Mana.Current >= mana) && (user.Energy.Current >= energy))
            {
                user.Mana.Current -= mana;
                user.Energy.Current -= energy;
                user.Health.Current += (int)(user.Health.Max * 0.6);
                if (user.Health.Current>user.Health.Max)
                {
                    user.Health.Current = user.Health.Max;
                }
                return true;
            }
            return false;
        }
    }
}
