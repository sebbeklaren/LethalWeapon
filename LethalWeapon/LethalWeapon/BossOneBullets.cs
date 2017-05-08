﻿using System;
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
        int screenWidth = 1024;
        int screenHeight = 768;
        Vector2 startPos;
        Vector2 difference;
        public BossOneBullets(Texture2D texture, Vector2 position, Rectangle sourceRect, Player player, int spread)
            : base(texture, position, sourceRect)
        {
            bulletRect = new Rectangle(0, 0, texture.Width, texture.Height);
            this.position = position;
            startPos = position;
            targetPosition = new Vector2(player.position.X + spread  , player.position.Y + spread) + new Vector2(0, -200);
            difference = targetPosition - position;            
            difference.Normalize();
        }
        public void Update(GameTime gameTime)
        {
            position = position + difference * 2f ;
            rotation = 0f;
        }



        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, bulletRect, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
        }

    }
}
  