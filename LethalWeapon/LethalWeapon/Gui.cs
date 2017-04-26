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
    class Gui
    {
        protected Texture2D healtBarTexture, energyBarTexture, borderTexture;        
        protected Vector2 healthPosition, energyPosition, borderPosition;
        Rectangle healthSourceRect, energySourceRect;
        protected Rectangle healthRect, energyRect;
        protected double health, energy;
        protected int healthBarOffset = 230; // sätta rätt position för hp och energi
        protected int energyBarOffset = 250;

        public Gui(ContentManager content, int health, int energy)
        {
            this.health = health;
            this.energy = energy;
            healtBarTexture = content.Load<Texture2D>(@"Gui/HealthBar");
            energyBarTexture = content.Load<Texture2D>(@"Gui/EnergyBar");
            borderTexture = content.Load<Texture2D>(@"Gui/barBorder");
            energy = 0;
        }

        public void Update(Vector2 cameraPosition, Player player, GameTime gameTime)
        {
            //health = 10; // för att testa så att det funkar att rita ut rätt storlek på mätarna   

            healthPosition = cameraPosition;
            health = (player.PlayerCurrentHealth / player.PlayerMaxHealth) * 100;
            energy = (player.PlayerCurrentEnergi / player.PlayerMaxEnergi) * 100;
            healthRect = new Rectangle((int)healthPosition.X, (int)healthPosition.Y + healthBarOffset,
                    (int)health, healtBarTexture.Height / 4);
            energyRect = new Rectangle((int)healthPosition.X, (int)healthPosition.Y + energyBarOffset,
                    (int)energy, healtBarTexture.Height / 4);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            int borderOffset = 3;
            sb.Draw(healtBarTexture, healthRect, Color.White);
            sb.Draw(energyBarTexture, energyRect, Color.White);

            sb.Draw(borderTexture, new Rectangle((int)healthPosition.X, (int)healthPosition.Y + healthBarOffset,
                    healtBarTexture.Width / 4 + borderOffset, healtBarTexture.Height / 4), Color.White);
            sb.Draw(borderTexture, new Rectangle((int)healthPosition.X, (int)healthPosition.Y + energyBarOffset,
                    healtBarTexture.Width / 4, healtBarTexture.Height / 4), Color.White);
        }
    }
}
