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
        Rectangle rectangle; public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } } 
        Vector2 position; public Vector2 Position { get { return position; } set { position = value; } }

        public MapBot(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Generate(int positionX, int positionY, int size)
        {

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
