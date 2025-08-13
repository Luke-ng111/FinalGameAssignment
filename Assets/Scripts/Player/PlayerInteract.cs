using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Vector2 Destination;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float interactRange = 1f;
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
            Debug.Log(transform.position);
            foreach(Collider2D collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 1f;
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
            Debug.Log(transform.position);
            foreach (Collider2D collider in colliderArray)
            {
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

    //calls interactable object for UI
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

    //calls portal object for UI
    public TeleportInteract GetTeleportObject()
    {
        float interactRange = 1f;
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D collider in colliderArray)
        {
            //Debug.Log(collider.name);
            if (collider.TryGetComponent(out TeleportInteract teleportPlayer))
            {
                return teleportPlayer;
            }
        }
        return null;
    }

}
