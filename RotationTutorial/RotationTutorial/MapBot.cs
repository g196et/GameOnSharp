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
using System.IO;

namespace RotationTutorial
{
    public class MapBot
    {
        const int tileSize = 75;
        const int halfTileSize = 37;
        Texture2D texture;
        string textureFileName;
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        Rectangle rectangle; public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } } 
        Vector2 position; public Vector2 Position { get { return position; } set { position = value; } }
        Enemy enemy; public Enemy Enemy { get { return enemy; } }

        public MapBot(Vector2 newPosition, int strength, int stamina, int intellect, int vitality,string textureFileName)
        {
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);
            this.enemy = new Enemy(strength, stamina, intellect, vitality);
            this.textureFileName = textureFileName;
        }

        public void AddMobMap(Map map)
        {
            map.GetRectangle(new Point((int)position.X + halfTileSize, (int)position.Y + halfTileSize)).Mob = true;
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(textureFileName);
            //texture.Name = textureName;

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(this.position.X+"#"+this.position.Y);
            writer.WriteLine(this.rectangle);
            enemy.Save(writer);
        }
        public void Load(StreamReader reader)
        {
            string[] line=reader.ReadLine().Split(new char[]{ '#' },StringSplitOptions.RemoveEmptyEntries);
            this.position = new Vector2(float.Parse(line[0]), float.Parse(line[1]));
            line = reader.ReadLine().Split(new string[] { "#","X","Y","Width","Height","{","}",":" },
                StringSplitOptions.RemoveEmptyEntries);
            this.rectangle.X = int.Parse(line[0]);
            this.rectangle.Y = int.Parse(line[1]);
            this.rectangle.Width = int.Parse(line[2]);
            this.rectangle.Height = int.Parse(line[3]);
            if (enemy == null)
                enemy = new Enemy(0,0,0,0);
            enemy.Load(reader);
        }
    }
}
