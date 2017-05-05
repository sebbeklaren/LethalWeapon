using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LethalWeapon
{
    class CollisionDetection
    {
        Rectangle walHitBox, playerHitBox;
        List<Rectangle> tempList = new List<Rectangle>();
        


        public void CheckCollisionVertical(LevelManager level, Player player)
        {
            
            tempList = level.hitBoxWall;

            foreach (Rectangle wall in level.hitBoxWall)
            {
                int hitOffset = 10;
                //check uppe och nere
                if (player.playerHitboxHorisontal.Bottom >= wall.Top - 5 && player.playerHitboxHorisontal.Bottom <= wall.Top &&
                    player.playerHitboxHorisontal.Right >= wall.Left && player.playerHitboxHorisontal.Left <= wall.Right)
                {
                    player.position.Y = wall.Top - player.texture.Height - 6;
                    player.canMove = false;
                }
                else if (player.playerHitboxHorisontal.Top <= wall.Bottom && player.playerHitboxHorisontal.Top >= wall.Bottom - hitOffset &&
                    player.playerHitboxHorisontal.Right >= wall.Left && player.playerHitboxHorisontal.Left <= wall.Right)
                {
                    player.position.Y = wall.Bottom + player.texture.Height - 47;
                    player.canMove = false;
                }
                else
                {
                    player.canMove = true;
                }
            }
        }

        public void CheckCollisionHorizontal(LevelManager level, Player player)
        {
            int hitOffset = 10;

            foreach (Rectangle wall in level.hitBoxWall)
            {
                //check från sidorna
                if (player.playerHitboxHorisontal.Top >= wall.Bottom  && player.playerHitboxHorisontal.Bottom >= wall.Top  &&
                player.playerHitboxHorisontal.Left >= wall.Right && player.playerHitboxHorisontal.Left <= wall.Right - 5)
                {
                    player.position.X = wall.Right + player.texture.Width - 30;

                    player.canMove = false;
                    Console.Write("Höger träff");
                }
                else if (player.playerHitboxHorisontal.Top >= wall.Bottom - 20 && player.playerHitboxHorisontal.Bottom >= wall.Top - 20 &&
                    player.playerHitboxHorisontal.Right <= wall.Left && player.playerHitboxHorisontal.Right >= wall.Left - 6)
                {
                    //position.X = wall.Left - texture.Width - 10;
                    player.canMove = false;
                    Console.Write("Vänster träff");
                }
            }
        }
    }
}
