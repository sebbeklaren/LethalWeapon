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
        protected Texture2D overWorldTex, texture;
        protected Vector2 overWorldPos, map1Pos, map2Pos, position;
        public Rectangle map1Rect, map2Rect, playerRect, sourceRect;
        Player player;
        Game1 game;
        Enemy tempEnemy;

        public OverWorld (ContentManager content, Game1 game)
        {
            this.game = game;
            tempEnemy = new Enemy(content.Load<Texture2D>(@"Textures/TemporaryTextures/Cyclop"), new Vector2(2000, 2000), sourceRect);
            texture = content.Load<Texture2D>(@"Textures/PlayerTextures/HoodyBoy");
            position = new Vector2(500, 500);
            player = new Player(texture, position, playerRect, content, 500, 500);
            overWorldTex = content.Load<Texture2D>(@"Textures/TemporaryTextures/overworldmap");
            map1Pos = new Vector2(300, 300);
            map2Pos = new Vector2(700, 700);
            map1Rect = new Rectangle((int)map1Pos.X, (int)map1Pos.Y, 100, 100);
            map2Rect = new Rectangle((int)map2Pos.X, (int)map2Pos.Y, 100, 100);
          
        }

        public void UpdateOverWorld(GameTime gameTime)
        {
            playerRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            player.Update(gameTime, tempEnemy);
            if (playerRect.Intersects(map1Rect))
            {
                game.boolCityLevel = true;
            }
            else if(playerRect.Intersects(map2Rect))
            {
                game.boolRuinsLevel = true;
            }
     
        }


        public void DrawOverWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(overWorldTex, Vector2.Zero, Color.White);
            //  spriteBatch.Draw(overWorldPlayerTex, playerPos, Color.White);
         //   player.Draw(spriteBatch);
        }
            
           
    }
}
