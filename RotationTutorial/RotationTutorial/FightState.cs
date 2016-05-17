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

namespace RotationTutorial
{
    class FightState:IGame
    {
        enum State : int{ MapState=1, FightState, HeroInfo, MenuState}
        State state = new State();
        const double coefficient34 = 0.75;
        const int timeDelay = 300;
        
        bool turn = true;
        double counter = 0;
        string txt = "";

        ContentManager Content;
        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get; set; }
        Hero hero; public Hero Hero { get { return hero; } }
        public Enemy Enemy { get; set; }
        
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

        public FightState(ContentManager Content, GraphicsDeviceManager graphics)
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
            Enemy = new Enemy();
            hero = new Hero();
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
                 (int)(graphics.PreferredBackBufferHeight *coefficient34));
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
            hero.LoadContent(heroTexture, healthBarTexture, manaBarTexture, energyBarTexture);
            Enemy.LoadContent(enemyTexture, healthBarTexture, manaBarTexture, energyBarTexture);
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
                if (counter < timeDelay)
                {
                    counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    turn = hero.Input(Enemy);
                    if (hero.Check)
                    {
                        counter = 0;
                        hero.Check = false;
                    }

                    //turn = hero.Input(enemy);
                    txt = "Hero' turn";
                }
            }
            else
            {
                if (counter < timeDelay)
                {
                    counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    counter = 0;
                    turn = Enemy.Input(hero);
                    txt = "Bot's turn";
                }
            }
            hero.Update();
            Enemy.Update();
            //Если выйграл Герой
            if (Enemy.Health.Current <= 0)
            {
                hero.Level.CurrentExperience += 100;
                if (hero.Level.CheckLevel())
                {
                    hero.StatPoints += 1;
                    hero.Health.Current = hero.Health.Max;
                    hero.Mana.Current = hero.Mana.Max;
                }
                counter = 0;
                turn = true;
                hero.Energy.Current = hero.Energy.Max;
                state = State.MapState;
                return (int)state;
            }
            //Если выйграл Моб
            if (hero.Health.Current <= 0)
            {
                state = State.MenuState;
                return (int)state;
            }
            state = State.FightState;
            return (int)state;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundTexture1, backgroundRectangle1, Color.AliceBlue);
            spriteBatch.Draw(backgroundTexture2, backgroundRectangle2, Color.AliceBlue);
            spriteBatch.DrawString(MapState.spriteFont, txt, new Vector2(450, 20), Color.White);
            spriteBatch.DrawString(MapState.spriteFont, counter.ToString(), new Vector2(450, 70), Color.White);
            hero.Draw(spriteBatch);
            Enemy.Draw(spriteBatch);
        }
    }
}