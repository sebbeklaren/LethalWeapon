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
        protected Rectangle hitBox;

        public virtual Rectangle Dest
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }
        public virtual Rectangle HitBox
        {
            get { return Dest; }
        }

        public GameObject(Texture2D texture, Vector2 position, Rectangle sourceRect)
        {
            this.texture = texture;
            this.position = position;
            this.sourceRect = sourceRect;
            this.hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public virtual bool IsCollidingTop(Rectangle r1, Rectangle r2)
        {
            const int penetrationMargin = 1;
            return (r1.Bottom >= r2.Top - penetrationMargin && r1.Bottom <= r2.Top + penetrationMargin && r1.Right >= r2.Left + penetrationMargin &&
                r1.Left <= r2.Right - penetrationMargin);
        }

        public virtual void HandleCollision(Tiles other)
        {
            hitBox.Y = other.hitBox.Y - hitBox.Height;
            position.Y = hitBox.Y;
            //hitBox.X = other.hitBox.X - hitBox.Width;
            //position.X = hitBox.X;

        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, sourceRect ,Color.White);
        }
    }
}