using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    public GameObject[] Shapes;
    public GameObject StartingObject;
    public bool DisableGravity;
    public bool DisableSpawning;

    public void MoveActiveShapeDown()
    {
        if (activeShape == null)
        {
            return;
        }

        activeShape.transform.position += new Vector3(0, -1);
        if (IsUpdateValid(activeShape.transform))
        {
            // Update grid
        }
        else
        {
            // Reset if invalid
            activeShape.transform.position += new Vector3(0, 1);
        }
    }

    public void MoveActiveShapeLeft()
    {
        if (activeShape == null)
        {
            return;
        }

        activeShape.transform.position += new Vector3(-1, 0);
        if (IsUpdateValid(activeShape.transform))
        {
            // Update grid
        }
        else
        {
            // Reset if invalid
            activeShape.transform.position += new Vector3(1, 0);
        }
    }

    public void MoveActiveShapeRight()
    {
        if (activeShape == null)
        {
            return;
        }

        activeShape.transform.position += new Vector3(1, 0);
        if (IsUpdateValid(activeShape.transform))
        {
            // Update grid
        }
        else
        {
            // Reset if invalid
            activeShape.transform.position += new Vector3(-1, 0);
        }
    }

    public void RotateActiveShape()
    {
        if (activeShape == null)
        {
            return;
        }

        activeShape.transform.Rotate(0, 0, 90);
    }

    private void Start()
    {
        // Initialize shapes
        if (StartingObject == null)
        {
            throw new InvalidOperationException("startingObject must be set in editor.");
        }

        gridManager = GetComponent<GridManager>();

        StartingObject.transform.position = new Vector2(0, 20);
        activeShape = Instantiate(StartingObject);

        // Begin updates, and repeat every second
        InvokeRepeating("UpdateShapes", 1f, 1f);
    }

    private void UpdateShapes()
    {
        if (!DisableGravity && activeShape != null)
        {
            Vector3 position = activeShape.transform.position;
            float newY = position.y - 1;

            // Stop falling if we've reached the bottom
            if (newY < GridManager.MinY)
            {
                activeShape = null;
                SpawnNextShape();
            }
            else
            {
                activeShape.transform.position = new Vector3(position.x, position.y - 1, 0);
            }
        }
    }

    private void SpawnNextShape()
    {
        if (DisableSpawning)
        {
            return;
        }

        GameObject shape = (GameObject)Resources.Load("I");
        shape.transform.position = new Vector2(0, 20);
        activeShape = Instantiate(shape);
    }

    // Checks all children of shape to make sure they all move to a valid grid location
    private bool IsUpdateValid(Transform parent)
    {
        foreach (Transform block in GetBlocks(activeShape.transform))
        {
            if (!gridManager.IsValidPosition(block))
            {
                return false;
            }
        }

        return true;
    }

    private static Transform[] GetBlocks(Transform parent)
    {
        Transform[] blocks = new Transform[parent.childCount];
        for (int i = 0; i < parent.childCount; i++)
        {
            blocks[i] = parent.GetChild(i);
        }

        return blocks;
    }

    private GameObject activeShape;
    private GridManager gridManager;
}
