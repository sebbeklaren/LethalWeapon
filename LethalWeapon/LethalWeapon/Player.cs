using Microsoft.Xna.Framework;
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
        

        public Player(Texture2D texture, Vector2 position, Rectangle sourceRect): base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                position.Y -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                position.Y += 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                position.X -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                position.X += 2;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }


    }
}
