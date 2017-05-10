using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LethalWeapon
{
    class HelpTextManager
    {
        Texture2D killAllEnemiesText, exitMapText;
        Rectangle killAllRect, killDrawRect, exitMapDrawRect, exitMapRect;
        Vector2 killAllPos, exitMapPos;
        double elapsedTime  = 3;
        int alphaV = 254;
        int fadeIncr = 1;     
        double elapsedTimeMap = 0.035;

        public HelpTextManager(ContentManager content, Vector2 playerPos)
        {
            int killAllRectPosOffset = 200;
            int killAllRectWidthOffset = 2;
            int killAllRectHeightOffset = 4;
            int killDrawRectOffset = 2;
            this.killAllPos = playerPos;
            this.exitMapPos = playerPos;
            exitMapText = content.Load<Texture2D>(@"ExitMap");
            killAllEnemiesText = content.Load<Texture2D>(@"KillAllEnemies");
            exitMapDrawRect = new Rectangle(0, 0, exitMapText.Width, exitMapText.Height);
            killAllRect = new Rectangle((int)killAllPos.X - killAllRectPosOffset, (int)killAllPos.Y - killAllRectPosOffset, 
                                         killAllEnemiesText.Width / killAllRectWidthOffset, killAllEnemiesText.Height / killAllRectHeightOffset);
            killDrawRect = new Rectangle(0, 0, killAllEnemiesText.Width, killAllEnemiesText.Height / killDrawRectOffset);            
        }

        public void UpdateKillAll(GameTime gameTime)
        {
            elapsedTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if(elapsedTime <= 0)
            {
                int fadeTimer = 15;
                alphaV += fadeIncr * fadeTimer ;
                if(alphaV >= 255 )
                {
                    fadeIncr *= -1;
                }
            }
        }

        public void KillAllDraw(SpriteBatch sb)
        {
            sb.Draw(killAllEnemiesText, killAllRect, killDrawRect, new Color(alphaV, alphaV, alphaV, alphaV));
        }

        public void ExitMapUpdate(GameTime gameTime, Vector2 playerPos)
        {
            int exitMapRectPosX = 240;
            int exitMapRectPosY = 130;
            int exitMapWidthOffset = 2;
            int exitMapHeightOffset = 4;
            exitMapRect = new Rectangle((int)playerPos.X - exitMapRectPosX, (int)playerPos.Y - exitMapRectPosY,
                                        exitMapText.Width / exitMapWidthOffset, exitMapText.Height / exitMapHeightOffset);
            elapsedTimeMap -= gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTimeMap <= 0)
            {
                int flashTimer = 100;
                int maxAlpha = 255;
                int minAlpha = 0;
                elapsedTimeMap = 0.035;
                alphaV += fadeIncr * flashTimer;
                if (alphaV >= maxAlpha || alphaV <= minAlpha)
                {
                    fadeIncr *= -1;
                }
            }
        }
        public void ExitMapDraw(SpriteBatch sb)
        {
            sb.Draw(exitMapText, exitMapRect, exitMapDrawRect, new Color(alphaV, alphaV, alphaV, alphaV));
        }
    }
}
