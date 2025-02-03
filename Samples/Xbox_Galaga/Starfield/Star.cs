using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox.Galaga.Starfield;

internal class Star
{
    public int Width;
    public int Height;
    public Texture2D Texture;

    public Vector2 Location { get; set; }
    private Vector2 Velocity { get; }

    public Color Color = Color.White;

    private int ScreenWidth { get; set; }
    private int ScreenHeight { get; set; }

    private readonly Random _random = new();

    public Star(Texture2D texture, int screenWidth, int screenHeight)
    {
        Texture = texture;  
        Texture.SetData(new [] { Color });

        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;

        Location = new Vector2(_random.Next(0, screenWidth), _random.Next(0, screenHeight));

        var size = _random.Next(1, 3);
        Width = size;
        Height = size;

        Velocity = new Vector2(0, _random.Next(50, 100));
    }

    public void Update(GameTime gameTime)
    {
        var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Location += Velocity * elapsed;

        Color = new Color(
            _random.Next(0, 255),
            _random.Next(0, 255),
            _random.Next(0, 255)
        );

        // Screen Location Logic
        if (Location.Y > ScreenHeight)
            Location = new Vector2(_random.Next(0, ScreenWidth), 0);

        if (Location.X > ScreenWidth)
            Location = new Vector2(0, Location.Y);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            new Rectangle((int)Location.X, (int)Location.Y, Width, Height),
            Color
        );
    }
}