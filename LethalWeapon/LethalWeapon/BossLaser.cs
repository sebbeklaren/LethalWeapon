using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LethalWeapon
{
    class BossLaser : GameObject
    {
        Texture2D laserTexture;
        Rectangle destinationRect, sourceRect;
        Vector2 position, aimPosition, origin;
        float rotation;
        double elapsedTime;
        public int frame = 0;
        double delayTime = 50;        

        public BossLaser(Texture2D texture, Vector2 position, Rectangle sourceRect, Vector2 playerPosition)
            : base(texture, position, sourceRect)
        {
            
            laserTexture = texture;
            aimPosition = playerPosition;
            this.position = position;
            this.position = position;
            origin = new Vector2(265, 24);
            int playerOffsetX = 16;
            int playerOffsetY = 24;
            Vector2 difference = new Vector2(aimPosition.X + playerOffsetX, aimPosition.Y + playerOffsetY) - position;
            difference.Normalize();
            //position += difference;
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);

        }

        public void Update(GameTime gameTime, Vector2 bossPosition, Vector2 playerPosition)
        {
            int positionOffsetX = 110;
            int positionOffsetY = 120;

            destinationRect = new Rectangle((int)bossPosition.X + positionOffsetX, (int)bossPosition.Y + positionOffsetY, 250, 48);

            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            

            if (elapsedTime >= delayTime)
            {
                if(frame >= 18)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                elapsedTime = 0;
            }

            sourceRect = new Rectangle(250 * frame, 0, 250, 48);

        }
        
        public override void Draw(SpriteBatch sb)
        {
            float layerDepth = 1f;
            sb.Draw(TextureManager.BossLaserTexture, destinationRect , sourceRect, Color.White, (rotation), origin, SpriteEffects.None, layerDepth);
        }
    }
}
