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
        public bool boolOverWorld = false;
<<<<<<< HEAD
        public bool boolRuinsLevel = false;
        public bool boolCityLevel = false;
        bool gameOn;
        enum GameState { MainMenu, CityLevel, RuinsLevel, OverWorld }
        Game1 game;
=======
        public bool boolRuinslevel = false;
        bool gameOn;
        enum GameState { MainMenu, CityLevel, RuinsLevel, OverWorld, GameOver }
>>>>>>> origin/master
        GameState state;    
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
   
        protected override void Initialize()
        {
            gameOn = false;
            state = GameState.MainMenu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gamePlayManager = new GamePlayManager(graphics, Content, GraphicsDevice, this);
            mainMenu = new MainMenu(Content.Load<Texture2D>("Textures/TemporaryTextures/MainMenuWall"), new Vector2(0,0));
            overWorld = new OverWorld(Content, game);
            graphics.ApplyChanges();
            input = new InputManager();
        //    gamePlayManager.CurrentLevel("MapTextFiles/map01.txt", Content.Load<Texture2D>(@"Textures/Tilesets/LethalWeapon_RoadsAndWalls"));
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
            gamePlayManager.CurrentLevel("Content/Map/nullmap.txt", Content.Load<Texture2D>(@"Textures/TemporaryTextures/overworldmap"));
        }

        protected void LoadCityLevel()
        {
            state = GameState.CityLevel;
            gamePlayManager.CurrentLevel("Content/Map/map01.txt", Content.Load<Texture2D>(@"Textures/Tilesets/LethalWeapon_RoadsAndWalls"));
        }
        protected void LoadRuinsLevel()
        {
            state = GameState.RuinsLevel;
            gamePlayManager.CurrentLevel("Content/Mapmap02.txt", Content.Load<Texture2D>(@"Textures/Tilesets/DesertTile"));
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);
            
            switch (state)
            {
                case GameState.CityLevel:
                    gamePlayManager.UpdateCityLevel(gameTime);
                    UpdateWorldMap();   
                    break;

                case GameState.RuinsLevel:
                    gamePlayManager.UpdateRuinsLevel(gameTime);   
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

            if (gamePlayManager.isGameOver)
                state = GameState.GameOver;

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

                case GameState.GameOver:
                    DrawCurrentState(gameTime);
                    break;

            }

            base.Draw(gameTime);
        }

        public void UpdateWorldMap()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || boolCityLevel)
            {
                state = GameState.CityLevel;
                LoadCityLevel();
                gameOn = true;
            }
            
            else if (Keyboard.GetState().IsKeyDown(Keys.I) || boolOverWorld)
            {
                state = GameState.OverWorld;
                LoadOverWorld();
                gameOn = true; 
            }
<<<<<<< HEAD
            else if (Keyboard.GetState().IsKeyDown(Keys.P) || boolRuinsLevel)
=======
            else if (Keyboard.GetState().IsKeyDown(Keys.P) || boolRuinslevel)
>>>>>>> origin/master
            {
                state = GameState.RuinsLevel;
                LoadRuinsLevel();
                gameOn = true;
            }
        }

        public void DrawCurrentState(GameTime gameTime)
        {

            spriteBatch.Begin();
            if (state == GameState.MainMenu)
            {
                
                mainMenu.DrawMainMenu(spriteBatch);
                
            }
            else if(state == GameState.GameOver)
            {
                gamePlayManager.DrawGameOver(spriteBatch);
            }
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

//Fixa movement till player på Overworld