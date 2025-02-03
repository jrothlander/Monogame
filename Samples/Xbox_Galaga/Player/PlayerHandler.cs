using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xbox.Galaga.Handlers;

namespace Xbox.Galaga.Player;

internal class PlayerHandler
{

    public Player Player;
    public bool PlayerResetTimerElapsed = false;

    private int _counter = 1;
    private const int Limit = 3;
    private const float CountDuration = 2f; // every 2s.
    private float _currentTime = 0f;
    private bool _onlyRunOnce = false;

    private bool _firstBonusLife = false;
    private bool _secondBonusLife = false;
    private bool _thirdBonusLife = false;

    public void Initialize(int screenWidth, int screenHeight, Texture2D spriteSheet, Color color)
    {
        Player = new Player(screenWidth, screenHeight, spriteSheet, color);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Player.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        if (Player.IsAlive)
        {
            Player.Update(gameTime);
        }

        if (GameStatsHandler.Lives > 0 && Player.IsAlive)
        {
            if (GameStatsHandler.Score >= 25000 && !_secondBonusLife)
            {
                _secondBonusLife = true;
                GameStatsHandler.Lives++;
            }  
            else if (GameStatsHandler.Score >= 10000 && !_firstBonusLife)
            {      
                _firstBonusLife = true;
                GameStatsHandler.Lives++;
            }
        }

        if (GameStatsHandler.Lives > 0 && !Player.IsAlive && PlayerResetTimerElapsed)
        {
            // player died... wait 5 seconds and reset player      
            Player.IsAlive = true;
            GameStatsHandler.Lives--;
            PlayerResetTimerElapsed = false;
            return;
        }

        // --------------
        // Attack Timer 
        // --------------

        if (Player.IsAlive) return;

        _currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_currentTime >= CountDuration)
        {
            _counter++;
            _currentTime -= CountDuration;
        }

        if (_counter >= Limit && !PlayerResetTimerElapsed && !Player.IsAlive)
        {
            _counter = 0; //Reset the counter;
            PlayerResetTimerElapsed = true;
            //Player.IsAlive = true;
        }
    }
}