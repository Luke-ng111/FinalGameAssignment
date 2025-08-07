using UnityEngine;

public class Row : MonoBehaviour
{
    public Tile[] Tiles { get; private set; }

    private void Awake()
    {
        Tiles = GetComponentsInChildren<Tile>();
    }
}
