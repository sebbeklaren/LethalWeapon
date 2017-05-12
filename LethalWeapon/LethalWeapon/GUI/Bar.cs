using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace LethalWeapon
{
    class Bar : Gui
    {
        private int healthYPositionOffset;

        public Bar(int maxHealth, int maxEnergy)
            : base(maxHealth, maxEnergy)
        {
            health = maxHealth;
            energy = maxEnergy;
            healthYPositionOffset = 10;
        }

        public void UpdateBar(Enemy enemy)
        {
            healthPosition.X = enemy.Position.X;
            healthPosition.Y = enemy.Position.Y - healthYPositionOffset;

            health = (float)(enemy.EnemyCurrentHealth / enemy.EnemyMaxHealth) * 100;

            healthRect = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, (int)(health / 4), healtBarTexture.Height / 20);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(healtBarTexture, healthRect, Color.White);
        }
    }
}