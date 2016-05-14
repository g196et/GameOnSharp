using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    public class Tiles
    {
        protected Texture2D texture;
        protected Texture2D backTexture;
        Rectangle rectangle;
        bool passability;
        bool mob;
        public bool Passability
        {
            get { return passability; }
        }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public bool Mob { get { return mob; } set { mob = value; } }

        static ContentManager content;

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Tiles(int i, Rectangle newRectangle, bool passability)//, bool mob)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
            this.passability = passability;
            this.mob = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            
        }
    }

}
