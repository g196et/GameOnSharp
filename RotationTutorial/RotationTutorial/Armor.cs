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
    public class Armor:IItem
    {
        string name;
        public string Name { get { return name; } }
        public Rectangle Rectangle { get; set; }
        Texture2D texture;
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Shit Armor");
        }
        
        int defense;
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }
        public Armor(string name,int defense)
        {
            this.name = name;
            this.defense = defense;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}
