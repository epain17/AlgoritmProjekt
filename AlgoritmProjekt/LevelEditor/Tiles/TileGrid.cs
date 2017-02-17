
using AlgoritmProjekt.Utility.json;
using LevelEditor;
using LevelEditor.Tiles.GameObjects;
using LevelEditor.Tiles.GameObjects.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Tiles
{
    class TileGrid
    {
        public enum TileType
        {
            AddPlayer,
            AddWall,
            AddTeleport,
            AddSpawner,
            AddPistol,
        }
        public TileType tileType = TileType.AddWall;



        public int width, height;
        Texture2D hollowTile, solidTile;
        Tile[,] tileGrid;
        int size;
        JsonObject jsonTile;
        List<JsonObject> jsonTiles = new List<JsonObject>();
        SpriteFont font;

        int level = 1;
        List<Tile> tiles = new List<Tile>();
        Player player;

        public TileGrid(Texture2D hollowTile, Texture2D solidTile, int size, int columns, int rows, SpriteFont font)
        {
            this.hollowTile = hollowTile;
            this.solidTile = solidTile;
            this.size = size;
            this.width = columns;
            this.height = rows;
            this.font = font;

            CreateTileGrid();
        }

        public void Update(Vector2 mouse)
        {


            if (Game1.navigateTabs == Game1.NavigationTabs.ChooseObject)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (tileGrid[i, j].Hovering(mouse))
                        {
                            if (tileGrid[i, j].Clicked())
                            {
                                CreateNewTile(tileGrid[i, j].myPosition);
                                foreach (Tile tile in tiles)
                                {
                                    //if (tile is Wall)
                                    SetOccupiedGrid(tile);
                                }

                            }
                        }
                    }
                }
            }
        }

        private void CreateNewTile(Vector2 position)
        {
            switch (tileType)
            {
                case TileType.AddPlayer:
                    player = new Player(solidTile, position, size, font);
                    break;
                case TileType.AddTeleport:
                    tiles.Add(new Teleport(solidTile, position, size, font));
                    break;
                case TileType.AddWall:
                    tiles.Add(new Wall(solidTile, position, size, font));
                    break;
                case TileType.AddSpawner:
                    tiles.Add(new EnemySpawner(solidTile, position, size, font));
                    break;
                case TileType.AddPistol:
                    tiles.Add(new Pistol(solidTile, position, size, font));
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (tileGrid != null)
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        tileGrid[i, j].Draw(spriteBatch);
                    }
                }
            if (player != null)
                player.Draw(spriteBatch);
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public void SetOccupiedGrid(Tile target)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (tileGrid[i, j].amIOccupied(target))
                        tileGrid[i, j].iamOccupied = true;
                }
            }
        }

        public void CreateTileGrid()
        {
            tileGrid = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileGrid[i, j] = new Tile(hollowTile, new Vector2(0 + i * size, 0 + j * size), size, font);
                }
            }
        }

        public void SaveToJsonFile()
        {
            jsonTile = new JsonObject() { Name = "Grid", PositionX = width, PositionY = height };
            jsonTiles.Add(jsonTile);

            foreach (Tile tile in tiles)
            {
                jsonTiles.Add(new JsonObject() { Name = tile.myName, PositionX = (int)tile.myPosition.X, PositionY = (int)tile.myPosition.Y });
            }

            jsonTiles.Add(new JsonObject() { Name = "Player", PositionX = (int)player.myPosition.X, PositionY = (int)player.myPosition.Y });

            JsonSerialization.WriteToJsonFile<List<JsonObject>>("Level" + level + ".json", jsonTiles, false);
        }
    }
}
