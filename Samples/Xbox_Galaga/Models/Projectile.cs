using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox.Galaga.Models;

public class Projectile
{

    public Vector2 Location;
    public int Speed = 400;
    public int Width = 5;
    public int Height = 11;

    private Texture2D Texture { get; }
    private Color TintColor { get; }

    private static Rectangle InitialFrame =>
        new(
            312,    // x position of sprite on spriteSheet
            121,    // y position of sprite on spriteSheet
            5,      // width of sprite on spriteSheet
            11      // height of sprite on spriteSheet
        );

    private Rectangle Destination =>
        new(
            (int)Location.X + 10,
            (int)Location.Y - 10,
            5, // width of sprite when drawn on screen  (can scale here)
            11 // height of sprite when drawn on screen (can scale here)
        );


    public Projectile(Vector2 position, Texture2D texture, Color tintColor)
    {
        Texture = texture;
        Location = new Vector2(position.X + 3, position.Y); // adding an offset of 3 to position the projectile on the players ship.
        TintColor = tintColor;
    }

    public void Update(GameTime gameTime)
    {
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Location.Y -= dt * Speed;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            Destination,
            InitialFrame,
            TintColor);
    }

}