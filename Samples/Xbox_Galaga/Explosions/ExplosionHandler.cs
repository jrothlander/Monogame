using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xbox.Galaga.Handlers;

namespace Xbox.Galaga.Explosions;

public enum ShipType
{
    Enemy = 1,
    Player = 2
}

internal class ExplosionHandler
{
    public static List<Explosion> Explosions = new(); // Need to pass in to maintain these outside of ExpHandler

    private Texture2D Texture { get; set; }
    private Color TintColor { get; set; }

    //var enemyExpSound = [new Audio(), new Audio(), new Audio(), new Audio()];
    //enemyExpSound[0].src = "./sounds/enemy_explosion.mp3";
    //enemyExpSound[1].src = "./sounds/enemy_explosion.mp3";
    //enemyExpSound[2].src = "./sounds/enemy_explosion.mp3";
    //enemyExpSound[3].src = "./sounds/enemy_explosion.mp3";

    public ExplosionHandler(int screenWidth, int screenHeight, Texture2D spriteSheet, Color color)
    {
        Texture = spriteSheet;
        TintColor = color;
    }

    public void AnimateExplosions(SpriteBatch spriteBatch)
    {
        for (var i = 0; i < Explosions.Count; i++)
        {
            Explosions[i].AnimCount++; // update animation frame/position discretely        
            if (Explosions[i].AnimCount >= Explosions[i].AnimCountMax)
            {
                Explosions[i].AnimCount = 0; // reset counter             
                if (++Explosions[i].CurrentFrame >= Explosions[i].NumFrames)
                {
                    Explosions.Remove(Explosions[i]); // terminate & remove explosion
                } // end if
            } // end if
        } // end each explosion

        // Draw all explosions
        foreach (var explosion in Explosions)
        {
            DrawExplosionFromSpriteSheet(spriteBatch, explosion);
        }
    }

    public void HandleEnemyExplosion(Xbox.Galaga.Enemy.Enemy enemy)
    {
        Explosions.Add(new Explosion
        {
            X = enemy.Location.X,  // x-position of explosion
            Y = enemy.Location.Y,  // y-position of explosion  
            CurrentFrame = 0,      // current frame of explosion
            NumFrames = 5,         // number of frames for explosion
            AnimCount = 0,         // counts frames until the next frame is shown
            AnimCountMax = 3,      // threshold to advance to next frame (sets speed)
            Type = ShipType.Enemy  // Mark this as an enemy ship
        });

        // Play explosion sound effect  - scan for available sound
        //for (var sound of enemyExpSound)
        //{
        //    if (sound.ended || sound.currentTime == 0)
        //    {
        //        sound.volume = 0.5;
        //        sound.play();
        //        break;
        //    } // end if
        //} // for
        SoundManager.EnemyExplosionSound.Play();
    }

    public void HandlePlayerExplosion(Player.Player player)
    {
        Explosions.Add(new Explosion
        {
            X = player.Position.X,  // x position of explosion
            Y = player.Position.Y,  // y position of explosion
            CurrentFrame = 0,          // current frame of explosion
            NumFrames = 4,          // number of frames for explosion
            AnimCount = 0,          // counts the frames until the next frame is shown
            AnimCountMax = 12,      // threshold to advance to next frame (speed)
            Type = ShipType.Player // Mark this as a player ship
        });
        SoundManager.PlayerExplosionSound.Play();
    }

    public void DrawExplosionFromSpriteSheet(SpriteBatch spriteBatch, Explosion exp)
    {
        var spriteSheetXPos = 289;
        if (exp.Type == ShipType.Player) spriteSheetXPos = 145;

        const int borderWidth = 1;
        const int spriteWidth = 32; // same as height

        var offsetX = (((exp.CurrentFrame * borderWidth) * 2)
                       + (exp.CurrentFrame * spriteWidth) + spriteSheetXPos);
        const int offsetY = 1;

        spriteBatch.Draw(
            Texture,
            new Rectangle(
                (int)exp.X +2, (int)exp.Y - spriteWidth/2, // the y-cords are wrong because of the ship origin. We have to subtract half the width.
                spriteWidth,
                spriteWidth
            ),
            new Rectangle(offsetX, offsetY, spriteWidth, spriteWidth),
            TintColor);
    }
} // class ExplosionHandler