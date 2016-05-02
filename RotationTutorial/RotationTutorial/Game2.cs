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
        SpriteBatch spriteBatch;
        ContentManager Content;
        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }
        Hero hero;
        Enemy enemy;
        bool atackHero;
        
        Rectangle backgroundRectangle1;
        Rectangle backgroundRectangle2;

        Texture2D heroTexture;
        Rectangle heroRectangle;

        Texture2D enemyTexture;
        Rectangle enemyRectangle;

        Rectangle manaRectangle;
        Texture2D manaBarTexture;
        Rectangle enemyManaBar;

        Rectangle heroEnergyRectangle;
        Texture2D energyBarTexture;
        Rectangle enemyEnergyRectangle;

        Rectangle heroHealthRectangle;
        Texture2D healthBarTexture;
        Rectangle enemyHealthRectangle;

        //public static SpriteFont spriteFront;

        Texture2D backgroundTexture1;
        Texture2D backgroundTexture2;

        public Vector2 SpritePosition { get { return new Vector2(0,0); } }
        public Rectangle SpriteRectangle { get { return new Rectangle(0, 0, 0, 0); } }

        public Game2(ContentManager Content,GraphicsDeviceManager graphics)
        {
            this.Content = Content;
            this.graphics = graphics;
            hero = new Hero();
            enemy = new Enemy();
            atackHero = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize(Game game)
        {
   
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
            healthBarTexture = Content.Load<Texture2D>("healthBarTexture1");
            heroHealthRectangle = new Rectangle(10, 10, healthBarTexture.Width*hero.Health.Current,
                healthBarTexture.Height);
            enemyHealthRectangle = new Rectangle(900, 10, healthBarTexture.Width*enemy.Health.Current,
                healthBarTexture.Height);

            //Mana
            manaBarTexture = Content.Load<Texture2D>("manaBarTexture");
            manaRectangle = new Rectangle(heroHealthRectangle.X, heroHealthRectangle.Y
                + heroHealthRectangle.Height + 10,
                manaBarTexture.Width, manaBarTexture.Height);
            enemyManaBar = new Rectangle(enemyHealthRectangle.X,
                enemyHealthRectangle.Y + enemyHealthRectangle.Height + 10, manaBarTexture.Width,
                manaBarTexture.Height);

            //Stamina
            energyBarTexture = Content.Load<Texture2D>("energyBarTexture");
            heroEnergyRectangle = new Rectangle(manaRectangle.X, manaRectangle.Y
                + manaRectangle.Height + 10,
                energyBarTexture.Width, energyBarTexture.Height);
            enemyEnergyRectangle = new Rectangle(enemyManaBar.X, enemyManaBar.Y
                + enemyManaBar.Height + 10,
                enemyHealthRectangle.Width, enemyHealthRectangle.Height);

            //Hero
            heroTexture = Content.Load<Texture2D>("fightMan");
            heroRectangle = new Rectangle(200, 200, heroTexture.Width, heroTexture.Height);

            //Enemy
            enemyTexture = Content.Load<Texture2D>("fightEnemy");
            enemyRectangle = new Rectangle(900, 200, heroTexture.Width, heroTexture.Height);
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
            if ((Keyboard.GetState().IsKeyDown(Keys.Q)) && (atackHero == true))
            {
                enemy.Health.Current -=5;
                enemyHealthRectangle.Width = healthBarTexture.Width*enemy.Health.Current;
                atackHero = false;
            }
            else if ((Keyboard.GetState().IsKeyDown(Keys.E)) && (atackHero == false))
            {
                hero.Health.Current -= 1;
                heroHealthRectangle.Width = healthBarTexture.Width * hero.Health.Current;
                atackHero = true;
            }

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
            
            spriteBatch.Draw(healthBarTexture, heroHealthRectangle, Color.White);
            spriteBatch.Draw(healthBarTexture, enemyHealthRectangle, Color.White);
            
            spriteBatch.Draw(heroTexture, heroRectangle, Color.White);
            spriteBatch.Draw(enemyTexture, enemyRectangle, Color.White);
            
            spriteBatch.Draw(manaBarTexture, manaRectangle, Color.White);
            spriteBatch.Draw(manaBarTexture, enemyManaBar, Color.White);
            
            spriteBatch.Draw(energyBarTexture, heroEnergyRectangle, Color.White);            
            spriteBatch.Draw(energyBarTexture, enemyEnergyRectangle, Color.White);
        }
    }
}
