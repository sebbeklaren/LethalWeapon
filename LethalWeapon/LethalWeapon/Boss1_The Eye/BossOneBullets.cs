using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LethalWeapon
{
    class BossOneBullets : GameObject
    {
        float rotation;       
        public Rectangle bulletRect, laserPosRect, hitBox;
        public Vector2 position, vectorTargetOffset;
        Vector2 targetPosition;       
        Vector2 difference;
        //Vector2 origin;
        int spaceing;
        int bulletCount;
        double elapsedTime;
        int frame = 0;
        double delay = 5;
        bool bossBullets, minionLaser;
        int laserDimensions = 15;
        int rectanglePosOffset = 8;
        public BossOneBullets(Texture2D texture, Vector2 position, Rectangle sourceRect, Player player, 
            int spreadX, int spreadY, Vector2 targetOffset, int spaceing, int bulletCount, bool minionLaser, bool bossBullets) : base(texture, position, sourceRect)
        {
            this.bossBullets = bossBullets;
            this.minionLaser = minionLaser;
            this.spaceing = spaceing;
            this.bulletCount = bulletCount;
            vectorTargetOffset = targetOffset;
            //origin = new Vector2(texture.Width / 2, texture.Height / 2);
            bulletRect = new Rectangle(0, 0, texture.Width, texture.Height);
            this.position = position;            
            targetPosition = new Vector2(player.position.X + spreadX  , player.position.Y + spreadY) + vectorTargetOffset;
            difference = targetPosition - position;            
            difference.Normalize();
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);
        }
        public void Update(GameTime gameTime)
        {

            
            elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;

            int positionMultiplier = 2;
            position = position + difference * positionMultiplier * bulletCount;
            laserPosRect = new Rectangle((int)position.X, (int)position.Y, laserDimensions, laserDimensions);
           
            sourceRect = new Rectangle(75, 0, 15, 15);
            if (bossBullets)
            {
                hitBox = new Rectangle((int)position.X - rectanglePosOffset, (int)position.Y - rectanglePosOffset, texture.Width/2, texture.Height/2 );
            }
            if(minionLaser)
            {
                hitBox = new Rectangle((int)position.X - rectanglePosOffset, (int)position.Y - rectanglePosOffset, laserDimensions, laserDimensions);
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            float scale = 0.8f;
            float layerDepth = 1f;
            if (bossBullets)
            {
                Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
                sb.Draw(texture, position, bulletRect, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
               // sb.Draw(TextureManager.HealtBarTexture, new Vector2((int)position.X - 8, (int)position.Y - 8), hitBox, Color.White);
            }
            if(minionLaser)
            {
                Vector2 origin = new Vector2(6, 6);
                sb.Draw(texture,new Rectangle((int)position.X, (int)position.Y, laserDimensions, laserDimensions),sourceRect, Color.White, rotation, origin, SpriteEffects.None, layerDepth);
               // sb.Draw(TextureManager.HealtBarTexture, new Vector2((int)position.X - 8, (int)position.Y - 8), sourceRect, Color.White);
            }
        }

    }
}
  