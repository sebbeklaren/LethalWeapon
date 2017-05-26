using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class Player : GameObject
    {
        public InputManager input = new InputManager();
        float speed = 4.0f;
        float rotation = 1.0f;
        float layerDepth = 1f;
        float aimSpeed = 5.0f;
        double dodgeTimer;
        double hitTimer;
        public bool isDodging = false;
        public bool playerIsHit = false;
        public bool canMove = true;
        public Rectangle playerHitboxVertical, playerHitboxHorizontal;
        Texture2D aimTexture;
        KeyboardState current;
        KeyboardState last;
        public Vector2 position;
        public Texture2D texture;
        Rectangle playerSourceRect;
        public int playerAnimationStatusInt { get; set; }
        public int playerFaceDirectionInt { get; set; }
        double timePerFrame = 300; //Försök på att få det att funka
        double elapsedTime = 150;
        int frame = 0;
        //Stats for Player to read and display
        public double PlayerMaxHealth { get; set; }
        public double PlayerMaxEnergi { get; set; }
        public double PlayerCurrentHealth { get; set; }
        public double PlayerCurrentEnergi { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerExperiencePoints { get; set; }
        protected float regenTimer;
        protected int regen = 10;
        protected bool canRegen = false;
        int screenWidth, screenHeight;
        public bool isWalking = false;
       
        Vector2 aimPosition;
        Vector2 dodgeSpeed;
        public Rectangle checkRec;

        public Vector2 AimPosition
        {
            get { return aimPosition; } 
        }
        public Vector2 Position
        {
            get { return position; }           
        }

        public Player(Texture2D texture, Vector2 position, Rectangle sourceRect, int screenWidth, int screenHeight): 
            base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            this.playerSourceRect = sourceRect;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            PlayerMaxHealth = 100;      
            PlayerMaxEnergi = 100;
            PlayerCurrentHealth = 100;
            PlayerCurrentEnergi = 100;
            PlayerLevel = 1;
            PlayerExperiencePoints = 0;
            dodgeSpeed = new Vector2(3, 3);
            aimTexture = TextureManager.PlayerAimTexture; 
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
          
            last = current;
            current = Keyboard.GetState();
            KeyboardInput();
            PlayerTextureDirection();
            playerHitboxVertical = new Rectangle((int)position.X - 4, (int)position.Y + 12 , (texture.Width/4 + 8), (texture.Height/5) - 24);
            playerHitboxHorizontal = new Rectangle((int)position.X, (int)position.Y, (texture.Width/4), (texture.Height/5));
            checkRec = new Rectangle((int)position.X - 16, (int)position.Y - 24, (texture.Width/4) + 32, (texture.Height/5) + 48);
            energiRegen(gameTime);
            PlayerDodge(gameTime);
            PlayerAim();
            AnimatePlayer(gameTime);
        }

        public void PlayerDodge(GameTime gametime)
        {
            if (isDodging == true)
            {
                dodgeTimer += gametime.ElapsedGameTime.TotalMilliseconds;
                speed = 7;
            }
            if (playerIsHit == true)
            {
                hitTimer += gametime.ElapsedGameTime.TotalSeconds;
            }

            if (dodgeTimer > 300/* || input.gamePadState.Triggers.Left <= 0*/)//Kommentera bort sista vilkoret för tangentbord ska funka korrekt

                if (dodgeTimer > 300 /*|| input.gamePadState.Triggers.Left <= 0*/)

                {
                    speed = 4.0f;
                    isDodging = false;
                    dodgeTimer = 0;
                }
            if (hitTimer >= 1)
            {
                playerIsHit = false;
                hitTimer = 0;
            }
        }

        public void PlayerAim()
        {
            if (!input.isConnected)
            {
                aimSpeed = 5.0f;
                aimPosition = input.aimDirection;
            }
            else
            {
                aimSpeed = 15f;
                aimPosition += input.aimDirection * (aimSpeed + speed);
            }
            double maxAimDistYBot = 170;
            double maxAimDistYTop = 185;
            double maxAimDistXLeft = 235;
            double maxAimDistXRight = 250;

            if (aimPosition.X < position.X - maxAimDistXLeft)
            {
                aimPosition.X = position.X - (float)maxAimDistXLeft;
            }
            if (aimPosition.Y < position.Y - maxAimDistYTop)
            {
                aimPosition.Y = position.Y - (float)maxAimDistYTop;
            }
            if (aimPosition.X > position.X + maxAimDistXRight)
            {
                aimPosition.X = position.X + (float)maxAimDistXRight;
            }
            if (aimPosition.Y > position.Y + maxAimDistYBot)
            {
                aimPosition.Y = position.Y + (float)maxAimDistYBot;
            }
        }
        public void energiRegen(GameTime gameTime)
        {
            if (PlayerCurrentEnergi <= 100 && canRegen == false)
            {
                regenTimer = 1;
                canRegen = true;
            }

            if (canRegen == true)
            {
                if (regenTimer == 1)
                {
                    PlayerCurrentEnergi = PlayerCurrentEnergi + regen;
                }
                regenTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (regenTimer <= 0)
                {
                    canRegen = false;
                }
            }
            if (PlayerCurrentEnergi >= 100)
            {
                PlayerCurrentEnergi = 100;
                canRegen = false;
            }
        }

        public void PlayerTextureDirection() //Nytt, kollar efter vilket håll spelaren vänder sig eftersom alla håll texturen vänder sig ligger bredvid varandra.
        {
            int aimPosOffset = 100;
            if (!isWalking)
            {
                playerAnimationStatusInt = 4; //Vilken horisontell rad i spritesheeten basically.
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W) || aimPosition.Y < position.Y)
            {
                playerAnimationStatusInt = 0;
                playerFaceDirectionInt = 2; // om isWalking är true så ger denna vilket håll spelaren ska vända sig.
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) || aimPosition.Y > position.Y)
            {
                playerAnimationStatusInt = 1;
                playerFaceDirectionInt = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) || aimPosition.X < position.X - aimPosOffset)
            {
                playerAnimationStatusInt = 2;
                playerFaceDirectionInt = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) || aimPosition.X > position.X + aimPosOffset)
            {
                playerAnimationStatusInt = 3;
                playerFaceDirectionInt = 0;
            }
        }

        public void AnimatePlayer(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(isWalking)
            {
                playerSourceRect = new Rectangle((texture.Width / 4) * frame, (texture.Height/ 5) * playerAnimationStatusInt, texture.Width / 4, texture.Height / 5);
            }
            else
            {
                playerSourceRect = new Rectangle((texture.Width / 4) * playerFaceDirectionInt, (texture.Height / 5) * playerAnimationStatusInt, texture.Width / 4, texture.Height / 5);
            }

            if (elapsedTime >= timePerFrame)
            {
                if (frame >= 4)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                elapsedTime = 0;
            }
        }

        #region GamlaAnimatioGrejer
        //public void AnimateWalking(GameTime gameTime)
        //{
        //    if (isWalking)
        //    {
        //        elapsedTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
        //        if (playerFaceDirectionInt == 2)
        //        {
        //            texture = TextureManager.PlayerWalkingUp;
        //        }
        //        else if (playerFaceDirectionInt == 3)
        //        {
        //            texture = TextureManager.PlayerWalkingDown;
        //        }
        //        else if (playerFaceDirectionInt == 1)
        //        {
        //            texture = TextureManager.PlayerWalkingLeft;
        //        }
        //        else if (playerFaceDirectionInt == 0)
        //        {
        //            texture = TextureManager.PlayerWalkingRight;
        //        }

        //        if (elapsedTime <= timePerFrame)
        //        {
        //            if (frame >= 4)
        //            {
        //                frame = 0;
        //            }
        //            else
        //            {
        //                frame++;
        //            }
        //            elapsedTime = timePerFrame;
        //        }
        //        playerSourceRect = new Rectangle((texture.Width / 4) * frame, 0, texture.Width / 5, texture.Height);
        //    }
        //}
        #endregion

        public void KeyboardInput()
        {
            if (canMove)
            {
                input.Update();
                position += input.position * speed;
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    position.Y -= speed;
                    isWalking = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    position.Y += speed;
                    isWalking = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    position.X -= speed;
                    isWalking = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    position.X += speed;
                    isWalking = true;
                }
                else { isWalking = false; }
            }

              if (current.IsKeyDown(Keys.LeftControl) && last.IsKeyUp(Keys.LeftControl) || input.gamePadState.Triggers.Left > 0 && PlayerCurrentEnergi >= 20)
            if (current.IsKeyDown(Keys.LeftControl) && last.IsKeyUp(Keys.LeftControl) || input.gamePadState.Triggers.Left > 0 && PlayerCurrentEnergi >= 20) //Kommentera bort sista vilkoret för tangentbord ska funka korrekt
            {
                isDodging = true;
                PlayerCurrentEnergi -= 20;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, playerSourceRect, Color.White);
            sb.Draw(aimTexture, aimPosition, null, Color.White, 0, new Vector2(-5,-5), 1, SpriteEffects.None, 1f);
            if (playerIsHit == true)
            {
                sb.Draw(texture, position, playerSourceRect, Color.Red);
            }
        }    
    }
}
