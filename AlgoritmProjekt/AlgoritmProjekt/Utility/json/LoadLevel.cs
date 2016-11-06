using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Utility.json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility
{
    class LoadLevel
    {
        static void JsonWalls(ref List<Wall> walls, ref List<JsonObject> jsonTiles, ref Texture2D texture, string filePath, int size)
        {
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if(jsonTiles[i].Name == "Surface")
                {
                    int x, y;
                    x = jsonTiles[i].PositionX;
                    y = jsonTiles[i].PositionY;
                    walls.Add(new Wall(texture, new Vector2(x, y), size));
                }
            }
        }

        static void JsonEnemySpawner(ref List<EnemySpawner> enemies, ref List<JsonObject> jsonTiles, ref Texture2D texture, string filePath, int size)
        {
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].Name == "Spikes")
                {
                    int x, y;
                    x = jsonTiles[i].PositionX;
                    y = jsonTiles[i].PositionY;
                    enemies.Add(new EnemySpawner(texture, new Vector2(x, y), size));
                }
            }
        }

        static void JsonPlayer(ref Player player, ref List<JsonObject> jsonTiles, ref Texture2D texture, ref Texture2D projectileTex, string filePath, int size)
        {
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].Name == "Player")
                {
                    int x, y;
                    x = jsonTiles[i].PositionX;
                    y = jsonTiles[i].PositionY;
                    player = (new Player(texture, new Vector2(x, y), size));
                }
            }
        }

        public static void LoadingLevel(string filePath, ref List<JsonObject> jsonTiles, ref List<Wall> walls, 
            ref List<EnemySpawner> spawners, ref Player player, ref Texture2D solidSquare, ref Texture2D hollowSquare, ref Texture2D smallHollowSquare, int size)
        {
            if(Game1.LoadJsonLevel && filePath != null)
            {
                Game1.LoadJsonLevel = false;
                jsonTiles = null;
                JsonPlayer(ref player, ref jsonTiles, ref solidSquare, ref smallHollowSquare, filePath, size);
                JsonEnemySpawner(ref spawners, ref jsonTiles, ref hollowSquare, filePath, size);
                JsonWalls(ref walls, ref jsonTiles, ref solidSquare, filePath, size);
            }
        }
    }
}
