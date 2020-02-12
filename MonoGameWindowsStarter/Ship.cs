using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
namespace MonoGameWindowsStarter
{
    enum State
    {
        Idle = 0,
        Up = 1,
        Right = 2,
        Left = 3
        
    }
    class Ship
    {
        Game1 game;
        ContentManager content;
        BoundingRectangle bounds;
        Texture2D texture;
        SoundEffect laserShot;
        TimeSpan timer;
        int frame;
        Boolean destroyed = false;
        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 100;
        const int FRAME_HEIGHT = 90;
        State state;
       
        public bool Destroyed
        {
            get { return destroyed; }
            set { destroyed = value; }
        }

        public BoundingRectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }
        public Rectangle RectBounds
        {
            get { return bounds; }
        }

        public Ship(Game1 game)
        {
            this.game = game;

            state = State.Idle;
            timer = new TimeSpan(0);
        }
        
        public void reset()
        {
            bounds.Y = game.GraphicsDevice.Viewport.Height - 50;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2 - 38;
            destroyed = false;
        }
        public void LoadContent(ContentManager content)
        {
            this.content = content;
            laserShot = content.Load<SoundEffect>("Laser_Shoot");
            texture = content.Load<Texture2D>("playerShip");
            
            //set master volume of sound effect
            SoundEffect.MasterVolume = 0.4f;

            bounds.Width = 80;
            bounds.Height = 70;
            bounds.Y = game.GraphicsDevice.Viewport.Height - 50;
            bounds.X = game.GraphicsDevice.Viewport.Width/2 - 38;
        }
        float shootLag = 0;
        public void Update(GameTime gameTime, ref List<Laser> lasers)
        {
            if(!destroyed)
            {
                shootLag += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                var keyboardState = Keyboard.GetState();
                if(keyboardState.IsKeyDown(Keys.Up))
                {
                    bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds/2;
                    state = State.Up;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                    state = State.Up;
                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                    state = State.Left;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                    state = State.Right;
                }
                if(keyboardState.IsKeyUp(Keys.Right) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Up))
                {
                    state = State.Idle;
                }
                if(keyboardState.IsKeyDown(Keys.Space))
                {
                    if(shootLag > 500)
                    {
                        lasers.Add(new Laser(game, content, (int)bounds.X + 50, (int)bounds.Y));
                        laserShot.Play();
                        shootLag = 0;
                    }
                }

                if (state != State.Idle) timer += gameTime.ElapsedGameTime;

                while(timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
                {
                    frame++;

                    timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
                }

                frame %= 1;
            }
            //check Y bounds
            if (bounds.Y < 0)
            {
                bounds.Y = 0;
            }
            if(bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            //check X bounds
            if(bounds.X < 0)
            {
                bounds.X = 0;
            }
            if(bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!destroyed)
            {
                var source = new Rectangle(
                    frame * FRAME_WIDTH,
                    (int)state % 4 * FRAME_HEIGHT,
                    FRAME_WIDTH,
                    FRAME_HEIGHT);
                spriteBatch.Draw(texture, bounds, source, Color.White);
            }
        }
    }
}
