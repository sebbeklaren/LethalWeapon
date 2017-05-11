using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LethalWeapon
{
    public class Tiles : GameObject
    {
        protected bool wall;
        public bool Wall
        {
            get { return wall; }
            set { wall = value; }
        }
        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }
        public Vector2 Position
        {
            get { return position; }
        }

        public Tiles(Texture2D texture, Vector2 position, Rectangle sourceRect, bool wall)
            : base(texture, position, sourceRect)
        {
            this.wall = wall;
        }
    }
}
