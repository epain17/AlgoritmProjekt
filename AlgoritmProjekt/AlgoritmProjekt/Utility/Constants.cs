using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility
{
    public static class Constants
    {
        public static int totalScore;
        public static int screenWidth = 800, 
            screenHeight = 600, 
            tileSize = 48;

        // Game Manager
        public static int PlayerStartHP = 3,
                          PlayerStartSpeed = 140,
                          MaxLevels = 10;

        public static string scoreFilePath = "highscore.txt";
    }
}
