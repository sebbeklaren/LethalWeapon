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
    class MainMenu
    {
        protected Texture2D texture;
        protected Vector2 position;
  

        public MainMenu(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
      
        }

        public void Update(GameTime gameTinme) {  }

        public virtual void DrawMainMenu(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, position, Color.White);
        
        }
    }
}
