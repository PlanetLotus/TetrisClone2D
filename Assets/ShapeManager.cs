﻿using UnityEngine;
using System;

using Random = System.Random;

public class ShapeManager : MonoBehaviour
{
    public GameObject[] Shapes;
    public GameObject StartingObject;
    public bool DisableGravity;
    public bool DisableSpawning;
    public bool TestRows;

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

        // Position and rotation are value types so this actually copies the value, not the reference
        Vector3 oldPosition = activeShape.transform.position;
        Quaternion oldRotation = activeShape.transform.rotation;

        Vector3 rotationPoint = GetRotationPoint(activeShape);
        activeShape.transform.RotateAround(activeShape.transform.position + rotationPoint, new Vector3(0, 0, 1), 90);

        if (IsUpdateValid(activeShape.transform))
        {
            UpdatePosition(activeShape.transform);

            activeShapeRotationIndex++;
            if (activeShapeRotationIndex == NumRotations)
            {
                activeShapeRotationIndex = 0;
            }
        }
        else
        {
            activeShape.transform.position = oldPosition;
            activeShape.transform.rotation = oldRotation;
        }
    }

    private void Start()
    {
        gridManager = GetComponent<GridManager>();

        // Spawn two nearly-complete rows with the next shape as an O for testing
        if (TestRows)
        {
            StartingObject = (GameObject)Resources.Load("O");
            SpawnTestRows();
        }

        // Initialize shapes
        if (StartingObject == null)
        {
            SpawnNextShape();
        }
        else
        {
            StartingObject.transform.position = new Vector2(0, 20);
            activeShape = Instantiate(StartingObject);
        }

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
                gridManager.ClearFullRows();
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

        Random random = new Random();
        int randomIndex = random.Next(Shapes.Length);
        GameObject randomShape = Shapes[randomIndex];

        randomShape.transform.position = new Vector2(0, 20);
        activeShape = Instantiate(randomShape);
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
                    return new Vector3(0, 2);
                case 1:
                    return new Vector3(-2, 0);
                case 2:
                    return new Vector3(0, -2);
                case 3:
                    return new Vector3(2, 0);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("J"))
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
        else if (shape.name.StartsWith("L"))
        {
            switch (activeShapeRotationIndex)
            {
                case 0:
                    return new Vector3(0.5f, 1.5f);
                case 1:
                    return new Vector3(-1.5f, 0.5f);
                case 2:
                    return new Vector3(-0.5f, -1.5f);
                case 3:
                    return new Vector3(1.5f, -0.5f);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("O"))
        {
            switch (activeShapeRotationIndex)
            {
                case 0:
                    return new Vector3(1, 1);
                case 1:
                    return new Vector3(-1, 1);
                case 2:
                    return new Vector3(-1, -1);
                case 3:
                    return new Vector3(1, -1);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("S"))
        {
            switch (activeShapeRotationIndex)
            {
                case 0:
                    return new Vector3(1.5f, 0.5f);
                case 1:
                    return new Vector3(-1.5f, 1.5f);
                case 2:
                    return new Vector3(-1.5f, -0.5f);
                case 3:
                    return new Vector3(1.5f, -1.5f);
                default:
                    throw new InvalidOperationException(string.Format("Unexpected rotation index: {0}", activeShapeRotationIndex));
            }
        }
        else if (shape.name.StartsWith("Z"))
        {
            switch (activeShapeRotationIndex)
            {
                case 0:
                    return new Vector3(1.5f, 0.5f);
                case 1:
                    return new Vector3(-1.5f, 1.5f);
                case 2:
                    return new Vector3(-1.5f, -0.5f);
                case 3:
                    return new Vector3(1.5f, -1.5f);
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
        foreach (Transform block in GetBlocks(parent))
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
        foreach (Transform block in GetBlocks(parent))
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

    private void SpawnTestRows()
    {
        // Spawn a nearly-complete row along the bottom
        GameObject I = (GameObject)Resources.Load("I");
        for (int i = 0; i < 8; i++)
        {
            I.transform.position = new Vector3(i, 0);
            GameObject testShape = Instantiate(I);
            UpdatePosition(testShape.transform);
        }
    }

    private GameObject activeShape;
    private int activeShapeRotationIndex;
    private GridManager gridManager;
    private const int NumRotations = 4;
}
