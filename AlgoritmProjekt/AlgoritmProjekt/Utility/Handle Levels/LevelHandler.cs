using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Utility.Handle_Levels;
using AlgoritmProjekt.Utility.Handle_Levels.Levels;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers
{
    class LevelHandler
    {
        Level level;
        int levelIndex;
        int maxLevels;
        int tileSize;

        Texture2D hollowSquare, smallHollowSquare, solidSquare, smoothTexture;
        Player player;

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
            CreateALevel();
        }

        public void Update(float time, Camera camera)
        {
            level.Update(time, camera);
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

        void CreateALevel()
        {
            switch (levelIndex)
            {
                case 0:
                    level = new Level("Level0.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize);
                    ++levelIndex;
                    break;
                case 1:
                    level = new Level1("Level1.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize);
                    ++levelIndex;
                    break;
                case 2:
                    level = new Level2("Level1.json", player, solidSquare, hollowSquare, smallHollowSquare, smoothTexture, tileSize);
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
