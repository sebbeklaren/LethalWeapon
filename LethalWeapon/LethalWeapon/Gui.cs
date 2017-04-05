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
        Texture2D healtBarTexture, energyBarTexture, borderTexture;
        
        Vector2 healtPosition, energyPosition, borderPosition;
        Rectangle healthSourceRect, energySourceRect;
        Rectangle healthRect, energyRect;
        public int playerHealth, playerEnergy;
        int healthBarOffset = 230;
        int energyBarOffset = 250;

        public Gui(ContentManager content, int playerHealth, int playerEnergy)
        {
            this.playerHealth = playerHealth;
            this.playerEnergy = playerEnergy;
            healtBarTexture = content.Load<Texture2D>(@"Gui/HealthBar");
            energyBarTexture = content.Load<Texture2D>(@"Gui/EnergyBar");
            borderTexture = content.Load<Texture2D>(@"Gui/barBorder");
            
        }

        public void Update(Vector2 cameraPosition)
        {
            playerHealth = 10; // för att testa så att det funkar
            playerEnergy = 45;
            healtPosition = cameraPosition;

            healthRect = new Rectangle((int)healtPosition.X, (int)healtPosition.Y + healthBarOffset,
                    playerHealth, healtBarTexture.Height / 4);
            energyRect = new Rectangle((int)healtPosition.X, (int)healtPosition.Y + energyBarOffset,
                    playerEnergy, healtBarTexture.Height / 4);
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(healtBarTexture, healthRect, Color.White);
            sb.Draw(energyBarTexture, energyRect, Color.White);

            sb.Draw(borderTexture, new Rectangle((int)healtPosition.X, (int)healtPosition.Y + healthBarOffset,
                    healtBarTexture.Width / 4, healtBarTexture.Height / 4), Color.White);
            sb.Draw(borderTexture, new Rectangle((int)healtPosition.X, (int)healtPosition.Y + energyBarOffset,
                    healtBarTexture.Width / 4, healtBarTexture.Height / 4), Color.White);
        }
    }
}
