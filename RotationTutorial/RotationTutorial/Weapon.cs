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
    public class Weapon:IItem
    {
        string name;
        Texture2D texture;
        Rectangle rectangle;
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public bool ChangeRectangle { get; set; }
        public string Name { get { return name; } }
        int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public Weapon(string name,int damage)
        {
            this.name = name;
            this.damage = damage;
        }
        public void Update()
        {
            if(ChangeRectangle)
            {
                MouseState ms=Mouse.GetState();
                rectangle.X = ms.X;
                rectangle.Y = ms.Y;
            }
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("sword1");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}
