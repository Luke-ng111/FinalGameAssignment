using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{

    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;

    private void show()
    {
        containerGameObject.SetActive(true);
    }

    private void hide()
    {
        containerGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            show();
        }
        else
        {
            hide();
        }
    }
}
