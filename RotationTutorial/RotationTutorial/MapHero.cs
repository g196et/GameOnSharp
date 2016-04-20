using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationTutorial
{
    class MapHero
    {
        Texture2D texture;
        Rectangle rectangle; public Rectangle Rectangle1 { get { return rectangle; } set { rectangle = value; } }
        Vector2 position; public Vector2 Position { get { return position; } set { position = value; } }
        //Vector2 origin;
        Vector2 velocity;

        public MapHero(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }

        public void Update(GameTime gameTime)
        { 
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            position += velocity;
            Input(gameTime);
        }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                velocity.Y = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                velocity.Y = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else velocity = Vector2.Zero;
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            //Касание верхушки
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = -1f;
            }
            //Касание левого края объекта
            if (rectangle.TouchLeftOf(newRectangle))
            {
                rectangle.X = newRectangle.Left - (rectangle.Width + 1);
                velocity.X = -1f;
            }
            //Касание правого края
            if (rectangle.TouchRightOf(newRectangle))
            {
                rectangle.X = newRectangle.Right + 1 ;
                velocity.X = 1f;
            }
            //Касание дна объекта
            if (rectangle.TouchBottomOf(newRectangle))
            {
                rectangle.Y = newRectangle.Bottom + 1;
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
