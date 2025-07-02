using System;
using System.Collections.Generic;
using SplashKitSDK;

public class RobotDodge
{
    private Window _GameWindow;
    private Player _Player;
    private List<Robot> _Robots;
    private List<Bullet> _Bullets;

    public bool Quit
    {
        get
        {
            return _Player.Quit || _Player.Lives <= 0;
        }
    }

    public RobotDodge(Window gameWindow)
    {
        _GameWindow = gameWindow;
        _Player = new Player(_GameWindow);
        _Robots = new List<Robot>();
        _Bullets = new List<Bullet>();
    }

    public void HandleInput()
    {
        _Player.HandleInput();
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Bullet bullet = new Bullet(_Player.X, _Player.Y, SplashKit.MouseX(), SplashKit.MouseY());
            _Bullets.Add(bullet);
        }
    }

    public void Draw()
    {
        _GameWindow.Clear(Color.White);
        _Player.Draw();
        foreach (Robot robot in _Robots)
        {
            robot.Draw();
        }
        foreach (Bullet bullet in _Bullets)
        {
            bullet.Draw();
        }
        _GameWindow.Refresh(60);
    }

    public void Update()
    {
        List<Robot> robotsToRemove = new List<Robot>();
        List<Bullet> bulletsToRemove = new List<Bullet>();

        _Player.Update();

        foreach (Robot robot in _Robots)
        {
            robot.Update();
            if (_Player.CollidedWith(robot) || robot.IsOffscreen(_GameWindow))
            {
                robotsToRemove.Add(robot);
            }
        }

        foreach (Bullet bullet in _Bullets)
        {
            bullet.Update();
            if (bullet.IsOffscreen(_GameWindow))
            {
                bulletsToRemove.Add(bullet);
            }
            else
            {
                foreach (Robot robot in _Robots)
                {
                    if (bullet.CollidedWith(robot))
                    {
                        bulletsToRemove.Add(bullet);
                        robotsToRemove.Add(robot);
                        break;
                    }
                }
            }
        }

        foreach (Robot robot in robotsToRemove)
        {
            _Robots.Remove(robot);
        }

        foreach (Bullet bullet in bulletsToRemove)
        {
            _Bullets.Remove(bullet);
        }

        if (SplashKit.Rnd() < 0.02)
        {
            _Robots.Add(RandomRobot());
        }
    }

    public Robot RandomRobot()
    {
        if (SplashKit.Rnd() < 0.33)
        {
            return new Boxy(_GameWindow, _Player);
        }
        else if (SplashKit.Rnd() < 0.66)
        {
            return new Roundy(_GameWindow, _Player);
        }
        else
        {
            return new Triangly(_GameWindow, _Player); 
        }
    }
}
