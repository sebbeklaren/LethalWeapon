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
        public Texture2D texture;
        public Vector2 position;
        public Vector2 speed;
        public Vector2 bulletStartingPosition;
        public Vector2 bulletDestination;
        public bool shotFired = false;
        public Bullet(Texture2D texture)
        {
            this.texture = texture;
            speed = new Vector2(1, 1);
        }

        public void Update(Player player)
        {
            if (shotFired == false)
            {
                position = bulletStartingPosition;
            }
            if (position == player.Position)
            {
                shotFired = true;
                bulletDestination = player.AimPosition - player.Position;
            }
            if (shotFired == true)
            {
                position += Vector2.Normalize(bulletDestination);
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
