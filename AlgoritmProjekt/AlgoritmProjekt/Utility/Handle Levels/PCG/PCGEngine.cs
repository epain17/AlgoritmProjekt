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
    class PCGEngine
    {
        LevelHandler levels;
        TileGrid myGrid;
        BSPTree myBSPTree;
        Level currentLevel;

        public PCGEngine(LevelHandler levels)
        {
            this.levels = levels;
            Random random = new Random();
            int temp;

            do
                temp = random.Next(40, 60);
            while (temp % 4 != 0);
            Console.WriteLine(temp);

            myBSPTree = new BSPTree(temp, temp, true);

            myGrid = InitializeGrid(temp);
            Room room = null;
            foreach (BSPNode node in myBSPTree.LeafNodes)
            {
                room = new Room(Room.PlacementType.Floor, myGrid, node.x, node.y, node.width, node.height);
            }
            room = null;

            foreach (BSPNode node in myBSPTree.Conjunctions)
            {
                room = new Room(Room.PlacementType.Corridor, myGrid, node.x, node.y, node.width, node.height);
            }

            currentLevel = CreateLevel();
            BSPNode smallest = null;
            foreach (BSPNode node in myBSPTree.LeafNodes)
            {
                if (smallest == null)
                    smallest = node;
                else if (node.width * node.height < smallest.width * smallest.height)
                    smallest = node;
            }
            currentLevel.myStartPosition = myGrid.ReturnTileCenter(smallest.CentralX(), smallest.CentralY());
            levels.AddLevel(currentLevel);
        }

        TileGrid InitializeGrid(int randomSeed)
        {
            return new TileGrid(Constants.tileSize, Constants.tileSize, randomSeed, randomSeed);
        }

        Level CreateLevel()
        {
            return new Level(myGrid);
        }
    }
}
