using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LethalWeapon
{
 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Enemy enemy;
        Weapon weapon;
        Bullet bullet;
        LevelManager level;
        Rectangle sourceRect;
        Texture2D bulletTexture;
        int screenHeight, screenWidth;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
   
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            screenHeight = 32 * 24;
            screenWidth = 32 * 32;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load <Texture2D>(@"HoodyBoy"), new Vector2(100, 100), sourceRect);
            enemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240), sourceRect);
            weapon = new Weapon(Content.Load<Texture2D>(@"Pistol"), new Vector2(100, 300), sourceRect);
            bullet = new Bullet(Content.Load<Texture2D>(@"Bullet"), new Vector2(0, 0));
            level = new LevelManager(Content);
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.ApplyChanges();

            if (!graphics.IsFullScreen)
            {
                graphics.ToggleFullScreen();
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            enemy.Update(player);
            weapon.Update(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);        
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);
            bullet.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}