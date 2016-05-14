using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class Level
    {
        int[] levels;
        int curLevel; 
        public int CurrentLevel { get { return curLevel; } private set { } }
        int curExperience; 
        public int CurrentExperience { get { return curExperience; } set { curExperience = value; } }
        int requiredExperience;
        public int RequiredExperience { get { return requiredExperience; } set { requiredExperience = value; } }
        
        public Level(int[] arr)
        {
            levels = arr;
            curLevel = 0;
            curExperience = 0;
            requiredExperience = levels[0];
        }

        public bool CheckLevel()
        {
            if((curExperience>=requiredExperience)&&(curLevel+1<levels.Length))
            {
                curLevel++;
                curExperience -= requiredExperience;
                requiredExperience = levels[curLevel];
                return true;
            }
            return false;
        }
    }
}
