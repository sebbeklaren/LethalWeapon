using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LethalWeapon
{
    class Missile : GameObject
    {
        float rotation;       
        public Rectangle missilePosRect, hitBox;
        public Vector2 position, origin;
        double elapsedTime;
        int frame = 0;
        double delayTime = 60;

        public Missile(Texture2D texture, Vector2 position, Rectangle sourceRect)
            : base(texture, position, sourceRect)
        {   
            this.position = position;
            origin = new Vector2(32, 32);      
        }

        public void Update(Vector2 playerPos, GameTime gameTime)
        {
            int positionOffsetX = 16;
            int positionOffsetY = 24;
            Vector2 difference = new Vector2(playerPos.X + positionOffsetX, playerPos.Y + positionOffsetY) - position;
            hitBox = new Rectangle((int)position.X - 15, (int)position.Y - 20,48, 48);
            difference.Normalize();
            position += difference;
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);
            missilePosRect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= delayTime)
            {

                if (frame >= 9)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                elapsedTime = 0;
            }
            sourceRect = new Rectangle(64 * frame, 0, 64, 64);
        }

        public override void Draw(SpriteBatch sb)
        {
            float layerDepth = 0f;
            sb.Draw(texture, missilePosRect, sourceRect, Color.White, rotation, origin, SpriteEffects.None, layerDepth);
            //till för att se hitbox
           // sb.Draw(TextureManager.HealtBarTexture, new Vector2(hitBox.X, hitBox.Y), hitBox, Color.White);
        }

    }
}
