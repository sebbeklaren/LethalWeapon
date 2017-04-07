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
<<<<<<< HEAD
        float rotation = 1.0f;
        float layerDepth = 1;
=======
        float aimSpeed = 5.0f;
>>>>>>> origin/master
        public Rectangle playerHitbox;
        Texture2D aimTexture;
        //Stats for Player to read and display
        public double PlayerMaxHealth { get; set; }
        public double PlayerCurrentHealth { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerExperiencePoints { get; set; }
        //ContentManager content;
        Vector2 aimPosition;
        public Vector2 Position
        {
            get { return position; }
        }

        public Player(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content): base (texture, position, sourceRect)
        {            
            this.texture = texture;
            this.position = position;
            PlayerMaxHealth = 100;      //ändrat till double för att kunna räkna ut rätt storlek på mätaren i förhållande till max hp 
            PlayerCurrentHealth = 75;
            PlayerLevel = 1;
            PlayerExperiencePoints = 0;
            aimTexture = content.Load<Texture2D>(@"crosshair");
        }

        public void Update()
        {
            playerHitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                position.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                position.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                position.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                position.X += speed;

            input.Update();
            position += input.position * speed;
            aimPosition += input.aimDirection * aimSpeed;
           
        }

        


        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
            sb.Draw(aimTexture, aimPosition, Color.White);
        }
    }
}
