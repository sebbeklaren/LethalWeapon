using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LethalWeapon
{
    class BossOne : GameObject
    {        
        Vector2 bossVelocity;
        Rectangle bossRect, hitBox;
        Texture2D missileTexture, bulletTexture;
        Missile missile;
        BossOneBullets bullet;
        List<Missile> missileList = new List<Missile>();
        List<BossOneBullets> bulletList = new List<BossOneBullets>();
        InputManager input;
        double timeMissileRight, timeMissileLeft;
        int screenHeight, screenWidth, randPosX, randPosY;
        double elapsedBulletTime = 0;
        Random calculateRandomPos;
        double bossMaxHealth = 100;
        bool bossIsAlive = true;
        protected Texture2D healtBarTexture, borderTexture;
        protected Vector2 healthPosition;
        protected Rectangle healthRect;
        protected double health;

        public double BossCurrentHealth { get; set; }


        public BossOne(Texture2D texture, Vector2 position, Rectangle sourceRect, int screenWidth, int screenHeight): 
            base (texture, position, sourceRect)
        {
            int bossRectX = 0;
            int bossRectY = 0;
            int bossRectMultiplyX = 2;
            int bossRectMultiplyY = 2;
            int bossStartVelocityX = -1;
            int bossStartVelocityY = 0;
            bossRect = new Rectangle(bossRectX, bossRectY, texture.Width * bossRectMultiplyX, texture.Height * bossRectMultiplyY);            
            missileTexture = TextureManager.BossMissileTexture;
            bulletTexture = TextureManager.BossBulletTexture;            
            healtBarTexture = TextureManager.HealtBarTexture;
            borderTexture = TextureManager.HealthBorderTexture;
            input = new InputManager();
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            bossVelocity = new Vector2(bossStartVelocityX, bossStartVelocityY);
            BossCurrentHealth = 100;     
               
        }

        public void Update(Player player, GameTime gameTime, Weapon weapon, Vector2 cameraPosition)
        {
            int healthBarMultiplier = 200;
            int healthRectOffset = 200;
            int healthRectHeightOffset = 4;
            healthPosition = cameraPosition;
            health = (BossCurrentHealth / bossMaxHealth) * healthBarMultiplier;
            if (bossIsAlive)
            {
                MissileAway(gameTime, player);
                BulletAway(gameTime, player);
                Movement();                
                SoundManager.BossAmbientHover.Play();
            }
            else if(!bossIsAlive)
            {
                bulletList.Clear();
                missileList.Clear();               
            }
            int hitBoxOffset = 90;
            int hitBoxWidth = 32;
            int hitBoxHeight = 48;
            hitBox = new Rectangle((int)position.X + hitBoxOffset, (int)position.Y + hitBoxOffset, hitBoxWidth, hitBoxHeight);
            ProjectileCollision(player, weapon);
            ProjectileCollision(player, weapon);
            healthRect = new Rectangle((int)healthPosition.X - healthRectOffset, (int)healthPosition.Y,
                   (int)health, healtBarTexture.Height / healthRectHeightOffset);
        }

        private void MissileAway(GameTime gameTime, Player player)
        {
            timeMissileRight += gameTime.ElapsedGameTime.TotalSeconds;
            timeMissileLeft += gameTime.ElapsedGameTime.TotalSeconds;
            int startPosOffsetRight = 170;
            int startPosOffsetLeft = 0;
            int missileTimerRight = 6;
            int missileTimerLeft = 8;
            if (timeMissileRight >= missileTimerRight)
            {
                SoundManager.BossMissile.Play();
                ShootMissile(startPosOffsetRight);
                timeMissileRight = 0;
            }
            else if (timeMissileLeft >= missileTimerLeft)
            {
                SoundManager.BossMissile.Play();
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
            int bulletTimer = 2;
            if(elapsedBulletTime >= bulletTimer)
            {
                ShootBullets(startPos, player);
                elapsedBulletTime = 0;
                SoundManager.BossBullets.Play();
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
            int healtBarHeightOffset = 4;
            int borderWidthOffset = 2;
            sb.Draw(healtBarTexture, new Rectangle(healthRect.X + helthrectOffsetX, healthRect.Y - helthrectOffset, 
                                                (int)health, healtBarTexture.Height / healtBarHeightOffset), Color.White);
            sb.Draw(borderTexture, new Rectangle((int)healthRect.X + helthrectOffsetX, (int)healthRect.Y  - helthrectOffset,
                                                (int)bossMaxHealth * borderWidthOffset, healtBarTexture.Height / 4), Color.White);
        }

        public void ProjectileCollision(Player player, Weapon weapon)
        {
            //träff mellan playerbullets och misiler
            int distancePlayerBulletsMissile = 10;
            for (int i = 0; i < missileList.Count; i++)
            {
                for (int j = 0; j < weapon.bullets.Count; j++)
                {
                    if (i <= missileList.Count -1)
                    {
                        if (Vector2.Distance(weapon.bullets[j].position, missileList[i].position) < distancePlayerBulletsMissile && missileList.Count >= 1)
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
            //träff mellan misiler och player
            for (int i = 0; i < missileList.Count; i++)
            {
                int playerHitOffsetX = 12;
                int playerHitOffsetY = 24;
                int distancePlayerMissile = 50;
                if (Vector2.Distance(missileList[i].position, new Vector2(player.position.X + playerHitOffsetX, player.position.Y + playerHitOffsetY)) < distancePlayerMissile
                    && missileList.Count >= 1)
                {
                    missileList.Remove(missileList[i]);
                    player.PlayerCurrentHealth -= 30;
                }
            }
            //träff mellan bossbullets och spelare
            for (int i = 0; i < bulletList.Count; i++)
            {
                int playerHitOffsetX = 16;
                int playerHitOffsetY = 24;
                int distancePlayerBullets = 20;
                if (Vector2.Distance(bulletList[i].position, new Vector2(player.position.X + playerHitOffsetX, player.position.Y + playerHitOffsetY)) < distancePlayerBullets && bulletList.Count >= 1)
                {
                    bulletList.Remove(bulletList[i]);
                    player.PlayerCurrentHealth -= 10;
                }
            }
            //träff mellan playerbullets och boss
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
             calculateRandomPos = new Random();
             randPosX = calculateRandomPos.Next(-2, 2);
             randPosY = calculateRandomPos.Next(-2, 2);
        }
        public void Movement()
        {
            Random();
            
            if(position.X >= screenWidth - texture.Width)
            {
                position.X = screenWidth - texture.Width ;
                int noValidSpeed = 0;
                if (randPosX == noValidSpeed || randPosY == noValidSpeed)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);                
            }
            int screenWidthBegining = 0;
            int screenHeightBegining = 0;
            if (position.X <= screenWidthBegining)
            {
                if(position.Y <= screenHeightBegining)
                {
                    bossVelocity = new Vector2(randPosX, randPosY);
                }
                position.X = 3;
                int noValidSpeed = 0;
                if (randPosX == noValidSpeed || randPosY == noValidSpeed)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            if(position.Y >= screenHeight - texture.Height)
            {
                position.Y = screenHeight - texture.Height;
                int noValidSpeed = 0;
                if (randPosX == noValidSpeed || randPosY == noValidSpeed)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            if(position.Y <= 0)
            {
                position.Y = 3;
                int noValidSpeed = 0;
                if (randPosX == noValidSpeed || randPosY == noValidSpeed)
                {
                    Random();
                }
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            position += bossVelocity;
        }

        private void ShootMissile(int startPos)
        {
            int missileStartOffsetX = 30;
            int missileStartOffsetY = 30;
            Rectangle missileRect = new Rectangle(0, 0, missileTexture.Width, missileTexture.Height);
            Vector2 missilePosition = new Vector2(position.X + missileStartOffsetX + startPos, position.Y+ missileStartOffsetY);

            missile = new Missile(missileTexture, missilePosition, missileRect);
            missileList.Add(missile);
        }

        private void ShootBullets(int startPos, Player player)
        {
            Rectangle bulletRect = new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height);
            Vector2 bulletPosition = new Vector2(position.X + startPos, position.Y + startPos);
            int bulletSpread = 30;
            for (int i = 0; i <= 10; i++)
            {
                bullet = new BossOneBullets(bulletTexture, new Vector2(bulletPosition.X , bulletPosition.Y), bulletRect, player, (bulletSpread * i));
                bulletList.Add(bullet);
            }
        }

        private void ShootLaser()
        {

        }
    }
}
