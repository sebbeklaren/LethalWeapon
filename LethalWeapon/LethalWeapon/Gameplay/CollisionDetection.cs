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
        public void CheckPlayerBounds(Player player, int screenHeight, int screenWidth)
        {
            int screenHeightCheckOffset = 45;
            int screenHeightPosOffset = 46;
            int screenWidthCheckOffset = 32;
            int screenWidthPosOffset = 32;
            if (player.position.Y <= 0)
            {
                player.position.Y = 1;
                player.canMove = false;
            }
            else if (player.position.Y >= screenHeight - screenHeightCheckOffset)
            {
                player.position.Y = screenHeight - screenHeightPosOffset;
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
            else if (player.position.X >= screenWidth - screenWidthCheckOffset)
            {
                player.position.X = screenWidth - screenWidthPosOffset;
                player.canMove = false;
            }
            else
            {
                player.canMove = true;
            }
        }

        public void CheckCollisionVertical(LevelManager level, Player player)
        {
            foreach (Rectangle wall in level.hitBoxWall)
            {
                int wallBottomHitOffset = 10;
                int wallBottomPosOffset = 47;
                int wallTopHitOffset = 5;
                int wallTopPosOffset = 6;
                if (player.playerHitboxHorizontal.Bottom >= wall.Top - wallTopHitOffset && player.playerHitboxHorizontal.Bottom <= wall.Top &&
                    player.playerHitboxHorizontal.Right >= wall.Left && player.playerHitboxHorizontal.Left <= wall.Right)
                {
                    player.position.Y = wall.Top - player.texture.Height - wallTopPosOffset;
                    player.canMove = false;
                }
                else if (player.playerHitboxHorizontal.Top <= wall.Bottom && player.playerHitboxHorizontal.Top >= wall.Bottom - wallBottomHitOffset &&
                        player.playerHitboxHorizontal.Right >= wall.Left && player.playerHitboxHorizontal.Left <= wall.Right)
                {
                    player.position.Y = wall.Bottom + player.texture.Height - wallBottomPosOffset;
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
                int wallLeftHitOffset = 7;
                int wallLeftPosOffset = 12;
                int wallRightHitOffset = 6;
                int wallRightPosOffset = 24;
                if (player.playerHitboxVertical.Right >= wall.Left - wallLeftHitOffset && player.playerHitboxVertical.Right <= wall.Left &&
                    player.playerHitboxVertical.Bottom >= wall.Top && player.playerHitboxVertical.Top <= wall.Bottom)
                {
                    player.position.X = wall.Left - player.texture.Width - wallLeftPosOffset;
                    
                    player.canMove = false;
                }
                else if (player.playerHitboxVertical.Left <= wall.Right && player.playerHitboxVertical.Left >= wall.Right - wallRightHitOffset &&
                    player.playerHitboxVertical.Bottom >= wall.Top && player.playerHitboxVertical.Top <= wall.Bottom)
                {
                    player.position.X = wall.Right + player.texture.Width  - wallRightPosOffset;
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
            int haltCameraPosCheckX = 270;
            int haltCameraPosX = 235;
            int haltCameraPosCheckY = 565;
            int haltCameraPosY = 505;
            int haltCameraPosCheckLesserX = 180;
            int haltCameraPoskLesserX = 120;
            int haltCameraPosCheckGreaterX = 684;
            int haltCameraPoskGreaterX = 658;
            if (player.Position.X < haltCameraPosCheckX)
            {
                camera.SetPosition(new Vector2(haltCameraPosX - player.input.position.X * inputCameraMultiplier,
                                              (player.Position.Y - cameraOffset.Y) - player.input.position.Y * inputCameraMultiplier));
                if (player.Position.Y > haltCameraPosCheckY)
                {
                    camera.SetPosition(new Vector2(haltCameraPosX - player.input.position.X * inputCameraMultiplier,
                                                    haltCameraPosY - player.input.position.Y * inputCameraMultiplier));
                }
                else if (player.Position.Y < haltCameraPosCheckLesserX)
                {
                    camera.SetPosition(new Vector2(haltCameraPosX - player.input.position.X * inputCameraMultiplier,
                                                   haltCameraPoskLesserX - player.input.position.Y * inputCameraMultiplier));
                }
            }
            else if (player.Position.X > haltCameraPosCheckGreaterX)
            {
                camera.SetPosition(new Vector2(haltCameraPoskGreaterX - player.input.position.X * inputCameraMultiplier,
                                            (player.Position.Y - cameraOffset.Y) - player.input.position.Y * inputCameraMultiplier));
                if (player.Position.Y > haltCameraPosCheckY)
                {
                    camera.SetPosition(new Vector2(haltCameraPoskGreaterX - player.input.position.X * inputCameraMultiplier,
                                                  haltCameraPosY - player.input.position.Y * inputCameraMultiplier));
                }
                else if (player.Position.Y < haltCameraPosCheckLesserX)
                {
                    camera.SetPosition(new Vector2(haltCameraPoskGreaterX - player.input.position.X * inputCameraMultiplier,
                                                   haltCameraPoskLesserX - player.input.position.Y * inputCameraMultiplier));
                }
            }
            else if (player.Position.Y > haltCameraPosCheckY)
            {
                camera.SetPosition(new Vector2((player.Position.X - cameraOffset.X) - player.input.position.X * inputCameraMultiplier,
                                               haltCameraPosY - player.input.position.Y * inputCameraMultiplier));
            }
            else if (player.Position.Y < haltCameraPosCheckLesserX)
            {
                camera.SetPosition(new Vector2((player.Position.X - cameraOffset.X) - player.input.position.X * inputCameraMultiplier,
                                               haltCameraPoskLesserX - player.input.position.Y * inputCameraMultiplier));
            }
            else
            {
                camera.SetPosition(new Vector2((player.Position.X - cameraOffset.X) - player.input.position.X * inputCameraMultiplier,
                                               (player.Position.Y - cameraOffset.Y) - player.input.position.Y * inputCameraMultiplier));
            }
        }
    }
}
