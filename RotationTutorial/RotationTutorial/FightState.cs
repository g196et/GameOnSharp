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
    public class FightState:IGame
    {
        enum State : int{ MapState=1, FightState, HeroInfo, MenuState}
        enum Skill : int { Punch = -1, FireBall = 0, Regeneration = 1 }
        State state = new State();
        const double coefficient34 = 0.75;
        const int forLog = 15;
        const int size = 75;
        const int space = 100;
        const int timeDelay = 300;
        const int skip = 150;
        int yFightPanel;
        
        int? turn = null;
        double counter = 0;
        string txt = "Hero";
        List<string> log;

        ContentManager Content;
        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get; set; }
        Hero hero; public Hero Hero { get { return hero; } set { hero = value; } }
        public Enemy Enemy { get; set; }
        IPerson currentPerson;
        IPerson notCurrentPerson;
        
        Rectangle backgroundRectangle1;
        Rectangle backgroundRectangle2;

        Texture2D heroTexture;

        Texture2D enemyTexture;

        Texture2D manaBarTexture;
        Texture2D energyBarTexture;
        Texture2D healthBarTexture;

        Dictionary<String, Texture2D> skillDictionary;
        List<Rectangle> listRectungle;

        //Texture2D fireBallTexture;
        //Rectangle fireBallRectangle;
        //Texture2D regenTexture;
        //Rectangle regenRectangle;

        Texture2D backgroundTexture1;
        Texture2D backgroundTexture2;

        public Vector2 SpritePosition { get { return new Vector2(0,0); } }
        public Rectangle SpriteRectangle { get { return new Rectangle(0, 0, 0, 0); } }

        public FightState(ContentManager Content, GraphicsDeviceManager graphics,Hero hero)
        {
            this.hero = hero;
            this.Content = Content;
            this.graphics = graphics;
            yFightPanel = graphics.PreferredBackBufferHeight / 4 * 3;
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
            log = new List<string>();
            currentPerson = Enemy;
            notCurrentPerson = hero;
            skillDictionary = new Dictionary<string, Texture2D>();
            listRectungle = new List<Rectangle>();
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
            
            enemyTexture = Content.Load<Texture2D>("fightEnemy");
            heroTexture = Content.Load<Texture2D>("fightMan");
            hero.LoadContent(heroTexture, healthBarTexture, manaBarTexture, energyBarTexture, Content);
            Enemy.LoadContent(enemyTexture, healthBarTexture, manaBarTexture, energyBarTexture);

            log = new List<string>();

            listRectungle = new List<Rectangle>();
            skillDictionary = new Dictionary<string, Texture2D>();
            skillDictionary.Add("Punch", Content.Load<Texture2D>("Punch"));
            listRectungle.Add(new Rectangle(0, graphics.PreferredBackBufferHeight / 4 * 3,
                size, size));
            int i = 1;
            foreach(ISkill skill in hero.ListSkill)
            {
                skillDictionary.Add(skill.Name, Content.Load<Texture2D>(skill.Name));
                listRectungle.Add(new Rectangle(space * i, graphics.PreferredBackBufferHeight / 4 * 3,
                    size, size));
                i++;
            }
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
            if (turn != null)
            {
                if (counter < timeDelay)
                {
                    counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    turn = currentPerson.Input(notCurrentPerson);
                    if (((turn != null) && ((int)turn != skip)))
                    {
                        counter = 0;
                        if (turn == (int)Skill.Punch)
                            log.Add(txt + " : Punch");
                        else
                            log.Add(txt + " : " + currentPerson.ListSkill[(int)turn].Name);
                    }
                }
            }
            else
            {
                if (notCurrentPerson == hero)
                {
                    txt = "Hero";
                    currentPerson = hero;
                    notCurrentPerson = Enemy;                  
                }
                else
                {
                    txt = "Enemy";
                    currentPerson = Enemy;
                    notCurrentPerson = hero;
                }
                turn = 0;
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
                turn = null;
                notCurrentPerson = hero;
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
            //Вывод лога
            for (int i = log.Count - 1; i >= 0; i--)
            {
                spriteBatch.DrawString(MapState.spriteFont, log[i], new Vector2(graphics.PreferredBackBufferWidth / 3,
                    graphics.PreferredBackBufferHeight / 4 * 3 + forLog * (log.Count-i)), Color.Black);
            }
            //Отрисовка скиллов
            spriteBatch.DrawString(MapState.spriteFont, "Q - ", new Vector2(0, yFightPanel), Color.White);
            spriteBatch.Draw(skillDictionary["Punch"], listRectungle[0], Color.White);
            int j = 1;
            foreach (ISkill skill in hero.ListSkill)
            {
                //Доделать
                spriteBatch.DrawString(MapState.spriteFont, hero.ListSkill[j-1].Key.ToString(), 
                    new Vector2(space*j, yFightPanel), Color.White);
                spriteBatch.Draw(skillDictionary[skill.Name], listRectungle[j], Color.White);
                j++;
            }
        }
    }
}
