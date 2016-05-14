using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    /// <summary>
    /// Навык, позволяющий нанести урон, затратив на это ману
    /// </summary>
    class SkillFireBall:ISkill
    {
        const int mana = 10;
        const int damage = 35;
        const int energy = 50;
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
