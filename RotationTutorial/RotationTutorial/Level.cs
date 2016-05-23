using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RotationTutorial
{
    public class Level
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
            if((curExperience>=requiredExperience)&&(curLevel<levels.Length))
            {
                curLevel++;
                curExperience -= requiredExperience;
                requiredExperience = levels[curLevel-1];
                return true;
            }
            return false;
        }
        public void Save(StreamWriter writer)
        {
            writer.WriteLine(this.curLevel + "#" + this.curExperience + "#");
            foreach(int border in levels)
            {
                writer.Write(border + "#");
            }
            writer.WriteLine();
        }
        public void Load(StreamReader reader)
        {
            string[] line = reader.ReadLine().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            this.curLevel = int.Parse(line[0]);
            this.curExperience = int.Parse(line[1]);
            line = reader.ReadLine().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            levels = new int[line.Length];
            for(int i=0;i<line.Length;i++)
            {
                levels[i] = int.Parse(line[i]);
            }
        }
    }
}
