using SplashKitSDK;

public class Game
{
    private Window _window;
    private Mario _mario;
    private Level _level;

    public Game()
    {
        _window = new Window("Mario - Super Hard Mode", 800, 600);
        _mario = new Mario();
        _level = new Level();
    }

    public void Start()
    {
        while (!_window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            HandleInput();
            Update();
            Draw();
        }

        _window.Close();
    }

    private void HandleInput()
    {
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            _mario.MoveLeft();
        }
        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            _mario.MoveRight();
        }
        if (SplashKit.KeyTyped(KeyCode.SpaceKey))
        {
            _mario.Jump();
        }
    }

    private void Update()
    {
        _mario.Update();
        _level.Update();
    }

    private void Draw()
    {
        _window.Clear(Color.White);
        _mario.Draw();
        _level.Draw();
        _window.Refresh(60);
    }
}
