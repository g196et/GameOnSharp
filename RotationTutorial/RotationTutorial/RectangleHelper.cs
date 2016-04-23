using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    /// <summary>
    /// Класс-помощник в расётах коллизий
    /// </summary>
    static class RectangleHelper
    {
        /// <summary>
        /// Проверка на коллизию сверху
        /// </summary>
        /// <param name="r1">Текущий Rectangle</param>
        /// <param name="r2">Rectangle, которого касаемся</param>
        /// <returns>true, если коснулись</returns>
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= (r2.Top)&&
                r1.Right == r2.Right&&
                r1.Left == r2.Left);
        }

        /// <summary>
        /// Проверка на коллизию снизу
        /// </summary>
        /// <param name="r1">Текущий Rectangle</param>
        /// <param name="r2">Rectangle, которого касаемся</param>
        /// <returns>true, если коснулись</returns>
        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top >= r2.Bottom &&
                r1.Right == r2.Right &&
                r1.Left == r2.Left);
        }

        /// <summary>
        /// Проверка на коллизию слева
        /// </summary>
        /// <param name="r1">Текущий Rectangle</param>
        /// <param name="r2">Rectangle, которого касаемся</param>
        /// <returns>true, если коснулись</returns>
        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Left &&
                r1.Top == r2.Top &&
                r1.Bottom == r2.Bottom);
        }

        /// <summary>
        /// Проверка на коллизию справа
        /// </summary>
        /// <param name="r1">Текущий Rectangle</param>
        /// <param name="r2">Rectangle, которого касаемся</param>
        /// <returns>true, если коснулись</returns>
        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Right &&
                r1.Top == r2.Top &&
                r1.Bottom == r2.Bottom);
        }
    }
}
