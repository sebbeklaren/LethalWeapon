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
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load <Texture2D>(@"Elderly_Dragon_Head_Gray"), new Vector2(100, 100), sourceRect);
            enemy = new Enemy(Content.Load<Texture2D>(@"Cyclop"), new Vector2(400, 240), sourceRect);
            weapon = new Weapon(Content.Load<Texture2D>(@"Pistol"), new Vector2(100, 300), sourceRect);
            level = new LevelManager(Content);
            bulletTexture = Content.Load<Texture2D>("Bullet");
            IsMouseVisible = true;

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
            GraphicsDevice.Clear(Color.LightGray);        
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);         
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
