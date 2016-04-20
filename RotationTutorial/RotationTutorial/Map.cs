using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class Map
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
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tiles tile in tiles)
                tile.Draw(spriteBatch);
        }
    }
}
