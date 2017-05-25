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
        public Rectangle bulletSource;
        public Rectangle hitBox;
        public Rectangle HitBox
        {
            get { return hitBox; }
        }
        public int speed;
        public float bulletScale;
        public int currentBullet;
        public Vector2 bulletStartingPosition;
        public Vector2 bulletDestination;
        public Vector2 bulletOrigin;
        public float bulletRotation;
        public float startRotation;
        public bool shotFired = false;
        List<Rectangle> hitBoxList = new List<Rectangle>();
        public Bullet(Texture2D texture)
        {
            this.texture = texture;
            speed = 5;
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            bulletSource = new Rectangle(0, 0, 10, 10);
        }

        public void Update(Player player)
        {
            SwitchBullet();
            BulletMovment(player);      
            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
        }
        
        public void SwitchBullet() // Byter texture och värden för bullet beroende på vilket vapen som används
        {
            if (currentBullet == 1)
            {
                bulletOrigin = new Vector2(texture.Bounds.Center.X / 2, texture.Bounds.Center.Y / 2);
                bulletSource = new Rectangle(0, 0, 10, 10);
                bulletScale = 1f;
            }
            if (currentBullet == 2)
            {
                bulletOrigin = new Vector2(texture.Bounds.Center.X, texture.Bounds.Center.Y);
                bulletSource = new Rectangle(0, 0, 32, 32);
                bulletScale = 2.5f;
            }
        }
        public void BulletMovment(Player player)
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
        }
        public void Draw(SpriteBatch sb)
        {
                sb.Draw(texture, position, bulletSource, Color.White, bulletRotation, bulletOrigin, bulletScale, SpriteEffects.None, 0f);
        }
    }
}
