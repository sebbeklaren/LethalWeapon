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
        enum GameState { MainMenu, CityLevel, RuinsLevel, OverWorld }
        GameState state;    
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
   
        protected override void Initialize()
        {
            gameOn = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gamePlayManager = new GamePlayManager(graphics, Content, GraphicsDevice);
            mainMenu = new MainMenu(Content.Load<Texture2D>("MainMenuWall"), new Vector2(0,0));
            overWorld = new OverWorld(Content);
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
                case GameState.MainMenu:
                    DrawCurrentState(gameTime);
                    break;

                case GameState.CityLevel:
                    DrawCurrentState(gameTime);
                    break;

                case GameState.RuinsLevel:
                    DrawCurrentState(gameTime);
                    break;

                case GameState.OverWorld:
                    DrawCurrentState(gameTime);
                    break;

            }

            base.Draw(gameTime);
        }

        public void UpdateWorldMap()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                LoadCityLevel();
                gameOn = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                state = GameState.CityLevel;
                LoadCityLevel();
             
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                state = GameState.OverWorld;
                LoadOverWorld();
<<<<<<< HEAD
                
=======
                gameOn = false; 
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                state = GameState.MainMenu;
                //gameOn = false;
>>>>>>> origin/master
            }
        }

        public void DrawCurrentState(GameTime gameTime)
        {
          
            spriteBatch.Begin();
            mainMenu.DrawMainMenu(spriteBatch);
            spriteBatch.End();
            
            if (gameOn == true)
            {

                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gamePlayManager.camera.GetTransform());

                if (state == GameState.CityLevel)
                {
                    gamePlayManager.DrawCityLevel(spriteBatch);
                }

                else if (state == GameState.RuinsLevel)
                {
                    gamePlayManager.DrawRuinsLevel(spriteBatch);
                }

                else if (state == GameState.OverWorld)
                {
                    overWorld.DrawOverWorld(spriteBatch);
                }

                spriteBatch.End();
            }

           
        }
    }
}