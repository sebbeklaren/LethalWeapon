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
        Camera camera;
        Vector2 cameraOffset;
        int screenHeight, screenWidth;
        string currentLevel;

        enum GameState {  CityLevel, RuinsLevel }
        GameState state;

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
            Viewport view = GraphicsDevice.Viewport;
            camera = new Camera(view);
            cameraOffset = new Vector2(100, 100);
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.ApplyChanges();

            //if (!graphics.IsFullScreen)
            //{
            //    graphics.ToggleFullScreen();
            //}
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

            //Kamera funktioner
            camera.SetPosition(player.Position - cameraOffset);
            camera.Zoom = 2.0f;
            camera.Rotation = 0f;

            switch (state)
            {
                case GameState.CityLevel:
                    CurrentLevel("Content/Map/map01.txt");
                    UpdateWorldMap(gameTime);
                    break;

                case GameState.RuinsLevel:                    
                    CurrentLevel("Content/Map/map02.txt");
                    UpdateWorldMap(gameTime);
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);    

            switch (state)
            {
                case GameState.CityLevel:
                    
                    DrawWorldMap(gameTime);
                    break;
                case GameState.RuinsLevel:

                    DrawWorldMap(gameTime);
                    break;
            }
            
            base.Draw(gameTime);
        }
        public void UpdateWorldMap(GameTime gameTime)
        {
            state = GameState.RuinsLevel;

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                state = GameState.CityLevel;
            }

        }
        public void DrawWorldMap(GameTime gameTime)
        {
            // spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.GetTransform());
            level.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
            player.Draw(spriteBatch);            
            enemy.Draw(spriteBatch);            
            bullet.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void CurrentLevel(string newLevel)
        {
            currentLevel = newLevel;

            level = new LevelManager(Content, currentLevel);
        }
    }
}