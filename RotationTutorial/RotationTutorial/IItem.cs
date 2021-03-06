﻿using System;
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
    public interface IItem
    {
        string Name { get; }
        Rectangle Rectangle { get; set; }
        bool ChangeRectangle { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void LoadContent(ContentManager Content);
    }
}
