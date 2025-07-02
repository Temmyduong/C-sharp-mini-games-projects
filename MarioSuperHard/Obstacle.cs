using SplashKitSDK;

public class Obstacle
{
    public double X { get; private set; }
    public double Y { get; private set; }

    public Obstacle(double startX, double startY)
    {
        X = startX;
        Y = startY;
    }

    public void Draw()
    {
        SplashKit.FillRectangle(Color.Green, X, Y, 50, 50);
    }
}
