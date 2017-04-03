using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class Bullet
    {
        Weapon weapon;
        public Texture2D texture;
        public Vector2 position;
        public Bullet(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
        }

        public void Update()
        {
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
