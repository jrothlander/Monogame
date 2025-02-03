using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Xbox.Galaga.Handlers;

public class TextToSpriteHandler
{

    private static Texture2D Texture { get; set; }
    private static Color TintColor { get; set; }

    private const int SpriteWidthX = 8;  // width without border
    private const int SpriteHeightY = 8; // height without border
    private const int SpriteBorder = 1;  // border (height and width)

    private const int SpriteStartXNum = 453;  // starting x-position of first sprite for numeric characters
    private const int SpriteStartXAlpha = 543; // starting x-position of first sprite for alpha characters
    private static int SpriteStartYRow1 = 443; //462;  // starting y-position of row1
    private static int SpriteStartYRow2 = 452; //471;  // starting y-position of row2

    public static void Initialize(Texture2D texture, Color tintColor)
    {
        Texture = texture;
        TintColor = tintColor;
    }

    public static void DrawString(
        SpriteBatch spriteBatch,
        string str,
        Color color,
        int screenWidth, int screenHeight, 
        int startY,
        int dx, int dy
        )
    {
        str = str.ToLower();
        TintColor = color;

        var startX = screenWidth - str.Length * screenHeight;
        
        for (var i = 0; i < str.Length; i++)
        {
            DrawSprite(spriteBatch, str[i], startX + i * dx, startY, dx, dy);
        };
    }

    // Draw a string as sprites on the canvas
    public static void DrawString(
        SpriteBatch spriteBatch, 
        string str, 
        Color color, 
        int startX = 0, int startY = 0, 
        int dx = 16, int dy = 16)
    {
        str = str.ToLower();
        TintColor = color;

        // Converts string to array of characters and translate each character to a sprite and 
        // displays on the canvas. 
        //
        // Note: I am using the dx value as the number of pixels of spacing between characters. This 
        // may need some tweaking but seems to work well to create a fixed-width font style vs a 
        // dynamic/variable width font.

        for (var i = 0; i < str.Length; i++)
        {
            DrawSprite(spriteBatch, str[i], startX + i * dx, startY, dx, dy);
        };
    }

    // Maps a char to a given sprite and displays on the canvas
    private static void DrawSprite(SpriteBatch spriteBatch, char character, int x = 0, int y = 0, int dx = 8, int dy = 8)
    {
        const string alphabetRow1 = "abcdefghijklmno"; // row 1 of alpha chars from the sprite sheet
        const string alphabetRow2 = "pqrstuvwxyz|-%.!@";     // row 2 of alpha chars the sprite sheet
        const string number = "0123456789";

        var col = 0;
        var row = 0;

        if (alphabetRow1.Contains(character))
        {
            col = SpriteStartXAlpha + alphabetRow1.IndexOf(character) * (SpriteWidthX + SpriteBorder);
            row = SpriteStartYRow1;
        }
        else if (alphabetRow2.Contains(character))
        {
            col = SpriteStartXNum + alphabetRow2.IndexOf(character) * (SpriteWidthX + SpriteBorder);
            row = SpriteStartYRow2;
        }
        else if (number.Contains(character))
        {
            col = SpriteStartXNum + number.IndexOf(character) * (SpriteWidthX + SpriteBorder);
            row = SpriteStartYRow1;
        }

        if (row == 0 && col == 0) return;

        spriteBatch.Draw(
            Texture,
            new Rectangle(
                x, y,
                SpriteWidthX,
                SpriteHeightY
            ),
            new Rectangle(
                col, row,
                dx, dy),
            TintColor
        );
    }

}