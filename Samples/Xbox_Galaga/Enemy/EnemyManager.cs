using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox.Galaga.Enemy;

public class EnemyManager
{
    private static Texture2D Texture { get; set; }
    private static Color TintColor { get; set; } = Color.White;

    private int ScreenWidth { get; set; }
    private int ScreenHeight { get; set; }

    private readonly Random _random = new();

    public List<Xbox.Galaga.Enemy.Enemy> Enemies = new();

    // Marching Timer
    private int _counter = 1;
    private const int Limit = 3;
    private const float CountDuration = .35f;
    private float _currentTime;
    private int _cellX = 6;
        
    private int _xAdj = 20;

    public EnemyManager(int screenWidth, int screenHeight, Texture2D spriteSheet, GraphicsDevice graphicsDevice)
    {
        Texture = spriteSheet;
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
    }
          
    public void Update(GameTime gameTime, int level, bool attackTimerElapsed)
    {
        _currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        for (var i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].Update(gameTime, level, Enemies.Count);
        }

        if (attackTimerElapsed)
        {
            SelectNextEnemyToAttack();
        }
    }

    public void Draw(SpriteBatch spriteBatch, int level)
    {
        if (_currentTime >= CountDuration)
        {
            _counter++;
            _currentTime -= CountDuration;
        }

        if (_counter >= Limit)
        {
            _counter = 0; //Reset the counter;
            if (_cellX == 6)
            {
                _cellX = 7;
                //_xAdj = 5; // you can adjust the movement here but the collision logic will be off 
            }
            else
            {
                _cellX = 6;
                //_xAdj = -5;
            }
        }

        for (var i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].Draw(spriteBatch, _cellX, _xAdj, level);
        }
    }

    public void SelectNextEnemyToAttack()
    {
        if (Enemies.Count <= 0) return;

        // Randomly pick an enemy from the hive to start its attack
        var rndInd = _random.Next(0, Enemies.Count);
        var ranEnemy = Enemies[rndInd];
            
        if (ranEnemy != null) 
            ranEnemy.Attacking = true;

    }

    public void Reset()
    { 
        Enemies = new List<Xbox.Galaga.Enemy.Enemy>();    
    }
}