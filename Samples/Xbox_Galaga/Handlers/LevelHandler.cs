using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xbox.Galaga.Enemy;

namespace Xbox.Galaga.Handlers;

public interface ILevelHandler
{
    
}

public class LevelHandler : ILevelHandler
{
    public int Round { get; set; } = 1;
    public int Level { get; set; } = 0;
    private int ScreenWidth { get; set; }
    private int ScreenHeight { get; set; }
    private Texture2D SpriteSheet { get; set; }
    private Color Tint { get; set; }

    private const int NumberOfEnemiesPerRound = 8;
    private const int SpaceBetweenEnemyShips = 10;
    private const int EnemyShipConvergenceHiveTop = 150;
    private const int EnemyShipWidth = 32;

    public bool RoundCompleted { get; set; }

    static int counter = 1;
    static int limit = 3;
    static float countDuration = 2f; //every  2s.
    static float currentTime = 0f;

    private bool AttackTimerElapsed = false;
    private readonly Random Rnd = new();

    private EnemyManager _enemyManager;
    private CollisionHandler _collisionHandler;

    public void Initialize(int screenWidth, int screenHeight, Texture2D spriteSheet, Color color, GraphicsDevice graphicsDevice)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        SpriteSheet = spriteSheet;
        Tint = color;

        _enemyManager = new EnemyManager(ScreenWidth, ScreenHeight, spriteSheet, graphicsDevice);
        _collisionHandler = new CollisionHandler(screenWidth, screenHeight, spriteSheet, color);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _enemyManager.Draw(spriteBatch, Level);
        _collisionHandler.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime, Player.Player player, GraphicsDevice graphicsDevice)
    {

        _enemyManager.Update(gameTime, Level, AttackTimerElapsed);

        _collisionHandler.Update(player, player.Projectiles, _enemyManager.Enemies, Level);

        AttackTimerElapsed = false;

        if (_enemyManager.Enemies.Count == 0)
        {
            RoundCompleted = false;
            for (var i = 0; i < NumberOfEnemiesPerRound; i++)
            {

                var screenCenter = ScreenWidth * .5;
                const int enemyShipsCenter = (int)((42 * (NumberOfEnemiesPerRound - 1)) * 0.5);
                var initialEnemyShipPositionX = screenCenter - enemyShipsCenter;

                _enemyManager.Enemies.Add(
                    new Xbox.Galaga.Enemy.Enemy(
                        ScreenWidth, ScreenHeight,
                        new Vector2((float)(i * (EnemyShipWidth + SpaceBetweenEnemyShips) + initialEnemyShipPositionX), EnemyShipConvergenceHiveTop),
                        SpriteSheet,
                        Tint,
                        graphicsDevice
                    )
                );
            }

            Level++;
            SoundManager.LevelStartSound.Play();
        }

        // --------------
        // Attack Timer 
        // --------------
        currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (currentTime >= countDuration)
        {
            counter++;
            currentTime -= countDuration;
        }

        if (counter >= limit && !AttackTimerElapsed)
        {
            counter = 0; //Reset the counter;
            AttackTimerElapsed = true;
        }

        if (Level != 5) return;

        Level = 1;
        //MySounds.RoundStartSound.Play();

        Round++;
        RoundCompleted = true;
    }

    public void Reset()
    {
        AttackTimerElapsed = false;
        Round = 1;
        RoundCompleted = false;
        Level = 1;

        _enemyManager.Reset();
    }

}