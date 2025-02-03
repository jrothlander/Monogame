using System.Collections.Generic;
using System.Linq;

namespace Xbox.Galaga;

public class FrameCounter
{
    public float AverageFps { get; private set; }
    public float CurrentFps { get; private set; }

    private readonly Queue<float> _queue = new();

    public void Update(float time)
    {
        CurrentFps = 1.0f / time;
        _queue.Enqueue(CurrentFps);

        if (_queue.Count > 100) 
            _queue.Dequeue();

        AverageFps = _queue.Average();
    }
}