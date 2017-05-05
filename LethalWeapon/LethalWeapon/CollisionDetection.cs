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
        List<Rectangle> tempList = new List<Rectangle>();

        public void CheckBounds(Player player, int screenHeight, int screenWidth)
        {
            if (player.position.Y <= 0)
            {
                player.position.Y = 1;
                player.canMove = false;
            }
            else if (player.position.Y >= screenHeight - 45)
            {
                player.position.Y = screenHeight - 46;
                player.canMove = false;
            }
            else
            {
                player.canMove = true;
            }
            if (player.position.X <= 0)
            {
                player.position.X = 1;
                player.canMove = false;
            }
            else if (player.position.X >= screenWidth - 32)
            {
                player.position.X = screenWidth - 33;
                player.canMove = false;
            }
            else
            {
                player.canMove = true;
            }
        }
        public void CheckCollisionVertical(LevelManager level, Player player)
        {
            
            tempList = level.hitBoxWall;

            foreach (Rectangle wall in level.hitBoxWall)
            {
                int hitOffset = 10;
                //check uppe och nere
                if (player.playerHitboxHorizontal.Bottom >= wall.Top - 5 && player.playerHitboxHorizontal.Bottom <= wall.Top &&
                    player.playerHitboxHorizontal.Right >= wall.Left && player.playerHitboxHorizontal.Left <= wall.Right)
                {
                    player.position.Y = wall.Top - player.texture.Height - 6;
                    player.canMove = false;
                }
                else if (player.playerHitboxHorizontal.Top <= wall.Bottom && player.playerHitboxHorizontal.Top >= wall.Bottom - hitOffset &&
                    player.playerHitboxHorizontal.Right >= wall.Left && player.playerHitboxHorizontal.Left <= wall.Right)
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
            foreach (Rectangle wall in level.hitBoxWall)
            {
                //check från sidorna
                if (player.playerHitboxVertical.Right >= wall.Left - 6 && player.playerHitboxVertical.Right <= wall.Left + 12 &&
                    player.playerHitboxVertical.Bottom >= wall.Top && player.playerHitboxVertical.Top <= wall.Bottom)
                {
                    player.position.X = wall.Left - player.texture.Width - 12;
                    player.canMove = false;
                }
                else if (player.playerHitboxVertical.Left <= wall.Right + 12 && player.playerHitboxVertical.Left >= wall.Right - 6 &&
                    player.playerHitboxVertical.Bottom >= wall.Top && player.playerHitboxVertical.Top <= wall.Bottom)
                {
                    player.position.X = wall.Right + player.texture.Width  - 15;
                    player.canMove = false;
                }
                else
                {
                    player.canMove = true;
                }
            }
        }

        public void CameraBoundCheck(Player player, Camera camera)
        {
            Vector2 cameraOffset = new Vector2(35, 65);
            int inputCameraMultiplier = 10;
            if (player.Position.X < 270)
            {
                camera.SetPosition(new Vector2(235 - player.input.position.X * inputCameraMultiplier,
                                              (player.Position.Y - cameraOffset.Y) - player.input.position.Y * inputCameraMultiplier));
                if (player.Position.Y > 565)
                {
                    camera.SetPosition(new Vector2(235 - player.input.position.X * inputCameraMultiplier,
                                                    505 - player.input.position.Y * inputCameraMultiplier));
                }
                else if (player.Position.Y < 180)
                {
                    camera.SetPosition(new Vector2(235 - player.input.position.X * inputCameraMultiplier,
                                                   120 - player.input.position.Y * inputCameraMultiplier));
                }
            }
            else if (player.Position.X > 684)
            {
                camera.SetPosition(new Vector2(658 - player.input.position.X * inputCameraMultiplier,
                                            (player.Position.Y - cameraOffset.Y) - player.input.position.Y * inputCameraMultiplier));
                if (player.Position.Y > 565)
                {
                    camera.SetPosition(new Vector2(658 - player.input.position.X * inputCameraMultiplier,
                                                  505 - player.input.position.Y * inputCameraMultiplier));
                }
                else if (player.Position.Y < 180)
                {
                    camera.SetPosition(new Vector2(658 - player.input.position.X * inputCameraMultiplier,
                                                   120 - player.input.position.Y * inputCameraMultiplier));
                }
            }
            else if (player.Position.Y > 565)
            {
                camera.SetPosition(new Vector2((player.Position.X - cameraOffset.X) - player.input.position.X * inputCameraMultiplier,
                                               505 - player.input.position.Y * inputCameraMultiplier));
            }
            else if (player.Position.Y < 180)
            {
                camera.SetPosition(new Vector2((player.Position.X - cameraOffset.X) - player.input.position.X * inputCameraMultiplier,
                                               120 - player.input.position.Y * inputCameraMultiplier));
            }
            else
            {
                camera.SetPosition(new Vector2((player.Position.X - cameraOffset.X) - player.input.position.X * inputCameraMultiplier,
                                               (player.Position.Y - cameraOffset.Y) - player.input.position.Y * inputCameraMultiplier));
            }
        }
    }
}
