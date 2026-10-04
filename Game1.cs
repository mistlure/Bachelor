using System.Collections.Generic;
using Bachelor.Config;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Enums;
using Bachelor.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bachelor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private World _world;

        private MapGenerationSystem _mainWorldGenerator;
        private RenderSystem _renderSystem;
        private CursorInputSystem _cursorInputSystem;

        private CursorFactory _cursorFactory;

        // (!) As a good idea to create an AssetManager.cs later 
        private Dictionary<TileType, Texture2D[]> _tileTextures = new();
        private Dictionary<string, Texture2D[]> _objectTextures = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _world = new World();
            _mainWorldGenerator = new MapGenerationSystem();
            _renderSystem = new RenderSystem();
            _cursorInputSystem = new CursorInputSystem();

            _cursorFactory = new CursorFactory();

            _mainWorldGenerator.GenerateWorld(_world, GameSettings.MapWidth, GameSettings.MapHeight);
            _cursorFactory.CreateCursor(_world, 0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // (!)Convert to JSON later
            _tileTextures[TileType.Water] = new Texture2D[]
            {
                Content.Load<Texture2D>("WaterTile")
            };
            _tileTextures[TileType.Grass] = new Texture2D[]
            {
                Content.Load<Texture2D>("GrassTile")
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _cursorInputSystem.HandleKeyboard(_world);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _renderSystem.Draw(_spriteBatch, _world, _tileTextures);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
