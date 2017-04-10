using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class Bullet
    {
        public Texture2D texture;
        public Vector2 position;
        public float speed;
        public Vector2 bulletStartingPosition;
        int bulletDirection;
        public Bullet(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            speed = 1;
        }

        public void Update(Player player)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                bulletDirection = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                bulletDirection = 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                bulletDirection = 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                bulletDirection = 4;
            }

            ShotDirection();
        }
        public void ShotDirection()
        {
            if (bulletDirection == 1)
            {
                bulletStartingPosition.Y -= speed;
                position = bulletStartingPosition;
            }
            if (bulletDirection == 2)
            {
                bulletStartingPosition.X -= speed;
                position = bulletStartingPosition;
            }
            if (bulletDirection == 3)
            {
                bulletStartingPosition.X += speed;
                position = bulletStartingPosition;
            }
            if (bulletDirection == 4)
            {
                bulletStartingPosition.Y += speed;
                position = bulletStartingPosition;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
