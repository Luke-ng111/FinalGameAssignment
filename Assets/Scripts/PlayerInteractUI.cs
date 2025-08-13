using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{

    [SerializeField] private GameObject interactGameObject;
    [SerializeField] private GameObject teleportGameObject;
    [SerializeField] private PlayerInteract playerInteract;

    private void showFKey()
    {
        interactGameObject.SetActive(true);
    }

    private void hideFKey()
    {
        interactGameObject.SetActive(false);
    }

    private void showEKey()
    {
        teleportGameObject.SetActive(true);
    }

    private void hideEKey()
    {
        teleportGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //shows and hides UI element in range of animal interaction
        if (playerInteract.GetInteractableObject() != null) 
        {
            showFKey();
        }
        else
        {
            hideFKey();
        }

        //shows and hides UI element in range of scene transition
        if (playerInteract.GetTeleportObject() != null)
        {
            showEKey();
        }
        else
        {
            hideEKey();
        }
    }
}
