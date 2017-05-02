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
        Enemy enemy;
        Bar enemyHealthBar;
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
            enemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240), sourceRect);
            enemyHealthBar = new Bar(Content, (int)enemy.EnemyMaxHealth, 0);
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
            player.CheckCollision(level);
            player.Update(gameTime, enemy);
            enemy.Update(player);
            enemyHealthBar.UpdateBar(enemy);
            weapon.Update(player, enemy, bullet, gui, gameTime);
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
            enemy.Draw(spriteBatch);
            enemyHealthBar.Draw(spriteBatch);
            gui.Draw(spriteBatch);
            
        }
        public void CurrentLevel(string newLevel)
        {
            currentLevel = newLevel;
            level = new LevelManager(Content, currentLevel);
        }



    }
}
