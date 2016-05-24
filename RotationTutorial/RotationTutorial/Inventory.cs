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

namespace RotationTutorial
{
    public class Inventory
    {
        IItem[,] inventory;
        Texture2D cellTexture;
        const string textureName="Cell";
        const int backgroundSize = 500;
        const int cellSize = 70;
        const int itemSize = 60;
        const int cellIndent = 20;
        const int itemIndent = 5;
        const int startX = 300;
        const int startY = 20;
        const int nextY = 50;

        public IItem this[int x,int y]
        {
            get { return inventory[x, y]; }
            set { inventory[x, y] = value; }
        }
        
        public Inventory(int x,int y)
        {
            inventory = new IItem[x, y];
        }
        public void LoadContent(ContentManager Content)
        {
            cellTexture = Content.Load<Texture2D>(textureName);
        }
        public int X { get { return inventory.GetLength(0); } }
        public int Y { get { return inventory.GetLength(1); } }
        public int CellSize { get; set; }
        public void AddItem(IItem item)
        {
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    if (this[j, i] == null)
                    {
                        item.Rectangle = new Rectangle(startX + cellIndent + j * (cellSize + cellIndent)
                            + itemIndent, nextY + i * (cellSize + cellIndent) + itemIndent, itemSize,
                            itemSize);
                        this[j, i] = item; 
                        return;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cellTexture, new Rectangle(startX, startY, backgroundSize, backgroundSize), 
                Color.White);
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    spriteBatch.Draw(cellTexture, new Rectangle(startX+cellIndent + i * (cellSize + 
                        cellIndent), nextY + j * (cellSize + cellIndent), cellSize, cellSize), 
                        Color.White);
                    if (this[i, j] != null)
                    {
                        this[i, j].Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
