using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox.Galaga.Handlers;

public class TitleScreenHandler
{

    private int ScreenWidth { get; set; }
    private int ScreenHeight { get; set; }
    private Texture2D SpriteSheet { get; set; }
    private Texture2D TitleSheet { get; set; }

    private static int _counter = 1;
    private const int Limit = 3;
    private const float CountDuration = 2f; //every  2s.
    private static float _currentTime = 0f;

    private Screens.Screens _screens;
    private int _screenIndex;

    public void Initialize(int screenWidth, int screenHeight, Texture2D spriteSheet, Texture2D titleSheet)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        SpriteSheet = spriteSheet;
        TitleSheet = titleSheet;

        _screens = new Screens.Screens();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (GameStatsHandler.ScreenFlip)
        {
            GameStatsHandler.ScreenFlip = false;
            _screenIndex++;
            if (_screenIndex >= 4) _screenIndex = 0;
        }

        //if (GameStatsHandler.ScreenFlip)
        //    _screens.DrawScoreScreen(spriteBatch, SpriteSheet, ScreenWidth);
        ////_screens.DrawHighScoresScreen(spriteBatch, SpriteSheet, ScreenWidth); // Add or replace with the top-5 high-scores screen
        //else
        //    _screens.DrawSplashScreen(spriteBatch, TitleSheet, ScreenWidth, ScreenHeight);

        switch (_screenIndex)
        {
            case 0:
                _screens.DrawSplashScreen(spriteBatch, TitleSheet, ScreenWidth, ScreenHeight);
                break;
            case 1:
                _screens.DrawScoreScreen(spriteBatch, SpriteSheet, ScreenWidth);
                break;
            case 2:
                _screens.DrawHighScoresScreen(spriteBatch, SpriteSheet, ScreenWidth); // Add or replace with the top-5 high-scores screen
                break;
            case 3:
                _screens.DrawBonusLivesScreen(spriteBatch, SpriteSheet, ScreenWidth);
                break;
        }

    }

    public void Update(GameTime gameTime)
    {

        _currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_currentTime >= CountDuration)
        {
            _counter++;
            _currentTime -= CountDuration;
        }

        if (_counter >= Limit)
        {
            _counter = 0; //Reset the counter;
            GameStatsHandler.ScreenFlip = !GameStatsHandler.ScreenFlip; 
        }
    }
}
