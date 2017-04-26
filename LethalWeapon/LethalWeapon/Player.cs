using Microsoft.Xna.Framework;
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
        InputManager input = new InputManager();
        float speed = 2.0f;
        float rotation = 1.0f;
        float layerDepth = 1f;
        float aimSpeed = 0.1f;
        double dodgeTimer;
        double hitTimer;
        bool isDodging = false;
        bool playerIsHit = false;
        public Rectangle playerHitbox;
        Texture2D aimTexture;
        KeyboardState current;
        KeyboardState last;
        //Stats for Player to read and display
        public double PlayerMaxHealth { get; set; }
        public double PlayerMaxEnergi { get; set; }
        public double PlayerCurrentHealth { get; set; }
        public double PlayerCurrentEnergi { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerExperiencePoints { get; set; }
        //ContentManager content;
        Vector2 aimPosition;
        Vector2 dodgeSpeed;
        public Vector2 AimPosition
        {
            get { return aimPosition; } 
        }
        public Vector2 Position
        {
            get { return position; }
        }

        public Player(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content): base (texture, position, sourceRect)
        {
            
            this.texture = texture;
            this.position = position;
            PlayerMaxHealth = 100;      //ändrat till double för att kunna räkna ut rätt storlek på mätaren i förhållande till max hp 
            PlayerMaxEnergi = 100;
            PlayerCurrentHealth = 100;
            PlayerCurrentEnergi = 100;
            PlayerLevel = 1;
            PlayerExperiencePoints = 0;
            dodgeSpeed = new Vector2(3, 3);
            aimTexture = content.Load<Texture2D>(@"crosshair");
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            last = current;
            current = Keyboard.GetState();
            playerHitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    position.Y -= speed;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    position.Y += speed;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    position.X -= speed;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    position.X += speed;
                if (current.IsKeyDown(Keys.LeftShift) && last.IsKeyUp(Keys.LeftShift) && PlayerCurrentEnergi >= 20)
                    {
                        isDodging = true;
                        PlayerCurrentEnergi -= 20;
                    }
            if (playerHitbox.Intersects(enemy.HitBox) && isDodging == false && playerIsHit == false)
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
            if (dodgeTimer > 300)
            {
                speed = 2;
                isDodging = false;
                dodgeTimer = 0;
            }
            if(hitTimer >= 1)
            {
                playerIsHit = false;
                hitTimer = 0;
            }
            input.Update();
            position += input.position * speed;
            aimPosition = input.aimDirection;// * aimSpeed;

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

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
            sb.Draw(aimTexture, aimPosition, Color.White);
            if(playerIsHit == true)
            {
                sb.Draw(texture, position, Color.Red);
            }
        }
    }
}
