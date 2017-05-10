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
        public Rectangle missileRect, hitBox;
        public Vector2 position;

        public Missile(Texture2D texture, Vector2 position, Rectangle sourceRect)
            : base(texture, position, sourceRect)
        {            
            missileRect = new Rectangle(0, 0, texture.Width, texture.Height);
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);    
            this.position = position;        
        }

        public void Update(Vector2 playerPos)
        {
            int positionOffsetX = 16;
            int positionOffsetY = 24;
            Vector2 difference = new Vector2(playerPos.X + positionOffsetX, playerPos.Y + positionOffsetY) - position;
            difference.Normalize();
            position += difference;
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, missileRect, Color.White, rotation, new Vector2(texture.Width/2, texture.Height/2), 1f, SpriteEffects.None, 1f);
        }

    }
}
