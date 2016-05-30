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
        Armor curArmor;
        Weapon curWeapon;
        public Armor CurrentArmor { get { return curArmor; } private set { curArmor = value; } }
        public Weapon CurrentWeapon { get { return curWeapon; } private set { curWeapon = value; } }
        Rectangle armorRectangle;
        Rectangle weaponRectangle;
        public IItem this[int x,int y]
        {
            get { return inventory[x, y]; }
            set { inventory[x, y] = value; }
        }
        
        public Inventory(int x,int y)
        {
            inventory = new IItem[x, y];
            armorRectangle = new Rectangle(600, 600, cellSize, cellSize);
            weaponRectangle = new Rectangle(700, 600, cellSize, cellSize);
            AddItem(new Armor("armor",5));
            AddItem(new Weapon("weapon", 5));
        }
        public void LoadContent(ContentManager Content)
        {
            cellTexture = Content.Load<Texture2D>(textureName);
            foreach(IItem item in inventory)
            {
                if(item!=null)
                {
                    item.LoadContent(Content);
                }
            }
        }
        public int X { get { return inventory.GetLength(0); } }
        public int Y { get { return inventory.GetLength(1); } }
        public int CellSize { get; set; }
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            if ((curArmor!=null)&&(armorRectangle.Contains(mouseState.X, mouseState.Y)) &&
                (mouseState.LeftButton == ButtonState.Pressed))
            {
                AddItem(curArmor);
                curArmor = null;
            }
            if ((curWeapon != null) && (weaponRectangle.Contains(mouseState.X, mouseState.Y)) &&
                (mouseState.LeftButton == ButtonState.Pressed))
            {
                AddItem(curWeapon);
                curWeapon = null;
            }
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    if ((this[i, j] != null)&&(this[i,j].Rectangle.Contains(new Point(
                        mouseState.X,mouseState.Y))))
                    {
                        if(mouseState.LeftButton==ButtonState.Pressed)
                        {
                            if(this[i,j].GetType()==(new Armor("",0)).GetType())
                            {
                                if (curArmor == null)
                                {
                                    curArmor = (Armor)this[i, j];
                                    curArmor.Rectangle = armorRectangle;
                                    this[i, j] = null;
                                }
                                else
                                {
                                    Armor swapArmor = curArmor;
                                    curArmor = (Armor)this[i, j];
                                    this[i, j] = swapArmor;
                                }
                                continue;
                            }
                            if (this[i, j].GetType() == (new Weapon("", 0)).GetType())
                            {
                                Weapon swapWeapon = curWeapon;
                                curWeapon = (Weapon)this[i, j];
                                curWeapon.Rectangle = weaponRectangle;
                                this[i, j] = swapWeapon;
                            }
                        }
                    }
                }
            }
        }
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
            spriteBatch.Draw(cellTexture, armorRectangle, Color.White);
            spriteBatch.Draw(cellTexture, weaponRectangle, Color.White);
            try
            {
                curArmor.Draw(spriteBatch);
            }
            catch { }
            try
            {
                curWeapon.Draw(spriteBatch);
            }
            catch { }
        }
    }
}
