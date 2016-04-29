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
    interface IPerson
    {
        PointClass Health{get;set;}
        PointClass Mana{get;set;}
        PointClass Stamina { get; set; }
        int Damage { get; set; }
        int Defense { get; set; }
        void Attack(IPerson person);
        void Update();
        bool Input(IPerson enemy);
        void Draw(SpriteBatch spriteBatch);
    }
}
