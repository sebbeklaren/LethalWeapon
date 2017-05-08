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
        Texture2D overWorldTex;
        Enemy tempEnemy;
        List<Bar> enemyHealthBarList = new List<Bar>();
        Bar tempEnemyHealthBar;
        Weapon weapon;
        Gui gui;
        Bullet bullet;
        BossOne bossOne;
        ContentManager Content;
        Rectangle sourceRect;
        LevelManager level;
        string currentLevel;
        public Camera camera;
        Vector2 cameraOffset;
        int screenHeight, screenWidth;
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;
        CollisionDetection collision;
        Texture2D craterText;


        public GamePlayManager(GraphicsDeviceManager graphics, ContentManager Content, GraphicsDevice graphicsDevice)
        {
            screenHeight = 32 * 24;
            screenWidth = 32 * 32;
            this.Content = Content;
            this.graphics = graphics;
            this.graphicsDevice = graphicsDevice;
            craterText = Content.Load<Texture2D>(@"DesertBackground01");
            player = new Player(Content.Load<Texture2D>(@"HoodyBoy"), new Vector2(250, 540), sourceRect, Content, screenWidth, screenHeight);
            for (int i = 0; i < 3; i++)
            {
                tempEnemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240 + 50 * i), sourceRect);
                enemyList.Add(tempEnemy);
                tempEnemyHealthBar = new Bar(Content, (int)tempEnemy.EnemyMaxHealth, 0);
                enemyHealthBarList.Add(tempEnemyHealthBar);
            }
            weapon = new Weapon(Content.Load<Texture2D>(@"PlaceHolderUzi"), new Vector2(100, 300), sourceRect, Content);
            bullet = new Bullet(Content.Load<Texture2D>(@"Bullet"));
            bossOne = new BossOne(Content.Load<Texture2D>(@"BossOne"), new Vector2(500, 300), sourceRect, Content, screenWidth, screenHeight);           
            gui = new Gui(Content, 1, 1);
            overWorldTex = Content.Load<Texture2D>(@"overworldmap");
            collision = new CollisionDetection();
            Viewport view = graphicsDevice.Viewport;
            camera = new Camera(view);
            cameraOffset = new Vector2(35, 65);            
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            camera.ZoomX = 1.7f;
            camera.ZoomY = 2.0f;
            camera.Rotation = 0f;
        }

        public void Update(GameTime gameTime)
        {
            collision.CheckBounds(player, screenHeight, screenWidth);
            collision.CheckCollisionHorizontal(level, player);
            collision.CheckCollisionVertical(level, player);
            collision.CameraBoundCheck(player, camera);
            weapon.Update(player, enemyList, bullet, gui, gameTime);
            player.Update(gameTime, tempEnemy);
            gui.Update(camera.GetPosition(), player, gameTime);
            bossOne.Update(player, gameTime);
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update(player);
                enemyHealthBarList[i].UpdateBar(enemyList[i]);
            }
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
        }

        public void DrawRuinsLevel(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
            spriteBatch.Draw(craterText, new Vector2(100, 0), Color.White);
            bossOne.Draw(spriteBatch);
            player.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
            gui.Draw(spriteBatch);
        }

        public void CurrentLevel(string newLevel, Texture2D texture)
        {
            currentLevel = newLevel;
            level = new LevelManager(Content, currentLevel, texture);
        }        
    }
}
