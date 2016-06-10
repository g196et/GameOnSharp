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
        Rectangle rectangle;
        bool passability;
        bool mob;
        bool portal;
        int tileTextureIndex;
        public bool Passability
        {
            get { return passability; }
        }
        public bool Portal
        {
            get { return portal; }
        }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public bool Mob { get { return mob; } set { mob = value; } }

        static ContentManager content;

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Tiles(int tileTextureIndex, Rectangle newRectangle, bool passability, bool portal)//, bool mob)
        {
            this.tileTextureIndex = tileTextureIndex;
            this.Rectangle = newRectangle;
            this.passability = passability;
            this.mob = false;
            this.portal = portal;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            
        }

        public void LoadContent (ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Tile" + tileTextureIndex);
        }
    }

}
