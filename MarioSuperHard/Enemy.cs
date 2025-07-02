using SplashKitSDK;

public class Enemy
{
    public double X { get; private set; }
    public double Y { get; private set; }
    private double Speed = 3;

    public Enemy(double startX, double startY)
    {
        X = startX;
        Y = startY;
    }

    public void Update()
    {
        X -= Speed;
    }

    public void Draw()
    {
        SplashKit.FillRectangle(Color.Red, X, Y, 50, 50);
    }
}
