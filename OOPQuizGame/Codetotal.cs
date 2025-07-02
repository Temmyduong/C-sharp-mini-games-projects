using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        QuizGame quizGame = new QuizGame();
        quizGame.Start();
    }
}
 

using System.Collections.Generic;

public class Question
{
    public string Text { get; private set; }
    public List<string> Options { get; private set; }
    public int CorrectOption { get; private set; }

    public Question(string text, List<string> options, int correctOption)
    {
        Text = text;
        Options = options;
        CorrectOption = correctOption;
    }

    public bool IsCorrect(int option)
    {
        return option == CorrectOption;
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using SplashKitSDK;

public class QuizGame
{
    private List<Question> _questions;
    private int _currentQuestionIndex;
    private int _score;
    private Bitmap _background;
    private string _playerName = "";
    private bool _isGameOver = false;
    private bool _showHighScores = false;
    private string _highScoreFile = "highscores.txt"; // File to store high scores

    public QuizGame()
    {
        _questions = new List<Question>
        {
            new Question("What is an Object?", new List<string> { "A blueprint", "An instance of a class", "A variable", "A method" }, 1),
            new Question("What is Inheritance?", new List<string> { "A way to reuse code", "A method to protect data", "A class variable", "None of the above" }, 0),
            new Question("What is Polymorphism?", new List<string> { "Multiple forms of a method", "A way to create classes", "A type of data structure", "None of the above" }, 0)
        };
        _currentQuestionIndex = 0;
        _score = 0;
        _background = new Bitmap("background", "background.png");
    }

    public void Start()
    {
        Window gameWindow = new Window("OOP Quiz Game", 800, 600);
        while (!gameWindow.CloseRequested)
        {
            SplashKit.ProcessEvents();
            gameWindow.Clear(Color.White);

            if (_showHighScores)
            {
                DisplayHighScoresScreen(gameWindow);
            }
            else if (_isGameOver)
            {
                DisplayGameOver(gameWindow);
            }
            else
            {
                SplashKit.DrawBitmap(_background, 0, 0);
                DisplayQuestion(gameWindow);
            }

            gameWindow.Refresh(60);
        }

        gameWindow.Close();
    }

    private void DisplayQuestion(Window window)
    {
        if (_currentQuestionIndex >= _questions.Count)
        {
            _isGameOver = true;
            return;
        }

        Question currentQuestion = _questions[_currentQuestionIndex];

        // Draw the question text
        SplashKit.DrawText(currentQuestion.Text, Color.Black, 50, 50);

        // Draw the answer options
        for (int i = 0; i < currentQuestion.Options.Count; i++)
        {
            SplashKit.DrawText($"{i + 1}. {currentQuestion.Options[i]}", Color.Black, 50, 100 + i * 30);
        }

        // Display the score at the top right corner
        SplashKit.DrawText($"Score: {_score}", Color.Black, 650, 50);

        // Check for key input to answer the question
        for (int i = 0; i < 4; i++)
        {
            if (SplashKit.KeyTyped((KeyCode)Enum.Parse(typeof(KeyCode), $"Num{i + 1}Key")))
            {
                CheckAnswer(i);
                _currentQuestionIndex++;
            }
        }
    }

    private void DisplayGameOver(Window window)
    {
        SplashKit.DrawText("Game Over!", Color.Red, 300, 200);
        SplashKit.DrawText($"Final Score: {_score}", Color.Black, 300, 250);
        SplashKit.DrawText("Enter your name:", Color.Black, 300, 300);
        SplashKit.DrawText(_playerName, Color.Black, 300, 350);

        // Handle player input and transition to high scores
        HandlePlayerNameInput(window);
    }

    private void HandlePlayerNameInput(Window window)
    {
        if (SplashKit.KeyTyped(KeyCode.AKey)) _playerName += "A";
        if (SplashKit.KeyTyped(KeyCode.BKey)) _playerName += "B";
        if (SplashKit.KeyTyped(KeyCode.CKey)) _playerName += "C";
        if (SplashKit.KeyTyped(KeyCode.DKey)) _playerName += "D";
        if (SplashKit.KeyTyped(KeyCode.EKey)) _playerName += "E";
        if (SplashKit.KeyTyped(KeyCode.FKey)) _playerName += "F";
        if (SplashKit.KeyTyped(KeyCode.GKey)) _playerName += "G";
        if (SplashKit.KeyTyped(KeyCode.HKey)) _playerName += "H";
        if (SplashKit.KeyTyped(KeyCode.IKey)) _playerName += "I";
        if (SplashKit.KeyTyped(KeyCode.JKey)) _playerName += "J";
        if (SplashKit.KeyTyped(KeyCode.KKey)) _playerName += "K";
        if (SplashKit.KeyTyped(KeyCode.LKey)) _playerName += "L";
        if (SplashKit.KeyTyped(KeyCode.MKey)) _playerName += "M";
        if (SplashKit.KeyTyped(KeyCode.NKey)) _playerName += "N";
        if (SplashKit.KeyTyped(KeyCode.OKey)) _playerName += "O";
        if (SplashKit.KeyTyped(KeyCode.PKey)) _playerName += "P";
        if (SplashKit.KeyTyped(KeyCode.QKey)) _playerName += "Q";
        if (SplashKit.KeyTyped(KeyCode.RKey)) _playerName += "R";
        if (SplashKit.KeyTyped(KeyCode.SKey)) _playerName += "S";
        if (SplashKit.KeyTyped(KeyCode.TKey)) _playerName += "T";
        if (SplashKit.KeyTyped(KeyCode.UKey)) _playerName += "U";
        if (SplashKit.KeyTyped(KeyCode.VKey)) _playerName += "V";
        if (SplashKit.KeyTyped(KeyCode.WKey)) _playerName += "W";
        if (SplashKit.KeyTyped(KeyCode.XKey)) _playerName += "X";
        if (SplashKit.KeyTyped(KeyCode.YKey)) _playerName += "Y";
        if (SplashKit.KeyTyped(KeyCode.ZKey)) _playerName += "Z";

        if (SplashKit.KeyTyped(KeyCode.BackspaceKey) && _playerName.Length > 0)
        {
            _playerName = _playerName.Substring(0, _playerName.Length - 1);
        }

        if (SplashKit.KeyTyped(KeyCode.ReturnKey) && _playerName.Length > 0)
        {
            SaveHighScore(_playerName, _score);
            _playerName = "";
            _showHighScores = true; // Set flag to show high scores
        }
    }

    private void DisplayHighScoresScreen(Window window)
    {
        window.Clear(Color.White); // Clear the screen

        DisplayHighScores(window); // Display the high scores

        // Draw Play Again and Quit buttons
        DrawButton(window, "Play Again", 300, 400, 200, 50);
        DrawButton(window, "Quit Game", 300, 460, 200, 50);

        // Check for button clicks
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            if (IsMouseOverButton(300, 400, 200, 50))
            {
                // Reset the game to play again
                _score = 0;
                _currentQuestionIndex = 0;
                _isGameOver = false;
                _showHighScores = false;
            }
            else if (IsMouseOverButton(300, 460, 200, 50))
            {
                // Quit the game
                window.Close();
            }
        }
    }

    private void DrawButton(Window window, string text, int x, int y, int width, int height)
    {
        SplashKit.FillRectangle(Color.Gray, x, y, width, height);
        SplashKit.DrawRectangle(Color.Black, x, y, width, height);
        SplashKit.DrawText(text, Color.White, x + 20, y + 15);
    }

    private bool IsMouseOverButton(int x, int y, int width, int height)
    {
        double mouseX = SplashKit.MouseX();
        double mouseY = SplashKit.MouseY();
        return (mouseX > x && mouseX < x + width && mouseY > y && mouseY < y + height);
    }

    private void CheckAnswer(int selectedOption)
    {
        if (_questions[_currentQuestionIndex].IsCorrect(selectedOption))
        {
            _score += 10;
            SplashKit.LoadSoundEffect("correct", "correct.wav");
            SplashKit.PlaySoundEffect("correct");
        }
        else
        {
            SplashKit.LoadSoundEffect("incorrect", "incorrect.mp3");
            SplashKit.PlaySoundEffect("incorrect");
        }
    }

    private void SaveHighScore(string playerName, int score)
    {
        using (StreamWriter sw = new StreamWriter(_highScoreFile, true))
        {
            sw.WriteLine($"{playerName},{score}");
        }
    }

    private void DisplayHighScores(Window window)
    {
        List<Tuple<string, int>> scores = new List<Tuple<string, int>>();

        using (StreamReader sr = new StreamReader(_highScoreFile))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    string name = parts[0];
                    int score;
                    if (int.TryParse(parts[1], out score))
                    {
                        scores.Add(new Tuple<string, int>(name, score));
                    }
                }
            }
        }

        scores.Sort((x, y) => y.Item2.CompareTo(x.Item2)); // Sort scores descending

        SplashKit.DrawText("High Scores:", Color.Black, 300, 100);
        for (int i = 0; i < scores.Count && i < 10; i++) // Display top 10 scores
        {
            SplashKit.DrawText($"{i + 1}. {scores[i].Item1} - {scores[i].Item2}", Color.Black, 300, 130 + i * 30);
        }
    }
}


using SplashKitSDK;

public static class UI
{
    public static void DrawText(string text, int x, int y, Color color)
    {
        SplashKit.DrawText(text, color, x, y);
    }
}

