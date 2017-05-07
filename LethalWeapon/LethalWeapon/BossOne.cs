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
        Rectangle hitBox;
        Texture2D missileTexture;
        Missile missileLeft;
        List<Missile> missileList = new List<Missile>();
        InputManager input;
        double elapsedTime;
        int missileTimer  = 3;

        public BossOne(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content): 
            base (texture, position, sourceRect)
        {
            hitBox = new Rectangle(0, 0, texture.Width * 2, texture.Height * 2);
            missileTexture = content.Load<Texture2D>(@"Missile");
            input = new InputManager();


        }

        public void Update(Player player, GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            int startPosOffsetRight = 170;
            int startPosOffsetLeft = 0;
            if (elapsedTime >= 3)
            {
                ShootMissile(startPosOffsetRight);
                elapsedTime = 0;
            }
            //else if(elapsedTime >= 6)
            //{
            //    ShootMissile(startPosOffsetLeft);
            //    elapsedTime = 0;
            //} 
            //if(Keyboard.GetState().IsKeyDown(Keys.Space)/*input.mousePosOld.LeftButton == ButtonState.Released && input.mousePosNew.LeftButton == ButtonState.Pressed*/)
            //{
            //    ShootMissile();
            //}
            
            foreach(Missile projectile in missileList)
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
