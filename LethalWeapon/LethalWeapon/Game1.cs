using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

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
        public bool boolRuinslevel = false;
        public bool boolCityLevel = false;
        bool gameOn;
        enum GameState { MainMenu, CityLevel, RuinsLevel, OverWorld, GameOver }
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
            TextureManager.LoadTextures(Content);
            SoundManager.LoadSound(Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gamePlayManager = new GamePlayManager(graphics, GraphicsDevice, this);
            mainMenu = new MainMenu(TextureManager.MainMenuTexture, new Vector2(0,0));
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

        public void LoadOverWorld()
        {
            MediaPlayer.Stop();
            state = GameState.OverWorld;
            gamePlayManager.CurrentLevel("Content/Map/nullmap.txt", TextureManager.OverWorldtexture);
        }

        public void LoadCityLevel()
        {
            state = GameState.CityLevel;
            gamePlayManager.CurrentLevel("Content/Map/map01.txt", TextureManager.Tileset01Texture);
            MediaPlayer.Play(SoundManager.CityLevelBGM);
        }
        public void LoadRuinsLevel()
        {
            state = GameState.RuinsLevel;
            gamePlayManager.CurrentLevel("Content/Map/map02.txt", TextureManager.DesertTile);
            MediaPlayer.Play(SoundManager.BossLevelBGM);
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
                    gamePlayManager.UpdateOverWorld(gameTime);
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
                boolOverWorld = false;
                boolRuinslevel = false;
                LoadCityLevel();
                gameOn = true;
            }
            
            else if (Keyboard.GetState().IsKeyDown(Keys.I) || boolOverWorld)
            {
                state = GameState.OverWorld;
                boolRuinslevel = false;
                boolCityLevel = false;
                LoadOverWorld();
                gameOn = false; 
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) || boolRuinslevel)
            {
                state = GameState.RuinsLevel;
                boolOverWorld = false;
                boolCityLevel = false;
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
            else if(state == GameState.OverWorld)
            {
                gamePlayManager.DrawOverWorld(spriteBatch);
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
                spriteBatch.End();
            }

           
        }
    }
}