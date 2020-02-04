using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    class Ship
    {
        Game1 game;
        BoundingRectangle bounds;
        Texture2D texture;
        Texture2D texture2;
        Boolean destroyed = false;

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
        }
        
        public void reset()
        {
            bounds.Y = game.GraphicsDevice.Viewport.Height - 50;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2 - 38;
            destroyed = false;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerShip");
            texture2 = content.Load<Texture2D>("explosion");
            bounds.Width = 50;
            bounds.Height = 38;
            bounds.Y = game.GraphicsDevice.Viewport.Height - 50;
            bounds.X = game.GraphicsDevice.Viewport.Width/2 - 38;
        }
        public void Update(GameTime gameTime)
        {
            if(!destroyed)
            {
                var keyboardState = Keyboard.GetState();
                if(keyboardState.IsKeyDown(Keys.Up))
                {
                    bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds/2;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                }
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
                spriteBatch.Draw(texture, bounds, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture2, bounds, Color.White);
            }

        }
    }
}
