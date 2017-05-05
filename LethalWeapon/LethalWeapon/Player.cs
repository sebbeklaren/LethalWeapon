﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
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
        bool isDodging = false;
        bool playerIsHit = false;
        public bool canMove = true;
        public Rectangle playerHitboxVertical, playerHitboxHorizontal;
        Texture2D aimTexture;
        KeyboardState current;
        KeyboardState last;
        public Vector2 position;
        public Texture2D texture;

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
       
        Vector2 aimPosition;
        Vector2 dodgeSpeed;
        
        Texture2D tempText;
        LevelManager Level;
        public Rectangle checkRec;

        public Vector2 AimPosition
        {
            get { return aimPosition; } 
        }
        public Vector2 Position
        {
            get { return position; }           
        }

        public Player(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content, int screenWidth, int screenHeight): 
            base (texture, position, sourceRect)
        {
            
            this.texture = texture;
            this.position = position;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            PlayerMaxHealth = 100;      
            PlayerMaxEnergi = 100;
            PlayerCurrentHealth = 100;
            PlayerCurrentEnergi = 100;
            PlayerLevel = 1;
            PlayerExperiencePoints = 0;
            dodgeSpeed = new Vector2(3, 3);
            aimTexture = content.Load<Texture2D>(@"crosshair"); 
            tempText = content.Load<Texture2D>(@"Bullet");
            
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            last = current;
            current = Keyboard.GetState();

            playerHitboxVertical = new Rectangle((int)position.X - 4, (int)position.Y + 12 , texture.Width + 8, texture.Height - 24);
            playerHitboxHorizontal = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            checkRec = new Rectangle((int)position.X - 16, (int)position.Y - 24, texture.Width + 32, texture.Height + 48);
            energiRegen(gameTime);
            if (canMove)
            {
                input.Update();
                position += input.position * speed;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    position.Y -= speed;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    position.Y += speed;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    position.X -= speed;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    position.X += speed;
            }
                if (current.IsKeyDown(Keys.LeftControl) && last.IsKeyUp(Keys.LeftControl) || input.gamePad.Triggers.Left > 0 && PlayerCurrentEnergi >= 20)
                    {
                        isDodging = true;
                        PlayerCurrentEnergi -= 20;
                    }

            if (playerHitboxHorizontal.Intersects(enemy.HitBox) && isDodging == false && playerIsHit == false)
            {
                PlayerCurrentHealth -= 20;
                playerIsHit = true;
            }

            if (isDodging == true)
            {
                dodgeTimer += gameTime.ElapsedGameTime.Milliseconds;
                speed = 5;
            }
            if (playerIsHit == true)
            {
                hitTimer += gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (dodgeTimer > 300 || input.gamePad.Triggers.Left <= 0)
            {
                //speed = 2;
                isDodging = false;
                dodgeTimer = 0;
            }
            if(hitTimer >= 1)
            {
                playerIsHit = false;
                hitTimer = 0;
            }      
            if (!input.isConnected)
            {
                aimPosition = input.aimDirection;
            }
            else
            {
                aimPosition += input.aimDirection * aimSpeed;
            }
            double maxAimDistYBot= 170;
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
        public override void Draw(SpriteBatch sb)
        {            
            sb.Draw(texture, position, Color.White);
            sb.Draw(aimTexture, aimPosition, null, Color.White, 0, new Vector2(13,13), 1, SpriteEffects.None, 1f);
            if (playerIsHit == true)
            {
                sb.Draw(texture, position, Color.Red);
            }
        }    
    }
}
