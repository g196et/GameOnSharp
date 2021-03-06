﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace RotationTutorial
{
    class MapHero
    {
        const int tileSize = 75;
        const int timeDelay = 500;
        Texture2D texture;
        Keys right = Keys.D;
        Keys left = Keys.A;
        Keys forward = Keys.W;
        Keys back = Keys.S;
        
        Rectangle rectangle; 
        public Rectangle Rectangle1 { get { return rectangle; } set { rectangle = value; } }
        Vector2 position; 
        public Vector2 Position { get { return position; } set { position = value; } }
        bool checkAtack; 
        public bool CheckAtackField { get { return checkAtack; } set { checkAtack = value; } }
        string text = string.Empty;
        double counter; 
        public double Counter { get { return counter; } set { counter = value; } }
        public MapHero(Vector2 newPosition)
        {
            position = newPosition;
            checkAtack = false;
            counter = 1001;
        }

        public void LoadContent(ContentManager Content,string textureName)
        {
            texture = Content.Load<Texture2D>(textureName);
        }

        public void Update(GameTime gameTime, Map map)
        { 
            rectangle = new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);
            if (counter < timeDelay)
            {
                UpdateTime(gameTime);
            }
            else
            {
                Input(gameTime, map);
            }

            if (map.GetRectangle(rectangle.Center).Mob)
            {
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
                if ((Keyboard.GetState().IsKeyDown(right)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X+tileSize,rectangle.Center.Y))).Passability))
                {
                    position.X += (float)tileSize;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(left)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X - tileSize, rectangle.Center.Y))).Passability))
                {
                    position.X -= (float)tileSize;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(forward)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X, rectangle.Center.Y - tileSize))).Passability))
                {
                    position.Y -= (float)tileSize;
                    counter = 0;
                }
                else if ((Keyboard.GetState().IsKeyDown(back)) &&
                    ((map.GetRectangle(new Point(rectangle.Center.X, rectangle.Center.Y + tileSize))).Passability))
                {
                    position.Y += (float)tileSize;
                    counter = 0;
                }
            }
        }

        public void CheckAtack(MapBot mapBot)
        {
            if (mapBot.Position == position)
            {
                checkAtack = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.DrawString(MapState.spriteFont, text, new Vector2(-50, -50), Color.White);
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(this.position.X + "#" + this.position.Y + "#" + this.rectangle);
        }
        public void Load(StreamReader reader)
        {
            string[] line = reader.ReadLine().Split(new string[] { "#","{","}",":","X","Y","Width","Height" }, StringSplitOptions.RemoveEmptyEntries);
            this.position = new Vector2(float.Parse(line[0]), float.Parse(line[1]));
            this.rectangle.X = int.Parse(line[2]);
            this.rectangle.Y = int.Parse(line[3]);
            this.rectangle.Width = int.Parse(line[4]);
            this.rectangle.Height = int.Parse(line[5]);
        }

    }
}
