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
        Rectangle bossRect, hitBox, checkForLaserRect;
        Texture2D missileTexture, bulletTexture, laserTexture;
        Missile missile;
        BossOneBullets bullet;
        BossLaser bossLaser;
        List<BossLaser> laserList = new List<BossLaser>();
        List<Missile> missileList = new List<Missile>();
        List<BossOneBullets> bulletList = new List<BossOneBullets>();
        InputManager input;
        double timeMissileRight, timeMissileLeft;
        int screenHeight, screenWidth, randPosX, randPosY;
        double elapsedBulletTime = 0;
        Random calculateRandomPos;
        double bossMaxHealth = 500;
        bool bossIsAlive = true;
        protected Texture2D healtBarTexture, borderTexture;
        protected Vector2 healthPosition;
        protected Rectangle healthRect;
        protected double health;
        int bulletSpreadX, bulletSpreadY;
        public double BossCurrentHealth { get; set; }
        bool insideLaserRect;
        bool laserHasFired;
        double toCloseTimer = 0;

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
            laserTexture = TextureManager.BossLaserTexture;
            input = new InputManager();
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            bossVelocity = new Vector2(bossStartVelocityX, bossStartVelocityY);
            BossCurrentHealth = 500;
          

        }

        public void Update(Player player, GameTime gameTime, Weapon weapon, Vector2 cameraPosition)
        {
            input.Update();
            int healthBarMultiplier = 200;
            int healthRectOffset = 200;
            int healthRectHeightOffset = 4;
            healthPosition = cameraPosition;
            health = (BossCurrentHealth / bossMaxHealth) * healthBarMultiplier;
            if (bossIsAlive)
            {
                if (checkForLaserRect.Contains(player.playerHitboxVertical))
                {
                    insideLaserRect = true;
                }
                else
                {
                    insideLaserRect = false;
                }
                LaserAway(gameTime, player);
                MissileAway(gameTime, player);
                BulletAway(gameTime, player);                
              //  Movement();                
                SoundManager.BossAmbientHover.Play();
            }
            else if(!bossIsAlive)
            {
                bulletList.Clear();
                missileList.Clear();
                laserList.Clear();
                SoundManager.BossAmbientHover.Dispose();              
            }
            int hitBoxOffset = 90;
            int hitBoxWidth = 32;
            int hitBoxHeight = 48;
            hitBox = new Rectangle((int)position.X + hitBoxOffset, (int)position.Y + hitBoxOffset, hitBoxWidth, hitBoxHeight);
            checkForLaserRect = new Rectangle((int)position.X - 100, (int)position.Y  - 75, TextureManager.BossOneTexture.Width + 200, 
                                               TextureManager.BossOneTexture.Height +150);
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
                projectile.Update(player.position, gameTime);
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
        private void LaserAway(GameTime gameTime, Player player)
        {
            
            toCloseTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            //räkna ut när den ska skjuta öaser och när den inte ska göra det
            if (insideLaserRect && !laserHasFired && toCloseTimer <= 0)
            {
                if (laserList.Count < 1)
                {
                    ShootLaser(player);
                }
                foreach (BossLaser laser in laserList)
                {
                    if (laser.frame <= 18)
                    {
                        laser.Update(gameTime, position, player.position);
                        if (laser.frame >= 18)
                        {
                            laserHasFired = true;
                            toCloseTimer = 0;
                        }
                    }
                }
            }           
            if (!insideLaserRect && laserList.Count == 1)
            {
                for (int i = 0; i < laserList.Count; i++)
                {
                    laserList[i].Update(gameTime, position, player.position);
                    if (laserList[i].frame >= 18)
                    {
                        laserHasFired = true;
                    }
                        if (laserList[i].frame >= 18 && laserHasFired && !insideLaserRect)
                    {
                        laserList.Remove(laserList[i]);
                        toCloseTimer = 0;
                        laserHasFired = false;
                    }       
                }
            }

            if (laserHasFired && insideLaserRect && laserList.Count != 0)
            {
                for (int i = 0; i < laserList.Count; i++)
                {
                    laserList.Remove(laserList[i]);
                    toCloseTimer = 0;
                    laserHasFired = false;
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            
            int helthrectOffset = 100;
            int helthrectOffsetX = 200;
            //För att se hitboxen som känner av när spelaren är för nära 
            //sb.Draw(TextureManager.HealtBarTexture, new Vector2(checkForLaserRect.X, checkForLaserRect.Y), checkForLaserRect, Color.White);
            sb.Draw(texture, position, bossRect, Color.White);
            

            foreach (Missile projectile in missileList)
            {
                projectile.Draw(sb);
            }
            foreach(BossOneBullets bullets in bulletList)
            {
                bullets.Draw(sb);
            }

            for(int i = 0; i < laserList.Count; i++)            
            {
                laserList[0].Draw(sb);
            }
            int healtBarHeightOffset = 4;
            int borderWidthOffset = 200;
            sb.Draw(healtBarTexture, new Rectangle(healthRect.X + helthrectOffsetX, healthRect.Y - helthrectOffset, 
                                                (int)health, healtBarTexture.Height / healtBarHeightOffset), Color.White);
            sb.Draw(borderTexture, new Rectangle((int)healthRect.X + helthrectOffsetX, (int)healthRect.Y  - helthrectOffset,
                                                borderWidthOffset, healtBarTexture.Height / 4), Color.White);
            
        }

        public void ProjectileCollision(Player player, Weapon weapon)
        {
            //träff mellan playerbullets och misiler           
            for (int i = 0; i < missileList.Count; i++)
            {
                for (int j = 0; j < weapon.bullets.Count; j++)
                {
                    if (i <= missileList.Count -1)
                    {
                        if (missileList[i].hitBox.Intersects(weapon.bullets[j].HitBox) && missileList.Count >= 1)
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
                if (player.playerHitboxVertical.Intersects(missileList[i].hitBox)
                    && missileList.Count >= 1)
                {
                    missileList.Remove(missileList[i]);
                   // player.PlayerCurrentHealth -= 20;
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
                  //  player.PlayerCurrentHealth -= 10;
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

            //träff mellan spelare och laser
            for(int i = 0; i < laserList.Count; i++)
            {
                for (int j = 0; j < laserList[i].beemList.Count; j++)
                {
                    if (Vector2.Distance(laserList[i].beemList[j], player.position) <= 30)
                    {
                        input.vibrate = true;
                       // player.PlayerCurrentHealth -= 1;
                    }
                    else
                    {
                        input.vibrate = false;
                    }
                }
            }
            
            if (BossCurrentHealth <= 0)
            {
                bossIsAlive = false;
            }
        }
        public void Random(int x, int y)
        {
             calculateRandomPos = new Random();
             randPosX = calculateRandomPos.Next(x, y);
             randPosY = calculateRandomPos.Next(x, y);
        }
        public void Movement()
        {
            Random(-2, 2);
            int screenWidthBegining = 0;
            int screenHeightBegining = 0;
            if (position.X >= screenWidth - texture.Width)
            {
                position.X = screenWidth - texture.Width ;
                bossVelocity = new Vector2(randPosX, randPosY);                
            }
            if (position.X <= screenWidthBegining)
            {                
                position.X = 3;
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            if(position.Y >= screenHeight - texture.Height)
            {
                position.Y = screenHeight - texture.Height;
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            if(position.Y <= screenHeightBegining)
            {
                position.Y = 3;
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            position += bossVelocity;
            int noValidSpeed = 0;

            if (bossVelocity.X == noValidSpeed && bossVelocity.Y== noValidSpeed)
            {
                Random(-2, 2);
                position += bossVelocity;
            }

        }

        private void ShootMissile(int startPos)
        {
            int missileStartOffsetX = 30;
            int missileStartOffsetY = 30;
            Rectangle missileRect = new Rectangle(0, 0, missileTexture.Width, missileTexture.Height);
            Vector2 missilePosition = new Vector2(position.X + missileStartOffsetX + startPos, position.Y+ missileStartOffsetY);

            missile = new Missile(missileTexture, missilePosition, sourceRect);
            missileList.Add(missile);
        }

        private void ShootBullets(int startPos, Player player)
        {
            Rectangle bulletRect = new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height);
            Vector2 bulletPosition = new Vector2(position.X + startPos, position.Y + startPos);
            if (player.position.Y <= position.Y && player.position.X <= position.X)
            {
                bulletSpreadX = -100;
                bulletSpreadY = -5;
            }
            else if(player.position.Y >= position.Y &&player.position.X <= position.X)
            {
                bulletSpreadX = 30;
                bulletSpreadY = 30;
            }
            else if(player.position.X >= position.X && player.position.Y <= position.Y)
            {
                bulletSpreadX = 100;
                bulletSpreadY = 5;
            }
            else if(player.position.Y >= position.Y && player.position.X >= position.X)
            {
                bulletSpreadX = -30;
                bulletSpreadY = 30;
            }

            for (int i = 0; i <= 10; i++)
            {
                bullet = new BossOneBullets(bulletTexture, new Vector2(bulletPosition.X , bulletPosition.Y), bulletRect, player, (bulletSpreadX * i), (bulletSpreadY * i));
                bulletList.Add(bullet);
            }
        }

        public void ShootLaser(Player player)
        {
            for (int i = 0; i <= 0; i++)
            {
                bossLaser = new BossLaser(laserTexture, position, sourceRect, player.position);
                laserList.Add(bossLaser);
            }
        }
    }
}
