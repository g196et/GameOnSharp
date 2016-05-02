using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    interface ISkill
    {
        bool Effect(IPerson user, IPerson victim);
    }
}
