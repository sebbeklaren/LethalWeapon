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
        public Rectangle hitBox;
        public Rectangle HitBox
        {
            get { return hitBox; }
        }
        public int speed;
        public Vector2 bulletStartingPosition;
        public Vector2 bulletDestination;
        public float bulletRotation;
        public float startRotation;
        public bool shotFired = false;
        public Bullet(Texture2D texture)
        {
            this.texture = texture;
            speed = 5;
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(Player player)
        {
            if (shotFired == false)
            {
                position = bulletStartingPosition + new Vector2(16, 24);
                bulletRotation = startRotation;
            }
            if (position == player.Position + new Vector2(16, 24))
            {
                shotFired = true;
                bulletDestination = player.AimPosition - player.Position;
            }
            if (shotFired == true)
            {
                position += Vector2.Normalize(bulletDestination) * speed;
            }
            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, null, Color.White, bulletRotation, new Vector2 (5,5), 1, SpriteEffects.None, 0f);
        }
    }
}
