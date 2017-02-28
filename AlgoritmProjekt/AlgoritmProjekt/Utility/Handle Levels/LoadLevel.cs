using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.Weapons;
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
        static void JsonGrid(ref TileGrid grid, ref List<JsonObject> jsonTiles, ref Texture2D texture, string filePath, int size)
        {
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].Name == "Grid")
                {
                    grid = new TileGrid(texture, size, jsonTiles[i].PositionX, jsonTiles[i].PositionY);
                }
            }
        }

        static void JsonWalls(ref List<Wall> walls, ref List<JsonObject> jsonTiles, ref Texture2D texture, string filePath, int size)
        {
            if (walls != null)
                walls.Clear();
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].Name == "Wall")
                {
                    int x, y;
                    x = jsonTiles[i].PositionX;
                    y = jsonTiles[i].PositionY;
                    walls.Add(new Wall(texture, new Vector2(x, y), size));
                }
            }
        }

        static void JsonTeleports(ref Goal teleport, ref List<JsonObject> jsonTiles, ref Texture2D texture, ref Texture2D smoothTexture, string filePath, int size)
        {
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].Name == "Tele")
                {
                    Vector2 pos;
                    pos.X = jsonTiles[i].PositionX;
                    pos.Y = jsonTiles[i].PositionY;
                    teleport = new Goal(texture, smoothTexture, pos, size);
                }
            }
        }

        static void JsonEnemySpawner(ref List<EnemySpawner> enemies, ref List<JsonObject> jsonTiles, ref Texture2D texture, string filePath, int size)
        {
            if (enemies != null)
                enemies.Clear();
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].Name == "Spawner")
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
                    Vector2 pos;
                    pos.X = jsonTiles[i].PositionX;
                    pos.Y = jsonTiles[i].PositionY;
                    player.myPosition = pos;
                }
            }
        }

        static void JsonPistol(ref List<Item> items, ref List<JsonObject> jsonTiles, ref Texture2D texture, string filePath, int size)
        {
            if (items != null)
                items.Clear();
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].Name == "Pistol")
                {
                    int x, y;
                    x = jsonTiles[i].PositionX;
                    y = jsonTiles[i].PositionY;
                    items.Add(new Item(texture, new Vector2(x, y), size));
                }
            }
        }


        public static void LoadingLevel(string filePath, ref List<JsonObject> jsonTiles, ref TileGrid grid, ref List<Wall> walls,
            ref List<EnemySpawner> spawners, ref Player player, ref List<Item> weapons, ref Texture2D solidSquare,
            ref Goal teleport, ref Texture2D hollowSquare, ref Texture2D smallHollowSquare, ref Texture2D smoothTexture, int size)
        {
            if (filePath != null)
            {
                Game1.LoadJsonLevel = false;
                jsonTiles = null;
                JsonGrid(ref grid, ref jsonTiles, ref hollowSquare, filePath, size);
                JsonPlayer(ref player, ref jsonTiles, ref solidSquare, ref smallHollowSquare, filePath, size);
                JsonEnemySpawner(ref spawners, ref jsonTiles, ref solidSquare, filePath, size);
                JsonWalls(ref walls, ref jsonTiles, ref solidSquare, filePath, size);
                JsonPistol(ref weapons, ref jsonTiles, ref solidSquare, filePath, size);
                JsonTeleports(ref teleport, ref jsonTiles, ref solidSquare, ref smoothTexture, filePath, size);
            }
        }
    }
}
