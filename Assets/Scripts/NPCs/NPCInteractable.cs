using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            //InteractionManager.Instance.SetInteractionTextActive(true);
            StartCoroutine(CompletedInteractionText());

            return;
        }

        IEnumerator CompletedInteractionText()
        {
            Debug.Log("grr");
            InteractionManager.Instance.SetInteractionTextActive(true);
            yield return new WaitForSeconds(1f);
            InteractionManager.Instance.SetInteractionTextActive(false);
            Debug.Log("end coroutine");
        }


        //mark NPC as interacted with
        DataContainer.interactedNPCs.Add(animal.Species);

        DataContainer.Word = animal.Species;
        DataContainer.AudioClip = animal.AudioClip;

        PlayerPosition.playerPosition = GameObject.FindWithTag("Player").transform.position;
        Debug.Log("load new scene");
        SceneManager.LoadScene("Wordle");
    }
}
