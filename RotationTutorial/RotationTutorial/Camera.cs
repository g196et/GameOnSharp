﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            centre = new Vector2(game.spritePosition.X + (game.spriteRectangle.Width / 2) - 400,
                game.spritePosition.Y + (game.spriteRectangle.Height / 2) - 200);
            transform = Matrix.CreateScale(new Vector3(1,1,0)) *
                Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
