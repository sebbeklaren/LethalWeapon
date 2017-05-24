using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Xna.Framework.Audio;

namespace LethalWeapon
{
    class Enemy : GameObject
    {
        SoundEffect effect;
        SoundEffectInstance effectInstacne;
        protected Rectangle hitBox;
        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        protected Vector2 startingPosition;
        protected Vector2 speed;
        protected static Random random = new Random();
        protected int direction;
        protected bool isMoving;
        protected int distanceLimit;
        protected int aggroRange;
        public int damageTaken;     
        protected int minDistanceToPlayer;
        protected bool enemyTooCloseToPlayer;
        protected bool enemyIsNearPlayer;
        protected bool hasCorrectStartingPosition;
        protected Vector2 destination;
        public bool isAlive;

        List<EnemyBullet> enemyBulletList = new List<EnemyBullet>();
        protected int attackRange;
        protected Texture2D bulletTexture;
        protected float shotInterval;
        protected float shotTimer;
        protected bool canShoot;

        protected Vector2 moveAwayFromEnemyDestination;
        protected bool movingAwayFromEnemy;
        protected float moveAwayFromEnemyTimer;
        protected float moveAwayFromEnemyInterval;

        protected Vector2 moveAwayFromWallDestination;
        protected bool movingAwayFromWall;
        protected float moveAwayFromWallTimer;
        protected float moveAwayFromWallInterval;

        public double EnemyCurrentHealth
        {
            get; set;
        }

        public double EnemyMaxHealth
        {
            get; set;
        }

        public Enemy(Texture2D texture, Vector2 position, Rectangle sourceRect)
            : base(texture, position, sourceRect)
        {
            effect = SoundManager.RobotEnemy01;
            effectInstacne = effect.CreateInstance();
            this.texture = texture;
            this.position = position;
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            startingPosition = position;
            isMoving = false;
            distanceLimit = 35;
            aggroRange = 300;
            attackRange = 175;
            minDistanceToPlayer = 50;
            enemyTooCloseToPlayer = false;
            enemyIsNearPlayer = false;
            hasCorrectStartingPosition = true;

            isAlive = true;
            EnemyMaxHealth = 50;
            EnemyCurrentHealth = EnemyMaxHealth;

            bulletTexture = TextureManager.Bullet01Texture;
            canShoot = true;
            shotInterval = 1000;
            shotTimer = shotInterval;

            movingAwayFromEnemy = false;
            moveAwayFromEnemyInterval = 250;
            moveAwayFromEnemyTimer = moveAwayFromEnemyInterval;

            movingAwayFromWall = false;
            moveAwayFromWallInterval = 500;
            moveAwayFromWallTimer = moveAwayFromWallInterval;
        }

        public void Update(Player player, GameTime gameTime)
        {
            if (!movingAwayFromEnemy && !movingAwayFromWall)
            {
                Movement();

                IsPlayerNear(player);
                IsTooCloseToPlayer(player);
            }

            if(movingAwayFromEnemy)
            {
                MoveAwayFromEnemy(gameTime);
            }

            if(movingAwayFromWall)
            {
                MoveAwayFromWall(gameTime);
            }

            if (enemyIsNearPlayer && !enemyTooCloseToPlayer)
                MoveTowardsPlayer(player); //Flyttar fienden närmare spelaren
            else if (!hasCorrectStartingPosition)
                MakeNewStartingPosition(); //Ändrar fiendens grundposition
            if(shotTimer > 0)
                shotTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            else if(shotTimer <= 0 && canShoot == false)
            {
                canShoot = true;
            }
            
            if (canShoot)
            {
                IsInShootingRange(player); //Erhåller playerns position och kollar om playern är inom robotens shooting range
            }

            foreach(EnemyBullet bullet in enemyBulletList) //Uppdaterar fiendens skott
            {
                bullet.UpdateEnemyBullet(player);
                if (bullet.isActive)
                {
                    if (bullet.hitBox.Intersects(player.playerHitboxVertical)) //Kollar om playerns rektangel intersect med bullet rectangeln och om det gör, tar playern skada.
                    {
                        if (!player.isDodging && !player.playerIsHit)
                        {
                            player.PlayerCurrentHealth -= 20;
                            player.playerIsHit = true;
                        }
                        bullet.isActive = false;
                    }
                }
            }

            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;

            if (player.playerHitboxHorizontal.Intersects(hitBox) && player.isDodging == false && player.playerIsHit == false) // Kollar om spelaren kan ta skada 
            {                                                                                                                 // och om fienden kolliderar med spelaren
                player.PlayerCurrentHealth -= 20;
                player.playerIsHit = true;
            }

            HasDied();

            if (!isAlive) //Om roboten dör, försvinner all dess skott i listan.
            {
                enemyBulletList.Clear();
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isAlive)
                sb.Draw(texture, position, Color.White);

            foreach(EnemyBullet bullet in enemyBulletList)
            {
                bullet.DrawEnemyBullet(sb);
            }
        }

        private void Movement() //Robotens rörelse
        {
            if (!isMoving)
            {
                direction = random.Next(1, 5);
                if (direction == 1)
                    speed.X = 1;
                else if (direction == 2)
                    speed.X = -1;
                else if (direction == 3)
                    speed.Y = 1;
                else if (direction == 4)
                    speed.Y = -1;

                isMoving = true;
            }

            //Kollar om fienden har gått max range från startpositionen. Om detta är sant vänder den håll
            if (position.X >= startingPosition.X + distanceLimit || position.X <= startingPosition.X - distanceLimit
                || position.Y >= startingPosition.Y + distanceLimit || position.Y <= startingPosition.Y - distanceLimit)
                speed *= -1;

            position += speed;

            if (position == startingPosition)
            {
                isMoving = false;
                speed.X = 0;
                speed.Y = 0;
            }
        }

        private void IsPlayerNear(Player player)  //Om spelaren är inom x pixlar börjar fienden gå mot spelaren
        {
            if (Vector2.Distance(player.Position, position) <= aggroRange)
            {
                enemyIsNearPlayer = true;
                hasCorrectStartingPosition = false;
                speed.X = 0;
                speed.Y = 0;
                effectInstacne.Play();
                
            }
            else
                enemyIsNearPlayer = false;
        }

        private void MoveTowardsPlayer(Player player)
        {
            destination = player.Position - position;
            position += Vector2.Normalize(destination);
        }

        private void MakeNewStartingPosition() //När spelaren går utanför fiendens "chase range" 
        {                                      //får fienden en ny startposition
            startingPosition = position;
            hasCorrectStartingPosition = true;
        }

        public void TakeDamage()
        {
            EnemyCurrentHealth -= damageTaken;
        }

        protected void HasDied()
        {
            if (EnemyCurrentHealth <= 0)
                isAlive = false;
        }

        protected void IsTooCloseToPlayer(Player player)
        {
            if (Vector2.Distance(player.Position, position) <= minDistanceToPlayer)
                enemyTooCloseToPlayer = true;
            else
                enemyTooCloseToPlayer = false;
        }

        protected void IsInShootingRange(Player player) //Är spelaren inom skotthåll skjuts ett skott mot hen
        {
            if(Vector2.Distance(player.Position, position) <= attackRange)
            {
                ShootBullet(player);
            }
        }

        protected void ShootBullet(Player player)
        {
            EnemyBullet tempBullet = new EnemyBullet(bulletTexture, position, player.position);
            enemyBulletList.Add(tempBullet);
            canShoot = false;
            shotTimer = shotInterval;
        }

        public void EnemyTooClose(Vector2 destination)
        {
            moveAwayFromEnemyDestination = destination;
            movingAwayFromEnemy = true;
            moveAwayFromEnemyTimer = moveAwayFromEnemyInterval;
        }

        protected void MoveAwayFromEnemy(GameTime gameTime) //Flyttar fienden bort från den andra fienden under x millisekunder
        {
            if (moveAwayFromEnemyTimer > 0)
            {
                moveAwayFromEnemyTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                position += Vector2.Normalize(moveAwayFromEnemyDestination);
            }
            else
                movingAwayFromEnemy = false;
        }

        public void WallTooClose(Vector2 destination)
        {
            moveAwayFromWallDestination = destination;
            movingAwayFromWall = true;
            moveAwayFromWallTimer = moveAwayFromWallInterval;
        }

        protected void MoveAwayFromWall(GameTime gameTime)
        {
            if (moveAwayFromWallTimer > 0)
            {
                moveAwayFromWallTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                position += Vector2.Normalize(moveAwayFromWallDestination);
            }
            else
                movingAwayFromWall = false;
        }
    }
}
//Ta bort blankrader och kommentera metoder. -Johnny