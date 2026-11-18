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
        private InputState _input;

        private World _world;
        private TileMap _tileMap;

        private MapGenerationSystem _mainWorldGenerator;
        private TileRenderSystem _tileRenderSystem;
        private ContextMenuRenderSystem _contextMenuRenderSystem;
        private CoinsUiRenderSystem _coinsUiRenderSystem;
        private CursorMapMovementSystem _cursorMapMovementSystem;
        private CursorMapHighlightSystem _cursorMapHighlightSystem;
        private ContextMenuTriggerSystem _contextMenuTriggerSystem;
        private ContextMenuInputSystem _contextMenuInputSystem;
        private ActionExecutionSystem _actionExecutionSystem;

        private CursorFactory _cursorFactory;
        private GlobalGameStateFactory _globalGameStateFactory;

        // (!) As a good idea to create an AssetManager.cs later 
        private Dictionary<TileType, Texture2D[]> _tileTextures = new();
        private Dictionary<string, Texture2D[]> _objectTextures = new();
        private Dictionary<string, Texture2D[]> _uiTextures = new();
        private SpriteFont _font;

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
            _input = new InputState();

            _mainWorldGenerator = new MapGenerationSystem();
            _tileRenderSystem = new TileRenderSystem();
            _contextMenuRenderSystem = new ContextMenuRenderSystem();
            _coinsUiRenderSystem = new CoinsUiRenderSystem();
            _cursorMapMovementSystem = new CursorMapMovementSystem();
            _cursorMapHighlightSystem = new CursorMapHighlightSystem();
            _contextMenuTriggerSystem = new ContextMenuTriggerSystem();
            _contextMenuInputSystem = new ContextMenuInputSystem();
            _actionExecutionSystem = new ActionExecutionSystem();

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
            _tileTextures[TileType.Seedbed] = new Texture2D[]
            {
                Content.Load<Texture2D>("SeedbedTile")
            };

            //_uiTextures["ContextMenu"] = new Texture2D[]{ Content.Load<Texture2D>("ContextMenu") };

            _uiTextures["ExitIcon"] = new Texture2D[] { Content.Load<Texture2D>("ExitIcon") };
            _uiTextures["GrassTileIcon"] = new Texture2D[] { Content.Load<Texture2D>("GrassTileIcon") };
            _uiTextures["WaterTileIcon"] = new Texture2D[] { Content.Load<Texture2D>("WaterTileIcon") };
            _uiTextures["SandTileIcon"] = new Texture2D[] { Content.Load<Texture2D>("SandTileIcon") };
            _uiTextures["SeedbedTileIcon"] = new Texture2D[] { Content.Load<Texture2D>("SeedbedTileIcon") };

            _font = Content.Load<SpriteFont>("TestFont");
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F10))
                Exit();

            // TODO: Add your update logic here

            _cursorMapMovementSystem.HandleInput(_world, _input);
            _cursorMapHighlightSystem.ComputeHighlight(_world, _tileMap);
            _contextMenuTriggerSystem.HandleInput(_world, _tileMap, _input);
            _contextMenuInputSystem.HandleInput(_world, _input);
            _actionExecutionSystem.Update(_world);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Lavender);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _tileRenderSystem.Draw(_spriteBatch, _world, _tileTextures);

            _contextMenuRenderSystem.Draw(_world, _spriteBatch, _uiTextures);
            _coinsUiRenderSystem.Draw(_world, _spriteBatch, _font, GraphicsDevice.Viewport.Width);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
