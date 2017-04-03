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
        LevelManager level;
        Rectangle sourceRect;
        Texture2D bulletTexture;
        int screenHeight, screenWidth;

        enum GameState {  MainWorld }
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
            player = new Player(Content.Load <Texture2D>(@"Elderly_Dragon_Head_Gray"), new Vector2(100, 100), sourceRect);
            enemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240), sourceRect);
            weapon = new Weapon(Content.Load<Texture2D>(@"Pistol"), new Vector2(100, 300), sourceRect);
            level = new LevelManager(Content);
            bulletTexture = Content.Load<Texture2D>("Bullet");
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

            switch (state)
            {
                case GameState.MainWorld:
                    
                    Updateworldmap(gameTime);
                    break;

            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);    

            switch (state)
            {
                case GameState.MainWorld:
                    Drawworldmap(gameTime);
                    break;
            }
            
            base.Draw(gameTime);
        }
        public void Updateworldmap(GameTime gameTime)
        {
            state = GameState.MainWorld;
        }
        public void Drawworldmap(GameTime gameTime)
        {
            spriteBatch.Begin();

            
            enemy.Draw(spriteBatch);
            level.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
            player.Draw(spriteBatch);


            spriteBatch.End();
        }
    }
}
