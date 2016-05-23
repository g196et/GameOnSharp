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
    class Button
    {
        Action action;
        public Action Action { get { return action; } set { action = value; } }

        Texture2D curTexture;
        Texture2D[] textures;
        
        Rectangle rectangle;
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        
        bool pressed;
        
        string text; 
        public string Text { get { return text; } set { text = value; } }
        public Button(Rectangle rectangle,string text)
        {
            this.rectangle = rectangle;
            textures=new Texture2D[2];
            pressed = false;
            this.text = text;
        }

        public void LoadContent(ContentManager Content)
        {
            textures[0] = Content.Load<Texture2D>("ButtonTexture0");
            textures[1] = Content.Load<Texture2D>("ButtonTexture1");
            curTexture = textures[0];
        }

        public void Update(GameTime gameTime)
        {
            // Получаем состояние мыши.
            MouseState mouseState = Mouse.GetState();

            // Проверяем, находится ли курсор внутри кнопки.
            if (rectangle.Contains(mouseState.X, mouseState.Y))
            {
                curTexture = textures[0];
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // Выполняем действие. 
                    if ((Action != null) && (!pressed))
                        Action.Invoke();
                    // Меняем состояние.
                    pressed = true;
                }
                else
                    pressed = false;
            }
            else
            {
                curTexture = textures[1];
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(curTexture,rectangle,Color.White);
            spriteBatch.DrawString(MapState.spriteFont, text, new Vector2(rectangle.Location.X, rectangle.Location.Y), Color.White);
        }
    }
}
