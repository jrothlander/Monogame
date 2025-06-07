using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Section_1.Starfield;

internal class Starfield : DrawableGameComponent
{
    private GraphicsDeviceManager Graphics { get; }
    private SpriteBatch SpriteBatch { get; }

    private readonly List<Star> _stars = new();
    private const int StarsCount = 35;
    private readonly Game _game;
    private readonly Point _screen;
    private readonly Texture2D _texture;
    
    public Starfield(Game game, Texture2D texture) : base(game)
    {
        _game = game;
        _texture = texture;

        Graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
        if (Graphics == null) return; 

        SpriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
        if (SpriteBatch == null || Graphics == null) return;

        _screen.X = Graphics.PreferredBackBufferWidth;
        _screen.Y = Graphics.PreferredBackBufferHeight;
    }

    public override void Initialize()
    {
        for (var i = 0; i < StarsCount; i++)
        {
            _stars.Add(new Star(_texture, _screen));
        }

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var star in _stars)
        {
            star.Update(gameTime);
        }

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        //_game.SpriteBatch.Begin();
        SpriteBatch.Begin();
        foreach (var star in _stars)
        {
            //star.Draw(_game.SpriteBatch); // Accesses Spritebatch from the Game class's public spritebatch property.
            star.Draw(SpriteBatch); // Accesses Spritebatch from the registered services.
        }
        //_game.SpriteBatch.End();
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}