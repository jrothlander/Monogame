using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xbox.Galaga.Handlers;
using Xbox.Galaga.Models;

namespace Xbox.Galaga.Player;

public class Player
{
    private int ScreenWidth { get; }
    private int ScreenHeight { get; }
    private Texture2D Texture { get; set; }
    private Color TintColor { get; set; }

    public Vector2 Position;

    public int Width { get; set; } = 32;
    public int Height { get; set; } = 32;
    public float Speed { get; set; } = 200f;
    public int Lives { get; set; } = 3;
    public bool IsAlive { get; set; } = true;

    private KeyboardState _keyState, _prevKeyState;

    public List<Projectile> Projectiles = new();

    // Marching Timer
    private int _counter = 1;
    private const int Limit = 3;
    private const float CountDuration = .35f;
    private float _currentTime;
    private bool _resetPlayerTimerElapsed;

    private static Rectangle InitialFrame =>
        new(
            109,   // x position of sprite on spriteSheet
            1,     // y position of sprite on spriteSheet
            16,    // width of sprite on spriteSheet
            16     // height of sprite on spriteSheet
        );

    private Rectangle Destination =>
        new(
            (int)Position.X,
            ScreenHeight - 75, // TODO... remove hard coding from here
            Width, // width of sprite when drawn on screen  (can scale here)
            Height // height of sprite when drawn on screen (can scale here)
        );

    public Player(int screenWidth, int screenHeight, Texture2D texture, Color tintColor)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        Texture = texture;
        Position = new Vector2((float)screenWidth / 2, (float)screenHeight - 150);
        TintColor = tintColor;
        //CollisionHandler = new CollisionHandler(screenWidth, screenHeight, texture, tintColor);
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsAlive)
        {
            spriteBatch.Draw(
                Texture,
                Destination,
                InitialFrame,
                TintColor
            );
        }

        foreach (var projectile in Projectiles)
        {
            projectile.Draw(spriteBatch);
        }

        // Reset player after dying
        if (Lives > 0 && !IsAlive && _resetPlayerTimerElapsed)
        {
            IsAlive = true;
            Lives--;
            _resetPlayerTimerElapsed = false;
        }
    }

    public void Update(GameTime gameTime) //, EnemyManager enemyManager)
    {
        _keyState = Keyboard.GetState();

        if (_keyState.IsKeyDown(Keys.Up))
            Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_keyState.IsKeyDown(Keys.Down))
            Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_keyState.IsKeyDown(Keys.Left) && Position.X > 0)
            Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_keyState.IsKeyDown(Keys.Right) && Position.X < ScreenWidth - Width)
            Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_keyState.IsKeyDown(Keys.Space) && _prevKeyState.IsKeyUp(Keys.Space))
        {
            GameStatsHandler.ShotsFired++;

             Projectiles.Add(new Projectile(
                            Position,
                            Texture, 
                            TintColor
                        )
                );

            SoundManager.PlayerFiringSound.CreateInstance();
            SoundManager.PlayerFiringSound.Play();
        }

        foreach (var projectile in Projectiles)
        {
            projectile.Update(gameTime);
        }

        // ------------------
        // Player Reset Timer 
        // ------------------
        _currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_currentTime >= CountDuration)
        {
            _counter++;
            _currentTime -= CountDuration;
        }

        if (_counter >= Limit && !_resetPlayerTimerElapsed)
        {
            _counter = 0; //Reset the counter;
            _resetPlayerTimerElapsed = true;
        }

        _prevKeyState = _keyState;
    }

}