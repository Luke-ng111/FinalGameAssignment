using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void AltF4()
    {
        Application.Quit();
    }
}
