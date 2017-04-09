using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    class Weapon : GameObject
    {
        Rectangle weaponHitbox;
        //Behandling av utritning
        public float weaponRotation;
        public float weaponScale = 1;
        Vector2 dPos; 
        Vector2 weaponOrigin;

        Texture2D bulletTexture;
        bool weaponOnGround = true;
        bool weaponPickedUp = false;
        float bulletSpeed = 2f;
        Vector2 bulletPosition;
        int bulletDirection;
        public List<Bullet> bullets = new List<Bullet>();
        public Weapon(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content) : base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            bulletTexture = content.Load<Texture2D>("Bullet");
            weaponOrigin = new Vector2(texture.Bounds.Center.X, texture.Bounds.Center.Y);
        }
        public void Update(Player player)
        {
            weaponHitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            bulletPosition = player.Position;
            if (player.playerHitbox.Intersects(weaponHitbox))
            {
                weaponOnGround = false;
                weaponPickedUp = true;
            }
            if (weaponOnGround == false && weaponPickedUp == true)
            {
                dPos = position - player.AimPosition;
                position = new Vector2(player.Position.X, player.Position.Y + 10);
                weaponRotation = (float)Math.Atan2(dPos.Y, dPos.X);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                bulletDirection = 1;
                Bullet b = new Bullet(bulletTexture, bulletPosition);
                bullets.Add(b);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                bulletDirection = 2;
                Bullet b = new Bullet(bulletTexture, bulletPosition);
                bullets.Add(b);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                bulletDirection = 3;
                Bullet b = new Bullet(bulletTexture, bulletPosition);
                bullets.Add(b);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                bulletDirection = 4;
                Bullet b = new Bullet(bulletTexture, bulletPosition);
                bullets.Add(b);
            }
        
            ShotDirection();
        }

        public void ShotDirection()
        {
            foreach (Bullet b in bullets)
            {
                if (bulletDirection == 1)
                {
                    bulletPosition.Y -= bulletSpeed;
                }
                if (bulletDirection == 2)
                {
                    bulletPosition.X -= bulletSpeed;
                }
                if (bulletDirection == 3)
                {
                    bulletPosition.X += bulletSpeed;
                }
                if (bulletDirection == 4)
                {
                    bulletPosition.Y += bulletSpeed;
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, null, Color.White, weaponRotation, weaponOrigin, weaponScale, SpriteEffects.None, 0f);

           
            foreach(Bullet b in bullets)
            {
                sb.Draw(bulletTexture, bulletPosition, Color.White);
            }
        }
    }
}
