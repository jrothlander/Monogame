using System;
using Microsoft.Xna.Framework;

namespace Xbox.Galaga.Enemy;

public enum ShipType
{
    Player = 0,
    Enemy = 1
}

internal class EnemyPatternEngine
{

    private int _enemyCount;
    private int _hiveCntr;
    private int _xAdj;
    private int _idx;
    private int _level;

    private readonly Random _rnd = new();

    private int ScreenWidth { get; set; }
    private int ScreenHeight { get; set; }

    public EnemyPatternEngine(int screenWidth, int screenHeight)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
    }

    public void ProcessPattern(PatternState pattern, Xbox.Galaga.Enemy.Enemy enemy, GameTime gameTime, int level, int enemyCount)
    {
        _level = level;
        _enemyCount = enemyCount;

        // Translates the pattern state to action via a given handler.

        switch (pattern)
        {
            case PatternState.Forward:
                ForwardPattern(enemy);
                break;
            case PatternState.Backwards:
                BackwardsPattern(enemy);
                break;
            case PatternState.Hive:
                HivePattern(enemy);
                break;
            //case PatternState.Random:
            //    RandomPattern(enemy);
            //    break;
            case PatternState.LeftDiagonal:
                LeftDiagonalPattern(enemy);
                break;
            case PatternState.RightDiagonal:
                RightDiagonalPattern(enemy);
                break;
            default:
                ForwardPattern(enemy);
                break;
        }
    }

    private void RightDiagonalPattern(Xbox.Galaga.Enemy.Enemy enemy)
    {
        var p1x = enemy.Location.X;
        var p1y = enemy.Location.Y;

        //enemy.x += enemy.speedY;
        //enemy.y += enemy.speedY;

        var loop =
            "dddddddddddddddd" +
            "dddddddddddddddd" +
            "2222222222222222" +
            "2222222222222222" +
            "2222222222222222" +
            "dddddddddddddddd" +
            "dddddddddddddddd" +
            "2222222222222222" +
            "2222222222222222" +
            "2222222222222222" +
            "uuuuuuuuuuuuuuuu" +
            "uuuuuuuuuuuuuuuu" +
            "uuuuuuuuuuuuuuuu" +
            "1111111111111111" +
            ".";

        //    executeMotionPattern(p1x, p1y, enemy, loop);
        //    rotateToPath(p1x, p1y, enemy);

        //    if ((enemy.y > canvas.height + enemy.height) ||
        //        (enemy.x > canvas.width + enemy.width))
        //    {
        //        enemy.x = Math.random() * canvas.width - enemy.width; ;
        //        enemy.y = 0 - enemy.width;
        //    }
    }

    private void LeftDiagonalPattern(Xbox.Galaga.Enemy.Enemy enemy)
    {
        //    if (enemy.type > 4) cellX = 6
        //  cellX = 6;

        //    var p1x = enemy.x;
        //    var p1y = enemy.y;
        //    //enemy.x -= enemy.speedY;      
        //    //enemy.y += enemy.speedY;

        //    let loop =
        //      "dddddddddddddddd" +
        //      "3333333333333333" +
        //      "3333333333333333" +
        //      ".";

        //    executeMotionPattern(p1x, p1y, enemy, loop);
        //    rotateToPath(p1x, p1y, enemy);

        //    if ((enemy.y > canvas.height) ||
        //        (enemy.x < (0 - enemy.width)))
        //    {
        //        enemy.x = Math.random() * (canvas.width - enemy.width);
        //        enemy.y = 0 - enemy.width;
        //    }
    }

    private void BackwardsPattern(Xbox.Galaga.Enemy.Enemy enemy)
    {
        //    // Flip to 7th position but only for 1st four images, as 
        //    // the others have no 7th. 
        //    cellX = 7;

        //    var p1x = enemy.x;
        //    var p1y = enemy.y;
        //    //enemy.x += 0;
        //    //enemy.y += enemy.speedY - 4;

        //    let loop =
        //      "uuuuuuuuuuuuuuuu" +
        //      "1111111111111111" +
        //      "llllllllllllllll" +
        //      "3333333333333333" +
        //      "dddddddddddddddd" +
        //      "2222222222222222" +
        //      "rrrrrrrrrrrrrrrr" +
        //      "4444444444444444" +
        //      "uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu" +
        //      ".";

        //    executeMotionPattern(p1x, p1y, enemy, loop);
        //    rotateToPath(p1x, p1y, enemy);

        //    if (enemy.y < 0)
        //    {
        //        enemy.x = Math.random() * canvas.width - 50;
        //        enemy.y = canvas.height;
        //    }
    }

    private void RandomPattern(Xbox.Galaga.Enemy.Enemy enemy)
    {
        //    enemy.x += enemy.speedX;
        //    enemy.y += enemy.speedY;

        //    if (enemy.y > canvas.height ||
        //        enemy.x > canvas.width)
        //    {
        //        enemy.x = 0;
        //        enemy.y = Math.random() * canvas.width - 50;
        //    }

        //    if (enemy.y < 0)
        //    {
        //        enemy.x = Math.random() * canvas.width - 50;
        //        enemy.y = canvas.height;
        //    }
    }

    private void FlyInPattern(Xbox.Galaga.Enemy.Enemy enemy)
    {
        var p1X = enemy.Location.X;
        var p1Y = enemy.Location.Y;
        var p2X = p1X;
        var p2Y = p1Y;

        const string loop = "dddddddddddddddddddddddddddddd" +
                            "333333333333333333333333333333" +
                            "333333333333333333333333333333" +
                            "dddddddddddddddddddddddddddddd" +
                            "2222222222" +
                            "rrrrrrrrrr" +
                            "4444444444444444444" +
                            "uuuuuuuuuuuuuuuuuuuuuu" +
                            "";

        ExecuteMotionPattern(ref p2X, ref p2Y, enemy, loop);
        RotateToPath(p1X, p1Y, enemy);

        if ((enemy.Location.Y >= ScreenHeight + enemy.Height) || (enemy.Location.X <= (0 - enemy.Width)))
        {
            var x = _rnd.Next(1, ScreenWidth) - enemy.Width;
            var y = 0 - enemy.Height;
            enemy.Location = new Vector2(x, y);
        }
    }

    private void HivePattern(Xbox.Galaga.Enemy.Enemy enemy)
    {

        // HIVE (4 rows x 8 columns)) 4x10 2d array
        // [0] [1] [2] [3] [4] [5] [6] [7] - bug
        // [0] [1] [2] [3] [4] [5] [6] [7] - bee
        // [0] [1] [2] [3] [4] [5] [6] [7] - fly  
        // [0] [1] [2] [3] [4] [5] [6] [7] - wasp

        // Pulse between sprite position 6 and 7 every 45th frame refresh.   
        const int framesPerPulse = 50;
        _hiveCntr += 1;

        if (_hiveCntr % (framesPerPulse * _enemyCount) == 0)
        {
            _hiveCntr = 0;

            if (enemy.CellX == 6)
            {
                enemy.CellX = 7;
                _xAdj += 20;
            }
            else
            {
                enemy.CellX = 6;
                _xAdj -= 20;
            }
        }

        // Assign attack patterns to a given level. 
        if (enemy.Attacking)
        {
            switch (_level)
            {
                case 1:
                case 3:
                    FlyInPattern(enemy);
                    break;
                default:
                    ForwardPattern(enemy);
                    break;
            }
        }
    }

    private void ForwardPattern(Xbox.Galaga.Enemy.Enemy enemy)
    {
        var p1X = enemy.Location.X;
        var p1Y = enemy.Location.Y;
        var p2X = p1X;
        var p2Y = p1Y;

        const int patternSpeed = 10;

        //// -----------------------------
        //// Manual 45-degree angled loop
        //// -----------------------------
        ////
        //var pattern = new Pattern
        //{
        //    Steps = new PatternStep[]
        //    {
        //        new() { Speed = patternSpeed, Repeat = 40, Type = PatternType.Down },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.DownRightDiagonal },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.Right },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.UpRightDiagonal },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.Up },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.UpLeftDiagonal },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.Left },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.DownLeftDiagonal },
        //        new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.Down }
        //    }
        //};

        var pattern = new Pattern
        {
            Steps = new PatternStep[]
            {
                new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.Down },
                new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.DownRightDiagonal },
                new() { Speed = patternSpeed, Repeat = 90, Type = PatternType.Loop },
                new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.DownLeftDiagonal },
                new() { Speed = patternSpeed, Repeat = 10, Type = PatternType.Down },
                new() { Speed = patternSpeed, Repeat = 40, Type = PatternType.Pause },
            }
        };

        ExecutePattern(ref p2X, ref p2Y, enemy, pattern);
        RotateToPath(p1X, p1Y, enemy);

        if (!(enemy.Location.Y > ScreenHeight + enemy.Height) && !(enemy.Location.X < 0 - enemy.Width)) return;

        var x = _rnd.Next(1, ScreenWidth) - enemy.Width;
        var y = 0 - enemy.Height;
        enemy.Location = new Vector2(x, y);
    }

    private static void ExecuteMotionPattern(ref float p1X, ref float p1Y, Xbox.Galaga.Enemy.Enemy enemy, string loop)
    {
        const float xSpdAdj = 0.707f;
        const float ySpdAdj = 0.707f;

        //const float degToRad = 0.0174532924f; // (PI/180)
        //var xSpdAdj = MathF.Abs(MathF.Round(MathF.Cos(enemy.Angle * degToRad), 3)); // need to round to only 3 dec
        //var ySpdAdj = MathF.Abs(MathF.Round(MathF.Sin(enemy.Angle * degToRad), 3)); // need to round to only 3 dec
        //console.log(`${angle}:${xSpdAdj},${ySpdAdj}`);

        enemy.Cntr += 1;
        var x = p1X;
        var y = p1Y;

        var idx = enemy.Cntr % loop.Length; // continues cycling through the loop

        switch (loop[idx])
        {
            // ----------------
            // Strait Movement
            // ----------------
            case 'r': // MOTION_PATTERN.RIGHT:
                x += enemy.SpeedX;
                y = p1Y;
                break;
            case 'l': //MOTION_PATTERN.LEFT:
                x -= enemy.SpeedX;
                y = p1Y;
                break;
            case 'u': //MOTION_PATTERN.UP:
                y -= enemy.SpeedY;
                x = p1X;
                break;
            case 'd': //MOTION_PATTERN.DOWN:
                y += enemy.SpeedY;
                x = p1X;
                break;

            // ----------------
            // Diagonal Movement
            // ----------------
            case '1': // up-to-left diagonal up towards left:
                x -= enemy.SpeedX * xSpdAdj;
                y -= enemy.SpeedY * ySpdAdj;
                break;
            case '2': // down-to-right diagonal down towards right:  
                x += enemy.SpeedX * xSpdAdj;
                y += enemy.SpeedY * ySpdAdj;
                break;
            case '3': // 
                x -= enemy.SpeedX * xSpdAdj;
                y += enemy.SpeedY * ySpdAdj;
                break;
            case '4': // 
                x += enemy.SpeedX * xSpdAdj;
                y -= enemy.SpeedY * ySpdAdj;
                break;
                //default: // any other pattern is ignored
                //    break;
        }

        enemy.Location = new Vector2(x, y);

        p1X = x;
        p1Y = y;
    }

    private void ExecutePattern(ref float p1X, ref float p1Y, Xbox.Galaga.Enemy.Enemy enemy, Pattern pattern)
    {
        // Each step in the pattern is executed at one step per frame. The speed determines how many pixels to move per frame.
        // patterns.Steps.Length is the number of steps in the pattern, without accounting for repetition.

        const float xSpdAdj = 0.707f; // constants so we don't have to recalculate the values for each 45-degree angle. Should be moved to a common library.
        const float ySpdAdj = 0.707f;

        var x = p1X;
        var y = p1Y;

        if (enemy.RepeatCntr == 0)
        {
            if (_idx >= pattern.Steps.Length -1) // Like the commented line above, We could create an index counter and divide the counter by the step lengths, but that would add another counter that we don't need anywhere else.
                _idx = 0;
            else
                _idx++;

            enemy.RepeatCntr = pattern.Steps[_idx].Repeat;
        }
        else
            enemy.RepeatCntr -= 1; // Count down from the repeating counter for each enemy, so that we can control how many times a steps is repeated, without having to copy it that many times.

        switch (pattern.Steps[_idx].Type)
        {
            case PatternType.Down:
                y += pattern.Steps[_idx].Speed;
                x = p1X;
                break;
            case PatternType.Up:
                y -= pattern.Steps[_idx].Speed;
                x = p1X;
                break;
            case PatternType.Left:
                x -= pattern.Steps[_idx].Speed;
                y = p1Y;
                break;
            case PatternType.Right:
                x += pattern.Steps[_idx].Speed;
                y = p1Y;
                break;
            case PatternType.UpLeftDiagonal:
                x -= pattern.Steps[_idx].Speed * xSpdAdj;
                y -= pattern.Steps[_idx].Speed * ySpdAdj;
                break;
            case PatternType.DownLeftDiagonal:
                x -= pattern.Steps[_idx].Speed * xSpdAdj;
                y += pattern.Steps[_idx].Speed * ySpdAdj;
                break;
            case PatternType.UpRightDiagonal:
                x += pattern.Steps[_idx].Speed * xSpdAdj;
                y -= pattern.Steps[_idx].Speed * ySpdAdj;
                break;
            case PatternType.DownRightDiagonal:
                x += pattern.Steps[_idx].Speed * xSpdAdj;
                y += pattern.Steps[_idx].Speed * ySpdAdj;
                break;

            case PatternType.Loop:
                var rad = MathHelper.ToRadians(enemy.RepeatCntr * pattern.Steps[_idx].Speed); 
                x = (float)(enemy.Location.X + Math.Cos(rad) * pattern.Steps[_idx].Speed);
                y = (float)(enemy.Location.Y + Math.Sin(rad) * pattern.Steps[_idx].Speed);
                break;

            case PatternType.Pause:
            default:
                break;
        }

        enemy.Location = new Vector2(x, y);
        p1X = enemy.Location.X;
        p1Y = enemy.Location.Y;
    }
    private static void RotateToPath(float p1X, float p1Y, Xbox.Galaga.Enemy.Enemy enemy)
    {
        var p2X = enemy.Location.X;
        var p2Y = enemy.Location.Y;

        enemy.Angle = MathF.Atan2(p2Y - p1Y, p2X - p1X) + (MathF.PI / 2);
    }

    public enum PatternState
    {
        Hive = 1,
        Backwards = 2,
        Forward = 3,
        LeftDiagonal = 4,
        RightDiagonal = 5
    }

    public class Pattern
    {
        public PatternStep[] Steps { get; set; }

    }

    public class PatternStep
    {
        public PatternType Type { get; set; }
        public int Speed { get; set; }
        public int Repeat { get; set; }

    }

    public enum PatternType
    {
        Up,
        Down,
        Left,
        Right,
        UpLeftDiagonal,
        DownLeftDiagonal,
        UpRightDiagonal,
        DownRightDiagonal,
        Loop,
        Pause
    }

}