using System.Collections.Generic;
using SplashKitSDK;

public class Level
{
    private List<Enemy> _enemies;
    private List<Obstacle> _obstacles;

    public Level()
    {
        _enemies = new List<Enemy>();
        _obstacles = new List<Obstacle>();

        // Add enemies and obstacles
        _enemies.Add(new Enemy(800, 350));
        _enemies.Add(new Enemy(1000, 350));
        _obstacles.Add(new Obstacle(400, 350));
    }

    public void Update()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Update();
        }
    }

    public void Draw()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Draw();
        }

        foreach (Obstacle obstacle in _obstacles)
        {
            obstacle.Draw();
        }
    }
}
