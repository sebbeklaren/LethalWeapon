using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace LethalWeapon
{

    public static class TextureManager
    {
        public static Texture2D MainMenuTexture { get; private set; }        
        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D EnemyTexture { get; private set; }
        public static Texture2D Weapon01Texture { get; private set; }
        public static Texture2D Bullet01Texture { get; private set; }

        public static Texture2D BossOneTexture { get; private set; }        
        public static Texture2D OverWorldtexture { get; private set; }
        public static Texture2D GameOverTexture { get; private set; }
        public static Texture2D HealtBarTexture { get; private set; }
        public static Texture2D EnergyBarTexture { get; private set; }

        public static Texture2D HealthBorderTexture { get; private set; }
        public static Texture2D ActiveWeaponBorderTexture { get; private set; }
        public static Texture2D ExitMapTexture { get; private set; }
        public static Texture2D KillAllEnemiesTexture { get; private set; }
        public static Texture2D BossMissileTexture { get; private set; }

        public static Texture2D BossBulletTexture { get; private set; }
        public static Texture2D PlayerAimTexture { get; private set; }
        public static Texture2D Tileset01Texture { get; private set; }
        public static Texture2D DesertTile { get; private set; }
        public static Texture2D DesertBackgroundTexture { get; private set; }

        public static Texture2D BossLaserTexture { get; private set; }

        public static void LoadTextures(ContentManager content)
        {
            MainMenuTexture = content.Load<Texture2D>("MainMenuWall");
            PlayerTexture = content.Load<Texture2D>(@"HoodyBoy");
            EnemyTexture = content.Load<Texture2D>(@"Cyclop");
            Tileset01Texture = content.Load<Texture2D>(@"Tileset01");
            DesertTile = content.Load<Texture2D>(@"DesertTile");

            OverWorldtexture = content.Load<Texture2D>(@"overworldmap");
            Weapon01Texture = content.Load<Texture2D>(@"PlaceHolderUzi");
            Bullet01Texture = content.Load<Texture2D>(@"Bullet");
            BossOneTexture = content.Load<Texture2D>(@"BossOne");
            BossBulletTexture = content.Load<Texture2D>(@"BossBullet");

            BossMissileTexture = content.Load<Texture2D>(@"Missile");
            DesertBackgroundTexture = content.Load<Texture2D>(@"DesertBackground01");
            DesertTile = content.Load<Texture2D>(@"DesertTile");
            HealtBarTexture = content.Load<Texture2D>(@"Gui/HealthBar");
            EnergyBarTexture = content.Load<Texture2D>(@"Gui/EnergyBar");

            HealthBorderTexture = content.Load<Texture2D>(@"Gui/barBorder");
            ActiveWeaponBorderTexture = content.Load<Texture2D>(@"Gui/MetalBorder");
            KillAllEnemiesTexture = content.Load<Texture2D>(@"KillAllEnemies");
            ExitMapTexture = content.Load<Texture2D>(@"ExitMap");
            GameOverTexture = content.Load<Texture2D>(@"Game Over");

            PlayerAimTexture = content.Load<Texture2D>(@"crosshair");
            BossLaserTexture = content.Load<Texture2D>(@"LaserSpriteSheet");
        }        
    }
}
