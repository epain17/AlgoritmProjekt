using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.Utility.Handle_Levels.FromFile.json;
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
        static void JsonGrid(ref TileGrid grid, ref List<JsonObject> jsonTiles, string filePath, int size)
        {
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                if (jsonTiles[i].ID == "Grid")
                {
                    grid = new TileGrid(size, size, jsonTiles[i].PositionX, jsonTiles[i].PositionY);
                }
            }
        }

        public static List<Tile> Tiles(ref List<JsonObject> jsonTiles, string filePath, int size)
        {
            jsonTiles = JsonSerialization.ReadFromJsonFile<List<JsonObject>>(filePath);
            List<Tile> tempList = new List<Tile>();

            for (int i = 0; i < jsonTiles.Count; i++)
            {
                Tile tempTile = new Tile(new Vector2(jsonTiles[i].PositionX, jsonTiles[i].PositionY), size, size);
                tempTile.myID = jsonTiles[i].ID;
                tempList.Add(tempTile);
            }
            return tempList;
        }

        public static void LoadingLevel(string filePath, ref List<JsonObject> jsonTiles, ref TileGrid grid, int size)
        {
            if (filePath != null)
            {
                Game1.LoadJsonLevel = false;
                jsonTiles = null;
                JsonGrid(ref grid, ref jsonTiles, filePath, size);
            }
        }
    }
}
