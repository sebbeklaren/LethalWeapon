using Microsoft.Xna.Framework;
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
        Game1 game1;
        Enemy tempEnemy;
        public OverWorld()
        {
            game1 = new Game1();
            overWorldTex = TextureManager.OverWorldtexture;
            overWorldPlayerTex = TextureManager.PlayerTexture;
            playerPos = new Vector2(500, 500);
            player = new Player(overWorldPlayerTex, playerPos, playerRect, 500, 500);
            tempEnemy = new Enemy(overWorldPlayerTex, new Vector2(1, 1) , new Rectangle(1, 1, 1, 1));
            
            map1Pos = new Vector2(200, 200);
            map2Pos = new Vector2(700, 700);
            map1Rect = new Rectangle((int)map1Pos.X, (int)map1Pos.Y, 100, 100);
            map2Rect = new Rectangle((int)map2Pos.X, (int)map2Pos.Y, 100, 100);
        }

        public void UpdateOverWorld(GameTime gameTime)
        {
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, 32, 48);
            player.Update(gameTime, tempEnemy);

            if (playerRect.Intersects(map1Rect))
            {
                game1.boolCityLevel = true;
                
            }
            else if (playerRect.Intersects(map2Rect))
            {
                game1.boolRuinslevel = true;
                
            }
        }

        public void DrawOverWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(overWorldTex, Vector2.Zero, Color.White);
            spriteBatch.Draw(overWorldPlayerTex, map1Rect, Color.Red);
            player.Draw(spriteBatch);
        }
            
           
    }
}
