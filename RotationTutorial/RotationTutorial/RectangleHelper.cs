using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    static class RectangleHelper
    {
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= (r2.Top)&&
                r1.Right == r2.Right&&
                r1.Left == r2.Left);
        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top >= r2.Bottom &&
                r1.Right == r2.Right &&
                r1.Left == r2.Left);
        }

        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Left &&
                r1.Top == r2.Top &&
                r1.Bottom == r2.Bottom);
        }

        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Right &&
                r1.Top == r2.Top &&
                r1.Bottom == r2.Bottom);
        }
    }
}
