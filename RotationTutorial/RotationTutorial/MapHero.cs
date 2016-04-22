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
        bool checkAtack; public bool CheckAtackField { get { return checkAtack; } set { checkAtack = value; } }
        bool checkInput;
        //private Keyboard oldState;
        string text = "";
        double counter; public double Counter { get { return counter; } set { counter = value; } }
        public MapHero(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            checkAtack = false;
            checkInput = true;
            counter = 1001;
        }

        public void Update(GameTime gameTime, Map map)
        { 
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            position += velocity;
            //foreach (Tiles tile in map.MapTiles)
            //    if (!tile.Passability)
            //        Collision(tile.Rectangle, map.Width, map.Height);
            if (counter < 500)
            {
                UpdateTime(gameTime);
            }
            else
            {
                Input(gameTime, map);
            }

            if (map.GetRectangle(rectangle.Center).Mob)
            {
                text = "WIN BLEAT'";
                map.GetRectangle(rectangle.Center).Mob = false;
            }
        }

        public void UpdateTime(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        private void Input(GameTime gameTime, Map map)
        {
            {
                if ((Keyboard.GetState().IsKeyDown(Keys.D)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X+75,rectangle.Center.Y))).Passability))
                {
                    position.X += (float)75;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(Keys.A)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X - 75, rectangle.Center.Y))).Passability))
                {
                    position.X -= (float)75;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(Keys.W)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X, rectangle.Center.Y - 75))).Passability))
                {
                    position.Y -= (float)75;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(Keys.S)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X, rectangle.Center.Y + 75))).Passability))
                {
                    position.Y += (float)75;
                    counter = 0;
                }
                else velocity = Vector2.Zero;
            }
        }

        public void CheckAtack(MapBot mapBot)
        {
            if (mapBot.Position.X - position.X > 75 &&
                mapBot.Position.Y - position.Y < 75)
            {
                checkAtack = true;
            }
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
            if (position.Y < 0) position.Y = 0;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.DrawString(Game1.spriteFront, text, new Vector2(-50, -50), Color.White);
        }
    }
}
