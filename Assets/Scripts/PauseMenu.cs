using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject HowToPlayUI;

    private void Start()
    {
        PauseMenuUI.SetActive(false);
        HowToPlayUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void InstructionMenuLoad()
    {
        PauseMenuUI.SetActive(false);
        HowToPlayUI.SetActive(true);
    }

    public void InstructionMenuLeave()
    {
        PauseMenuUI.SetActive(true);
        HowToPlayUI.SetActive(false);
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        HowToPlayUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start Scene");
    }

}
