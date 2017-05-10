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
        protected Texture2D healtBarTexture, energyBarTexture, borderTexture, activeWeaponBorderTex, activeWeaponTex;
        protected Vector2 healthPosition, activeWeaponBorderPosition;        
        protected Rectangle healthRect, energyRect, activeWeaponBorderRect;
        protected double health, energy;
        protected int healthBarOffset = 230;
        protected int energyBarOffset = 250;
        protected int activeWeaponBorderOffsetX = 200;
        protected int activeWeaponBorderOffsetY = 100;
        public float activeWeaponRotation, activeWeaponScale;
        Vector2 weaponOrigin;

        public bool WeaponIsPickedUp { get; set; } //= false;

        public Gui(ContentManager content, int health, int energy)
        {
            this.health = health;
            this.energy = energy;
            healtBarTexture = content.Load<Texture2D>(@"Gui/HealthBar");
            energyBarTexture = content.Load<Texture2D>(@"Gui/EnergyBar");
            borderTexture = content.Load<Texture2D>(@"Gui/barBorder");
            activeWeaponBorderTex = content.Load<Texture2D>(@"Gui/MetalBorder");
            activeWeaponTex = content.Load<Texture2D>("PlaceHolderUzi");
            activeWeaponScale = 2;
            weaponOrigin = new Vector2(activeWeaponTex.Bounds.Center.X / 2, borderTexture.Bounds.Center.Y);
        }

        public void Update(Vector2 cameraPosition, Player player, GameTime gameTime)
        {
            int healthMultiplier = 100;
            healthPosition = cameraPosition;
            activeWeaponBorderPosition = cameraPosition;
            health = (player.PlayerCurrentHealth / player.PlayerMaxHealth) * healthMultiplier;
            energy = (player.PlayerCurrentEnergi / player.PlayerMaxEnergi) * healthMultiplier;
            healthRect = new Rectangle((int)healthPosition.X, (int)healthPosition.Y + healthBarOffset,
                    (int)health, healtBarTexture.Height / 4);
            energyRect = new Rectangle((int)healthPosition.X, (int)healthPosition.Y + energyBarOffset,
                    (int)energy, healtBarTexture.Height / 4);
            activeWeaponBorderRect = new Rectangle((int)activeWeaponBorderPosition.X - activeWeaponBorderOffsetX,
            (int)activeWeaponBorderPosition.Y - activeWeaponBorderOffsetY, activeWeaponBorderTex.Width,
                    activeWeaponBorderTex.Height);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            int borderOffset = 3;
            int heightOffset = 4;
            sb.Draw(healtBarTexture, healthRect, Color.White);
            sb.Draw(energyBarTexture, energyRect, Color.White);

            sb.Draw(borderTexture, new Rectangle((int)healthPosition.X, (int)healthPosition.Y + healthBarOffset,
                    healtBarTexture.Width / 4 + borderOffset, healtBarTexture.Height / heightOffset), Color.White);
            sb.Draw(borderTexture, new Rectangle((int)healthPosition.X, (int)healthPosition.Y + energyBarOffset,
                    healtBarTexture.Width / 4 + borderOffset, healtBarTexture.Height / heightOffset), Color.White);
            sb.Draw(activeWeaponBorderTex, new Vector2(activeWeaponBorderRect.X, activeWeaponBorderRect.Y), null, Color.White, activeWeaponRotation,
                    new Vector2(0, 0), activeWeaponScale, SpriteEffects.None, 0f);

            if (WeaponIsPickedUp == true)
            {
                int offset = 20;
                float rotation = 0f;
                float scale = 1.5f;
                float depth = 1f;
                Vector2 weaponPos = new Vector2(activeWeaponBorderRect.X + offset, activeWeaponBorderRect.Y + offset);
                sb.Draw(activeWeaponTex, weaponPos, null, Color.White, rotation, weaponOrigin, scale, SpriteEffects.None, depth);
            }
        }
    }
}
