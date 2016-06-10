using Microsoft.Xna.Framework;
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

        public void Update(GameTime gameTime, MapState game)
        {
            centre = new Vector2(game.SpritePosition.X + (game.SpriteRectangle.Width / 2) - 400,
                game.SpritePosition.Y + (game.SpriteRectangle.Height / 2) - 200);
            transform = Matrix.CreateScale(new Vector3(1,1,0)) *
                Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
