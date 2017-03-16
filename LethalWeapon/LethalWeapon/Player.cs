using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class Player : GameObject
    {


        public Player(Texture2D texture, Vector2 position): base (texture, position)
        {
            this.texture = texture;
            this.position = position;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }


    }
}
