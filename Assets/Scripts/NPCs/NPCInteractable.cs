using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractable : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Interaction!");
        PlayerPosition.playerPosition = GameObject.FindWithTag("Player").transform.position;
        Debug.Log("load new scene");
        SceneManager.LoadScene("Wordle");
    }
}
