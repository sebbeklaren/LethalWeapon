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
        Rectangle hitBox;
        Texture2D missileTexture;
        Missile missileLeft;
        List<Missile> missileList = new List<Missile>();
        InputManager input;
        double timeMissileRight, timeMissileLeft;
        int screenHeight, screenWidth, randPosX, randPosY;
        Random randomPos;

        public BossOne(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content, int screenWidth, int screenHeight): 
            base (texture, position, sourceRect)
        {
            hitBox = new Rectangle(0, 0, texture.Width * 2, texture.Height * 2);
            missileTexture = content.Load<Texture2D>(@"Missile");
            input = new InputManager();
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            bossVelocity = new Vector2(1, 0);
           
        }

        public void Update(Player player, GameTime gameTime)
        {
            MissileAway(gameTime, player);
            Move();
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
        public override void Draw(SpriteBatch sb)
        {
            foreach (Missile projectile in missileList)
            {
                projectile.Draw(sb);
            }
            sb.Draw(texture, position, hitBox, Color.White);
        }

        public void GetPlayerPos()
        {

        }
        public void Random()
        {
             randomPos = new Random();
             randPosX = randomPos.Next(-1, 1);
             randPosY = randomPos.Next(-1, 1);
            
        }
        public void Move()
        {
            Random();
            
            if(position.X >= screenWidth - texture.Width)
            {
                position.X = screenWidth - texture.Width ;
                bossVelocity = new Vector2(randPosX, randPosY);
                
            }
            if(position.X <= 0)
            {
                position.X = 1;
                bossVelocity = new Vector2(randPosX, randPosY);
            }
            position += bossVelocity;
        }

        private void ShootMissile(int startPos)
        {
            Rectangle missileRect = new Rectangle(0, 0, missileTexture.Width, missileTexture.Height);
            Vector2 missilePosition = new Vector2(position.X+ 30 + startPos, position.Y+ 30);

            missileLeft = new Missile(missileTexture, missilePosition, missileRect);
            missileList.Add(missileLeft);

        }

        private void ShootBullets()
        {

        }

        private void ShootLaser()
        {

        }
    }
}
