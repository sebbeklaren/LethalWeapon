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
        Rectangle railSource;
        private int frame;
        double frameinterval = 30, frametimer = 30;
        double animationTime;
        public int shotSpeed;
        public int currentWeapon;
        public int damage;
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
        Texture2D lazerTexture;
        Texture2D uziTexture;
        Texture2D railgunOnGround;
        bool weaponOnGround = true;
        bool weaponPickedUp = false;
        bool shotRemoved = false;
        bool canShot = true;
        bool playerHasWeapon = false;
        bool hasTwoWeapons = false;
        double shotTimer;
        public List<Bullet> bullets = new List<Bullet>();
        public List<Bullet> lazers = new List<Bullet>();
        public List<Bullet> shouldBeDeleted = new List<Bullet>();

        public Weapon(Texture2D texture, Vector2 position, Rectangle sourceRect) : base (texture, position, sourceRect)
        {
            this.texture = texture;
            this.position = position;
            bulletTexture = TextureManager.Bullet01Texture;
            lazerTexture = TextureManager.Bullet02Texture;
            railPos = new Vector2(350, 400);
            railgunTexture = TextureManager.Weapon02TextureAnimated;
            railgunOnGround = TextureManager.Weapon02Texture;
            uziTexture = TextureManager.Weapon01Texture;
            uziPos = new Vector2(100, 300);
            uziTexture = TextureManager.Weapon01Texture;
            weaponOrigin = new Vector2(texture.Bounds.Center.X / 2, texture.Bounds.Center.Y);
            //railOrigin = new Vector2(railgunTexture.Bounds.Center.X / 2, railgunTexture.Bounds.Center.Y / 10);
            railOrigin = new Vector2(railgunOnGround.Bounds.Center.X / 2, railgunOnGround.Bounds.Center.Y);
            shotSpeed = 300;
            uziHitbox = new Rectangle((int)uziPos.X, (int)uziPos.Y, uziTexture.Width, uziTexture.Height);
            railgunHitbox = new Rectangle((int)railPos.X, (int)railPos.Y, railgunTexture.Width, railgunTexture.Height);
            railSource = new Rectangle(0, 64, 64, 64);
        }

        public void checkWeapon(Player player) // Kollar vilken av vapnena som spelaren plockade upp
        {
            if ((player.playerHitboxVertical.Intersects(uziHitbox)) || (player.playerHitboxVertical.Intersects(railgunHitbox)))
            {
                numberOfWeapons += 1;
            }
            if (player.playerHitboxVertical.Intersects(uziHitbox) || currentWeapon == 1)
            {
                damage = 10;
                prevWeapon = 2;
                currentWeapon = 1;
                //uziPos = position;
                texture = uziTexture;
                playerHasWeapon = true;
                uziHitbox = new Rectangle(0, 0, 0, 0);

            }
            if (player.playerHitboxVertical.Intersects(railgunHitbox) || currentWeapon == 2)
            {
                damage = 30;
                prevWeapon = 1;
                currentWeapon = 2;
                //railPos = position;
                texture = railgunOnGround;
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
            frametimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            input.Update();
            checkWeapon(player);
            last = current;
            current = Keyboard.GetState();
            animationTime += gameTime.ElapsedGameTime.TotalMilliseconds;
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
                input.vibrate = true;
                int weaponOffsetX = 20;
                int weaponOffsetY = 30;


                position = new Vector2(player.Position.X + weaponOffsetX, player.Position.Y + weaponOffsetY);
                dPos = player.AimPosition - position;
                weaponRotation = (float)Math.Atan2(dPos.Y, dPos.X);
                if (currentWeapon == 1)
                {
                    uziPos = position;
                }
                if (currentWeapon == 2)
                {
                    railPos = position;
                }
                //if( frametimer <= 0)
                //{
                //    if (Keyboard.GetState().IsKeyDown(Keys.Space) && shotTimer >= shotSpeed
                //    || input.gamePadState.Triggers.Right > 0
                //    || animationTime <= 301)
                //    {
                //        if (animationTime >= 300)
                //        {
                //            frame = 0;
                //        }
                //        frametimer = frameinterval;
                //        frame++;
                //        railSource.Y = (frame % 10) * 64;
                //    }
                //}
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && shotTimer >= shotSpeed || input.gamePadState.Triggers.Right > 0 && canShot == true && shotTimer >= shotSpeed
                 || input.mousePosOld.LeftButton == ButtonState.Released && input.mousePosNew.LeftButton == ButtonState.Pressed && shotTimer >= shotSpeed)

            {
                if (shotTimer >= shotSpeed)
                {
                    canShot = true;
                    shotTimer = 0;
                }
                animationTime = 0;
                    Bullet b = new Bullet(bulletTexture);
                    b.bulletStartingPosition = player.Position;
                    b.startRotation = weaponRotation;
                    bullets.Add(b);
                    SoundManager.Bullet01Sound.Play();
                //if( currentWeapon == 2)
                //{
                //    Bullet b = new Bullet(lazerTexture);
                //    b.bulletStartingPosition = player.Position;
                //    b.startRotation = weaponRotation;
                //    bullets.Add(b);
                //}

                canShot = false;
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
                        e.weaponDamage = damage;
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
                sb.Draw(railgunOnGround, new Vector2(position.X, position.Y), null, Color.White, weaponRotation, railOrigin, weaponScale, SpriteEffects.None, 0f);
            }
            else if (hasTwoWeapons == false)
            {
                sb.Draw(railgunOnGround, railPos, Color.White);
            }
            foreach (Bullet b in bullets)
            {
                b.Draw(sb);
            }
        }
    }
}
