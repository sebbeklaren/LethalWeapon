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
        public static Texture2D Weapon02TextureAnimated { get; private set; }
        public static Texture2D Weapon02Texture { get; private set; }
        public static Texture2D Weapon02IconTexture { get; private set; }
        public static Texture2D Bullet01Texture { get; private set; }
        public static Texture2D Bullet02Texture { get; private set; }

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
        public static Texture2D BossMinionTeleport { get; private set; }
        public static Texture2D BossMinion { get; private set; }
        public static void LoadTextures(ContentManager content)
        {
            MainMenuTexture = content.Load<Texture2D>(@"Textures/Temporary_Textures/MainMenuWall");
            PlayerTexture = content.Load<Texture2D>(@"Textures/Player_Textures/HoodyBoy");
            EnemyTexture = content.Load<Texture2D>(@"Textures/Enemy_Textures/Cyclop");
            Tileset01Texture = content.Load<Texture2D>(@"Textures/Tilesets/Tileset01");
            DesertTile = content.Load<Texture2D>(@"Textures/Tilesets/DesertTile");

            OverWorldtexture = content.Load<Texture2D>(@"Textures/Temporary_Textures/overworldmap");
            Weapon01Texture = content.Load<Texture2D>(@"Textures/Weapon_Textures/Default_Weapon_Textures/Deagle2000");
            Bullet01Texture = content.Load<Texture2D>(@"Textures/Weapon_Textures/Default_Weapon_Textures/Bullet");
            Bullet02Texture = content.Load<Texture2D>(@"Textures/Weapon_Textures/Railgun_Textures/Lazeranimation");
            Weapon02TextureAnimated = content.Load<Texture2D>(@"Textures/Weapon_Textures/Railgun_Textures/Railgunanimation");
            Weapon02Texture = content.Load<Texture2D>(@"Textures/Weapon_Textures/Railgun_Textures/Railgun");
            Weapon02IconTexture = content.Load<Texture2D>(@"Textures/Weapon_Textures/Railgun_Textures/Railicon");
            BossOneTexture = content.Load<Texture2D>(@"Textures/Boss1_Textures/BossOne");
            BossBulletTexture = content.Load<Texture2D>(@"Textures/Boss1_Textures/BossBullet");

            BossMissileTexture = content.Load<Texture2D>(@"Textures/Boss1_Textures/MissileAnimation");
            DesertBackgroundTexture = content.Load<Texture2D>(@"Textures/Map_Additions/Desert/DesertBackground01");
            DesertTile = content.Load<Texture2D>(@"Textures/Tilesets/DesertTile");
            HealtBarTexture = content.Load<Texture2D>(@"Textures/GUI_Textures/HealthBar");
            EnergyBarTexture = content.Load<Texture2D>(@"Textures/GUI_Textures/EnergyBar");

            HealthBorderTexture = content.Load<Texture2D>(@"Textures/GUI_Textures/barBorder");
            ActiveWeaponBorderTexture = content.Load<Texture2D>(@"Textures/GUI_Textures/MetalBorder");
            KillAllEnemiesTexture = content.Load<Texture2D>(@"Textures/GUI_Textures/KillAllEnemies");
            ExitMapTexture = content.Load<Texture2D>(@"Textures/GUI_Textures/ExitMap");
            GameOverTexture = content.Load<Texture2D>(@"Textures/Temporary_Textures/Game Over");

            PlayerAimTexture = content.Load<Texture2D>(@"Textures/GUI_Textures/crosshair");
            BossLaserTexture = content.Load<Texture2D>(@"Textures/Boss1_Textures/LaserSpriteSheet01");
            BossMinionTeleport = content.Load<Texture2D>(@"Textures/Boss1_Textures/TeleportAnimation");
            BossMinion = content.Load<Texture2D>(@"Textures/Boss1_Textures/BossMinion");

        }        
    }
}
