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
    class SettingsButtons
    {
        Button Left { get; set; }
        Button Right { get; set; }
        Rectangle rectangle;
        int index;
        List<string> values;
        public SettingsButtons(List<string> newValues,Rectangle rectangle)
        {
            this.values = newValues;
            this.rectangle = rectangle;
            index = 0;
            Left = new Button(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width/5,rectangle.Height/5)
                , "<=");
            Right = new Button(new Rectangle(rectangle.X + rectangle.X *4/ 5, rectangle.Y,rectangle.Width/5,
                rectangle.Height/5), "=>");
        }
        public string CurValue { get { return values[index]; } set { values[index] = value; } }
        Vector2 textPosition;
        public void Initialize(Game game)
        {
            textPosition = new Vector2(rectangle.X + rectangle.X / 5, rectangle.Y);
            Left.Action += () =>
            {
                index -= 1;
                if(index<0)
                {
                    index = values.Count - 1;
                }
                
            };
            Right.Action += () =>
            {
                index += 1;
                if (index >= values.Count)
                {
                    index = 0;
                }
            };
            
        }
        public void LoadContent(ContentManager Content)
        {
            Left.LoadContent(Content);
            Right.LoadContent(Content);
        }
        public void Update(GameTime gameTime)
        {
            Left.Update(gameTime);
            Right.Update(gameTime);
            CurValue = values[index];
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Left.Draw(spriteBatch);
            Right.Draw(spriteBatch);
            spriteBatch.DrawString(MapState.spriteFont, CurValue, textPosition, Color.Red);
        }
    }
}
