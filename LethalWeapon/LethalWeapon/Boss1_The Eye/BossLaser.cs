using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LethalWeapon
{
    class BossLaser : GameObject
    {
        Texture2D laserTexture;
        public Rectangle destinationRect, hitBox;
        public Rectangle HitBox
        {
            get { return hitBox; }
        }       
        Vector2 position, aimPosition, origin, hitBoxPosition, difference;
        Vector2 laserBeemPos;
        public List<Vector2> beemList = new List<Vector2>();
        float rotation;
        double elapsedTime, warningElapsedTime;
        public int frame = 0;
        public int warningFrame = 0;
        double delayTime = 60;
        double warningDelayTime = 0;
        List<Rectangle> hitBoxList = new List<Rectangle>();
        Rectangle warningLaserDestRect, warningSourceRect;
        bool laserIsReady = false;


        public BossLaser(Texture2D texture, Vector2 position, Rectangle sourceRect, Vector2 playerPosition)
            : base(texture, position, sourceRect)
        {
            //this.hitBoxPosition = position;
            laserTexture = texture;
            aimPosition = playerPosition;                       
            origin = new Vector2(274, 24);
            int playerOffsetX = 100;
            int playerOffsetY = 100;                 
            aimPosition = new Vector2(aimPosition.X - playerOffsetX, aimPosition.Y - playerOffsetY);
            difference =  aimPosition- position;
            difference.Normalize();            
            rotation = (float)Math.Atan2(-difference.Y, -difference.X);
        }
        
        public void Update(GameTime gameTime, Vector2 bossPosition, Vector2 playerPosition)
        {
            int destRectWidth = 350;
            int destRectHeight = 48;
            int positionOffsetX = 110;
            int positionOffsetY = 120;
            int hitboxSize = 30;
            int hiboxPosOffset = 20;
            int maxFrame = 26;
            int hitBoxStartFrame = 12;
            int hitBoxPosSpacing = 50;
            int sourceRectWidth = 298;
            int sourceRectHeight = 48;
            int numberOfhitboxes = 8;
            hitBoxPosition = bossPosition;
            LaserWarning(gameTime, bossPosition);
            if (laserIsReady)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                destinationRect = new Rectangle((int)bossPosition.X + positionOffsetX, (int)bossPosition.Y + positionOffsetY, destRectWidth, destRectHeight);
                hitBox = new Rectangle((int)hitBoxPosition.X - hiboxPosOffset, (int)hitBoxPosition.Y - hiboxPosOffset, hitboxSize, hitboxSize);
                if (elapsedTime >= delayTime)
                {
                    if (frame >= maxFrame)
                    {
                        frame = 0;
                    }
                    else
                    {
                        frame++;
                    }
                    if (frame >= hitBoxStartFrame)
                    {
                        for (int i = 1; i < numberOfhitboxes; i++)
                        {
                            int laserPosOffset = 100;
                            laserBeemPos = new Vector2(hitBoxPosition.X + laserPosOffset, hitBoxPosition.Y + laserPosOffset) + difference * hitBoxPosSpacing * i;
                            beemList.Add(laserBeemPos);
                        }
                    }                   
                    elapsedTime = 0;
                }
                sourceRect = new Rectangle(sourceRectWidth * frame, 0, sourceRectWidth, sourceRectHeight);
            }
            else
            {
                laserIsReady = false;
            }            
        }
        public override void Draw(SpriteBatch sb)
        {
            float layerDepth = 0f;
            //Koll för utskriften av vektorer
            //for (int i = 0; i < beemList.Count; i++)
            //{
            //    sb.Draw(TextureManager.HealtBarTexture, beemList[i], new Rectangle((int)beemList[i].X, (int)beemList[i].Y, 48, 48), Color.White);
            //}
           // sb.Draw(TextureManager.HealtBarTexture, hitBoxPosition, hitBox, Color.White);
            sb.Draw(texture, destinationRect , sourceRect, Color.White, rotation, origin, SpriteEffects.None, layerDepth);         
        }
        //Räknare för att bestämma när lasern ska börja naimeras
         private void LaserWarning(GameTime gameTime, Vector2 bossPosition)
        {
            int numberOfwarningframes = 18;
            warningElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;          
            if (warningElapsedTime >= warningDelayTime)
            {
                if (warningFrame >= numberOfwarningframes)
                {                  
                    laserIsReady = true;
                }
                else
                {
                    warningFrame++;
                    laserIsReady = false;
                }
                warningElapsedTime = 0;
            }
        }
    }
}
