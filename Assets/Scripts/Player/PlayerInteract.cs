using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Vector2 Destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
        float interactRange = 1f;
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach(Collider2D collider in colliderArray)
            {
                //Debug.Log(collider.name);
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }

                if (collider.TryGetComponent(out TeleportInteract teleportPlayer))
                {
                    Destination = teleportPlayer.PlayerTPCoords;
                    teleportPlayer.teleport();
                    transform.position = Destination;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Use the 'F' key you Muppet");
        }

    }

    public NPCInteractable GetInteractableObject()
    {
        float interactRange = 1f;
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D collider in colliderArray)
        {
            //Debug.Log(collider.name);
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                return npcInteractable;
            }
        }
        return null;
    }
}
