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
    class InputManager
    {
        public Vector2 position, aimDirection;
        public float rotation;
        bool fire;
        public InputManager()
        {
        }

        public void Update()
        {           
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
            if (gamePad.IsConnected)
            {
                position = gamePad.ThumbSticks.Left;
                position.Y *= -1;

                aimDirection = gamePad.ThumbSticks.Right;
                aimDirection.Y *= -1;

                if(gamePad.Triggers.Right > 0)
                {
                    fire = true;
                }
                else if(gamePad.Triggers.Right <= 0)
                {
                    fire = false;
                }
                GamePad.SetVibration(PlayerIndex.One, gamePad.Triggers.Right, gamePad.Triggers.Right);
            }
        }       
    }
}
