using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LethalWeapon
{
    class GamePlayManager
    {
        Player player;
        List<Enemy> enemyList = new List<Enemy>();
        Enemy tempEnemy;
        List<Bar> enemyHealthBarList = new List<Bar>();
        Bar tempEnemyHealthBar;
        Weapon weapon;
        Gui gui;
        Bullet bullet;
        ContentManager Content;
        Rectangle sourceRect;
        LevelManager level;
        string currentLevel;
        public Camera camera;
        Vector2 cameraOffset;
        int screenHeight, screenWidth;
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;
 

        public GamePlayManager(GraphicsDeviceManager graphics, ContentManager Content, GraphicsDevice graphicsDevice)
        {
            screenHeight = 32 * 24;
            screenWidth = 32 * 32;
            this.Content = Content;
            this.graphics = graphics;
            this.graphicsDevice = graphicsDevice;
            player = new Player(Content.Load<Texture2D>(@"HoodyBoy"), new Vector2(300, 530), sourceRect, Content, screenWidth, screenHeight);
            for (int i = 0; i < 3; i++)
            {
                tempEnemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240 + 50 * i), sourceRect);
                enemyList.Add(tempEnemy);
                tempEnemyHealthBar = new Bar(Content, (int)tempEnemy.EnemyMaxHealth, 0);
                enemyHealthBarList.Add(tempEnemyHealthBar);
            }
            weapon = new Weapon(Content.Load<Texture2D>(@"PlaceHolderUzi"), new Vector2(100, 300), sourceRect, Content);
            bullet = new Bullet(Content.Load<Texture2D>(@"Bullet"));
            gui = new Gui(Content, 1, 1);

 

            Viewport view = graphicsDevice.Viewport;
            camera = new Camera(view);
            cameraOffset = new Vector2(35, 65);            
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
        }

        public void Update(GameTime gameTime)
        {
<<<<<<< HEAD
            foreach (Rectangle wall in level.hitBoxWall)
            {
                if (player.checkRec.Intersects(wall))
                { 
                player.CheckCollision(level);
                    }
            }
            player.Update(gameTime, enemy);
            enemy.Update(player);
            enemyHealthBar.UpdateBar(enemy);
            weapon.Update(player, enemy, bullet, gui);
=======
            player.CheckCollision(level);

            player.Update(gameTime, tempEnemy);
            for(int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update(player);
                enemyHealthBarList[i].UpdateBar(enemyList[i]);
            }
            weapon.Update(player, enemyList, bullet, gui, gameTime);

>>>>>>> origin/master
            camera.ZoomX = 1.7f;
            camera.ZoomY = 2.0f;
            camera.Rotation = 0f;
            gui.Update(camera.GetPosition(), player, gameTime);
            int inputCameraMultiplier = 10;   
            camera.SetPosition(new Vector2((player.Position.X - cameraOffset.X) - player.input.position.X  * inputCameraMultiplier, 
                                           (player.Position.Y - cameraOffset.Y) - player.input.position.Y * inputCameraMultiplier));
            
        }

        public void Draw(SpriteBatch spriteBatch)
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
            
        }
        public void CurrentLevel(string newLevel)
        {
            currentLevel = newLevel;
            level = new LevelManager(Content, currentLevel);
        }



    }
}
