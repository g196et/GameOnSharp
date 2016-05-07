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
            rectangle = new Rectangle((int)position.X, (int)position.Y, 75, 75);
        }

        public void AddMobMap(Map map)
        {
            map.GetRectangle(new Point((int)position.X + 37, (int)position.Y + 37)).Mob = true;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
