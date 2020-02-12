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
    public class Laser
    {
        Game1 game;
        BoundingRectangle bounds;
        Texture2D texture;
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

        public Laser(Game1 game, ContentManager content, int boundsX, int boundsY)
        {
            this.game = game;
            LoadContent(content, boundsX, boundsY);
        }
        public void LoadContent(ContentManager content, int boundsX, int boundsY)
        {
            texture = content.Load<Texture2D>("laser");
            bounds.Width = 9;
            bounds.Height = 37;
            bounds.X = boundsX;
            bounds.Y = boundsY;
        }
        public void Update(GameTime gameTime)
        {
            bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if(bounds.Y < 0)
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
