using Microsoft.Xna.Framework;
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
        Rectangle railgunHitbox;
        //Behandling av utritning
        public float weaponRotation;
        public float weaponScale = 1;
        Vector2 dPos; 
        Vector2 weaponOrigin;
        Vector2 railPos;
        InputManager input = new InputManager();
        Texture2D bulletTexture;
        Texture2D railgunTexture;
        Texture2D uziTexture;
        bool weaponOnGround = true;
        bool weaponPickedUp = false;
        bool shotRemoved = false;
        bool canShot = true;
        double shotTimer;
        public List<Bullet> bullets = new List<Bullet>();
        public List<Bullet> shouldBeDeleted = new List<Bullet>();

        public Weapon(Texture2D texture, Vector2 position, Rectangle sourceRect) : base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            bulletTexture = TextureManager.Bullet01Texture;
            railPos = new Vector2(350, 400);           
           // railgunTexture = content.Load<Texture2D>("Railgun");
            uziTexture = TextureManager.Weapon01Texture;
            weaponOrigin = new Vector2(texture.Bounds.Center.X / 2, texture.Bounds.Center.Y);
        }

        public void Update(Player player, List<Enemy> enemyList, Bullet bullet, Gui gui, GameTime gameTime)

        {
            input.Update();
            if (weaponPickedUp)
            {
                shotTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
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
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && shotTimer >= 500 || input.gamePadState.Triggers.Right > 0 && canShot == true
                || input.mousePosOld.LeftButton == ButtonState.Released && input.mousePosNew.LeftButton == ButtonState.Pressed)
            {
                while (shotTimer >= 500)
                {
                    canShot = true;
                    shotTimer = 0;
                }
                Bullet b = new Bullet(bulletTexture);
                b.bulletStartingPosition = player.Position;
                b.startRotation = weaponRotation;
                bullets.Add(b);
                canShot = false;
                SoundManager.Bullet01Sound.Play();
            }
            else if(input.gamePadState.Triggers.Right <= 0)
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
