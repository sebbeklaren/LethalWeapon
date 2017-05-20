using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace LethalWeapon
{
    class GamePlayManager
    {
        Player player;
        List<Enemy> enemyList = new List<Enemy>();
        Texture2D overWorldTex;
        Enemy tempEnemy;
        List<Bar> enemyHealthBarList = new List<Bar>();
        Bar tempEnemyHealthBar;
        Weapon weapon;
        Gui gui;
        Bullet bullet;
        BossOne bossOne;        
        Rectangle sourceRect;
        LevelManager level;
        string currentLevel;
        public Camera camera;
        Vector2 cameraOffset;
        public int screenHeight, screenWidth;
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;
        CollisionDetection collision;
        Texture2D craterText;
        public bool levelCleard = false;
        Game1 game;
        GameOver gameOver;
        HelpTextManager killAllEnemies, exitMap;
        Texture2D gameOverTex;
        public bool isGameOver = false;
        OverWorld overWorld;
        Vector2 playerPosition = new Vector2(250, 540);
        Vector2 weaponPos = new Vector2(100, 300);
        Vector2 bossOnePos = new Vector2(500, 300);
        public GamePlayManager(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, Game1 game)
        {
            screenHeight = 32 * 24;
            screenWidth = 32 * 32;
            
            this.graphics = graphics;
            this.graphicsDevice = graphicsDevice;
            this.game = game;
            gameOverTex = TextureManager.GameOverTexture;
            craterText = TextureManager.DesertBackgroundTexture;
            player = new Player(TextureManager.PlayerIdleSpriteSheet, playerPosition, sourceRect, screenWidth, screenHeight);
            for (int i = 0; i < 3; i++)
            {
                Vector2 enemyPos = new Vector2(400, 240 + 50 * i);
                tempEnemy = new Enemy(TextureManager.EnemyTexture, enemyPos, sourceRect);
                enemyList.Add(tempEnemy);
                tempEnemyHealthBar = new Bar((int)tempEnemy.EnemyMaxHealth, 0);
                enemyHealthBarList.Add(tempEnemyHealthBar);
            }
            weapon = new Weapon(TextureManager.Weapon01Texture, weaponPos, sourceRect);
            bullet = new Bullet(TextureManager.Bullet01Texture);
            bossOne = new BossOne(TextureManager.BossOneTexture, bossOnePos, sourceRect, screenWidth, screenHeight);           
            gui = new Gui(1, 1);
            overWorldTex = TextureManager.OverWorldtexture;
            overWorld = new OverWorld(game);
            collision = new CollisionDetection();
            Viewport view = graphicsDevice.Viewport;
            camera = new Camera(view);
            cameraOffset = new Vector2(35, 65);            
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            camera.Rotation = 0f;
            gameOver = new GameOver(gameOverTex);
            killAllEnemies = new HelpTextManager(player.position);
            exitMap = new HelpTextManager( player.position);

        }

        public void Update(GameTime gameTime)
        {
            if (player.PlayerCurrentHealth <= 0)
                isGameOver = true;                
        }

        public void DrawCityLevel(SpriteBatch spriteBatch)
        {        
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);            
            weapon.Draw(spriteBatch);
            for(int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Draw(spriteBatch);
                enemyHealthBarList[i].Draw(spriteBatch);
            }
            gui.Draw(spriteBatch);
            killAllEnemies.KillAllDraw(spriteBatch);
            if (levelCleard)
            {
                exitMap.ExitMapDraw(spriteBatch);
            }            
        }

        public void DrawRuinsLevel(SpriteBatch spriteBatch)
        {
            Vector2 craterTexturePosition = new Vector2(100, 0);
            level.Draw(spriteBatch);
            spriteBatch.Draw(craterText, craterTexturePosition, Color.White);
            bossOne.Draw(spriteBatch);
            player.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
            gui.Draw(spriteBatch);
        }

        public void UpdateRuinsLevel(GameTime gameTime)
        {
            weapon.Update(player, enemyList, bullet, gui, gameTime, level);
            player.Update(gameTime, tempEnemy);
            gui.Update(camera.GetPosition(), player, gameTime, weapon);
            bossOne.Update(player, gameTime, weapon, camera.GetPosition());
            CheckForCollision();
        }

        public void UpdateCityLevel(GameTime gameTime)
        {
            weapon.Update(player, enemyList, bullet, gui, gameTime, level);
            player.Update(gameTime, tempEnemy);
            gui.Update(camera.GetPosition(), player, gameTime, weapon);
            if (!levelCleard)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    enemyList[i].Update(player, gameTime);
                    enemyHealthBarList[i].UpdateBar(enemyList[i]);
                }
                for(int i = 0; i < enemyList.Count; i++)
                {
                    if (!enemyList[i].isAlive)
                    {
                        enemyList.Remove(enemyList[i]);
                    }
                }
            }
            if(enemyList.Count <= 0)
            {
                levelCleard = true;                
                exitMap.ExitMapUpdate(gameTime, player.position);
            }
            if(levelCleard && player.position.X >= screenWidth - player.texture.Width || player.position.X <= 0 && levelCleard || 
                player.position.Y >= screenHeight - player.texture.Height && levelCleard || player.position.Y <= 0 && levelCleard)
            {
                // game.boolOverWorld = true;
                game.boolRuinslevel = true;
                player.position.X = 1;
            }
            CheckForCollision();
            //Texten i början
            killAllEnemies.UpdateKillAll(gameTime);

            CheckCollisionBetweenEnemies();
            CheckEnemyWallCollision();
        }

        public void UpdateOverWorld(GameTime gameTime)
        {
            overWorld.UpdateOverWorld(gameTime);
        }

        public void CurrentLevel(string newLevel, Texture2D texture)
        {
            currentLevel = newLevel;
            level = new LevelManager(currentLevel, texture);
        }        

        private void CheckForCollision()
        {
            collision.CheckPlayerBounds(player, screenHeight, screenWidth);
            collision.CheckCollisionHorizontal(level, player);
            collision.CheckCollisionVertical(level, player);
            collision.CameraBoundCheck(player, camera);
        }

        public void DrawGameOver(SpriteBatch spriteBatch)
        {
            gameOver.Draw(spriteBatch);
        }

        public void DrawOverWorld(SpriteBatch spriteBatch)
        {
            overWorld.DrawOverWorld(spriteBatch);
        }

        public void CheckCollisionBetweenEnemies()
        {
            for(int i = 0; i < enemyList.Count - 1; i++)
            {
                for(int j = i + 1; j < enemyList.Count; j++)
                {
                    if(Vector2.Distance(enemyList[i].Position, enemyList[j].Position) < 50)
                    {
                        enemyList[i].EnemyTooClose(enemyList[i].Position - enemyList[j].Position);
                        enemyList[j].EnemyTooClose(enemyList[j].Position - enemyList[i].Position);
                    }
                }
            }
        }

        public void CheckEnemyWallCollision()
        {
            Vector2 tempWallPosition;
            Vector2 tempEnemyOrigin;
            foreach(Enemy e in enemyList)
            {
                tempEnemyOrigin = new Vector2(e.Position.X + 16, e.Position.Y + 32);
                foreach (Rectangle wall in level.hitBoxWall)
                {
                    tempWallPosition = new Vector2(wall.X + 16, wall.Y + 16);                   
                    if(Vector2.Distance(tempEnemyOrigin, tempWallPosition) < 17)
                    {
                        e.WallTooClose(e.Position - tempWallPosition);
                    }
                }
            }
        }
    }
}
