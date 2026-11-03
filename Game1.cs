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
        private TileMap _tileMap;

        private MapGenerationSystem _mainWorldGenerator;
        private RenderSystem _renderSystem;
        private ContextMenuRenderSystem _contextMenuRenderSystem;
        private CursorMapMovementSystem _cursorMapMovementSystem;
        private CursorMapHighlightSystem _cursorMapHighlightSystem;
        private ContextMenuTriggerSystem _contextMenuTriggerSystem;
        private ContextMenuInputSystem _contextMenuInputSystem;

        private CursorFactory _cursorFactory;
        private GlobalGameStateFactory _globalGameStateFactory;

        // (!) As a good idea to create an AssetManager.cs later 
        private Dictionary<TileType, Texture2D[]> _tileTextures = new();
        private Dictionary<string, Texture2D[]> _objectTextures = new();
        private Dictionary<string, Texture2D[]> _uiTextures = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _tileMap = new TileMap();
            _world = new World(_tileMap);

            _mainWorldGenerator = new MapGenerationSystem();
            _renderSystem = new RenderSystem();
            _contextMenuRenderSystem = new ContextMenuRenderSystem();
            _cursorMapMovementSystem = new CursorMapMovementSystem();
            _cursorMapHighlightSystem = new CursorMapHighlightSystem();
            _contextMenuTriggerSystem = new ContextMenuTriggerSystem();
            _contextMenuInputSystem = new ContextMenuInputSystem();

            _cursorFactory = new CursorFactory();
            _globalGameStateFactory = new GlobalGameStateFactory();

            _mainWorldGenerator.GenerateWorld(_world, _tileMap, GameSettings.MapWidth, GameSettings.MapHeight);
            _cursorFactory.CreateCursor(_world, 0, 0);
            _globalGameStateFactory.CreateGlobalGameState(_world);

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
            _tileTextures[TileType.Sand] = new Texture2D[]
            {
                Content.Load<Texture2D>("SandTile")
            };

            _uiTextures["ContextMenu"] = new Texture2D[]{ Content.Load<Texture2D>("ContextMenu") };

            _uiTextures["ExitIcon"] = new Texture2D[] { Content.Load<Texture2D>("ExitIcon") };
            _uiTextures["GrassTileIcon"] = new Texture2D[] { Content.Load<Texture2D>("GrassTileIcon") };
            _uiTextures["WaterTileIcon"] = new Texture2D[] { Content.Load<Texture2D>("WaterTileIcon") };
            _uiTextures["SandTileIcon"] = new Texture2D[] { Content.Load<Texture2D>("SandTileIcon") };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _cursorMapMovementSystem.HandleKeyboard(_world);
            _cursorMapHighlightSystem.ComputeHighlight(_world, _tileMap);
            _contextMenuTriggerSystem.HandleInput(_world, _tileMap);
            _contextMenuInputSystem.HandleInput(_world);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Lavender);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _renderSystem.Draw(_spriteBatch, _world, _tileTextures, _cursorMapHighlightSystem.GetHighlightedIds());

            if (_uiTextures.TryGetValue("ContextMenu", out var menuTextures) && menuTextures.Length > 0)
            {
                _contextMenuRenderSystem.Draw(_world, _spriteBatch, menuTextures[0]);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
