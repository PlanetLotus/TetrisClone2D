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
            UpdatePosition(activeShape.transform);
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
            UpdatePosition(activeShape.transform);
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
            UpdatePosition(activeShape.transform);
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

        Vector3 rotationPoint = GetRotationPoint(activeShape);
        activeShape.transform.RotateAround(activeShape.transform.position + rotationPoint, new Vector3(0, 0, 1), 90);
        activeShapeRotationIndex++;
        if (activeShapeRotationIndex == NumRotations)
        {
            activeShapeRotationIndex = 0;
        }
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
            activeShape.transform.position += new Vector3(0, -1);
            if (IsUpdateValid(activeShape.transform))
            {
                UpdatePosition(activeShape.transform);
            }
            else
            {
                // Collision detected
                activeShape.transform.position += new Vector3(0, 1);
                activeShape = null;
                activeShapeRotationIndex = 0;
                SpawnNextShape();
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

    private Vector3 GetRotationPoint(GameObject shape)
    {
        // Wow, there's really got to be a better way to figure out which prefab this is
        // Maybe attach a script with an enum?
        if (shape.name.StartsWith("T"))
        {
            switch (activeShapeRotationIndex)
            {
                case 0:
                    return new Vector3(1.5f, 1.5f);
                case 1:
                    return new Vector3(-1.5f, 1.5f);
                case 2:
                    return new Vector3(-1.5f, -1.5f);
                case 3:
                    return new Vector3(1.5f, -1.5f);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("I"))
        {
            switch (activeShapeRotationIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return new Vector3(0, 2);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("J"))
        {
            switch ((int)shape.transform.rotation.z)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return new Vector3(0, 2);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("L"))
        {
            switch ((int)shape.transform.rotation.z)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return new Vector3(0, 2);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("O"))
        {
            switch ((int)shape.transform.rotation.z)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return new Vector3(0, 2);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("S"))
        {
            switch ((int)shape.transform.rotation.z)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return new Vector3(0, 2);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("Z"))
        {
            switch ((int)shape.transform.rotation.z)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return new Vector3(0, 2);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else
        {
            throw new ArgumentException(string.Format("Unrecognized shape name: {0}", shape.name));
        }
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

    private void UpdatePosition(Transform parent)
    {
        foreach (Transform block in GetBlocks(activeShape.transform))
        {
            gridManager.UpdatePosition(block);
        }
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
    private int activeShapeRotationIndex;
    private GridManager gridManager;
    private const int NumRotations = 4;
}
