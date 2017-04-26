using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LethalWeapon
{
 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        LevelManager level;
        InputManager input;
        string currentLevel;
        GamePlayManager gamePlayManager;
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
          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gamePlayManager = new GamePlayManager(graphics, Content, GraphicsDevice);
            graphics.ApplyChanges();
            input = new InputManager();

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
            gamePlayManager.Update(gameTime);
            base.Update(gameTime);

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
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                state = GameState.RuinsLevel;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                state = GameState.CityLevel;
            }
        }

        public void DrawWorldMap(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gamePlayManager.camera.GetTransform());
            level.Draw(spriteBatch);
            gamePlayManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void CurrentLevel(string newLevel)
        {           
            currentLevel = newLevel;
            level = new LevelManager(Content, currentLevel);
        }
    }
}