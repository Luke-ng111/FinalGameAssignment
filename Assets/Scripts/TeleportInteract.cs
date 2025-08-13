using UnityEngine;

public class TeleportInteract : MonoBehaviour
{ 
    public Vector2 PlayerTPCoords;

    public void teleport()
    {
        Debug.Log("Teleport to: " + PlayerTPCoords);
    }
}
