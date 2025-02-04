using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xbox.Galaga.Handlers;
using Xbox.Galaga.Player;
using Xbox_Galaga.InputControl;

namespace Xbox.Galaga
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int ScreenWidth { get; set; }
        private int ScreenHeight { get; set; }

        private readonly FrameCounter _frameCounter = new FrameCounter();
        private Starfield.Starfield _starfield;
        private LevelHandler _levelHandler;
        private Player.PlayerHandler _playerHandler;

        private Texture2D _title;
        private Texture2D _spriteSheet;
        private Texture2D _spriteSheetScreens;

        private Screens.Screens _screens;
        private TitleScreenHandler _titleScreenHandler;

        private GamePadController _gamePad;

        private enum GameStates
        {
            TitleScreen,
            Playing,
            GameOver
        };

        private GameStates _gameState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.Title = "Galaga Clone";
        }

        protected override void Initialize()
        {
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            _gameState = GameStates.GameOver;
            _starfield = new Starfield.Starfield(ScreenWidth, ScreenHeight, new Texture2D(GraphicsDevice, 1, 1));

            _levelHandler = new LevelHandler();
            _playerHandler = new PlayerHandler();
            _screens = new Screens.Screens();
            _titleScreenHandler = new TitleScreenHandler();
            _gamePad = new GamePadController(this);

            // Just testing adding services to see if it would work. But probably not where we want to use this.
            //Services.AddService(typeof(LevelHandler), new LevelHandler());
            Services.AddService(typeof(LevelHandler), _levelHandler);
            Services.AddService(typeof(PlayerHandler), _playerHandler);
            Services.AddService(typeof(TitleScreenHandler), _titleScreenHandler);
            Services.AddService(typeof(GamePadController), _gamePad);

            // This is probably one where it actually is useful
            //Services.AddService(typeof(SoundManager), SoundManager);
            //Services.AddService(typeof(GameStatsHandler), GameStatsHandler);

            //var game = new Microsoft.Xna.Framework.Game();
            //this.Window.Title = "Test";

            //var services = game.Services;
            //services.AddService(typeof(PlayerHandler), _playerHandler);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _title = Content.Load<Texture2D>("images/galaga-logo");
            _spriteSheet = Content.Load<Texture2D>("images/galaga-sprites_transparent");
            _spriteSheetScreens = Content.Load<Texture2D>("images/galaga-screens");

            TextToSpriteHandler.Initialize(_spriteSheetScreens, Color.White);

            SoundManager.PlayerFiringSound = Content.Load<SoundEffect>("sounds/galaga_player_firing_sound");
            SoundManager.PlayerExplosionSound = Content.Load<SoundEffect>("sounds/galaga_player_explosion_sound");
            SoundManager.EnemyExplosionSound = Content.Load<SoundEffect>("sounds/galaga_enemy_explosion_sound");
            SoundManager.LevelStartSound = Content.Load<SoundEffect>("sounds/galaga_level_start_sound");

            _playerHandler.Initialize(ScreenWidth, ScreenHeight, _spriteSheet, Color.White);
            _levelHandler.Initialize(ScreenWidth, ScreenHeight, _spriteSheet, Color.White, GraphicsDevice);
            _titleScreenHandler.Initialize(ScreenWidth, ScreenHeight, _spriteSheet, _title);
        }

        protected override void Update(GameTime gameTime)
        {
            var gamePad = GamePad.GetState(PlayerIndex.One);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GameStatsHandler.Lives == 0)
                _gameState = GameStates.GameOver;

            switch (_gameState)
            {
                case GameStates.TitleScreen:
                    _titleScreenHandler.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || gamePad.Buttons.A == ButtonState.Pressed)
                        _gameState = GameStates.Playing;
                    break;

                case GameStates.Playing:
                    _playerHandler.Update(gameTime);
                    _levelHandler.Update(gameTime, _playerHandler.Player, GraphicsDevice);
                    break;

                case GameStates.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) || gamePad.Buttons.B == ButtonState.Pressed)
                    {
                        _gameState = GameStates.TitleScreen;
                        _levelHandler.Reset();
                        _playerHandler.Initialize(ScreenWidth, ScreenHeight, _spriteSheet, Color.White);
                        GameStatsHandler.Reset();
                    }

                    break;
            }

            _starfield.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter.Update(deltaTime);

            Window.Title = "Galaga Clone";
            if (gameTime.IsRunningSlowly)
                Window.Title += $" - Lagging FPS({_frameCounter.AverageFps:.##})";

            _spriteBatch.Begin();

            switch (_gameState)
            {
                case GameStates.TitleScreen:
                    _titleScreenHandler.Draw(_spriteBatch);
                    break;

                case GameStates.Playing:
                    _playerHandler.Draw(_spriteBatch);
                    _levelHandler.Draw(_spriteBatch);
                    break;

                case GameStates.GameOver:
                    _screens.DrawResultsScreen(_spriteBatch, _spriteSheet, ScreenWidth, ScreenHeight);
                    break;
            }

            var halfScreenWidth = ScreenWidth * 0.5;

            //TextToSpriteHandler.DrawString(_spriteBatch, $"score {GameStatsHandler.Score}", Color.Red, 10, 10, 8, 8);
            //TextToSpriteHandler.DrawString(_spriteBatch, $"round {_levelHandler.Round}    level {_levelHandler.Level}", Color.Red, 325, 10, 8, 8);
            //TextToSpriteHandler.DrawString(_spriteBatch, $"lives {GameStatsHandler.Lives}", Color.Red, 700, 10, 8, 8);

            TextToSpriteHandler.DrawString(_spriteBatch, $"score {GameStatsHandler.Score}", Color.Red, (int)halfScreenWidth - 400, 10, 8, 8);
            TextToSpriteHandler.DrawString(_spriteBatch, $"round {_levelHandler.Round}    level {_levelHandler.Level}", Color.Red, (int)halfScreenWidth - 75, 10, 8, 8);
            TextToSpriteHandler.DrawString(_spriteBatch, $"lives {GameStatsHandler.Lives}", Color.Red, (int)halfScreenWidth + 400, 10, 8, 8);

            _starfield.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
