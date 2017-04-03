﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class Player : GameObject
    {
        float speed = 2.0f;
        public Rectangle playerHitbox;

        //Stats for Player to read and display
        public int PlayerMaxHealth { get; set; }
        public int PlayerCurrentHealth { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerExperiencePoints { get; set; }

        public Vector2 Position
        {
            get { return position; }
        }


        public Player(Texture2D texture, Vector2 position, Rectangle sourceRect): base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            PlayerMaxHealth = 100;
            PlayerCurrentHealth = 100;
            PlayerLevel = 1;
            PlayerExperiencePoints = 0;
        }

        public void Update()
        {
            playerHitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                position.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                position.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                position.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                position.X += speed;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
