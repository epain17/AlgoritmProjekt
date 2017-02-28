using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Utility.Handle_Levels;
using AlgoritmProjekt.Utility.Handle_Levels.Levels;
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
        Level level, level1, level2, level3;
        int levelIndex;
        int maxLevels;
        int tileSize;

        Texture2D hollowSquare, smallHollowSquare, solidSquare, smoothTexture;
        Player player;

        int levelManually;

        public TileGrid GetGrid()
        {
            return level.GetGrid();
        }

        public LevelHandler(Player player, int tileSize, Texture2D hollow, Texture2D smallHollow, Texture2D solid, Texture2D smooth)
        {
            this.player = player;
            this.tileSize = tileSize;
            this.hollowSquare = hollow;
            this.smallHollowSquare = smallHollow;
            this.solidSquare = solid;
            this.smoothTexture = smooth;
            maxLevels = 10;
            levelIndex = 0;
            InitializeLevels();
            CreateALevel();
        }

        void InitializeLevels()
        {
            level = new Level("Level0.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize);
            level1 = new Level1("Level1.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize);
            level2 = new Level2("Level1.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize);
            level3 = new Level3("Level3.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize);
        }

        public void Update(float time)
        {
            ChangeLevelManually();
            level.Update(time);
            ChangeLevel();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
        }

        public bool Winner()
        {
            if (levelIndex == maxLevels)
                return true;
            return false;
        }

        public bool GameOver()
        {
            if (level.LoseCondition())
                return true;
            return false;
        }

        void ChangeLevelManually()
        {
            if (KeyMouseReader.KeyPressed(Keys.D1))
            {
                levelIndex = 1;
                level = level1;
            }
            else if (KeyMouseReader.KeyPressed(Keys.D2))
            {
                levelIndex = 2;
                level = level2;
            }
            else if (KeyMouseReader.KeyPressed(Keys.D3))
            {
                levelIndex = 3;
                level = level3;
            }
        }

        void CreateALevel()
        {
            switch (levelIndex)
            {
                case 0:
                    level = new Level("Level0.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize); 
                    ++levelIndex;
                    break;
                case 1:
                    level = level1;
                    ++levelIndex;
                    break;
                case 2:
                    level = level2;
                    ++levelIndex;
                    break;
                case 3:
                    level = level3;
                    ++levelIndex;
                    break;
            }
        }

        void ChangeLevel()
        {
            if (level.WinCondition())
            {
                CreateALevel();
                player.ResetMovement(level.GetGrid());
            }
        }
    }
}
