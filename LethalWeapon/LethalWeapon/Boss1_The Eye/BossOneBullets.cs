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
        public Rectangle bulletRect;
        public Vector2 position;
        Vector2 targetPosition;       
        Vector2 difference;
        Vector2 origin;
        public BossOneBullets(Texture2D texture, Vector2 position, Rectangle sourceRect, Player player, int spreadX, int spreadY)
            : base(texture, position, sourceRect)
        {
            Vector2 vectorTargetOffset = new Vector2(0, -200);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            bulletRect = new Rectangle(0, 0, texture.Width, texture.Height);
            this.position = position;            
            targetPosition = new Vector2(player.position.X + spreadX  , player.position.Y + spreadY) + vectorTargetOffset;
            difference = targetPosition - position;            
            difference.Normalize();
        }
        public void Update(GameTime gameTime)
        {
            int positionMultiplier = 2;
            position = position + difference * positionMultiplier;
            rotation = 0f;
        }
        public override void Draw(SpriteBatch sb)
        {
            float scale = 1f;
            float layerDepth = 1f;
             sb.Draw(texture, position, bulletRect, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }

    }
}
  