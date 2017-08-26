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
        Random random = new Random();

        public LevelGenerator(LevelHandler levels)
        {
            this.levels = levels;
        }

        public void GenerateDungeonLevel()
        {
            int randomSeed;

            do
                randomSeed = random.Next(60, 100);
            while (randomSeed % 4 != 0);
            Console.WriteLine(randomSeed);

            myBSPTree = new BSPTree(randomSeed, randomSeed, true);
            myGrid = new TileGrid(Constants.tileSize, Constants.tileSize, randomSeed, randomSeed);
            currentLevel = new Level(myGrid);

            foreach (BSPNode node in myBSPTree.nodes)
            {
                if (node.isLeaf())
                    currentLevel.rooms.Add(new Room(Room.PlacementType.Floor, myGrid, node.x, node.y, node.width, node.height));
            }

            foreach (BSPNode hall in myBSPTree.halls)
            {
                currentLevel.rooms.Add(new Room(Room.PlacementType.Corridor, myGrid, hall.x, hall.y, hall.width, hall.height));
            }

            BSPNode tempNode = null;
            foreach (BSPNode node in myBSPTree.nodes)
            {
                if (tempNode == null)
                    tempNode = node;
                else if (node.width * node.height < tempNode.width * tempNode.height)
                    tempNode = node;
            }
            currentLevel.UpdateWalls();
            currentLevel.myStartPosition = myGrid.ReturnTileCenter(tempNode.xCenter(), tempNode.yCenter());
            levels.AddLevel(currentLevel);
        }
    }
}
