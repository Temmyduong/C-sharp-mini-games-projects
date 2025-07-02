using SplashKitSDK;

public static class UI
{
    public static void DrawText(string text, int x, int y, Color color)
    {
        SplashKit.DrawText(text, color, x, y);
    }
}
