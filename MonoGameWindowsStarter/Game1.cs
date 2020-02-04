using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background background;
        Ship ship;
        List<Meteor> meteors = new List<Meteor>();
        Random random = new Random();
        
        
        private SpriteFont scoreFont;
        private SpriteFont gameOverFont;
        private SpriteFont entertoContinueFont;
        private SpriteFont difficultyFont;
        private int score = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            background = new Background(this);
            ship = new Ship(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background.LoadContent(Content);
            ship.LoadContent(Content);
            scoreFont = Content.Load<SpriteFont>("Score");
            gameOverFont = Content.Load<SpriteFont>("GameOver");
            entertoContinueFont = Content.Load<SpriteFont>("EnterToContinue");
            difficultyFont = Content.Load<SpriteFont>("Difficulty");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        float spawn = 0;
        float difficulty = 1;
        float spawnRate = 1;
        float phasetimer = 0;
        int maxMeteors = 10;
        protected override void Update(GameTime gameTime)
        {
            if (!ship.Destroyed)
            {

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                ship.Update(gameTime);
                spawn += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                phasetimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (Meteor meteor in meteors)
                {
                    meteor.Update(gameTime);
                }
                for (int i = 0; i < meteors.Count; i++)
                {
                    if (!meteors[i].isVisible)
                    {
                        score += maxMeteors * 10;
                        meteors.RemoveAt(i);
                        i--;
                    }
                }
                LoadMeteors();
                foreach (Meteor meteor in meteors)
                {

                    if (ship.RectBounds.Intersects(meteor.RectBounds))
                    {
                        ship.Destroyed = true;
                        break;
                    }
                }
            }
            else
            {
                spawn = 0;
                difficulty = 1;
                phasetimer = 0;
                spawnRate = 1;
                maxMeteors = 10;
                meteors.Clear();
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    score = 0;
                    ship.reset();
                    
                }
            }

            base.Update(gameTime);
        }
        public void LoadMeteors()
        {
            if(phasetimer >= 15)
            {
                phasetimer = 0;
                difficulty += 1;
                if(spawnRate > 0.2)
                {
                    spawnRate -= (float)0.10;
                }
                maxMeteors += 2;
            }
            if(spawn/1000 > spawnRate)
            {
                spawn = 0;
                if (meteors.Count <= 1 || meteors.Count <= maxMeteors)
                {
                    meteors.Add(new Meteor(this, Content, random, difficulty));
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            if (!ship.Destroyed)
            {
                ship.Draw(spriteBatch);
                spriteBatch.DrawString(scoreFont, "Score: " + score, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(difficultyFont, "Difficulty: " + difficulty, new Vector2(0, 20), Color.White);
                foreach (Meteor meteor in meteors)
                {
                    meteor.Draw(spriteBatch);
                }   
            }
            else
            {
                spriteBatch.DrawString(gameOverFont, "Game Over: \n Score: " + score, new Vector2(this.graphics.GraphicsDevice.Viewport.Width / 2 - 100, this.graphics.GraphicsDevice.Viewport.Height / 2 - 50), Color.White);
                spriteBatch.DrawString(entertoContinueFont, "Press spacebar to play again!", new Vector2(this.graphics.GraphicsDevice.Viewport.Width / 2 - 100, this.graphics.GraphicsDevice.Viewport.Height- 50), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
