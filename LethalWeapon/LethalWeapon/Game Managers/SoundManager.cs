using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace LethalWeapon
{    
    public static class SoundManager
    {
        public static SoundEffect Bullet01Sound { get; private set; }
        public static SoundEffect BossMissile { get; private set; }
        public static SoundEffect BossBullets { get; private set; }
        public static SoundEffect RobotEnemy01 { get; private set; }
        public static SoundEffect BossAmbientHum02 { get; private set; }
        public static SoundEffect BossAmbientHover { get; private set; }

        public static Song CityLevelBGM { get; private set; }
        public static Song BossLevelBGM { get; private set; }

        public static void LoadSound(ContentManager content)
        {
            Bullet01Sound = content.Load<SoundEffect>(@"SoundEffects/Laser01");
            BossMissile = content.Load<SoundEffect>(@"SoundEffects/Missile");
            BossBullets = content.Load<SoundEffect>(@"SoundEffects/BossBulletSound");
            RobotEnemy01 = content.Load<SoundEffect>(@"SoundEffects/Robot01");
            BossAmbientHum02 = content.Load<SoundEffect>(@"SoundEffects/BossAimbientHum02");
            BossAmbientHover = content.Load<SoundEffect>(@"SoundEffects/BossHoverHum");

            CityLevelBGM = content.Load<Song>(@"BackgroundMusic/End_Of_The_Unknown"); 
            BossLevelBGM = content.Load<Song>(@"BackgroundMusic/Birth_Of_A_Wish");
        }
    }
}
