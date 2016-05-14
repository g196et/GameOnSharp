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
        const int tileSize = 75;
        const int halfTileSize = 37;
        Texture2D texture;
        Rectangle rectangle; public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } } 
        Vector2 position; public Vector2 Position { get { return position; } set { position = value; } }
        Enemy enemy; public Enemy Enemy { get { return enemy; } }

        public MapBot(Texture2D newTexture, Vector2 newPosition, Enemy enemy)
        {
            texture = newTexture;
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);
            this.enemy = enemy;
        }

        public void AddMobMap(Map map)
        {
            map.GetRectangle(new Point((int)position.X + halfTileSize, (int)position.Y + halfTileSize)).Mob = true;
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
