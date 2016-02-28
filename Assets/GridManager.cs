using UnityEngine;

public class GridManager : MonoBehaviour
{
    public const int MinX = 0;
    public const int MaxX = 10;
    public const int MinY = 0;

    public bool[,] Grid = new bool[Width, Height];

    private void Start()
    {
    }

    private void Update()
    {
        // Check for completed rows and clear them
    }

    private const int Width = 10;
    private const int Height = 20;
}
