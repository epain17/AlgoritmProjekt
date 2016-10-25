using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace AlgoritmProjekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Enemy enemy;
        TileGrid grid;
        Camera camera;

        Pathfinder pathfinder;
        public Vector2 pathPos;
        Point startPoint, endPoint;

        Queue<Vector2> waypoints = new Queue<Vector2>();
        List<Vector2> newPath = new List<Vector2>();
        List<Vector2> path = new List<Vector2>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grid = new TileGrid(createRectangle(32, 32, GraphicsDevice));
            player = new Player(createRectangle(32, 32, GraphicsDevice), new Vector2(300, 200), createRectangle(5,5, GraphicsDevice));
            enemy = new Enemy(createRectangle(32, 32, GraphicsDevice), new Vector2(64, 64));
            camera = new Camera(new Rectangle(0, 0, Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), new Rectangle(0, 0, Window.ClientBounds.Width * 2, Window.ClientBounds.Height * 2));
        }

     
        protected override void UnloadContent()
        {
        }

      
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyMouseReader.Update();
            foreach(Wall w in grid.GetWalls)
            {
                int n = player.Collision(w);
                if(n > 0)
                {
                    player.HandelCollision(w, n);
                }
            }
            player.Update();
            enemy.Update();
            camera.Update(player.pos);
            FindPath();
            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            grid.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);

            foreach (Vector2 v in waypoints)
            {

                spriteBatch.Draw(createRectangle(32, 32, GraphicsDevice), new Vector2(v.X, v.Y), null, Color.OrangeRed, 0, new Vector2(16, 16), 1, SpriteEffects.None, 1);

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void FindPath()
        {
            waypoints.Clear();

            pathfinder = new Pathfinder(grid);
            startPoint = enemy.EnemyPoint;
            endPoint = player.PlayerPoint;

            //Console.WriteLine("player" + player.Point);
            //Console.WriteLine("enemy" + enemy.Point);

            newPath.Clear();
            path = pathfinder.FindPath(startPoint, endPoint);
            if (path != null)
            {
                foreach (Vector2 point in path)
                {
                    foreach (Vector2 pathpos in path)
                    {
                        pathPos = new Vector2(pathpos.X, pathpos.Y);
                        newPath.Add(pathPos);
                        waypoints.Enqueue(pathPos);
                    }
                    enemy.SetWaypoints(waypoints);
                    break;
                }
            }
        }

        Texture2D createRectangle(int width, int height, GraphicsDevice graphicsDevice)
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
