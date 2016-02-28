﻿using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    public GameObject StartingObject;
    public bool DisableGravity;
    public bool DisableSpawning;

    public void MoveActiveShapeDown()
    {
        if (activeShape == null)
        {
            return;
        }

        Vector3 position = activeShape.transform.position;
        float newY = position.y - 1;

        if (newY < GridManager.MinY)
        {
            return;
        }

        activeShape.transform.position = new Vector3(position.x, newY, 0);
    }

    public void MoveActiveShapeLeft()
    {
        if (activeShape == null)
        {
            return;
        }

        Vector3 position = activeShape.transform.position;
        float newX = position.x - 1;

        if (newX < GridManager.MinX)
        {
            return;
        }

        activeShape.transform.position = new Vector3(newX, position.y, 0);
    }

    public void MoveActiveShapeRight()
    {
        if (activeShape == null)
        {
            return;
        }

        Vector3 position = activeShape.transform.position;
        float newX = position.x + 1;

        if (newX > GridManager.MaxX)
        {
            return;
        }

        activeShape.transform.position = new Vector3(newX, position.y, 0);
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

    private GameObject activeShape;
}
