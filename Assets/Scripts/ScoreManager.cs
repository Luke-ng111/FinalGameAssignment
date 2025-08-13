using UnityEngine;
using UnityEngine.UI;

public static class ScoreManager
{
    public static int Score { get; private set; } = 0;
    
    public static void AddPoints(int points)
    {
        Score += points;
        UnityEngine.Debug.Log("Score: " + Score);
    }

    public static void DebugScore()
    {
        Debug.Log("21000 score added");
        Score += 21000;
    }

    public static void ResetScore()
    {
        Score = 0;
    }
}
