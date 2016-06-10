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
    public interface IGame
    {
        Song Song { get; set; }
        void Initialize(Game game);
        void LoadContent(ContentManager content);
        void UnloadContent();
        int Update(GameTime gameTime);
        void Draw( SpriteBatch spriteBatch);
    }
}
