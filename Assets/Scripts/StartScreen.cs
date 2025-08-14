using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class StartScreen : MonoBehaviour
{
    public GameObject GameStartButton;
    public int score;

    private void Start()
    {
        score = ScoreManager.Score;
    }

    private void Update()
    {
        if (score >= 22000)
        {
            GameStartButton.SetActive(false);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("RPG-grassland");
    }

    public void AltF4()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
