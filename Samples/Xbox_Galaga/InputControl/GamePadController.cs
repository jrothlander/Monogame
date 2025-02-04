using System;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Xbox_Galaga.InputControl
{

    internal class GamePadController : DrawableGameComponent
    {
        private GraphicsDeviceManager Graphics { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        private FontSystem _fontSystem;
        private SpriteFontBase _font18;
        private Texture2D _rec;

        private KeyboardState _keyState, _prevKeyState;

        private bool _gamePadStateVisible;
        private readonly byte[] _gamePadState = new byte[12];  // bit-string
        private readonly float[] _gamPadValues = new float[6];

        public GamePadController(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            Graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            if (Graphics == null) return;

            SpriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            if (SpriteBatch == null || Graphics == null) return;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _rec = new Texture2D(GraphicsDevice, 1, 1);
            _rec.SetData(new[] { Color.White });

            _fontSystem = new FontSystem();
            _fontSystem.AddFont(File.ReadAllBytes("DejaVuSans.ttf"));
            _font18 = _fontSystem.GetFont(18);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            GetGamePadState();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _keyState = Keyboard.GetState();
            var _gamePad = GamePad.GetState(PlayerIndex.One);

            if (_gamePad.Buttons.Y == ButtonState.Pressed || (_keyState.IsKeyDown(Keys.F12) && _prevKeyState.IsKeyUp(Keys.F12)))
                _gamePadStateVisible = !_gamePadStateVisible;

            if (_gamePadStateVisible)
                DrawGamePadView();

            _prevKeyState = _keyState;

            base.Draw(gameTime);
        }

        private void GetGamePadState()
        {
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            _gamePadState[0] = Convert.ToByte(gamePadState.IsConnected);

            // Letter Buttons
            _gamePadState[1] = Convert.ToByte(gamePadState.Buttons.X);
            _gamePadState[2] = Convert.ToByte(gamePadState.Buttons.Y);
            _gamePadState[3] = Convert.ToByte(gamePadState.Buttons.A);
            _gamePadState[4] = Convert.ToByte(gamePadState.Buttons.B);
            _gamePadState[5] = Convert.ToByte(gamePadState.Buttons.Back);

            // Shoulder Buttons
            _gamePadState[6] = Convert.ToByte(gamePadState.Buttons.LeftShoulder);
            _gamePadState[7] = Convert.ToByte(gamePadState.Buttons.RightShoulder);

            // Directional Pad
            _gamePadState[8] = Convert.ToByte(gamePadState.DPad.Up);
            _gamePadState[9] = Convert.ToByte(gamePadState.DPad.Down);
            _gamePadState[10] = Convert.ToByte(gamePadState.DPad.Left);
            _gamePadState[11] = Convert.ToByte(gamePadState.DPad.Right);

            // ------------------------
            // Triggers
            // ------------------------
            _gamPadValues[0] = gamePadState.Triggers.Left;
            _gamPadValues[1] = gamePadState.Triggers.Right;

            // ------------------------
            // ThumbSticks
            // ------------------------
            _gamPadValues[2] = gamePadState.ThumbSticks.Left.X;
            _gamPadValues[3] = gamePadState.ThumbSticks.Left.Y;
            _gamPadValues[4] = gamePadState.ThumbSticks.Right.X;
            _gamPadValues[5] = gamePadState.ThumbSticks.Right.Y;

            // ------------------
            //  Vibrate GamePad
            // ------------------
            //GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
        }

        private void DrawGamePadView()
        {
            var color = Color.White;

            SpriteBatch.Begin();

            SpriteBatch.Draw(_rec, new Rectangle(5, 5, 500, 250), Color.CadetBlue * 0.5f); // Draw transparent popup window.

            var binary = "";
            foreach (var item in _gamePadState)
                binary += item.ToString();

            SpriteBatch.DrawString(_font18, _gamePadState[0] == 1 ? "Connected" : "Disconnected", new Vector2(10, 15), color);

            SpriteBatch.DrawString(_font18, "X", new Vector2(10, 35), color);
            SpriteBatch.DrawString(_font18, $"{_gamePadState[1]}", new Vector2(50, 35), color);
            SpriteBatch.DrawString(_font18, "Y", new Vector2(10, 55), color);
            SpriteBatch.DrawString(_font18, $"{_gamePadState[2]}", new Vector2(50, 55), color);
            SpriteBatch.DrawString(_font18, "A", new Vector2(10, 75), color);
            SpriteBatch.DrawString(_font18, $"{_gamePadState[3]}", new Vector2(50, 75), color);
            SpriteBatch.DrawString(_font18, "B", new Vector2(10, 95), color);
            SpriteBatch.DrawString(_font18, $"{_gamePadState[4]}", new Vector2(50, 95), color);

            SpriteBatch.DrawString(_font18, "Back", new Vector2(10, 125), color);
            SpriteBatch.DrawString(_font18, $"{_gamePadState[5]}", new Vector2(60, 125), color);

            SpriteBatch.DrawString(_font18, "Left Shoulder", new Vector2(10, 145), color);
            SpriteBatch.DrawString(_font18, $"{_gamePadState[6]}", new Vector2(135, 145), color);
            SpriteBatch.DrawString(_font18, "Right Shoulder", new Vector2(10, 165), color);
            SpriteBatch.DrawString(_font18, $"{_gamePadState[7]}", new Vector2(135, 165), color);

            SpriteBatch.DrawString(_font18, "DPad", new Vector2(10, 195), color);

            var dpString = "";
            if (_gamePadState[8] == 1) dpString = "Up";
            if (_gamePadState[9] == 1) dpString = "Down";
            if (_gamePadState[10] == 1)
            {
                if (!string.IsNullOrEmpty(dpString)) dpString += " & ";
                dpString += "Left";
            }
            if (_gamePadState[11] == 1)
            {
                if (!string.IsNullOrEmpty(dpString)) dpString += " & ";
                dpString += "Right";
            }

            SpriteBatch.DrawString(_font18, dpString, new Vector2(60, 195), color);

            SpriteBatch.DrawString(_font18, "Left Trigger", new Vector2(210, 35), color);
            SpriteBatch.DrawString(_font18, $"{_gamPadValues[0]}", new Vector2(325, 35), color);
            SpriteBatch.DrawString(_font18, "Right Trigger", new Vector2(210, 55), color);
            SpriteBatch.DrawString(_font18, $"{_gamPadValues[1]}", new Vector2(325, 55), color);
            SpriteBatch.DrawString(_font18, "Left ThumbStick", new Vector2(210, 85), color);
            SpriteBatch.DrawString(_font18, $"({_gamPadValues[2]:N2},{_gamPadValues[3]:N2})", new Vector2(355, 85), color);
            SpriteBatch.DrawString(_font18, "Right ThumbStick", new Vector2(210, 105), color);
            SpriteBatch.DrawString(_font18, $"({_gamPadValues[4]:N2},{_gamPadValues[5]:N2})", new Vector2(355, 105), color);
            SpriteBatch.DrawString(_font18, $"Bit String: [{binary}]", new Vector2(10, 230), color);

            SpriteBatch.End();
        }
    }
}
