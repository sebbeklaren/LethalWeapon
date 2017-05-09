using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LethalWeapon
{
    class BossOne : GameObject
    {
        Vector2 playerPosition;
        Vector2 bossVelocity;
        Rectangle bossRect, hitBox;
        Texture2D missileTexture, bulletTexture, tempText;
        Missile missile;
        BossOneBullets bullet;
        List<Missile> missileList = new List<Missile>();
        List<BossOneBullets> bulletList = new List<BossOneBullets>();
        InputManager input;
        double timeMissileRight, timeMissileLeft;
        int screenHeight, screenWidth, randPosX, randPosY;
        double elapsedBulletTime = 0;
        Random randomPos;
        double bossMaxHealth = 1000;
        bool bossIsAlive = true;
        protected Texture2D healtBarTexture, borderTexture;
        protected Vector2 healthPosition;
        protected Rectangle healthRect;
        protected double health;

        public double BossCurrentHealth { get; set; }


        public BossOne(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content, int screenWidth, int screenHeight): 
            base (texture, position, sourceRect)
        {
            bossRect = new Rectangle(0, 0, texture.Width * 2, texture.Height * 2);
            
            missileTexture = content.Load<Texture2D>(@"Missile");
            bulletTexture = content.Load<Texture2D>(@"BossBullet");            
            healtBarTexture = content.Load<Texture2D>(@"Gui/HealthBar");
            borderTexture = content.Load<Texture2D>(@"Gui/barBorder");
            input = new InputManager();
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            bossVelocity = new Vector2(1, 0);
            BossCurrentHealth = 1000;     
               
        }

        public void Update(Player player, GameTime gameTime, Weapon weapon, Vector2 cameraPosition)
        {

            healthPosition = cameraPosition;
            health = (BossCurrentHealth / bossMaxHealth) * 200;
            if (bossIsAlive)
            {
                MissileAway(gameTime, player);
                BulletAway(gameTime, player);
                Move();
            }
            hitBox = new Rectangle((int)position.X + 90,(int)position.Y + 90, 32, 48);
            ProjectileCollision(player, weapon);
            ProjectileCollision(player, weapon);
            healthRect = new Rectangle((int)healthPosition.X - 200, (int)healthPosition.Y,
                   (int)health, healtBarTexture.Height / 4);
        }

        private void MissileAway(GameTime gameTime, Player player)
        {
            timeMissileRight += gameTime.ElapsedGameTime.TotalSeconds;
            timeMissileLeft += gameTime.ElapsedGameTime.TotalSeconds;
            int startPosOffsetRight = 170;
            int startPosOffsetLeft = 0;
            if (timeMissileRight >= 6)
            {
                ShootMissile(startPosOffsetRight);
                timeMissileRight = 0;
            }
            else if (timeMissileLeft >= 8)
            {
                ShootMissile(startPosOffsetLeft);
                timeMissileLeft = 0;
            }
            foreach (Missile projectile in missileList)
            {
                projectile.Update(player.position);
            }
        }
        private void BulletAway(GameTime gameTime, Player player)
        {            
            elapsedBulletTime += gameTime.ElapsedGameTime.TotalSeconds;
            int startPos = 120;
            
            if(elapsedBulletTime >= 2)
            {
                ShootBullets(startPos, player);
                elapsedBulletTime = 0;
            }
            foreach(BossOneBullets bullets in bulletList)
            {
                bullets.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            int helthrectOffset = 100;
            int helthrectOffsetX = 200;
            sb.Draw(texture, position, bossRect, Color.White);         
            foreach (Missile projectile in missileList)
            {
                projectile.Draw(sb);
            }
            foreach(BossOneBullets bullets in bulletList)
            {
                bullets.Draw(sb);
            }
            sb.Draw(healtBarTexture, new Rectangle(healthRect.X + helthrectOffsetX, healthRect.Y - helthrectOffset, (int)health, healtBarTexture.Height /4  ), Color.White);
            sb.Draw(borderTexture, new Rectangle((int)healthRect.X + helthrectOffsetX, (int)healthRect.Y  - helthrectOffset,
                 (int)bossMaxHealth / 5 , healtBarTexture.Height / 4), Color.White);
        }

        public void ProjectileCollision(Player player, Weapon weapon)
        {
            for (int i = 0; i < missileList.Count; i++)
            {
                for (int j = 0; j < weapon.bullets.Count; j++)
                {
                    if (i <= missileList.Count -1)
                    {
                        if (Vector2.Distance(weapon.bullets[j].position, missileList[i].position) < 10 && missileList.Count >= 1)
                        {
                            missileList.Remove(missileList[i]);
                            weapon.bullets.Remove(weapon.bullets[j]);
                        }
                        else
                        {

                        }
                    }
                }
            }
            for (int i = 0; i < missileList.Count; i++)
            {
                if (Vector2.Distance(missileList[i].position, new Vector2(player.position.X + 12, player.position.Y + 24)) < 50 && missileList.Count >= 1)
                {
                    missileList.Remove(missileList[i]);
                    player.PlayerCurrentHealth -= 30;
                }
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (Vector2.Distance(bulletList[i].position, new Vector2(player.position.X + 16, player.position.Y + 24)) < 20.0f && bulletList.Count >= 1)
                {
                    bulletList.Remove(bulletList[i]);
                    player.PlayerCurrentHealth -= 10;
                }
            }
            for(int i = 0; i < weapon.bullets.Count; i++)
            {
                if(weapon.bullets[i].HitBox.Intersects(hitBox))
                {
                    BossCurrentHealth -= 30;
                    weapon.bullets.Remove(weapon.bullets[i]);
                }
            }
            
            if (BossCurrentHealth <= 0)
            {
                bossIsAlive = false;
            }
        }
        public void Random()
        {
             randomPos = new Random();
             randPosX = randomPos.Next(-2, 2);
             randPosY = randomPos.Next(-2, 2);
        }
        public void Move()
        {
            Random();
            
            if(position.X >= screenWidth - texture.Width)
            {
                position.X = screenWidth - texture.Width ;
                if (randPosX == 0 || randPosY == 0)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);                
            }
            if(position.X <= 0)
            {
                if(position.Y <= 0)
                {
                    bossVelocity = new Vector2(randPosX, randPosY);
                }
                position.X = 3;
                if (randPosX == 0 || randPosY == 0)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            if(position.Y >= screenHeight - texture.Height)
            {
                position.Y = screenHeight - texture.Height;
                if (randPosX == 0 || randPosY == 0)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            if(position.Y <= 0 || position.X <= 0)
            {
                position.Y = 3;
                if (randPosX == 0 || randPosY == 0)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            position += bossVelocity;
        }

        private void ShootMissile(int startPos)
        {
            Rectangle missileRect = new Rectangle(0, 0, missileTexture.Width, missileTexture.Height);
            Vector2 missilePosition = new Vector2(position.X+ 30 + startPos, position.Y+ 30);

            missile = new Missile(missileTexture, missilePosition, missileRect);
            missileList.Add(missile);
        }

        private void ShootBullets(int startPos, Player player)
        {
            Rectangle bulletRect = new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height);
            Vector2 bulletPosition = new Vector2(position.X + startPos, position.Y + startPos);
            int spread = 30;
            for (int i = 0; i <= 10; i++)
            {
                bullet = new BossOneBullets(bulletTexture, new Vector2(bulletPosition.X , bulletPosition.Y), bulletRect, player, (spread * i));
                bulletList.Add(bullet);
            }
        }

        private void ShootLaser()
        {

        }
    }
}
