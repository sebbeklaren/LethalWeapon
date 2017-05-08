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
        // float bulletSpeed;
        public Rectangle bulletRect;
        public Vector2 position;
        Vector2 targetPosition;
        double elapsedTime;
        Vector2 aimVector;
        int screenWidth = 1024;
        int screenHeight = 768;

        public BossOneBullets(Texture2D texture, Vector2 position, Rectangle sourceRect, Player player)
            : base(texture, position, sourceRect)
        {
            bulletRect = new Rectangle(0, 0, texture.Width, texture.Height);
            this.position = position;

            targetPosition = new Vector2(player.position.X, player.position.Y);
        }
        

        public void Update(GameTime gameTime)
        {
            elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            Vector2 difference = targetPosition - position;
            difference.Normalize();
            position += difference * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.2f;
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);
        }

        private void GetAimPosition()
        {
            aimVector = new Vector2(targetPosition.X - screenWidth, targetPosition.Y - screenHeight);
            

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, bulletRect, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
        }

    }
}
  