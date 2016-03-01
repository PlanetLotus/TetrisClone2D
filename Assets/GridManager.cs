using UnityEngine;

public class GridManager : MonoBehaviour
{
    public bool IsValidPosition(Transform transform)
    {
        // Check out of bounds
        if (transform.position.x > MaxX || transform.position.x < MinX ||
            transform.position.y < MinY)
        {
            return false;
        }

        // If spot is full (not null) and is occupied by another shape (different parent), it's invalid
        // This may be naive, but we assume that the shape won't try to collide with itself
        if (grid[(int)transform.position.x, (int)transform.position.y] != null &&
            grid[(int)transform.position.x, (int)transform.position.y].parent != transform.parent)
        {
            return false;
        }

        return true;
    }

    public void UpdatePosition(Transform transform)
    {
        // Also naive. Trusts the caller that this position is valid.
        // Should really be "UpdatePositionIfValid" and return whether it was updated.
        // Also confusing that this is just one block and not the whole shape.
        UnsetOldPosition(transform);
        grid[(int)transform.position.x, (int)transform.position.y] = transform;
    }

    private void UnsetOldPosition(Transform transform)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (grid[x, y] == transform)
                {
                    grid[x, y] = null;
                    return;
                }
            }
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        // Check for completed rows and clear them
    }

    private const int Width = 10;
    private const int Height = 26;
    private const int MinX = 0;
    private const int MaxX = 10;
    private const int MinY = 0;

    // Each transform is a block, not the entire shape
    // Instead of just setting a flag here, having the actual object helps
    // us determine how it relates to the objects around it, like
    // whether they're attached to the same shape.
    private Transform[,] grid = new Transform[Width, Height];
}
