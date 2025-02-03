using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xbox.Galaga.Handlers;

namespace Xbox.Galaga.Screens;

public class Screens
{

    public void DrawSplashScreen(SpriteBatch spriteBatch, Texture2D spriteSheet, int screenWidth, int screenHeight)
    {
        const int halfFontWidth = 4;
        double halfScreenWidth = screenWidth * 0.5;

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(screenWidth / 2 - (spriteSheet.Width * 2) / 2, screenHeight / 2 - (spriteSheet.Height * 2) / 2, spriteSheet.Width * 2, spriteSheet.Height * 2),
            Color.White
        );

        TextToSpriteHandler.DrawString(spriteBatch, "spacebar to begin", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 400, 8, 8);
    }

    public void DrawScoreScreen(SpriteBatch spriteBatch, Texture2D spriteSheet, int screenWidth)
    {
        const int halfFontWidth = 4;
        double halfScreenWidth = screenWidth * 0.5;

        TextToSpriteHandler.DrawString(spriteBatch, "galaga", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 100, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "-- score --", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 125, 8, 8);

        TextToSpriteHandler.DrawString(spriteBatch, "10", Color.Aqua, 415, 175, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "10", Color.Aqua, 415, 225, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "50", Color.Aqua, 415, 275, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "80", Color.Aqua, 415, 325, 8, 8);

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(370, 160, 32, 32),
            new Rectangle(109, 37, 16, 16),
            Color.White
        );

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(370, 210, 32, 32),
            new Rectangle(109, 55, 16, 16),
            Color.White
        );

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(370, 260, 32, 32),
            new Rectangle(109, 73, 16, 16),
            Color.White
        );

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(370, 310, 32, 32),
            new Rectangle(109, 91, 16, 16),
            Color.White
        );

        TextToSpriteHandler.DrawString(spriteBatch, "spacebar to begin", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 400, 8, 8);
    }

    public void DrawHighScoresScreen(SpriteBatch spriteBatch, Texture2D spriteSheet, int screenWidth)
    {
        const int halfFontWidth = 4;
        var halfScreenWidth = screenWidth * 0.5;

        TextToSpriteHandler.DrawString(spriteBatch, "the galactic heroes", Color.Blue,
            (int)halfScreenWidth, halfFontWidth, 105, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "-- best 5 --", Color.Red,
            (int)halfScreenWidth, halfFontWidth, 140, 8, 8);

        TextToSpriteHandler.DrawString(spriteBatch, "score", Color.Aqua, 375, 175, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "name", Color.Aqua, 450, 175, 8, 8);

        TextToSpriteHandler.DrawString(spriteBatch, "1st", Color.Aqua, 315, 200, 8, 8);
        if (GameStatsHandler.HighScores.Count > 1)
        {
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[0].HighScore.ToString(), Color.Aqua, 375, 200, 8, 8);
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[0].Name, Color.Aqua, 450, 200, 8, 8);
        }

        TextToSpriteHandler.DrawString(spriteBatch, "2nd", Color.Aqua, 315, 225, 8, 8);
        if (GameStatsHandler.HighScores.Count > 2)
        {
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[1].HighScore.ToString(), Color.Aqua, 375, 225, 8, 8);
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[1].Name, Color.Aqua, 450, 225, 8, 8);
        }

        TextToSpriteHandler.DrawString(spriteBatch, "3rd", Color.Aqua, 315, 250, 8, 8);
        if (GameStatsHandler.HighScores.Count > 3)
        {

            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[2].HighScore.ToString(), Color.Aqua, 375, 250, 8, 8);
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[2].Name, Color.Aqua, 450, 250, 8, 8);
        }

        TextToSpriteHandler.DrawString(spriteBatch, "4th", Color.Aqua, 315, 275, 8, 8);
        if (GameStatsHandler.HighScores.Count > 4)
        {
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[3].HighScore.ToString(), Color.Aqua, 375, 275, 8, 8);
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[3].Name, Color.Aqua, 450, 275, 8, 8);
        }

        TextToSpriteHandler.DrawString(spriteBatch, "5th", Color.Aqua, 315, 300, 8, 8);
        if (GameStatsHandler.HighScores.Count > 5)
        {
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[4].HighScore.ToString(), Color.Aqua, 375, 315, 8, 8);
            TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.HighScores[4].Name, Color.Aqua, 450, 315, 8, 8);
        }

        TextToSpriteHandler.DrawString(spriteBatch, "spacebar to begin", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 400, 8, 8);
    }

    public void DrawResultsScreen(SpriteBatch spriteBatch, Texture2D spriteSheet, int screenWidth)
    {
        const int halfFontWidth = 4;
        double halfScreenWidth = screenWidth * 0.5;

        //TextToSpriteHandler.DrawString(spriteBatch, "game over", Color.Red, (int)((int)(screenWidth * 0.5) - 9 * 0.5 * 8), 100, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "game over", Color.Red, (int)halfScreenWidth, halfFontWidth, 100, 8, 8);

        //TextToSpriteHandler.DrawString(spriteBatch, "-- results --", Color.Red, (int)((int)(screenWidth * 0.5) - 12 * 0.5 * 8), 165, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "-- results --", Color.Red, (int)halfScreenWidth, halfFontWidth, 165, 8, 8);

        TextToSpriteHandler.DrawString(spriteBatch, "shots fired", Color.Yellow, 310, 200, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.ShotsFired.ToString(CultureInfo.InvariantCulture), Color.Yellow, 465, 200, 8, 8);

        TextToSpriteHandler.DrawString(spriteBatch, "number of hits", Color.Yellow, 310, 225, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, GameStatsHandler.Hits.ToString(CultureInfo.InvariantCulture), Color.Yellow, 465, 225, 8, 8);

        TextToSpriteHandler.DrawString(spriteBatch, "hit-miss ratio", Color.White, 310, 250, 8, 8);

        decimal hitMissRatio = 0;
        if (GameStatsHandler.Hits > 0 && GameStatsHandler.ShotsFired > 0)
            hitMissRatio = (int)(GameStatsHandler.Hits / GameStatsHandler.ShotsFired * 100);

        TextToSpriteHandler.DrawString(spriteBatch, $"{hitMissRatio} %", Color.White, 465, 250, 8, 8);

        //TextToSpriteHandler.DrawString(spriteBatch, "enter to restart", Color.Aqua, (int)((int)(screenWidth * 0.5) - 16 * 0.5 * 8), 325, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "enter to restart", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 325, 8, 8);
    }

    public void DrawBonusLivesScreen(SpriteBatch spriteBatch, Texture2D spriteSheet, int screenWidth)
    {

        const int halfFontWidth = 4;
        double halfScreenWidth = screenWidth * 0.5;
        //TextToSpriteHandler.DrawString(spriteBatch, "player lives bonus", Color.Aqua, (int)((int)(screenWidth * 0.5) - (12 * 0.5 * 8)), 155, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "player lives bonus", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 155, 8, 8);

        TextToSpriteHandler.DrawString(spriteBatch, "1st bonus for 10000 pts", Color.Yellow, 350, 200, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "2nd bonus for 25000 pts", Color.Yellow, 350, 245, 8, 8);
        TextToSpriteHandler.DrawString(spriteBatch, "and for every 50000 pts", Color.Yellow, 350, 290, 8, 8);

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(300, 185, 32, 32),
            new Rectangle(109, 1, 16, 16),
            Color.White
        );

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(300, 230, 32, 32),
            new Rectangle(109, 1, 16, 16),
            Color.White
        );

        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(300, 275, 32, 32),
            new Rectangle(109, 1, 16, 16),
            Color.White
        );

        TextToSpriteHandler.DrawString(spriteBatch, "spacebar to begin", Color.Aqua, (int)halfScreenWidth, halfFontWidth, 400, 8, 8);

    }

}