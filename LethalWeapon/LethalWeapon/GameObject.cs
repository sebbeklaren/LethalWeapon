using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    public class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle sourceRect;

        public GameObject(Texture2D texture, Vector2 position, Rectangle sourceRect)
        {
            this.texture = texture;
            this.position = position;
            this.sourceRect = sourceRect;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, sourceRect,Color.White);
        }
    }
}