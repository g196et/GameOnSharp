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
    class Game2:IGame
    {
        bool turn = true;
        double couter = 0;

        SpriteBatch spriteBatch;
        ContentManager Content;
        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }
        Hero hero;
        Enemy enemy;
        
        Rectangle backgroundRectangle1;
        Rectangle backgroundRectangle2;

        Texture2D heroTexture;

        Texture2D enemyTexture;

        Texture2D manaBarTexture;
        Texture2D energyBarTexture;
        Texture2D healthBarTexture;
        

        //public static SpriteFont spriteFront;

        Texture2D backgroundTexture1;
        Texture2D backgroundTexture2;

        public Vector2 SpritePosition { get { return new Vector2(0,0); } }
        public Rectangle SpriteRectangle { get { return new Rectangle(0, 0, 0, 0); } }

        public Game2(ContentManager Content,GraphicsDeviceManager graphics)
        {
            this.Content = Content;
            this.graphics = graphics;            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize(Game game)
        {
            hero = new Hero(heroTexture, healthBarTexture, manaBarTexture, energyBarTexture);
            enemy = new Enemy(enemyTexture, healthBarTexture, manaBarTexture, energyBarTexture);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            //Background
            backgroundTexture1 = Content.Load<Texture2D>("FightBackground");
            backgroundTexture2 = Content.Load<Texture2D>("FightBackground1");
            backgroundRectangle1 = new Rectangle(0, 0, graphics.PreferredBackBufferWidth,
                3 * graphics.PreferredBackBufferHeight / 4);
            backgroundRectangle2 = new Rectangle(0, backgroundTexture1.Height,
                graphics.PreferredBackBufferWidth, backgroundTexture2.Height);

            //Health
            healthBarTexture = Content.Load<Texture2D>("healthBarTexture");
            

            //Mana
            manaBarTexture = Content.Load<Texture2D>("manaBarTexture");
            

            //Stamina
            energyBarTexture = Content.Load<Texture2D>("energyBarTexture");
            

            

            //Enemy
            enemyTexture = Content.Load<Texture2D>("fightEnemy");
            heroTexture = Content.Load<Texture2D>("fightMan");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public bool Update(GameTime gameTime)
        {
            if (turn)
            {
                turn = hero.Input(enemy);
            }
            else
            {
                if (couter < 500)
                {
                    couter += gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    couter = 0;
                    turn = enemy.Input(hero);
                }
            }
            hero.Update();
            enemy.Update();
            if ((enemy.Health.Current <= 0) || (hero.Health.Current <= 0))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundTexture1, backgroundRectangle1, Color.AliceBlue);
            spriteBatch.Draw(backgroundTexture2, backgroundRectangle2, Color.AliceBlue);

            hero.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            
        }
    }
}
