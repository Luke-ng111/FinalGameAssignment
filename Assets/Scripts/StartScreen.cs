using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
   public void LoadGame()
    {
        SceneManager.LoadScene("RPG-grassland");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
