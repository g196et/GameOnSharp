using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    public class Map
    {
        private List<Tiles> tiles = new List<Tiles>();
        public List<Tiles> MapTiles
        {
            get { return tiles; }
        }

        int width, height;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Map() { }
        /// <summary>
        /// Генерация карты
        /// </summary>
        /// <param name="map"></param>
        /// <param name="size">Размер ячеек</param>
        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y,x];
                    if (number > 1)
                        tiles.Add(new Tiles(number, new Rectangle(x * size, y * size, size, size), false));
                    else
                        tiles.Add(new Tiles(number, new Rectangle(x * size, y * size, size, size), true));
                    width = (x + 1) * size;
                    height = (x + 1) * size;
                }
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
                tile.Draw(spriteBatch);
            //spriteBatch.DrawString(Game1.spriteFront, i.ToString(), new Vector2(-50, -50), Color.White);
        }
    }
}
