﻿using Microsoft.Xna.Framework;
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
<<<<<<< HEAD
        bool gameOn;
        enum GameState {  CityLevel, RuinsLevel, MainMenu }
=======
        enum GameState {  CityLevel, RuinsLevel, MainMenu, OverWorld }
>>>>>>> origin/master
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
           // mainMenu = new MainMenu()
            graphics.ApplyChanges();
            input = new InputManager();
            gamePlayManager.CurrentLevel("Content/Map/map01.txt");
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
            
            base.Update(gameTime);
            
            switch (state)
            {
                case GameState.CityLevel:
                   gamePlayManager.CurrentLevel("Content/Map/map01.txt");
                    
                    UpdateWorldMap(gameTime);   
                    break;

                case GameState.RuinsLevel:                    
                    gamePlayManager.CurrentLevel("Content/Map/map02.txt");
                    UpdateWorldMap(gameTime);
                    break;

                case GameState.MainMenu:
                    // Test för mainmenu, världen ska vara en variabel
                    UpdateWorldMap(gameTime);
                    break;

                case GameState.OverWorld:
                    gamePlayManager.CurrentLevel("Content/Map/nullmap.txt");
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
<<<<<<< HEAD
                    DrawWorldMap(gameTime);
=======
                    
                    DrawCurrentState(gameTime);
>>>>>>> origin/master
                    break;

<<<<<<< HEAD
                case GameState.RuinsLevel:
                    DrawWorldMap(gameTime);
                    break;

                case GameState.MainMenu:
                    gameOn = false;
                    DrawWorldMap(gameTime);
=======
                    DrawCurrentState(gameTime);
                    break;

                case GameState.MainMenu:

                    DrawCurrentState(gameTime);
>>>>>>> origin/master
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
<<<<<<< HEAD
          
=======
            else if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                state = GameState.OverWorld;
            }
>>>>>>> origin/master
        }

        public void DrawCurrentState(GameTime gameTime)
        {
<<<<<<< HEAD
            if (gameOn == true)
            {

                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gamePlayManager.camera.GetTransform());

                gamePlayManager.Draw(spriteBatch);
            }

=======
>>>>>>> origin/master
            if (state == GameState.MainMenu)
            {

            }

            else if (state == GameState.CityLevel)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gamePlayManager.camera.GetTransform());
                gamePlayManager.DrawCityLevel(spriteBatch);
            }

            else if (state == GameState.RuinsLevel)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, gamePlayManager.camera.GetTransform());
                gamePlayManager.DrawRuinsLevel(spriteBatch);
            }

            else if (state == GameState.OverWorld)
            {
                spriteBatch.Begin();
                gamePlayManager.DrawOverWorld(spriteBatch);
            }


            spriteBatch.End();
        }

        //public void CurrentLevel(string newLevel)
        //{           
        //    currentLevel = newLevel;
        //    level = new LevelManager(Content, currentLevel);
        //}
    }
}