using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xbox.Galaga.Explosions;
using Xbox.Galaga.Models;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Xbox.Galaga.Handlers;

public class CollisionHandler
{

    private readonly ExplosionHandler _explosionHandler;

    public CollisionHandler(int screenWidth, int screenHeight, Texture2D spriteSheet, Color color)
    {
        _explosionHandler = new ExplosionHandler(screenWidth, screenHeight, spriteSheet, color);
    }

    public void Update(Player.Player player, List<Projectile> projectiles, List<Xbox.Galaga.Enemy.Enemy> enemies, int level)
    {

        for (var i = 0; i < projectiles.Count; i++)
        {
            for (var j = 0; j < enemies.Count; j++)
            {

                if (ProjectileCollision(enemies[j], projectiles[i]))
                {
                    projectiles.Remove(projectiles[i]);
                    _explosionHandler.HandleEnemyExplosion(enemies[j]);

                    enemies.Remove(enemies[j]);
                    SoundManager.EnemyExplosionSound.Play();

                    GameStatsHandler.Hits++;

                    switch (level)
                    {
                        case 3:
                            GameStatsHandler.Score += 50;
                            break;
                        case 4:
                            GameStatsHandler.Score += 80;
                            break;
                        default:
                            GameStatsHandler.Score += 10;
                            break;
                    }

                    break;
                }
            }
        }

        if (!player.IsAlive) return;

        // Player to Enemy ship collision
        for (var j = 0; j < enemies.Count; j++)
        {
            if (ShipCollision(enemies[j], player))
            {
                player.IsAlive = false;
                _explosionHandler.HandlePlayerExplosion(player);
            }
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _explosionHandler.AnimateExplosions(spriteBatch); // Run existing explosion that have been queued.
    }

    private static bool ProjectileCollision(Xbox.Galaga.Enemy.Enemy e, Projectile p)
    {
        if (e == null || p == null) return false;

        return e.Location.X + e.Width > p.Location.X        // Enemy is to the right of projectile
                && e.Location.X < p.Location.X + p.Width     // Enemy is to the left of projectile
                && e.Location.Y + e.Height > p.Location.Y    // Enemy is below projectile 
                && e.Location.Y < p.Location.Y + p.Height;  // Enemy is above projectile
    }

    private static bool ShipCollision(Xbox.Galaga.Enemy.Enemy a, Player.Player b)
    {
        if (a == null || b == null) return false;

        return a.Location.X < b.Position.X + b.Width &&
               a.Location.X + a.Width > b.Position.X &&
               a.Location.Y < b.Position.Y + b.Height &&
               a.Location.Y + a.Height > b.Position.Y;
    }
}