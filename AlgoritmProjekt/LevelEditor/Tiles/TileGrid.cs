
using AlgoritmProjekt.Utility.json;
using LevelEditor.Tiles.GameObjects;
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
        enum AddTile
        {
            AddPlayer,
            AddWall,
            AddSpawner,
            RemoveTile,
        }
        AddTile addTile = AddTile.AddPlayer;

        public int width, height;
        Texture2D hollowTile, solidTile;
        Tile[,] tileGrid;
        int size;
        JsonObject jsonTile;
        List<JsonObject> jsonTiles = new List<JsonObject>();

        int level = 00;
        List<Tile> tiles = new List<Tile>();
        Player player;

        public TileGrid(Texture2D hollowTile, Texture2D solidTile, int size, int columns, int rows)
        {
            this.hollowTile = hollowTile;
            this.solidTile = solidTile;
            this.size = size;
            this.width = columns;
            this.height = rows;
            
            CreateTileGrid();
        }

        public void Update(Vector2 mouse)
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
                        }
                    }
                }
            }
        }

        private void CreateNewTile(Vector2 position)
        {
            switch (addTile)
            {
                case AddTile.AddPlayer:
                    player = new Player(solidTile,position, size);
                    break;
                case AddTile.AddWall:
                    Wall wall = new Wall();
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
        }

        public void CreateTileGrid()
        {
            tileGrid = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileGrid[i, j] = new Tile(hollowTile, new Vector2(0 + i * size, 0 + j * size), size);
                }
            }
        }

        public void SaveToJsonFile()
        {
            // if constants.save
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    jsonTile = new JsonObject() { Name = "Grid", PositionX = (int)tileGrid[i, j].myPosition.X, PositionY = (int)tileGrid[i, j].myPosition.Y };
                    jsonTiles.Add(jsonTile);
                }
            }
            JsonSerialization.WriteToJsonFile<List<JsonObject>>("Level" + level + ".json", jsonTiles);
        }
    }
}
