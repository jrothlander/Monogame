// https://stackoverflow.com/questions/20676185/xna-monogame-getting-the-frames-per-second
// I wrote my own version of this and didn't like it, then found this example on Stack Overflow where the user
// did a job good with this one and I just implemented their version with a few tweaks.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Section_1;

public class FrameCounter : DrawableGameComponent
{
    public float AverageFramesPerSecond { get; private set; }
    public float CurrentFramesPerSecond { get; private set; }

    public const int MaximumSamples = 100;

    private readonly Queue<float> _sampleBuffer = new();

    private readonly Microsoft.Xna.Framework.Game _game;

    public FrameCounter(Microsoft.Xna.Framework.Game game) : base(game)
    {
        _game = game;
    }

    public override void Draw(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Update(deltaTime);

        _game.Window.Title = "Starfield Generator";
        if (gameTime.IsRunningSlowly)
        {
           _game.Window.Title += $" - Lagging FPS({AverageFramesPerSecond:.##})";
        }
    }

    public void Update(float deltaTime)
    {
        CurrentFramesPerSecond = 1.0f / deltaTime;

        _sampleBuffer.Enqueue(CurrentFramesPerSecond);

        if (_sampleBuffer.Count > MaximumSamples)
        {
            _sampleBuffer.Dequeue();
            AverageFramesPerSecond = _sampleBuffer.Average(i => i);
        }
        else
        {
            AverageFramesPerSecond = CurrentFramesPerSecond;
        }
    }

}