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
        public Rectangle destinationRect, sourceRect, hitBox;
        public Rectangle HitBox
        {
            get { return hitBox; }
        }
        Vector2 position, aimPosition, origin, hitBoxPosition, difference;
        float rotation;
        double elapsedTime;
        public int frame = 0;
        double delayTime = 50;
        List<Rectangle> hitBoxList = new List<Rectangle>();
        

        public BossLaser(Texture2D texture, Vector2 position, Rectangle sourceRect, Vector2 playerPosition)
            : base(texture, position, sourceRect)
        {
            laserTexture = texture;
            aimPosition = playerPosition;                       
            origin = new Vector2(265, 24);
            int playerOffsetX = 100;
            int playerOffsetY = 100;
            int positionOffsetX = 100;
            int positionOffsetY = 100;
            hitBoxPosition = new Vector2(position.X +positionOffsetX, position.Y + positionOffsetY);
            aimPosition = new Vector2(aimPosition.X - playerOffsetX, aimPosition.Y - playerOffsetY);
            difference =  aimPosition- position;
            difference.Normalize();            
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);
            
        }

        public void Update(GameTime gameTime, Vector2 bossPosition, Vector2 playerPosition)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            int positionOffsetX = 110;
            int positionOffsetY = 120;
            destinationRect = new Rectangle((int)bossPosition.X + positionOffsetX, (int)bossPosition.Y + positionOffsetY, 250, 48);
            
            hitBoxPosition = hitBoxPosition + difference * 6;
            hitBox = new Rectangle((int)hitBoxPosition.X - 20, (int)hitBoxPosition.Y - 20, 30, 30);
            if (elapsedTime >= delayTime)
            {

                if (frame >= 18)
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
            float layerDepth = 0f;           
           // sb.Draw(TextureManager.HealtBarTexture, hitBoxPosition, hitBox, Color.White);
            sb.Draw(texture, destinationRect , sourceRect, Color.White, rotation, origin, SpriteEffects.None, layerDepth);
           
        }
    }
}
