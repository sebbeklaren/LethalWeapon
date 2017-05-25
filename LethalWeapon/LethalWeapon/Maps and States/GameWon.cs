using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    public class GameWon
    {
        Vector2 position;
        Texture2D texture;

        public GameWon(Texture2D texture)
        {
            position = Vector2.Zero;
            this.texture = texture;
        } 

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
