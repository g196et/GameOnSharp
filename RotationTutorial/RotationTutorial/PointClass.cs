using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    /// <summary>
    /// Вспомагательный класс для IPerson
    /// </summary>
    public class PointClass
    {
        public PointClass(int max, int current)
        {
            this.max = max;
            this.current = current;
        }
        int max; public int Max { get { return max; } set { max = value; } }
        int current; public int Current { get { return current; } set { current = value; } }
    }
}
