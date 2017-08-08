using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Utility;
using AlgoritmProjekt.Utility.Handle_Levels;
using AlgoritmProjekt.Utility.Handle_Levels.PCG;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers
{
    class LevelHandler
    {
        int levelIndex;
        int maxLevels;

        List<Level> levels = new List<Level>();
        public Level CurrentLevel;
        PCGEngine levelEngine;
        Player player;

        public LevelHandler(Player player, int maxLevels)
        {
            this.player = player;
            this.maxLevels = maxLevels;
            levelIndex = 0;
            levelEngine = new PCGEngine(this);
            levels.Add(new Level(player, new TileGrid(Constants.tileSize, Constants.tileSize, 50, 50)));
            IncrementLevel();
        }

        public void Update(float time)
        {
            CurrentLevel.Update(time);

            ChangeLevel();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(spriteBatch);
        }

        public void AddLevel(Level level)
        {
            levels.Add(level);
        }

        public bool Winner()
        {
            if (levelIndex > maxLevels)
                return true;
            return false;
        }

        public bool GameOver()
        {
            if (CurrentLevel.LoseCondition())
                return true;
            return false;
        }

        void IncrementLevel()
        {
            CurrentLevel = levels[levelIndex];
            player.ResetMovement(CurrentLevel.NavigationMesh, CurrentLevel.StartPosition);
            ++levelIndex;
        }

        void ChangeLevel()
        {
            if (CurrentLevel.WinCondition())
            {
                IncrementLevel();
            }
        }
    }
}
