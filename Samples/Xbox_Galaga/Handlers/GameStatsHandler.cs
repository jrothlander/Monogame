using System;
using System.Collections.Generic;

namespace Xbox.Galaga.Handlers;

public static class GameStatsHandler
{
    public static int Score { get; set; }
    public static int Lives { get; set; } = 3;
    public static decimal ShotsFired { get; set; } = 0.0M;
    public static decimal Hits { get; set; } = 0.0M;
    public static bool ScreenFlip { get; set; } = false;
    public static List<HighScores> HighScores { get; set; } = new();

    public static void Reset()
    {
        Score = 0;
        Lives = 3;
        ShotsFired = 0;
        Hits = 0;
        ScreenFlip = false;
    }
}

public class HighScores
{
    public string Name { get; set; }
    public Int64 HighScore { get; set; }
}