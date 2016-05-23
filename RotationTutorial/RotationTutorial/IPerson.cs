using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RotationTutorial
{
    public interface IPerson
    {
        PointClass Health{get;set;}
        PointClass Mana{get;set;}
        PointClass Energy { get; set; }
        IList<ISkill> ListSkill { get; }
        int Strength { get; set; }
        int Stamina { get; set; }
        int Intellect { get; set; }
        bool Attack(IPerson person);
        void Update();
        int? Input(IPerson enemy);
        void Draw(SpriteBatch spriteBatch);
    }
}
