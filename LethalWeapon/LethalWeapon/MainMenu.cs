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
  

        public MainMenu(ContentManager content)
        {
            this.texture = content.Load<Texture2D>(@"leathalweaponwall");
            this.position = new Vector2(100, 100);
      
        }

        public void Update(GameTime gameTinme) {  }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
