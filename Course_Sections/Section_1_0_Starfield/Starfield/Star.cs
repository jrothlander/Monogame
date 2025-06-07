using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Section_1.Starfield;

internal class Star
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Texture2D Texture { get; set; }
    public Vector2 Location { get; set; }
    public Vector2 Velocity { get; }
    public Color Color { get; set; } = Color.White;

    private Point Screen { get; set; }

    private readonly Random _random = new();

    public Star(Texture2D texture, Point screen) 
    {
        Screen = screen;
        Texture = texture;
        Texture.SetData(new[] { Color });
        
        Location = new Vector2(_random.Next(0, screen.X), _random.Next(0, screen.Y));

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
        if (Location.Y > Screen.Y)
            Location = new Vector2(_random.Next(0, Screen.X), 0);

        if (Location.X > Screen.X)
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