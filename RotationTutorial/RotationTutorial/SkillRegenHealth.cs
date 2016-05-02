using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class SkillRegenHealth:ISkill
    {
        int mana = 30;
        public bool Effect(IPerson user,IPerson victim=null)
        {
            if(user.Mana.Current>=mana)
            {
                user.Mana.Current -= mana;
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
