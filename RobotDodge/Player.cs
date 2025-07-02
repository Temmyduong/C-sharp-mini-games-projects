using System;
using SplashKitSDK;

public class Player
{
    private Bitmap _PlayerBitmap;
    private double _X, _Y;
    private Window _GameWindow;
    private int _lives;
    private int _score;

    public int Lives
    {
        get { return _lives; }
    }

    public int Score
    {
        get { return _score; }
    }

    public double X
    {
        get { return _X; }
    }

    public double Y
    {
        get { return _Y; }
    }

    public bool Quit { get; private set; }

    public Player(Window gameWindow)
    {
        _GameWindow = gameWindow;
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        _X = (gameWindow.Width - _PlayerBitmap.Width) / 2;
        _Y = (gameWindow.Height - _PlayerBitmap.Height) / 2;
        Quit = false;
        _lives = 5;  // Start with 5 lives
        _score = 0;  // Start with 0 score
    }

    public void HandleInput()
    {
        const int Speed = 5;

        if (SplashKit.KeyDown(KeyCode.LeftKey)) _X -= Speed;
        if (SplashKit.KeyDown(KeyCode.RightKey)) _X += Speed;
        if (SplashKit.KeyDown(KeyCode.UpKey)) _Y -= Speed;
        if (SplashKit.KeyDown(KeyCode.DownKey)) _Y += Speed;
        if (SplashKit.KeyDown(KeyCode.EscapeKey)) Quit = true;

        StayOnWindow(_GameWindow);
    }

    public void Draw()
    {
        _PlayerBitmap.Draw(_X, _Y);
        SplashKit.DrawText("Lives: " + _lives.ToString(), Color.Black, 10, 10); // Display lives
        SplashKit.DrawText("Score: " + _score.ToString(), Color.Black, 10, 30); // Display score
    }

    public void Update()
    {
        _score += 1;  // Increase score every update
    }

    public bool CollidedWith(Robot robot)
    {
        if (_PlayerBitmap.CircleCollision(_X, _Y, robot.CollisionCircle))
        {
            _lives--;  // Decrease life if collided
            return true;
        }
        return false;
    }

    private void StayOnWindow(Window limit)
    {
        if (_X < 0) _X = 0;
        if (_X > limit.Width - _PlayerBitmap.Width) _X = limit.Width - _PlayerBitmap.Width;
        if (_Y < 0) _Y = 0;
        if (_Y > limit.Height - _PlayerBitmap.Height) _Y = limit.Height - _PlayerBitmap.Height;
    }
}
