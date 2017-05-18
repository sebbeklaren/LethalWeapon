using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class EnemyBullet : Bullet
    {
        public bool isActive;

        public EnemyBullet(Texture2D texture, Vector2 position, Vector2 playerPosition)
            : base(texture)
        {
            this.texture = texture;
            this.position = position;
            this.position.X += 10;
            this.position.Y += 10;
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            bulletDestination = playerPosition - position;
            isActive = true;
        }

        public void UpdateEnemyBullet(Player player)
        {
            if (isActive)
            {
                position += 2 * Vector2.Normalize(bulletDestination);
                hitBox.X = (int)position.X;
                hitBox.Y = (int)position.Y;
            }     
        }

        public void DrawEnemyBullet(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, position, Color.Red);
            }
        }
    }
}
