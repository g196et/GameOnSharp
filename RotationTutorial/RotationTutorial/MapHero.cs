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
        Vector2 velocity;
        bool checkAtack; public bool CheckAtackField { get { return checkAtack; } set { checkAtack = value; } }
        //bool checkInput;
        string text = "";
        double counter; public double Counter { get { return counter; } set { counter = value; } }

        public MapHero(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            checkAtack = false;
            counter = 1001;
        }

        public void Update(GameTime gameTime, Map map)
        { 
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            position += velocity;
            if (counter < 500)
            {
                UpdateTime(gameTime);
            }
            else
            {
                Input(gameTime, map);
            }

            if (map.GetTile(rectangle.Center).Mob)
            {
                text = "WIN!!!111!!11!!11!!)0)))))9()090909ARGJKE ISHUBYDOF94NEBPHVOJIT6V2";
                map.GetTile(rectangle.Center).Mob = false;
            }
        }

        /// <summary>
        /// Отсчёт времени до следющего возможного хода
        /// </summary>
        /// <param name="gameTime">Игровое время</param>
        public void UpdateTime(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Метод обработки ввода пользователя
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="map"></param>
        private void Input(GameTime gameTime, Map map)
        {
            {
                if ((Keyboard.GetState().IsKeyDown(Keys.D)) &&
                    ((map.GetTile(new Point(rectangle.Center.X+75,rectangle.Center.Y))).Passability))
                {
                    position.X += (float)75;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(Keys.A)) &&
                    ((map.GetTile(new Point(rectangle.Center.X - 75, rectangle.Center.Y))).Passability))
                {
                    position.X -= (float)75;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(Keys.W)) &&
                    ((map.GetTile(new Point(rectangle.Center.X, rectangle.Center.Y - 75))).Passability))
                {
                    position.Y -= (float)75;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(Keys.S)) &&
                    ((map.GetTile(new Point(rectangle.Center.X, rectangle.Center.Y + 75))).Passability))
                {
                    position.Y += (float)75;
                    counter = 0;
                }
                else velocity = Vector2.Zero;
            }
        }

        //FIX IT
        /// <summary>
        /// Проверка на возможность напасть на монстра
        /// </summary>
        /// <param name="mapBot"></param>
        public void CheckAtack(MapBot mapBot)
        {
            if (mapBot.Position.X - position.X > 75 &&
                mapBot.Position.Y - position.Y < 75)
            {
                checkAtack = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.DrawString(Game1.spriteFront, text, new Vector2(-50, -50), Color.White);
        }
    }
}
