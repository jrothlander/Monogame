using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox.Galaga.Starfield;

internal class Starfield
{
    private readonly List<Star> _stars = new();
    private const int StarsCount = 35;

    public Starfield(int screenWidth, int screenHeight, Texture2D texture)
    {
        for (var i = 0; i < StarsCount; i++)
        {
            _stars.Add(
                new Star(texture, screenWidth, screenHeight)
            );
        }
    }
    public void Update(GameTime gameTime)
    {
        foreach (var star in _stars)
        {
            star.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var star in _stars)
        {
            star.Draw(spriteBatch);
        }
    }
}