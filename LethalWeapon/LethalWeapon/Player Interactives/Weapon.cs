using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class Weapon : GameObject
    {
        Rectangle weaponHitbox;
        //Behandling av utritning
        public float weaponRotation;
        public float weaponScale = 1;
        Vector2 dPos; 
        Vector2 weaponOrigin;
        InputManager input = new InputManager();
        Texture2D bulletTexture;
        bool weaponOnGround = true;
        bool weaponPickedUp = false;
        bool shotRemoved = false;
        bool canShot = true;
        double shotTimer;
        public List<Bullet> bullets = new List<Bullet>();
        public List<Bullet> shouldBeDeleted = new List<Bullet>();

        public Weapon(Texture2D texture, Vector2 position, Rectangle sourceRect, ContentManager content) : base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            bulletTexture = content.Load<Texture2D>("Textures/PlayerBulletTextures/Bullet");
            weaponOrigin = new Vector2(texture.Bounds.Center.X / 2, texture.Bounds.Center.Y);
        }

        public void Update(Player player, List<Enemy> enemyList, Bullet bullet, Gui gui, GameTime gameTime)

        {
            input.Update();
            shotIntervall();
            shotTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            weaponHitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (player.playerHitboxVertical.Intersects(weaponHitbox))
            {
                weaponOnGround = false;
                weaponPickedUp = true;
                gui.WeaponIsPickedUp = true;
            }
            if (weaponOnGround == false && weaponPickedUp == true)
            {
                int weaponOffsetX = 20;
                int weaponOffsetY = 30;

                dPos = player.AimPosition - position;
                position = new Vector2(player.Position.X + weaponOffsetX, player.Position.Y + weaponOffsetY);
                weaponRotation = (float)Math.Atan2(dPos.Y, dPos.X);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && shotTimer >= 500 || input.gamePad.Triggers.Right > 0 && canShot == true
                || input.mousePosOld.LeftButton == ButtonState.Released && input.mousePosNew.LeftButton == ButtonState.Pressed)
            {
                while (shotTimer >= 500)
                {
                    canShot = true;
                    shotTimer = 0;
                }
                Bullet b = new Bullet(bulletTexture);
                b.bulletStartingPosition = player.Position;
                bullets.Add(b);
                canShot = false;
            }
            else if(input.gamePad.Triggers.Right <= 0)
            {
                canShot = true;
            }
            foreach (Bullet b in bullets.ToList())
            {
                b.Update(player);
                foreach (Enemy e in enemyList)
                {                    
                    if (e.HitBox.Intersects(b.HitBox))
                    {
                        e.TakeDamage();
                        shotRemoved = true;
                    }
                }
                if (shotRemoved == true)
                {
                    bullets.Remove(b);
                    shotRemoved = false;
                }
                if(Vector2.Distance(b.position, player.position) >= 500)
                {
                    bullets.Remove(b);
                }
            }
        }

        public void shotIntervall()
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, null, Color.White, weaponRotation, weaponOrigin, weaponScale, SpriteEffects.None, 0f);
            foreach (Bullet b in bullets)
            {
                b.Draw(sb);
            }
        }
    }
}
