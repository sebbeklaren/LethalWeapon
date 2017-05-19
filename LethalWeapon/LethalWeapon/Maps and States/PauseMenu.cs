using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class PauseMenu
    {
        protected Texture2D texture;
        protected Vector2 position;


        public PauseMenu(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

        }

        public void Update() { }

        public virtual void DrawPauseMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
