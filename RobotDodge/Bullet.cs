using System;
using SplashKitSDK;

public class Bullet
{
    private double _X, _Y;
    private Vector2D _Velocity;
    private const int BulletRadius = 5; 

    public Bullet(double startX, double startY, double targetX, double targetY)
    {
        _X = startX;
        _Y = startY;
        _Velocity = SplashKit.VectorPointToPoint(new Point2D() { X = startX, Y = startY }, new Point2D() { X = targetX, Y = targetY });
        _Velocity = SplashKit.UnitVector(_Velocity);  // Normalize velocity
    }

    public void Update()
    {
        _X += _Velocity.X * 10;  
        _Y += _Velocity.Y * 10;
    }

    public void Draw()
    {
        SplashKit.FillCircle(Color.Red, _X, _Y, BulletRadius);  
        SplashKit.DrawCircle(Color.Blue, _X, _Y, BulletRadius); 
    }

    public bool CollidedWith(Robot robot)
    {
        double distance = Math.Sqrt(Math.Pow(_X - robot.X, 2) + Math.Pow(_Y - robot.Y, 2));
        double collisionDistance = BulletRadius + robot.CollisionCircle.Radius;
        return distance <= collisionDistance;
    }

    public bool IsOffscreen(Window screen)
    {
        return (_X < 0 || _X > screen.Width || _Y < 0 || _Y > screen.Height);
    }
}
