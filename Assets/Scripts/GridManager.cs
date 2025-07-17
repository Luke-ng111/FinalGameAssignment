using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int rows = 5;
    private int cols = 8;
    private float tileSize = 1.0f;
    public GameObject tilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generateGrid();
    }

    private void generateGrid()
    {
        GameObject referenceTile = GameObject.Instantiate(tilePrefab);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject) Instantiate(referenceTile, transform);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2 (posX, posY);
            }
        }

        Destroy(referenceTile );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
