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
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;
        MainMenu mainMenu;
        GamePlayManager gamePlayManager;
        OverWorld overWorld;

        bool gameOn;
        enum GameState {  CityLevel, RuinsLevel, MainMenu, OverWorld }
        GameState state;    
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
   
        protected override void Initialize()
        {
            gameOn = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gamePlayManager = new GamePlayManager(graphics, Content, GraphicsDevice);
            overWorld = new OverWorld(Content);
           // mainMenu = new MainMenu()
            graphics.ApplyChanges();
            input = new InputManager();
            gamePlayManager.CurrentLevel("Content/Map/map01.txt", Content.Load<Texture2D>(@"Tileset01"));
            //if (!graphics.IsFullScreen)
            //{
            //    graphics.ToggleFullScreen();
            //}
        }

        protected override void UnloadContent()
        {
            
        }

        protected void LoadOverWorld()
        {
            state = GameState.OverWorld;
            gamePlayManager.CurrentLevel("Content/Map/nullmap.txt", Content.Load<Texture2D>(@"overworldmap"));
        }

        protected void LoadCityLevel()
        {
            state = GameState.CityLevel;
            gamePlayManager.CurrentLevel("Content/Map/map01.txt", Content.Load<Texture2D>(@"Tileset01"));
        }
        protected void LoadRuinsLevel()
        {
            state = GameState.RuinsLevel;
            gamePlayManager.CurrentLevel("Content/Map/map02.txt", Content.Load<Texture2D>(@"DesertTile"));
            
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);
            
            switch (state)
            {
                case GameState.CityLevel:
                    UpdateWorldMap();   
                    break;

                case GameState.RuinsLevel:   
                    UpdateWorldMap();
                    break;

                case GameState.MainMenu:
                    UpdateWorldMap();
                    break;

                case GameState.OverWorld:
                    UpdateWorldMap();
                    break;
            }
            gamePlayManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            switch (state)
            {
                case GameState.CityLevel:
                    DrawCurrentState(gameTime);
                    break;

                case GameState.RuinsLevel:
                    DrawCurrentState(gameTime);
                    break;

                case GameState.OverWorld:
                    DrawCurrentState(gameTime);
                    break;

                case GameState.MainMenu:
                    gameOn = false;
                    DrawCurrentState(gameTime);
                    break;
            }

            base.Draw(gameTime);
        }

        public void UpdateWorldMap()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                LoadRuinsLevel();
                gameOn = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                LoadCityLevel();
                gameOn = true;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                LoadOverWorld();
                gameOn = false;
            }
        }

        public void DrawCurrentState(GameTime gameTime)
        {

            if (gameOn == true)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gamePlayManager.camera.GetTransform());
            }

            if (state == GameState.MainMenu)
            {

            }

            else if (state == GameState.CityLevel)
            {
                
                gamePlayManager.DrawCityLevel(spriteBatch);
            }

            else if (state == GameState.RuinsLevel)
            {
                gamePlayManager.DrawRuinsLevel(spriteBatch);
            }

            else if (state == GameState.OverWorld)
            {
                spriteBatch.Begin();
                overWorld.DrawOverWorld(spriteBatch);
            }


            spriteBatch.End();
        }
    }
}