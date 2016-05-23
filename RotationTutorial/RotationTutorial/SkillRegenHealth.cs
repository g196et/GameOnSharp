using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace RotationTutorial
{
    /// <summary>
    /// Навык, позволяющий востановить очки здоровья, затратив на это ману
    /// </summary>
    class SkillRegenHealth:ISkill
    {
        const int mana = 30;
        const int energy = 50;
        const Keys key = Keys.W;
        public Keys Key { get { return key; } }
        public string Name { get { return "Regeneration"; } }
        /// <summary>
        /// возвращает число затрачиваемых очков маны
        /// </summary>
        public int MP { get { return mana; } }
        /// <summary>
        /// возвращает число затрачиваемых очков стамины
        /// </summary>
        public int SP { get { return energy; } }
        /// <summary>
        /// Проявляет эффект навыка
        /// </summary>
        /// <param name="user">тот, кто использует навык</param>
        /// <param name="victim">тот, на кого воздействует навык</param>
        /// <returns>true, если получилось применить, false, если не получилось</returns>
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
