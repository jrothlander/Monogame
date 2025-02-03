using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xbox.Galaga.Enemy;

namespace Xbox.Galaga.Enemy;

public class Enemy
{
    private GraphicsDevice _graphicsDevice; // passed to create bounding box.
    private EnemyPatternEngine _patternEngine;

    private int ScreenWidth { get; set; }
    private int ScreenHeight { get; set; }
    private Texture2D Texture { get; }
    private Color TintColor { get; set; }
    public Vector2 Location;

    public int Width { get; set; } = 32;
    public int Height { get; set; } = 32;
    public bool Attacking { get; set; } = false;
    public float Angle { get; set; }
    public int CellX { get; set; }

    public Rectangle Destination =>
        new(
            (int)Location.X,
            (int)Location.Y,
            InitialFrame.Width * 2,
            InitialFrame.Height * 2
        );

    private static Rectangle InitialFrame =>
        new(
            109, // x position of sprite on spriteSheet
            1,   // y position of sprite on spriteSheet
            16,  // width of sprite on spriteSheet
            16   // height of sprite on spriteSheet
        );

    public int Cntr { get; set; }
    public int RepeatCntr { get; set; }

    public float SpeedX { get; set; } = 3;
    public float SpeedY { get; set; } = 3;

    private enum ShipType
    {
        Player = 0,
        PlayerRed,
        Bug,
        BlueBug,
        Moth,
        Wasp,
        Scorpion,
        Arrowhead,
        Phoenix,
        DragonFly,
        Spinner,
        Enterprise
    };

    public Enemy(int screenWidth, int screenHeight, Vector2 location, Texture2D texture, Color tintColor, GraphicsDevice graphicsDevice)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        Location = location;
        Texture = texture;
        TintColor = tintColor;
        _graphicsDevice = graphicsDevice;

        _patternEngine = new EnemyPatternEngine(screenWidth, screenHeight);
    }

    public void Update(GameTime gameTime, int level, int enemyCount)
    {
        _patternEngine.ProcessPattern(EnemyPatternEngine.PatternState.Hive, this, gameTime, level, enemyCount);
    }

    public void Draw(SpriteBatch spriteBatch, int cellX, int xAdj, int level)
    {
        //Location = new Vector2(Location.X + xAdj, Location.Y);

        DrawFromSpriteSheet(
            spriteBatch,
            Texture,
            cellX, level + 1,
            (int)Location.X + xAdj, (int)Location.Y,
            2,
            InitialFrame.Width,
            InitialFrame.Height
        );

    }

    private void DrawFromSpriteSheet(SpriteBatch spriteBatch, Texture2D spriteSheet, int cellX, int cellY, int x, int y, int borderWidth, int spriteWidth, int spriteHeight)
    {
        var offsetX = (cellX + 1) * borderWidth + cellX * spriteWidth - 1;
        var offsetY = (cellY + 1) * borderWidth + cellY * spriteWidth - 1;

        //var texture = new Texture2D(GraphicsDevice, 1, 1);
        //texture.SetData(new[] { Color.White });

        //// draw bounding box
        //spriteBatch.Draw(
        //    texture,
        //    new Rectangle(x, y, Width, Height),                             // Destination 
        //    new Rectangle(offsetX, offsetY, spriteWidth, spriteHeight),     // Source
        //    Color.White * 0.2f,
        //    Angle,
        //    new Vector2((float)spriteWidth / 2, (float)spriteHeight / 2),   // Origin in the middle of the sprite
        //    SpriteEffects.None,
        //    0                                                               // Layer
        //);

        // Draw the enemy ship
        spriteBatch.Draw(
            spriteSheet,
            new Rectangle(
                x - 1, y, // it is off by -1 for some reason. Should resolve this. 
                spriteWidth * 2, spriteHeight * 2
            ),
            new Rectangle(offsetX, offsetY, spriteWidth, spriteHeight),
            TintColor,
            Angle,
            new Vector2((float)spriteWidth / 2, (float)spriteHeight / 2),
            SpriteEffects.None,
            1
        );
        
    }

} // enemy