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
 //v1.0
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;
        MainMenu mainMenu;
        GamePlayManager gamePlayManager;
        public bool boolOverWorld = false;
        public bool boolRuinslevel = false;
        public bool boolCityLevel = false;
        bool gameOn;

       public enum MusicState {  NotPlaying, Playing }
        public MusicState musicState;

       public enum GameState { MainMenu, CityLevel, RuinsLevel, OverWorld, GameOver }
        public GameState currentGameState, lastGameState;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
   
        protected override void Initialize()
        {
            gameOn = false;
            currentGameState = GameState.MainMenu;
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
            currentGameState = GameState.OverWorld;
            gamePlayManager.CurrentLevel("Content/Map/nullmap.txt", TextureManager.OverWorldtexture);
        }

        public void LoadCityLevel()
        {
            currentGameState = GameState.CityLevel;
            gamePlayManager.CurrentLevel("Content/Map/map01.txt", TextureManager.Tileset01Texture);
        }
        public void LoadRuinsLevel()
        {
            currentGameState = GameState.RuinsLevel;
            gamePlayManager.CurrentLevel("Content/Map/map02.txt", TextureManager.DesertTile);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);
            input.Update();
            switch (currentGameState)
            {
                case GameState.CityLevel:
                    gamePlayManager.UpdateCityLevel(gameTime);
                    UpdateWorldMap();
                    if (musicState == MusicState.Playing && currentGameState != lastGameState)
                    {
                        MediaPlayer.Stop();
                        musicState = MusicState.NotPlaying;
                    }
                    if (musicState == MusicState.NotPlaying)
                    {
                        MediaPlayer.Play(SoundManager.CityLevelBGM);
                        musicState = MusicState.Playing;
                    }
                    lastGameState = currentGameState;
                    break;

                case GameState.RuinsLevel:
                    gamePlayManager.UpdateRuinsLevel(gameTime);   
                    UpdateWorldMap();
                    if (musicState == MusicState.Playing && currentGameState != lastGameState)
                    {
                        MediaPlayer.Stop();
                        musicState = MusicState.NotPlaying;
                    }
                    if (musicState == MusicState.NotPlaying)
                    {
                        MediaPlayer.Play(SoundManager.BossLevelBGM);
                        musicState = MusicState.Playing;
                    }
                    lastGameState = currentGameState;
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
                currentGameState = GameState.GameOver;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            switch (currentGameState)
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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || boolCityLevel || input.startIsPressed)
            {
                currentGameState = GameState.CityLevel;
                boolOverWorld = false;
                boolRuinslevel = false;
                LoadCityLevel();
                gameOn = true;
            }           
            else if (Keyboard.GetState().IsKeyDown(Keys.I) || boolOverWorld)
            {
                currentGameState = GameState.OverWorld;
                boolRuinslevel = false;
                boolCityLevel = false;
                LoadOverWorld();
                gameOn = false; 
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) || boolRuinslevel)
            {
                currentGameState = GameState.RuinsLevel;
                boolOverWorld = false;
                boolCityLevel = false;
                LoadRuinsLevel();
                gameOn = true;
            }
                
        }

        public void DrawCurrentState(GameTime gameTime)
        {

            spriteBatch.Begin();
            if (currentGameState == GameState.MainMenu)
            {
                mainMenu.DrawMainMenu(spriteBatch);
            }
            else if(currentGameState == GameState.GameOver)
            {
                gamePlayManager.DrawGameOver(spriteBatch);
            }
            else if(currentGameState == GameState.OverWorld)
            {
                gamePlayManager.DrawOverWorld(spriteBatch);
            }
            else if(gameOn == false)
            {
                    gamePlayManager.camera.ZoomX = 1f;
                    gamePlayManager.camera.ZoomY = 1f;
            }
            spriteBatch.End();

            if (gameOn == true)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gamePlayManager.camera.GetTransform());

                if (currentGameState == GameState.CityLevel)
                {
                    gamePlayManager.DrawCityLevel(spriteBatch);
                    gamePlayManager.camera.ZoomX = 1.7f;
                    gamePlayManager.camera.ZoomY = 2.0f;
                }

                else if (currentGameState == GameState.RuinsLevel)
                {
                    gamePlayManager.DrawRuinsLevel(spriteBatch);
                    gamePlayManager.camera.ZoomX = 1.7f;
                    gamePlayManager.camera.ZoomY = 2.0f;
                }
               
                spriteBatch.End();
            }
        }
    }
}