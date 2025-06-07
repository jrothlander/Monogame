using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Section_1;

public class Game : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager _graphics;
    public SpriteBatch SpriteBatch;

    //private int ScreenWidth { get; set; }
    //private int ScreenHeight { get; set; }

    public Game()
    {
        _graphics = new GraphicsDeviceManager(this);
        //new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Starfield Simulator";
    }

    protected override void Initialize()
    {
        //ScreenWidth = _graphics.PreferredBackBufferWidth;
        //ScreenHeight = _graphics.PreferredBackBufferHeight;

        SpriteBatch = new SpriteBatch(GraphicsDevice);
        Services.AddService(typeof(SpriteBatch), SpriteBatch);

        Components.Add(new FrameCounter(this));
        Components.Add( new Starfield.Starfield(this, new Texture2D(GraphicsDevice, 1, 1)));
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        base.Draw(gameTime);
    }

}