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
        float missileSpeed;
        Rectangle missileRect;

        public Missile(Texture2D texture, Vector2 position, Rectangle sourceRect)
            : base(texture, position, sourceRect)
        {            
            missileRect = new Rectangle(0, 0, texture.Width, texture.Height);            
        }

        public void Update(Vector2 playerPos)
        {
            Vector2 difference = playerPos - position;
            difference.Normalize();
            position += difference * 1f;
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, missileRect, Color.White, rotation, new Vector2(texture.Width/2, texture.Height/2), 1f, SpriteEffects.None, 1f);
        }

    }
}
