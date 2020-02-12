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
    public class Meteor
    {
        Game1 game;
        BoundingRectangle bounds;
        Texture2D texture;
        int randxVelocity, randyVelocity, randxSpawn, randomSize;
        bool isvisible = true;
        public BoundingRectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }
        public Rectangle RectBounds
        {
            get { return bounds; }
        }
        public bool isVisible
        {
            get { return isvisible; }
        }


        public Meteor(Game1 game, ContentManager content, Random random, float difficulty)
        {
            this.game = game;
            randxVelocity = random.Next(1, (int)(2 * difficulty/2));
            randyVelocity = random.Next(1, (int)(10 * difficulty/2));
            randxSpawn = random.Next(1, 700);
            randomSize = random.Next(25, 150);
            LoadContent(content);
        }
        public Meteor(Game1 game, ContentManager content, Random random, float difficulty, int max_size, int xLocation, int yLocation)
        {
            this.game = game;
            randxVelocity = random.Next(-5, (int)(4 * difficulty));
            randyVelocity = random.Next(-5, (int)(10 * difficulty));
            randomSize = random.Next(15, max_size / 2);
            LoadContent(content, xLocation, yLocation);

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("meteor");
            bounds.Width = randomSize;
            bounds.Height = randomSize;
            bounds.Y = -10;
            bounds.X = randxSpawn;
        }
        public void LoadContent(ContentManager content, int xSpawn, int ySpawn)
        {
            texture = content.Load<Texture2D>("meteor");
            bounds.Width = randomSize;
            bounds.Height = randomSize;
            bounds.Y = ySpawn;
            bounds.X = xSpawn;
        }
        public void Update(GameTime gameTime)
        {
            bounds.Y += randyVelocity;
            bounds.X += randxVelocity;

            if (
                bounds.Y > game.GraphicsDevice.Viewport.Height ||
                bounds.X < 0 ||
                bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width
                )
            {
                isvisible = false;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }

    }
}
