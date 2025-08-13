using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    public void ReturnToEnd()
    {
        SceneManager.LoadScene("End Scene");
    }

    public void AltF4()
    {
        Debug.Log("Close the game");
        Application.Quit();
    }
}
