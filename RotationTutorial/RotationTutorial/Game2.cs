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
        double counter = 0;
        string txt = "";

        SpriteBatch spriteBatch;
        ContentManager Content;
        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }
        Hero hero; public Hero Hero { get { return hero; } }
        Enemy enemy; public Enemy Enemy { get { return enemy; } set { enemy = value; } }
        
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
            enemy = new Enemy();
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
            hero = new Hero(heroTexture, healthBarTexture, manaBarTexture, energyBarTexture);
            enemy.LoadContent(enemyTexture, healthBarTexture, manaBarTexture, energyBarTexture);
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
        public int Update(GameTime gameTime)
        {
            if (turn)
            {
                if (counter < 100)
                {
                    counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    counter = 0;
                    turn = hero.Input(enemy);
                    txt = "Hero";
                }
            }
            else
            {
                if (counter < 300)
                {
                    counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    counter = 0;
                    turn = enemy.Input(hero);
                    txt = "Bot";
                }
            }
            hero.Update();
            enemy.Update();
            //Если выйграл Герой
            if (enemy.Health.Current <= 0)
            {
                counter = 0;
                while (counter < 10000)
                {
                    counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                turn = true;
                return 1;
            }
            //Если выйграл Моб
            if (hero.Health.Current <= 0)
                return 4;
            return 2;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundTexture1, backgroundRectangle1, Color.AliceBlue);
            spriteBatch.Draw(backgroundTexture2, backgroundRectangle2, Color.AliceBlue);
            spriteBatch.DrawString(Game1.spriteFront, txt, new Vector2(450, 20), Color.White);
            hero.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            
        }
    }
}
