using SplashKitSDK;

public class Mario
{
    public double X { get; private set; }
    public double Y { get; private set; }
    private const int Width = 50;
    private const int Height = 50;
    private double Speed = 5;
    private bool IsJumping = false;
    private double JumpSpeed = 10;

    public Mario()
    {
        X = 100;
        Y = 350;  // Starting position
    }

    public void MoveLeft()
    {
        X -= Speed;
    }

    public void MoveRight()
    {
        X += Speed;
    }

    public void Jump()
    {
        if (!IsJumping)
        {
            IsJumping = true;
        }
    }

    public void Update()
    {
        if (IsJumping)
        {
            Y -= JumpSpeed;
            JumpSpeed -= 0.5;
            if (Y >= 350)
            {
                Y = 350;
                IsJumping = false;
                JumpSpeed = 10;
            }
        }
    }

    public void Draw()
    {
        SplashKit.FillRectangle(Color.Blue, X, Y, Width, Height);
    }
}
