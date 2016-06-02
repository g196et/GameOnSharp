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
    class TrackBar
    {
        Rectangle staticRectangle;
        Rectangle movingRectangle;
        Texture2D movingTexture;
        Texture2D staticTexture;

        public TrackBar(int x,int y,int width,int height)
        {
            staticRectangle = new Rectangle(x, y, width, height);
            movingRectangle=new Rectangle(x+(int)(width*MediaPlayer.Volume),y,(int)(width*0.1f),(int)(height));
        }
        public void LoadContent(ContentManager Content)
        {
            movingTexture = Content.Load<Texture2D>("ButtonTexture2");
            staticTexture = Content.Load<Texture2D>("ButtonTexture1");
        }
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            if ((staticRectangle.Contains(mouseState.X, mouseState.Y)) 
                && (mouseState.LeftButton == ButtonState.Pressed))
            {
                movingRectangle.X = mouseState.X;
                MediaPlayer.Pause();
                MediaPlayer.Volume = (float)(movingRectangle.X - staticRectangle.X)/ staticRectangle.Width;
                MediaPlayer.Resume();
            }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MapState.spriteFont, "Volume", new Vector2(staticRectangle.X, 
                staticRectangle.Y - staticRectangle.Height),Color.Red);
            spriteBatch.Draw(staticTexture, staticRectangle, Color.Red);
            spriteBatch.Draw(movingTexture, movingRectangle, Color.Red);
        }
    }
}
