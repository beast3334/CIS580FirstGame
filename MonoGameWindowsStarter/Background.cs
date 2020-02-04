using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
namespace MonoGameWindowsStarter
{
    public class Background
    {
        Texture2D texture;
        Game1 game;
        public Background(Game1 game)
        {
            this.game = game;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("background");

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
        }
    }
}
