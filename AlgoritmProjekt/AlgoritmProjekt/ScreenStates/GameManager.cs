using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.ParticleEngine.Emitters;
using AlgoritmProjekt.Utility;
using AlgoritmProjekt.Utility.Handle_Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers
{
    class GameManager
    {
        string lifeFont = "Life: ", 
            energyFont = "Energy: ", 
            gameOverFont = "Game Over";

        int screenWidth, screenHeight;
        int tileSize;
        float TotalTime;

        Camera camera;
        Player player;
        LevelHandler levels;

        int playerHP = Constants.PlayerStartHP,
            playerSpeed = Constants.PlayerStartSpeed,
            maxLevels = Constants.MaxLevels;

        public bool Winner()
        {
            return levels.Winner();
        }

        public bool GameOver()
        {
            return levels.GameOver();
        }

        public GameManager(int screenWidth, int screenHeight, int tileSize)
        {
            camera = new Camera(new Rectangle(0, 0, screenWidth / 2, screenHeight / 2), new Rectangle(0, 0, screenWidth * 4, screenHeight * 4));
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.tileSize = tileSize;
            player = new Player(Vector2.Zero, tileSize, playerHP, playerSpeed);
            levels = new LevelHandler(player, maxLevels);
        }

        public void Update(float time)
        {
            TotalTime += time;
            camera.Update(player.myPosition);
            player.Update(time, levels.CurrentLevel);
            levels.Update(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            levels.Draw(spriteBatch);
            player.Draw(spriteBatch);
            DrawFonts(spriteBatch);
            spriteBatch.End();
        }

        private void DrawFonts(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.defaultFont, lifeFont + player.myHP, new Vector2(-camera.CameraPos.X, TextureManager.defaultFont.MeasureString(lifeFont).Y / 4 - camera.CameraPos.Y), Color.LimeGreen);
            spriteBatch.DrawString(TextureManager.defaultFont, energyFont + (int)player.energy, new Vector2(-camera.CameraPos.X, TextureManager.defaultFont.MeasureString(energyFont).Y - camera.CameraPos.Y), Color.LimeGreen);
            if (levels.GameOver())
                spriteBatch.DrawString(TextureManager.defaultFont, gameOverFont, new Vector2(screenWidth / 2 - camera.CameraPos.X, screenHeight * 0.25f - camera.CameraPos.Y), Color.Red, 0, new Vector2(TextureManager.defaultFont.MeasureString(gameOverFont).X / 2, TextureManager.defaultFont.MeasureString(gameOverFont).Y / 2), 1.5f, SpriteEffects.None, 0);
        }
    }
}
