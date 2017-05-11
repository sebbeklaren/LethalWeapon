using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class OverWorld
    {
        protected Texture2D overWorldTex, overWorldPlayerTex;
        protected Vector2 overWorldPos, map1Pos, map2Pos, playerPos;
        public Rectangle map1Rect, map2Rect, playerRect;
        Player player;

        public OverWorld (ContentManager content)
        {
            player = new Player(overWorldPlayerTex, playerPos, playerRect, content, 500, 500);
            overWorldTex = content.Load<Texture2D>(@"overworldmap");
            overWorldPlayerTex = content.Load<Texture2D>(@"HoodyBoy");
            playerPos = new Vector2(500, 500);
            map1Pos = new Vector2(300, 300);
            map2Pos = new Vector2(700, 700);
            map1Rect = new Rectangle((int)map1Pos.X, (int)map1Pos.Y, 100, 100);
            map2Rect = new Rectangle((int)map2Pos.X, (int)map2Pos.Y, 100, 100);
        }

        public void UpdateOverWorld(GameTime gametime)
        {
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, overWorldTex.Width, overWorldTex.Height);
        }


        public void DrawOverWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(overWorldTex, Vector2.Zero, Color.White);
            spriteBatch.Draw(overWorldPlayerTex, playerPos, Color.White);
        }
            
           
    }
}
