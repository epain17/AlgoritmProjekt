using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Managers;
using AlgoritmProjekt.Objects.GameObjects.StaticObjects.Environment;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Handle_Levels.PCG
{
    class LevelGenerator
    {
        LevelHandler levels;
        TileGrid myGrid;
        BSPTree myBSPTree;
        Level currentLevel;
        public LevelGenerator(LevelHandler levels)
        {
            this.levels = levels;
            Random random = new Random();
            int randomSeed;

            do
                randomSeed = random.Next(60, 100);
            while (randomSeed % 4 != 0);
            Console.WriteLine(randomSeed);

            myBSPTree = new BSPTree(randomSeed, randomSeed, true);
            myGrid = new TileGrid(Constants.tileSize, Constants.tileSize, randomSeed, randomSeed);

            Room room = null;

            foreach (BSPNode node in myBSPTree.leafNodes)
            {
                room = new Room(Room.PlacementType.Floor, myGrid, node.x, node.y, node.width, node.height);
            }

            foreach (Rectangle hall in myBSPTree.halls)
            {
                room = new Room(Room.PlacementType.Corridor, myGrid, hall.X, hall.Y, hall.Width, hall.Height);
            }

            Vector2 position = Vector2.Zero;
            BSPNode tempNode = null;
            foreach (BSPNode node in myBSPTree.leafNodes)
            {
                if (tempNode == null)
                    tempNode = node;
                else if (node.size < tempNode.size)
                    tempNode = node;
            }

            position = new Vector2(tempNode.x + (tempNode.width / 2), tempNode.y + (tempNode.height / 2));

            currentLevel = new Level(myGrid);
            currentLevel.myStartPosition = myGrid.ReturnTileCenter((int)position.X, (int)position.Y);
            levels.AddLevel(currentLevel);
        }


    }
}
