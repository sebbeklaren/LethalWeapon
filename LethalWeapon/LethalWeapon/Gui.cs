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
        double playerHealth, playerEnergy;
        int healthBarOffset = 230; // sätta rätt position för hp och energi
        int energyBarOffset = 250;

        public Gui(ContentManager content, int playerHealth, int playerEnergy)
        {
            this.playerHealth = playerHealth;
            this.playerEnergy = playerEnergy;
            healtBarTexture = content.Load<Texture2D>(@"Gui/HealthBar");
            energyBarTexture = content.Load<Texture2D>(@"Gui/EnergyBar");
            borderTexture = content.Load<Texture2D>(@"Gui/barBorder");            
        }

        public void Update(Vector2 cameraPosition, Player player)
        {
            //playerHealth = 10; // för att testa så att det funkar att rita ut rätt storlek på mätarna
            playerEnergy = 45;
            healtPosition = cameraPosition;
            playerHealth = (player.PlayerCurrentHealth / player.PlayerMaxHealth) * 100;

            healthRect = new Rectangle((int)healtPosition.X, (int)healtPosition.Y + healthBarOffset,
                   /* player.PlayerCurrentHealth*/ (int)playerHealth, healtBarTexture.Height / 4);
            energyRect = new Rectangle((int)healtPosition.X, (int)healtPosition.Y + energyBarOffset,
                    (int)playerEnergy, healtBarTexture.Height / 4);
        }

        public void Draw(SpriteBatch sb)
        {
            int borderOffset = 3;
            sb.Draw(healtBarTexture, healthRect, Color.White);
            sb.Draw(energyBarTexture, energyRect, Color.White);

            sb.Draw(borderTexture, new Rectangle((int)healtPosition.X, (int)healtPosition.Y + healthBarOffset,
                    healtBarTexture.Width / 4 + borderOffset, healtBarTexture.Height / 4), Color.White);
            sb.Draw(borderTexture, new Rectangle((int)healtPosition.X, (int)healtPosition.Y + energyBarOffset,
                    healtBarTexture.Width / 4, healtBarTexture.Height / 4), Color.White);
        }
    }
}
