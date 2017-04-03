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
    class Weapon : GameObject
    {
        Rectangle weaponHitbox;
        bool weaponOnGround = true;
        Bullet b;
        public List<Bullet> bullets = new List<Bullet>();
        public Weapon(Texture2D texture, Vector2 position, Rectangle sourceRect): base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            b = new Bullet(texture, position);
        }
        public void Update(Player player)
        {
            weaponHitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (player.playerHitbox.Intersects(weaponHitbox))
            {
                weaponOnGround = false;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {         
                bullets.Add(b);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                bullets.Add(b);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                bullets.Add(b);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                bullets.Add(b);

            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (weaponOnGround == true)
            {
                sb.Draw(texture, position, Color.White);
            }
        }
    }
}
