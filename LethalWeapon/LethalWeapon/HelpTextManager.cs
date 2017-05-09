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
            this.killAllPos = playerPos;
            this.exitMapPos = playerPos;
            exitMapText = content.Load<Texture2D>(@"ExitMap");
            killAllEnemiesText = content.Load<Texture2D>(@"KillAllEnemies");
            exitMapDrawRect = new Rectangle(0, 0, exitMapText.Width, exitMapText.Height);
            killAllRect = new Rectangle((int)killAllPos.X - 200, (int)killAllPos.Y - 200, 
                                         killAllEnemiesText.Width / 2, killAllEnemiesText.Height / 4);
            killDrawRect = new Rectangle(0, 0, killAllEnemiesText.Width, killAllEnemiesText.Height / 2);            
        }

        public void UpdateKillAll(GameTime gameTime)
        {
            elapsedTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if(elapsedTime <= 0)
            {
                alphaV += fadeIncr;
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
            exitMapRect = new Rectangle((int)playerPos.X - 240, (int)playerPos.Y - 130,
                                        exitMapText.Width / 2, exitMapText.Height / 4);

            elapsedTimeMap -= gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTimeMap <= 0)
            {
                int flashTimer = 100;
                elapsedTimeMap = 0.035;
                alphaV += fadeIncr * flashTimer;
                if (alphaV >= 255 || alphaV <= 0)
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
