using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RotationTutorial
{
    public class Map
    {
        private List<Tiles> tiles;
        public List<Tiles> MapTiles
        {
            get { return tiles; }
        }

        public Map()
        {
            tiles = new List<Tiles>();
        }
        /// <summary>
        /// Генерация карты
        /// </summary>
        /// <param name="map"></param>
        /// <param name="size">Размер ячеек</param>
        public void LoadMap(StreamReader reader, int size)
        {
            RemoveMap();
            int num = 0;
            while(!reader.EndOfStream)
            {               
                string[] str = reader.ReadLine().Split(' ');
                for (int i = 0; i < str.Length;i++ )
                {
                    int number = int.Parse(str[i]);
                    if (number > 1)
                    {
                        tiles.Add(new Tiles(number, new Rectangle(i * size, num * size, size, size), false, false));
                    }
                    else
                    {
                        if (number == 0)
                        {
                            tiles.Add(new Tiles(number, new Rectangle(i * size, num * size, size, size), true, true));
                        }
                        else
                        {
                            tiles.Add(new Tiles(number, new Rectangle(i * size, num * size, size, size), true,false));
                        }
                    }
                }
                num += 1;
            }
        }
        public void RemoveMap()
        {
            tiles.Clear();
        }

        public Tiles GetRectangle(Point position)
        {
            int i = 0;
            while ((tiles[i].Rectangle.Center != position) && (i< tiles.Count))
                i++;
            return tiles[i];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tiles tile in tiles)
            { 
                tile.Draw(spriteBatch); 
            }
        }

        public void LoadContent (ContentManager Content)
        {
            int i = 0;
            foreach (Tiles tile in tiles)
            {
                tile.LoadContent(Content);
                i++;
            }
        }
    }
}
