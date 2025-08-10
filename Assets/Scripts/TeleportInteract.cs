using UnityEngine;

public class TeleportInteract : MonoBehaviour
{ 
    public Vector2 PlayerTPCoords;

    public void teleport()
    {
        Debug.Log("Teleport to: " + PlayerTPCoords);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
