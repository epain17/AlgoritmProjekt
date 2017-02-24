using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.Projectiles;
using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.ParticleEngine.Emitters;
using AlgoritmProjekt.Utility;
using AlgoritmProjekt.Utility.Handle_Levels;
using AlgoritmProjekt.Utility.Handle_Levels.Levels;
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
        Texture2D hollowSquare, smallHollowSquare, solidSquare, smoothTexture;
        Player player;
        Camera camera;

        int tileSize = 32;
        string scoreFont = "Score: ", lifeFont = "Life: ",
            energyFont = "Energy: ", gameOverFont = "Game Over", winFont = "Level Completed";

        SpriteFont font;
        List<Emitter> emitters = new List<Emitter>();
        float TotalTime;

        int screenWidth, screenHeight;
        float globalTime;
        LevelHandler levels;

        public bool Winner()
        {
            return levels.Winner();
        }

        public bool GameOver()
        {
            return levels.GameOver();
        }

        public GameManager(int screenWidth, int screenHeight, int tileSize, SpriteFont font,
            Texture2D solidSquare, Texture2D hollowSquare, Texture2D smallHollowSquare, Texture2D neon)
        {
            camera = new Camera(new Rectangle(0, 0, screenWidth / 2, screenHeight / 2), new Rectangle(0, 0, screenWidth * 4, screenHeight * 4));
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.tileSize = tileSize;
            this.font = font;
            this.solidSquare = solidSquare;
            this.hollowSquare = hollowSquare;
            this.smallHollowSquare = smallHollowSquare;
            this.smoothTexture = neon;
            player = new Player(solidSquare, hollowSquare, smallHollowSquare, Vector2.Zero, tileSize);
            levels = new LevelHandler(player, tileSize, hollowSquare, smallHollowSquare, solidSquare, smoothTexture);
        }

        public void Update(GameTime gameTime)
        {
            globalTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TotalTime += globalTime;
            camera.Update(player.myPosition);
            player.Update(ref globalTime, levels.GetGrid());
            levels.Update(globalTime);
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
            //spriteBatch.DrawString(font, scoreFont + score, new Vector2(-camera.CameraPos.X, -camera.CameraPos.Y), Color.LimeGreen);
            //spriteBatch.DrawString(font, timeFont + (int)TotalTime, new Vector2((screenWidth / 2) - (font.MeasureString(timeFont).X / 2) - camera.CameraPos.X, -camera.CameraPos.Y), Color.LimeGreen);
            spriteBatch.DrawString(font, lifeFont + player.myHP, new Vector2(-camera.CameraPos.X, screenHeight - font.MeasureString(lifeFont).Y - camera.CameraPos.Y), Color.LimeGreen);
            spriteBatch.DrawString(font, energyFont + (int)player.EnergyMeter, new Vector2(screenWidth - (font.MeasureString(energyFont).X + (tileSize * 3)) - camera.CameraPos.X, screenHeight - font.MeasureString(energyFont).Y - camera.CameraPos.Y), Color.LimeGreen);
            if (levels.GameOver())
                spriteBatch.DrawString(font, gameOverFont, new Vector2(screenWidth / 2 - camera.CameraPos.X, screenHeight * 0.25f - camera.CameraPos.Y), Color.Red, 0, new Vector2(font.MeasureString(gameOverFont).X / 2, font.MeasureString(gameOverFont).Y / 2), 1.5f, SpriteEffects.None, 0);
        }
    }
}
