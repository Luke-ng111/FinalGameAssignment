using UnityEngine;

public static class ScoreManager
{
    public static int Score { get; private set; } = 0;

    public static void AddPoints(int points)
    {
        Score += points;
        UnityEngine.Debug.Log("Score: " + Score);
    }

    public static void ResetScore()
    {
        Score = 0;
    }
}
