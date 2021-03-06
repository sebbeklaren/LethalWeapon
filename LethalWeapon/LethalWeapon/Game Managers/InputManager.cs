﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LethalWeapon
{
    class InputManager
    {
        public Vector2 position, aimDirection;
        public bool isConnected;
        public MouseState mousePosOld, mousePosNew;
        public GamePadState gamePadState, oldGamePadState;    
        public bool vibrate, yIsPressed, startIsPressed;
        public float rotation;  

        public InputManager()
        {
        }

        public void Update()
        {           
            gamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
            

            mousePosOld = mousePosNew;
            mousePosNew = Mouse.GetState();

            aimDirection.X = mousePosNew.X;
            aimDirection.Y = mousePosNew.Y;

            if (gamePadState.IsConnected)
            {
                isConnected = true;
                position = gamePadState.ThumbSticks.Left;
                position.Y *= -1;

                aimDirection = gamePadState.ThumbSticks.Right;
                aimDirection.Y *= -1;

                rotation = -(float)Math.Atan2(gamePadState.ThumbSticks.Right.Y, gamePadState.ThumbSticks.Right.X);
                
                if (vibrate)
                {
                   // GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
                    GamePad.SetVibration(PlayerIndex.One, gamePadState.Triggers.Right, gamePadState.Triggers.Right);
                }
                else
                {
                    //GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                }
                if(gamePadState.Buttons.Y == ButtonState.Pressed && oldGamePadState.Buttons.Y == ButtonState.Released)
                {
                    yIsPressed = true;
                }
                else
                {
                    yIsPressed = false;
                }
                if(gamePadState.Buttons.Start == ButtonState.Pressed && oldGamePadState.Buttons.Start == ButtonState.Released)
                {
                    startIsPressed = true;
                }
                else
                {
                    startIsPressed = false;
                }
                
            }

            else
            {
                isConnected = false;
            }
            oldGamePadState = gamePadState;
        }       
    }
}
