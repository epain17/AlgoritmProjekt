using AlgoritmProjekt.Input;
using AlgoritmProjekt.Tiles;
using LevelEditor.Extras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LevelEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum NavigationTabs
        {
            SetGrid,
            ChooseTool,
            ChooseCategory,
            ChooseObject,
        }
        public static NavigationTabs navigateTabs = NavigationTabs.SetGrid;

        public enum ChooseTools
        {
            AddTiles,
            RemoveTiles,
            SaveLevel,
        }
        public static ChooseTools chooseTool = ChooseTools.AddTiles;

        public enum ChooseCategory
        {
            Characters,
            Environment,
            Weapons,
        }
        public static ChooseCategory chooseCategory = ChooseCategory.Characters;

        public enum ChooseObject
        {
            Player,
            Enemy,
            EnemySpawner,
            Wall,
            Pistol
        }
        public static ChooseObject chooseObject = ChooseObject.Player;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileGrid grid;
        Hud hud;
        Camera camera;
        Texture2D hollowTile, solidTile;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            hollowTile = createHollowRectangle(Constants.tileSize, Constants.tileSize, GraphicsDevice);
            solidTile = createSolidRectangle(Constants.tileSize, Constants.tileSize, GraphicsDevice);
            grid = new TileGrid(hollowTile, solidTile, Constants.tileSize, Constants.columns, Constants.rows, font);
            camera = new Camera(new Rectangle(0, 0, Constants.windowWidth / 2, Constants.windowHeight / 2), new Rectangle(0, 0, Constants.windowWidth * 4, Constants.windowHeight * 4));
            hud = new Hud(createSolidRectangle(800, Constants.tileSize * 3, GraphicsDevice), font, new Vector2(0, 580 - (Constants.tileSize * 2)), 800, 600);
            camera.Update(Vector2.Zero);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            if(KeyMouseReader.RightClick())
            camera.Update(new Vector2(KeyMouseReader.mouseState.X - camera.CameraPos.X, KeyMouseReader.mouseState.Y - camera.CameraPos.Y));
            grid.Update(new Vector2(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y));
            if (navigateTabs == NavigationTabs.ChooseObject)
            {
                if (chooseObject == ChooseObject.Player)
                    grid.tileType = TileGrid.TileType.AddPlayer;
                else if (chooseObject == ChooseObject.Wall)
                    grid.tileType = TileGrid.TileType.AddWall;
                else if (chooseObject == ChooseObject.EnemySpawner)
                    grid.tileType = TileGrid.TileType.AddSpawner;
                else if (chooseObject == ChooseObject.Pistol)
                    grid.tileType = TileGrid.TileType.AddPistol;
            }
            hud.Update(camera.CameraPos);

            if (navigateTabs == NavigationTabs.SetGrid)
            {
                SetGrid();
            }
            else if(navigateTabs == NavigationTabs.ChooseTool && chooseTool == ChooseTools.SaveLevel)
            {
                grid.SaveToJsonFile();
                Console.WriteLine("SAVED");
                chooseTool = ChooseTools.AddTiles;
                hud.selected = 0;
            }

            switch (navigateTabs)
            {
                case NavigationTabs.ChooseTool:
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        navigateTabs = NavigationTabs.SetGrid;

                    if (KeyMouseReader.KeyPressed(Keys.Enter))
                    {
                        if (hud.selected == 0)
                        {
                            chooseTool = ChooseTools.AddTiles;
                            navigateTabs = NavigationTabs.ChooseCategory;
                        }
                        else if (hud.selected == 1)
                            chooseTool = ChooseTools.RemoveTiles;
                        else if (hud.selected == 2)
                            chooseTool = ChooseTools.SaveLevel;
                        hud.selected = 0;

                    }
                    break;
                case NavigationTabs.ChooseCategory:
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        navigateTabs = NavigationTabs.ChooseTool;

                    if (KeyMouseReader.KeyPressed(Keys.Enter))
                    {
                        if (hud.selected == 0)
                            chooseCategory = ChooseCategory.Characters;
                        else if (hud.selected == 1)
                            chooseCategory = ChooseCategory.Environment;
                        else if (hud.selected == 2)
                            chooseCategory = ChooseCategory.Weapons;
                        hud.selected = 0;
                        navigateTabs = NavigationTabs.ChooseObject;
                    }
                        break;
                case NavigationTabs.ChooseObject:
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        navigateTabs = NavigationTabs.ChooseCategory;

                    if (KeyMouseReader.KeyPressed(Keys.Enter))
                    {
                        if (chooseCategory == ChooseCategory.Characters)
                        {
                            if (hud.selected == 0)
                                chooseObject = ChooseObject.Player;
                            else if (hud.selected == 1)
                                chooseObject = ChooseObject.EnemySpawner;
                            else if (hud.selected == 2)
                                chooseObject = ChooseObject.Enemy;
                        }
                        else if (chooseCategory == ChooseCategory.Environment)
                        {
                            if (hud.selected == 0)
                            {
                                chooseObject = ChooseObject.Wall;
                            }
                        }
                        else if (chooseCategory == ChooseCategory.Weapons)
                        {
                            if (hud.selected == 0)
                                chooseObject = ChooseObject.Pistol;
                        }
                    }
                    break;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            grid.Draw(spriteBatch);
            hud.Draw(spriteBatch);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }

        void SetGrid()
        {
            if (KeyMouseReader.DelayedKeyPressed(Keys.Enter))
            {
                navigateTabs = NavigationTabs.ChooseTool;
            }


            if (KeyMouseReader.KeyPressed(Keys.D))
            {
                Constants.columns++;
                grid = new TileGrid(hollowTile, solidTile, Constants.tileSize, Constants.columns, Constants.rows, font);
            }
            else if (KeyMouseReader.KeyPressed(Keys.A) && Constants.columns > 1)
            {
                Constants.columns--;
                grid = new TileGrid(hollowTile, solidTile, Constants.tileSize, Constants.columns, Constants.rows, font);
            }
            else if (KeyMouseReader.KeyPressed(Keys.S))
            {
                Constants.rows++;
                grid = new TileGrid(hollowTile, solidTile, Constants.tileSize, Constants.columns, Constants.rows, font);
            }
            else if (KeyMouseReader.KeyPressed(Keys.W) && Constants.rows > 1)
            {
                Constants.rows--;
                grid = new TileGrid(hollowTile, solidTile, Constants.tileSize, Constants.columns, Constants.rows, font);
            }
        }

        Texture2D createSolidRectangle(int width, int height, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];

            // Colour the entire texture transparent first.             
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new Color(0.85f, 0.85f, 0.85f, 0.85f);
            }
            for (int i = 0; i < width; i++)
            {
                data[i] = Color.White;
            }
            for (int i = 0; i < width * height - width; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width - 1; i < width * height; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width * height - width; i < width * height; i++)
            {
                data[i] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }

        Texture2D createHollowRectangle(int width, int height, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];

            // Colour the entire texture transparent first.             
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }
            for (int i = 0; i < width; i++)
            {
                data[i] = Color.White;
            }
            for (int i = 0; i < width * height - width; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width - 1; i < width * height; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width * height - width; i < width * height; i++)
            {
                data[i] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
    }
}
