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
        Weapon weapon;
        Gui gui;
        Bullet bullet;
        ContentManager Content;
        Rectangle sourceRect;
        Rectangle hitBox;
        public Camera camera;
        Vector2 cameraOffset;
        int screenHeight, screenWidth;
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;
        public LevelManager level;
        string currentLevel;


        public GamePlayManager(GraphicsDeviceManager graphics, ContentManager Content, GraphicsDevice graphicsDevice)
        {
            this.Content = Content;
            this.graphics = graphics;
            this.graphicsDevice = graphicsDevice;
            player = new Player(Content.Load<Texture2D>(@"HoodyBoy"), new Vector2(32, 32), sourceRect,Content, this);
            enemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240), sourceRect);
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
            Colliding();
            player.Update(gameTime);
            enemy.Update(player);
            weapon.Update(player, enemy);
            camera.SetPosition(player.Position - cameraOffset);

            camera.ZoomX = 1.7f;
            camera.ZoomY = 2.0f;
            camera.Rotation = 0f;
            gui.Update(camera.GetPosition(), player);

            //if(level.IsWall())
            //{
            //    Console.Write("Wall");
            //}

            
        }

        public void Colliding()
        {
            for (int i = 0; i < level.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < level.tiles.GetLength(1); j++)
                {
                    if (player.IsCollidingTop(player.playerHitbox, level.tiles[i, j].hitBox))
                    {
                        Console.Write("fakking hell!!");
                        player.HandleCollision(level.tiles[i, j]);
                    }
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.GetTransform());
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            gui.Draw(spriteBatch);

        }

        public void CurrentLevel(string newLevel)
        {
            currentLevel = newLevel;
            level = new LevelManager(Content, currentLevel);
        }

    }
}
