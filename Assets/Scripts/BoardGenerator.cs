using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardGenerator : MonoBehaviour
{
    public string word;
    //[SerializeField] private GameObject gameBoardThree;
    [SerializeField] private GameObject gameBoardFour;
    [SerializeField] private GameObject gameBoardFive;
    [SerializeField]private GameObject gameBoardSix;

    //the Audio files
    public AudioSource audioSource;
    public AudioClip audioClip;

    [Header("UI")]
    public Button tryAgainButton;
    public Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioClip = GetComponent<AudioClip>();
        audioSource = GetComponent<AudioSource>();

        //sets audio
        audioSource.clip = DataContainer.AudioClip;

        tryAgainButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        Debug.Log("Started script!");
        word = DataContainer.Word;
        Debug.Log(word);
        int stringLength = word.Length;
        Debug.Log(stringLength);
        //this sets a value for each board prefab
        GameObject selectedBoard = null;

        if (stringLength == 5)
        {
            Debug.Log("Length is 5");
            selectedBoard = Instantiate(gameBoardFive, transform);
            
        }

        else if (stringLength == 6)
        {
            Debug.Log("Length is 6");
            selectedBoard = Instantiate(gameBoardSix, transform);
        }

        else if(stringLength == 4)
        {
            Debug.Log("Length is 4");
            selectedBoard = Instantiate(gameBoardFour, transform);
        }

        else
        {
            Debug.Log("odd word length");
        }

        // Wire buttons to the correct DynamicBoard instance
        if (selectedBoard != null)
        {
            DynamicBoard dynamicBoard = selectedBoard.GetComponent<DynamicBoard>();
            if (dynamicBoard != null)
            {
                dynamicBoard.Initialize(tryAgainButton, exitButton);
            }
            else
            {
                Debug.LogError("DynamicBoard not found on selectedBoard.");
            }
        }
    }

    public void winButton()
    {
        exitButton.gameObject.SetActive(true);
    }

    public void loseButton()
    {
        tryAgainButton.gameObject.SetActive(true);
    }
}
