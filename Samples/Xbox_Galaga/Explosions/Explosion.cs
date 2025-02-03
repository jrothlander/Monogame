namespace Xbox.Galaga.Explosions;

internal class Explosion
{
    public float X { get; set; }
    public float Y { get; set; }
    public int CurrentFrame { get; set; }
    public int NumFrames { get; set; }
    public int AnimCount { get; set; }
    public int AnimCountMax { get; set; }
    public ShipType Type { get; set; }
}