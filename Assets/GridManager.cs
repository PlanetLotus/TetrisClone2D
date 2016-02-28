using UnityEngine;

public class GridManager : MonoBehaviour
{
    public const int MinX = 0;
    public const int MaxX = 10;
    public const int MinY = 0;

    public bool IsValidPosition(Transform transform)
    {
        // Check out of bounds
        if (transform.position.x > MaxX || transform.position.x < MinX ||
            transform.position.y < MinY)
        {
            return false;
        }

        // If spot is full or if spot is occupied by another shape, it's invalid
        // This may be naive, but we assume that the shape won't try to collide with itself
        if (grid[(int)transform.position.x, (int)transform.position.y] != null &&
            grid[(int)transform.position.x, (int)transform.position.y].parent == transform.parent)
        {
            return false;
        }

        return true;
    }

    private void Start()
    {
    }

    private void Update()
    {
        // Check for completed rows and clear them
    }

    private const int Width = 10;
    private const int Height = 24;

    // Each transform is a block, not the entire shape
    // Instead of just setting a flag here, having the actual object helps
    // us determine how it relates to the objects around it, like
    // whether they're attached to the same shape.
    private Transform[,] grid = new Transform[Width, Height];
}
