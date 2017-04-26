﻿using System;
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
            this.Content = Content;
            this.graphics = graphics;
            this.graphicsDevice = graphicsDevice;
            player = new Player(Content.Load<Texture2D>(@"HoodyBoy"), new Vector2(32, 32), sourceRect, Content);
            enemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240), sourceRect);
            enemyHealthBar = new Bar(Content, (int)enemy.EnemyMaxHealth, 0);
            weapon = new Weapon(Content.Load<Texture2D>(@"PlaceHolderUzi"), new Vector2(100, 300), sourceRect, Content);
            bullet = new Bullet(Content.Load<Texture2D>(@"Bullet"));
            gui = new Gui(Content, 1, 1);

            screenHeight = 32 * 24;
            screenWidth = 32 * 32;

            Viewport view = graphicsDevice.Viewport;
            camera = new Camera(view);
            cameraOffset = new Vector2(35, 65);            
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            enemy.Update(player);
            enemyHealthBar.UpdateBar(enemy);
            weapon.Update(player, enemy);
            camera.SetPosition(player.Position - cameraOffset);
            //level.Update(player);
            camera.ZoomX = 1.7f;
            camera.ZoomY = 2.0f;
            camera.Rotation = 0f;
            gui.Update(camera.GetPosition(), player, gameTime);

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.GetTransform());
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
