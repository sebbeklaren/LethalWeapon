using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace LethalWeapon
{
    class BossMinion : GameObject
    {
        Random randomPositive, randomNegative, randomSelect;
        int randPositive, randNegative, randSelect;
        Vector2 minionDirection, aimVector, difference;
        protected Rectangle hitBox, minionPosRect, drawtRect;
        Texture2D animationTexture;
        public Rectangle HitBox
        {
            get { return hitBox; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public double MinionCurrentHealth
        {
            get; set;
        }
        private double minionMaxHealth = 100;
        double elapsedTimeTele, elapsedTimeMinion;
        int teleportFrame = 0;
        int minionFrame = 0;
        double teleDelayTime = 70;
        double minionDelayTime = 200;
        bool teleportDone = false;
        public BossMinion(Texture2D animationTexture, Texture2D texture, Vector2 position, Rectangle sourceRect, Vector2 playerPos, int randSelect) : base(texture, position, sourceRect)
        {
            this.position = position;
            this.texture = texture;
            this.aimVector = playerPos;
            this.animationTexture = animationTexture;
            this.randSelect = randSelect;
            MinionCurrentHealth = minionMaxHealth;
           // RandomDirection();
            if (randSelect <= 25)
            {
                minionDirection = new Vector2(0, 0.5f);
            }
            else if(randSelect >=26 && randSelect <= 50)
            {
                minionDirection = new Vector2(0, -0.5f);
            }
            else if(randSelect >= 51 && randSelect <= 75)
            {
                minionDirection = new Vector2(-0.5f, 0);
            }
            else if(randSelect >= 76 && randSelect <= 100)
            {
                minionDirection = new Vector2(0.5f, 0);
            }
            
        }
        
        public void Update(GameTime gameTime)
        {
            Animate(gameTime);
            Movement();
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width / 5, texture.Height);
            minionPosRect = new Rectangle((int)position.X, (int)position.Y, texture.Width / 5, texture.Height);


           
        }
        public override void Draw(SpriteBatch sb)
        {
            if (!teleportDone)
            {
                sb.Draw(animationTexture, minionPosRect, drawtRect, Color.White);
            }
            else
            {
                sb.Draw(texture, minionPosRect, drawtRect, Color.White);
            }
        }

        private void Movement()
        {
           // RandomDirection();
            if (teleportDone)
            {                
                if(position.Y >= 736)
                {
                    position.Y = 735;
                    minionDirection = new Vector2(0, -0.5f);
                }
                if (position.Y <= 0)
                {
                    position.Y = 1;
                    minionDirection = new Vector2(0, 0.5f);
                }
                if (position.X >= 991)
                {
                    position.X = 990;
                    minionDirection = new Vector2(-0.5f, 0);
                }
                if (position.X <= 0)
                {
                    position.X = 1;
                    minionDirection = new Vector2(0.5f, 0);
                }
                position += minionDirection;
            }
        }
        //private void RandomDirection()
        //{
        //    randomSelect = new Random();
        //    randSelect = randomSelect.Next(0, 100);
        //}

        private void Fire()
        {

        }

        private void Animate(GameTime gameTime)
        {
            elapsedTimeTele += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!teleportDone)
            {
                if (elapsedTimeTele >= teleDelayTime)
                {
                    if (teleportFrame >= 15)
                    {
                        teleportDone = true;
                    }
                    else
                    {
                        teleportFrame++;
                    }
                    elapsedTimeTele = 0;
                    drawtRect = new Rectangle((texture.Width / 5) * teleportFrame, 0, texture.Width / 5, texture.Height);
                }
            }
            else
            {
                elapsedTimeMinion += gameTime.ElapsedGameTime.TotalMilliseconds;
                if(elapsedTimeMinion >= minionDelayTime)
                {
                    if(minionFrame >= 4)
                    {
                        minionFrame = 0;
                    }
                    else
                    {
                        minionFrame++;
                    }
                    elapsedTimeMinion = 0;
                }
                drawtRect = new Rectangle((texture.Width / 5) * minionFrame, 0, texture.Width / 5, texture.Height);
            }           
        }        
    }
}
