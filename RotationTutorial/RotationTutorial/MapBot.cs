using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    public class MapBot
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position; public Vector2 Position { get { return position; } set { position = value; } }
        Vector2 velocity;

        public MapBot(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
