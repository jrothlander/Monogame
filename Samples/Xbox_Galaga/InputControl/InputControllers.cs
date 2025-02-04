using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Section_1_2_Matrices.InputControl;

internal class InputControllers : DrawableGameComponent
{
    private GraphicsDeviceManager Graphics { get; }
    private SpriteBatch SpriteBatch { get; }

    private readonly Game _game;
    private readonly Point _screen;
    private FontSystem _fontSystem;
    private SpriteFontBase _font18;
    private Texture2D _rec;

    //private string keyPressed;
    private float angle;
    //private Vector3 position;
    private Model model;
    private Matrix world;
    private Matrix view;
    private Matrix projection;
    private Vector3 camTarget;
    private Vector3 camPosition;

    private GamePadState oldState, _gamePadState, _preGamePadState;
    private KeyboardState _keyState, _prevKeyState;

    //private bool _gamePadStateVisible;
    //private byte[] _gamepadState = new byte[12];
    //private float[] _gamepadValues = new float[6];

    private Matrix objectMatrix;

    public InputControllers(Game game, Texture2D texture) : base(game)
    {
        _game = game;

        Graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
        if (Graphics == null) return;

        SpriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
        if (SpriteBatch == null || Graphics == null) return;

        _screen.X = Graphics.PreferredBackBufferWidth;
        _screen.Y = Graphics.PreferredBackBufferHeight;
    }

    public override void Initialize()
    {
        camTarget = new Vector3(0f, 0f, 0f);
        //camPosition = new Vector3(0f, 0f, -50f);
        camPosition = new Vector3(0f, 0f, -100f);

        //var aspectRatio = (_screen.X * 2f) / _screen.Y;
        //frustumRightProjected.X = (float)Math.Cos(fovWidthRadians / 2 * aspectRatio);  // Precompute the right edge of the view frustrum.
        //frustumRightProjected.Z = (float)Math.Sin(fovWidthRadians / 2 * aspectRatio);

        // Projection Matrix
        //projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1f, 1000f);
        projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), GraphicsDevice.DisplayMode.AspectRatio, 1f, 1000f);
        
        // View Matrix
        //view = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
        view = Matrix.CreateLookAt(camPosition, camTarget, new Vector3(0f, 0f, 0f)); // Y Up

        // World Matrix
        //world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        world = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);

        base.Initialize();
    }


    protected override void LoadContent()
    {
        _rec = new Texture2D(GraphicsDevice, 1, 1);
        _rec.SetData(new[] { Color.White });

        _fontSystem = new FontSystem();
        _fontSystem.AddFont(File.ReadAllBytes("DejaVuSans.ttf"));
        _font18 = _fontSystem.GetFont(18);

        model = _game.Content.Load<Model>("xwing");

        var xWingMatrix = new Matrix[model.Bones.Count];

        //var test1 = model.Bones[0];
        //var test2 = model.Bones[1];
        //var test3 = model.Bones[2];

        //model.CopyAbsoluteBoneTransformsTo(xWingMatrix);
        //var scaling = model.Meshes[0].BoundingSphere.Radius * xWingMatrix[0].Right.Length();
        //if (scaling == 0)
        //    scaling = 0.0001f;

        //objectMatrix *= Matrix.CreateScale(1.0f / scaling);

        //otherTexture = Content.Load<Texture2D>("GreenShipTexture");
        //position = new Vector3(0, 0, 0);

        angle = .003f;

        base.LoadContent();
    }

    private void GetKeyboardState()
    {
        const float speed = 1f;

        if (_keyState.IsKeyDown(Keys.Left))
        {
            camPosition.X -= speed;
            camTarget.X -= speed;
        }
        if (_keyState.IsKeyDown(Keys.Right))
        {
            camPosition.X += speed;
            camTarget.X += speed;
        }
        if (_keyState.IsKeyDown(Keys.Up))
        {
            camPosition.Y -= speed;
            camTarget.Y -= speed;
        }
        if (_keyState.IsKeyDown(Keys.Down))
        {
            camPosition.Y += speed;
            camTarget.Y += speed;
        }
        if (_keyState.IsKeyDown(Keys.OemPlus))
        {
            camPosition.Z += speed;
        }
        if (_keyState.IsKeyDown(Keys.OemMinus))
        {
            camPosition.Z -= speed;
        }
    }

    private void ProcessGamePadState()
    {
        const float speed = 1f;

        //if (_gamePadState.DPad.Up == ButtonState.Pressed && _preGamePadState.DPad.Up == ButtonState.Released)
        if (_gamePadState.DPad.Up == ButtonState.Pressed)
            camPosition.Z += speed;
        
        if (_gamePadState.DPad.Down == ButtonState.Pressed)
            camPosition.Z -= speed;

        if (_gamePadState.DPad.Left == ButtonState.Pressed)
            camPosition.X -= speed/2;

        if (_gamePadState.DPad.Right == ButtonState.Pressed)
            camPosition.X += speed/2;
    }

    public override void Update(GameTime gameTime)
    {
        _keyState = Keyboard.GetState();
        _gamePadState = GamePad.GetState(0); // single player

        GetKeyboardState();
        ProcessGamePadState();

        var gamePadState = GamePad.GetState(PlayerIndex.One);

        // ---------------------------------------------------------------------------
        // Player Camera Control 
        // ---------------------------------------------------------------------------
        // This is only adjusting the camera and not moving the player. This needs to 
        // be modified so that the right thumb-stick moves the camera and the left 
        // moves the player. 

        // ------------------------
        // Left Thumb-Stick
        // ------------------------
        //const float maxSpeed = .01f;
        //var changeInAngle = gamePadState.ThumbSticks.Left.Y * maxSpeed;

        //angle += changeInAngle;
        ////keyPressed += angle;

        //camPosition.Z += angle;
        ////camPosition.X += angle;
        ////camPosition.Z += speed;

        //world = Matrix.CreateRotationZ(gamePadState.ThumbSticks.Left.X * -1f);
        //camPosition.X += gamePadState.ThumbSticks.Left.X;

        // ------------------------
        // Right Thumb-Stick
        // ------------------------
        var speed = .01f;

        //camPosition.X -= gamePadState.ThumbSticks.Right.X * speed;
        //camPosition.Y -= gamePadState.ThumbSticks.Right.Y * speed;
        //camTarget.X -= gamePadState.ThumbSticks.Right.X * speed;
        //camTarget.Y -= gamePadState.ThumbSticks.Right.Y * speed;

        ////world = Matrix.CreateRotationY(gamePadState.ThumbSticks.Right.X);

        ////float changeInAngle = gamePadState.ThumbSticks.Left.X * maxSpeed;
        ////angle += changeInAngle;
        ////keyPressed += angle;

        // ---------------------------------------------------------------------------
        // Player Ship Control 
        // ---------------------------------------------------------------------------

        // -----------------------------
        //  Roll (left and right roll)
        // -----------------------------
        //if (_keyState.IsKeyDown(Keys.E))
        //world = Matrix.CreateRotationX((float)gameTime.TotalGameTime.TotalSeconds);
        
        //world *= Matrix.CreateRotationX(.01f);
        //world *= Matrix.CreateRotationX(gamePadState.ThumbSticks.Right.X);

        //if (_keyState.IsKeyDown(Keys.X))
        //world = Matrix.CreateRotationX((float)gameTime.TotalGameTime.TotalSeconds * -1);
        //world *= Matrix.CreateRotationX(.01f * -1);

        world *= Matrix.CreateRotationX(gamePadState.ThumbSticks.Right.Y * -1 * speed);
        world *= Matrix.CreateRotationY(gamePadState.ThumbSticks.Right.X * -1 * speed);

        // ------------------------------------
        //  Pitch (forward and backwards roll)
        // ------------------------------------
        //world *= Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds);
        if (_keyState.IsKeyDown(Keys.S))
            world *= Matrix.CreateRotationY(.01f);
        if (_keyState.IsKeyDown(Keys.D))
            world *= Matrix.CreateRotationY(.01f * -1);

        ////if (Keyboard.GetState().IsKeyDown(Keys.Space))
        ////{
        ////    orbit = !orbit;
        ////}

        ////if (orbit)
        ////{
        var rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(1f));
        //camPosition = Vector3.Transform(camPosition, rotationMatrix);
        ////}

        view = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);

        _prevKeyState = _keyState;
        _preGamePadState = _gamePadState;

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        DrawModel(model, world, view, projection);

        base.Draw(gameTime);
    }

    private static void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
    {

        //foreach (var bone in model.Bones)
        //{
        //    //bone.Transform(world, view, projection);
        //    //bone.ModelTransform(world, view, projection);
        //}

        foreach (var mesh in model.Meshes)
        {

            //mesh.ParentBone.Transform(world, view, projection);
            
            foreach (var effect1 in mesh.Effects)
            {
                var effect = (BasicEffect)effect1;
                effect.EnableDefaultLighting();

                //effect.LightingEnabled = true; // turn on the lighting subsystem.
                //effect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0, 0); // a red light
                //effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                //effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights

                //effect.DirectionalLight0.Enabled = true;

                //effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f); // Add some overall ambient light.
                //effect.EmissiveColor = new Vector3(1, 0, 0); // Sets some strange emissive lighting.  This just looks weird.

                //effect.Texture = otherTexture;
                //effect.TextureEnabled = true;

                //effect.FogEnabled = true;
                //effect.FogColor = Color.CornflowerBlue.ToVector3();
                //effect.FogStart = 9.75f;
                //effect.FogEnd = 10.25f;

                effect.World = world;
                effect.View = view;
                effect.Projection = projection;
            }

            mesh.Draw();
        }

        //model.Meshes[0].Draw();
    }

    //private void DrawGamePadView()
    //{
    //    var color = Color.White;

    //    SpriteBatch.Begin();

    //    // Draw popup window
    //    SpriteBatch.Draw(
    //        _rec,
    //        new Rectangle(5, 5, 500, 250),
    //    Color.CadetBlue * 0.5f);

    //    SpriteFontBase font = _fontSystem.GetFont(18);

    //    var binary = "";
    //    foreach (var item in _gamepadState)
    //        binary += item.ToString();

    //    SpriteBatch.DrawString(font, _gamepadState[0] == 1 ? "Connected" : "Disconnected", new Vector2(10, 15), color);

    //    SpriteBatch.DrawString(font, "X", new Vector2(10, 35), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadState[1]}", new Vector2(50, 35), color);
    //    SpriteBatch.DrawString(font, "Y", new Vector2(10, 55), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadState[2]}", new Vector2(50, 55), color);
    //    SpriteBatch.DrawString(font, "A", new Vector2(10, 75), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadState[3]}", new Vector2(50, 75), color);
    //    SpriteBatch.DrawString(font, "B", new Vector2(10, 95), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadState[4]}", new Vector2(50, 95), color);

    //    SpriteBatch.DrawString(font, "Back", new Vector2(10, 125), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadState[5]}", new Vector2(60, 125), color);

    //    SpriteBatch.DrawString(font, "Left Shoulder", new Vector2(10, 145), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadState[6]}", new Vector2(135, 145), color);
    //    SpriteBatch.DrawString(font, "Right Shoulder", new Vector2(10, 165), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadState[7]}", new Vector2(135, 165), color);

    //    SpriteBatch.DrawString(font, "DPad", new Vector2(10, 195), color);

    //    var dpString = "";
    //    if (_gamepadState[8] == 1) dpString = "Up";
    //    if (_gamepadState[9] == 1) dpString = "Down";
    //    if (_gamepadState[10] == 1)
    //    {
    //        if (!string.IsNullOrEmpty(dpString)) dpString += " & ";
    //        dpString += "Left";
    //    }
    //    if (_gamepadState[11] == 1)
    //    {
    //        if (!string.IsNullOrEmpty(dpString)) dpString += " & ";
    //        dpString += "Right";
    //    }

    //    SpriteBatch.DrawString(font, dpString, new Vector2(60, 195), color);

    //    SpriteBatch.DrawString(font, "Left Trigger", new Vector2(210, 35), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadValues[0]}", new Vector2(325, 35), color);
    //    SpriteBatch.DrawString(font, "Right Trigger", new Vector2(210, 55), color);
    //    SpriteBatch.DrawString(font, $"{_gamepadValues[1]}", new Vector2(325, 55), color);

    //    SpriteBatch.DrawString(font, "Left ThumbStick", new Vector2(210, 85), color);
    //    SpriteBatch.DrawString(font, $"({_gamepadValues[2]:N2},{_gamepadValues[3]:N2})", new Vector2(355, 85), color);
    //    SpriteBatch.DrawString(font, "Right ThumbStick", new Vector2(210, 105), color);
    //    SpriteBatch.DrawString(font, $"({_gamepadValues[4]:N2},{_gamepadValues[5]:N2})", new Vector2(355, 105), color);

    //    SpriteBatch.DrawString(font, $"Bit String: [{binary}]", new Vector2(10, 230), color);

    //    SpriteBatch.End();
    //}

}

public enum GamePadControls
{
    ThumbSticks_Right_X = 4,
    ThumbSticks_Right_Y = 5,

}