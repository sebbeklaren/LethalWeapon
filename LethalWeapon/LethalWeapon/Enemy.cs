using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace LethalWeapon
{
    class Enemy : GameObject
    {
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
        protected bool enemyIsNearPlayer;
        protected bool hasCorrectStartingPosition;
        protected Vector2 destination;
        protected bool isAlive;

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
            this.texture = texture;
            this.position = position;
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            startingPosition = position;
            isMoving = false;
            distanceLimit = 50;
            aggroRange = 150;
            enemyIsNearPlayer = false;
            hasCorrectStartingPosition = true;
            isAlive = true;
            EnemyMaxHealth = 10000;
            EnemyCurrentHealth = EnemyMaxHealth;
        }

        public void Update(Player player)
        {
            Movement();

            IsPlayerNear(player);

            if (enemyIsNearPlayer)
                MoveTowardsPlayer(player); //Flyttar fienden närmare spelaren
            else if (!hasCorrectStartingPosition)
                MakeNewStartingPosition(); //Ändrar fiendens grundposition

            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;

            HasDied();
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isAlive)
                sb.Draw(texture, position, Color.White);
        }

        private void Movement()
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

        private void IsPlayerNear(Player player)
        {
            if (Vector2.Distance(player.Position, position) <= aggroRange)
            {
                enemyIsNearPlayer = true;
                hasCorrectStartingPosition = false;
                speed.X = 0;
                speed.Y = 0;
            }
            else
                enemyIsNearPlayer = false;
        }

        private void MoveTowardsPlayer(Player player)
        {
            destination = player.Position - position;
            position += Vector2.Normalize(destination);
        }

        private void MakeNewStartingPosition()
        {
            startingPosition = position;
            hasCorrectStartingPosition = true;
        }

        public void TakeDamage()
        {
            EnemyCurrentHealth -= 50;
        }

        protected void HasDied()
        {
            if (EnemyCurrentHealth <= 0)
                isAlive = false;
        }
    }
}