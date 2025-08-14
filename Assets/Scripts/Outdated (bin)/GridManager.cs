using System;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("Grid setup")]
    [SerializeField] private GridLayoutGroup grid;     // Put this on the same object (or drag it in)
    [SerializeField] private GameObject tilePrefab;    // UI prefab (must have a RectTransform)

    [Header("Test values")]
    [SerializeField] private int rows = 2;
    [SerializeField] private int cols = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Reset()
    {
        grid = GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        SetupGrid(cols);                
        GenerateGrid(rows * cols);      
    }

    private void SetupGrid(int columns)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;
    }

    private void GenerateGrid(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(tilePrefab, grid.transform, false);
        }
    }
}
