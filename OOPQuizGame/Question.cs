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
