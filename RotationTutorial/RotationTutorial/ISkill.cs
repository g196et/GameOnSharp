using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    /// <summary>
    /// Отвечает за навыки персонажа и мобов
    /// </summary>
    interface ISkill
    {
        /// <summary>
        /// возвращает число затрачиваемых очков маны
        /// </summary>
        int MP { get; }
        /// <summary>
        /// возвращает число затрачиваемых очков стамины
        /// </summary>
        int SP { get; }
        /// <summary>
        /// Проявляет эффект навыка
        /// </summary>
        /// <param name="user">тот, кто использует навык</param>
        /// <param name="victim">тот, на кого воздействует навык</param>
        /// <returns>true, если получилось применить, false, если не получилось</returns>
        bool Effect(IPerson user, IPerson victim);
    }
}
