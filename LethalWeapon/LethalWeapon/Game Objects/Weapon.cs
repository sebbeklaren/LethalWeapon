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
        Rectangle uziHitbox;
        public int shotSpeed;
        public int currentWeapon;
        int prevWeapon;
        int numberOfWeapons;
        //Behandling av utritning
        public float weaponRotation;
        public float weaponScale = 1;
        Vector2 dPos; 
        Vector2 weaponOrigin;
        Vector2 railOrigin;
        Vector2 railPos;
        Vector2 uziPos;
        KeyboardState current;
        KeyboardState last;
        InputManager input = new InputManager();
        Texture2D bulletTexture;
        Texture2D railgunTexture;
        Texture2D uziTexture;
        bool weaponOnGround = true;
        bool weaponPickedUp = false;
        bool shotRemoved = false;
        bool canShot = true;
        bool playerHasWeapon = false;
        bool hasTwoWeapons = false;
        double shotTimer;
        public List<Bullet> bullets = new List<Bullet>();
        public List<Bullet> shouldBeDeleted = new List<Bullet>();

        public Weapon(Texture2D texture, Vector2 position, Rectangle sourceRect) : base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            bulletTexture = TextureManager.Bullet01Texture;
            railPos = new Vector2(350, 400);
            railgunTexture = TextureManager.Weapon02Texture;
            uziTexture = TextureManager.Weapon01Texture;
            uziPos = new Vector2(100, 300);
            uziTexture = TextureManager.Weapon01Texture;
            weaponOrigin = new Vector2(texture.Bounds.Center.X / 2, texture.Bounds.Center.Y);
            railOrigin = new Vector2(railgunTexture.Bounds.Center.X / 2, railgunTexture.Bounds.Center.Y);
            shotSpeed = 300;
            uziHitbox = new Rectangle((int)uziPos.X, (int)uziPos.Y, uziTexture.Width, uziTexture.Height);
            railgunHitbox = new Rectangle((int)railPos.X, (int)railPos.Y, railgunTexture.Width, railgunTexture.Height);
        }

        public void checkWeapon(Player player) // Kollar vilken av vapnena som spelaren plockade upp
        {
            if ((player.playerHitboxVertical.Intersects(uziHitbox)) || (player.playerHitboxVertical.Intersects(railgunHitbox)))
            {
                numberOfWeapons += 1;
            }
            if (player.playerHitboxVertical.Intersects(uziHitbox) || currentWeapon == 1)
            {
                prevWeapon = 2;
                currentWeapon = 1;
                //uziPos = position;
                texture = uziTexture;
                playerHasWeapon = true;
                uziHitbox = new Rectangle(0, 0, 0, 0);

            }
            if (player.playerHitboxVertical.Intersects(railgunHitbox) || currentWeapon == 2)
            {
                prevWeapon = 1;
                currentWeapon = 2;
                //railPos = position;
                texture = railgunTexture;
                playerHasWeapon = true;
                railgunHitbox = new Rectangle(0, 0, 0, 0);
            }
            if (numberOfWeapons == 2)
            {
                hasTwoWeapons = true;
            }
            if (current.IsKeyDown(Keys.LeftShift) && last.IsKeyUp(Keys.LeftShift) && hasTwoWeapons == true)
            {
                currentWeapon = prevWeapon;
            }
        }

        public void Update(Player player, List<Enemy> enemyList, Bullet bullet, Gui gui, GameTime gameTime, LevelManager level)

        {
            input.Update();
            checkWeapon(player);
            last = current;
            current = Keyboard.GetState();
            if (weaponPickedUp)
            {
                shotTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            weaponHitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (/*player.playerHitboxVertical.Intersects(weaponHitbox)*/ currentWeapon > 0)
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
                if (currentWeapon == 1)
                {
                    uziPos = position;
                }
                if (currentWeapon == 2)
                {
                    railPos = position;
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && shotTimer >= shotSpeed || input.gamePadState.Triggers.Right > 0 && canShot == true && shotTimer >= shotSpeed
                 || input.mousePosOld.LeftButton == ButtonState.Released && input.mousePosNew.LeftButton == ButtonState.Pressed && shotTimer >= shotSpeed)

            {
                if (shotTimer >= shotSpeed)
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
                foreach (Rectangle wall in level.hitBoxWall)
                {
                    if (b.HitBox.Intersects(wall))
                    {
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
            if (currentWeapon == 1)
            {               
                sb.Draw(texture, position, null, Color.White, weaponRotation, weaponOrigin, weaponScale, SpriteEffects.None, 0f);
            }
            else if (hasTwoWeapons == false)
            {
                sb.Draw(uziTexture, uziPos, Color.White);
            }
            if (currentWeapon == 2)
            {
                sb.Draw(texture, position, null, Color.White, weaponRotation, railOrigin, weaponScale, SpriteEffects.None, 0f);
            }
            else if (hasTwoWeapons == false)
            {
                sb.Draw(railgunTexture, railPos, Color.White);
            }
            foreach (Bullet b in bullets)
            {
                b.Draw(sb);
            }
        }
    }
}
