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
        int randSelect;
        Vector2 minionDirection, aimVector, difference, bulletPosition;
        protected Rectangle hitBox, minionPosRect, drawtRect, playerDistanceRect, bulletRect;
        Texture2D animationTexture, bulletTexture;
        BossOneBullets bullets;
        List<BossOneBullets> bulletList = new List<BossOneBullets>();
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
        bool closeToPlayer = false;
        public BossMinion(Texture2D animationTexture, Texture2D texture, Vector2 position, Rectangle sourceRect, Vector2 playerPos, int randSelect) : base(texture, position, sourceRect)
        {
            this.position = position;
            this.texture = texture;
            this.aimVector = playerPos;
            this.animationTexture = animationTexture;
            this.randSelect = randSelect;
            bulletTexture = TextureManager.MinionLaser;
            bulletRect = new Rectangle(0, 0, 15, 15);
            MinionCurrentHealth = minionMaxHealth;
           
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
        
        public void Update(GameTime gameTime, Player player)
        {
            int playerDestRectOffset = 64;
            int textureOffset = 5;
            Animate(gameTime);
            if (!closeToPlayer)
            {
                Movement();
            }
            else if(closeToPlayer)
            {
                Fire(player);
            }
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width / textureOffset, texture.Height);
            minionPosRect = new Rectangle((int)position.X, (int)position.Y, texture.Width / textureOffset, texture.Height);
            playerDistanceRect = new Rectangle((int)position.X - playerDestRectOffset, (int)position.Y - playerDestRectOffset, texture.Width, texture.Height * textureOffset);

            if(player.playerHitboxVertical.Intersects(playerDistanceRect))
            {
                closeToPlayer = true;
            }
            else
            {
                closeToPlayer = false;
            }
           for(int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update(gameTime);
                if(bulletList[i].position.X <= 0 || bulletList[i].position.X >= 1024 || bulletList[i].position.Y <= 0 || bulletList[i].position.Y >= 768 || Vector2.Distance(bulletList[i].position, position) >= 130 )
                {
                    bulletList.Remove(bulletList[i]);
                }                
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            //sb.Draw(TextureManager.HealtBarTexture, new Vector2(playerDistanceRect.X, playerDistanceRect.Y), playerDistanceRect, Color.White);
            if (!teleportDone)
            {
                sb.Draw(animationTexture, minionPosRect, drawtRect, Color.White);
            }
            else
            {
                sb.Draw(texture, minionPosRect, drawtRect, Color.White);
            }
            foreach(BossOneBullets bullet in bulletList)
            {
                bullet.Draw(sb);
            }
        }

        private void Movement()
        {
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

        private void Fire(Player player)
        {            
            for(int i = 1; i <= 1; i++)
            {
                if (bulletList.Count <= 0)
                {
                    bullets = new BossOneBullets(bulletTexture, new Vector2(position.X + 16, position.Y + 16),
                                            new Rectangle(0, 0, 15, 15), player, 12, 32, Vector2.Zero, 5, i, true, false);
                    bulletList.Add(bullets);
                }
            }
            
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
