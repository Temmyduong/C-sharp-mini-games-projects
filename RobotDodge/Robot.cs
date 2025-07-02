using System;
using SplashKitSDK;

public abstract class Robot
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public Vector2D Velocity { get; private set; }
    public Color _MainColor;
    protected const int Width = 50, Height = 50;
    protected bool _IsDestroyed;

    public Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);
        }
    }

    public Bitmap Bitmap { get; private set; }

    public Robot(Window gameWindow, Player player)
    {
        if (SplashKit.Rnd() < 0.5)
        {
            X = -Width;
        }
        else
        {
            X = gameWindow.Width;
        }
        Y = SplashKit.Rnd(gameWindow.Height - Height);
        _MainColor = Color.RandomRGB(200);
        _IsDestroyed = false;

        Velocity = SplashKit.UnitVector(SplashKit.VectorPointToPoint(new Point2D() { X = X, Y = Y }, new Point2D() { X = player.X, Y = player.Y }));

    }

    public void Update()
    {
        if (!_IsDestroyed)
        {
            X += Velocity.X * 2; 
            Y += Velocity.Y * 2;
        }
    }

    public bool IsOffscreen(Window screen)
    {
        return (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height);
    }

    public abstract void Draw();

    public void Destroy()
    {
        _IsDestroyed = true;
    }
}

public class Boxy : Robot
{
    public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
    {

    }

    public override void Draw()
    {
        if (!_IsDestroyed)
        {
            Bitmap boxyBitmap = new Bitmap("Boxy", Width, Height);
            boxyBitmap.FillRectangle(Color.Gray, 0, 0, Width, Height);
            boxyBitmap.FillRectangle(_MainColor, 12, 10, 10, 10); // left eye
            boxyBitmap.FillRectangle(_MainColor, 27, 10, 10, 10); // right eye
            boxyBitmap.FillRectangle(_MainColor, 12, 30, 25, 10); // mouth
            boxyBitmap.FillRectangle(_MainColor, 14, 32, 21, 6);  // mouth detail

            boxyBitmap.Draw(X, Y);
        }
    }
}

public class Roundy : Robot
{
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
    {
        
    }

    public override void Draw()
    {
        if (!_IsDestroyed)
        {
            double leftX, midX, rightX;
            double midY, eyeY, mouthY;

            leftX = X + 17;
            midX = X + 25;
            rightX = X + 33;
            midY = Y + 25;
            eyeY = Y + 20;
            mouthY = Y + 35;

            SplashKit.FillCircle(Color.White, midX, midY, 25);
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
            SplashKit.FillCircle(_MainColor, leftX, eyeY, 5);
            SplashKit.FillCircle(_MainColor, rightX, eyeY, 5);
            SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
            SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
        }
    }
}


public class Triangly : Robot
{
    public Triangly(Window gameWindow, Player player) : base(gameWindow, player)
    {
      
    }

    public override void Draw()
    {
        if (!_IsDestroyed)
        {
            // Draw Triangle
            SplashKit.FillTriangle(_MainColor, X + 25, Y, X, Y + 50, X + 50, Y + 50);

            // Draw Eyes
            SplashKit.FillRectangle(Color.White, X + 12, Y + 25, 10, 10);
            SplashKit.FillRectangle(Color.White, X + 33, Y + 25, 10, 10);

            // Draw Mouth
            SplashKit.FillRectangle(Color.White, X + 15, Y + 35, 25, 10);
            SplashKit.FillRectangle(_MainColor, X + 17, Y + 37, 21, 6);
        }
    }
}

