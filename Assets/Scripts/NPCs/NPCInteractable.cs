using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractable : MonoBehaviour
{
    public void Interact()
    {
        Animal animal = GetComponent<Animal>();
        if (animal == null)
            return;

        //Check if already interacted
        if (DataContainer.interactedNPCs.Contains(animal.Species))
        {
            Debug.Log("Already interacted with this NPC.");
            return;
        }

        //mark NPC as interacted with
        DataContainer.interactedNPCs.Add(animal.Species);

        DataContainer.Word = animal.Species;
        DataContainer.AudioClip = animal.AudioClip;

        Debug.Log("Interaction!");
        PlayerPosition.playerPosition = GameObject.FindWithTag("Player").transform.position;
        Debug.Log("load new scene");
        SceneManager.LoadScene("Wordle");
    }
}
