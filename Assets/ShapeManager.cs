using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    public GameObject StartingObject;

    public void MoveActiveShapeDown()
    {
        if (activeShape == null)
        {
            return;
        }

        Vector3 position = activeShape.transform.position;
        activeShape.transform.position = new Vector3(position.x, position.y - 1, 0);
    }

    public void MoveActiveShapeLeft()
    {
        if (activeShape == null)
        {
            return;
        }

        Vector3 position = activeShape.transform.position;
        activeShape.transform.position = new Vector3(position.x - 1, position.y, 0);
    }

    public void MoveActiveShapeRight()
    {
        if (activeShape == null)
        {
            return;
        }

        Vector3 position = activeShape.transform.position;
        activeShape.transform.position = new Vector3(position.x + 1, position.y, 0);
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

        StartingObject.transform.position = new Vector2(0, 10);
        StartingObject = Instantiate(StartingObject);

        activeShape = StartingObject;

        // Begin updates, and repeat every second
        InvokeRepeating("UpdateShapes", 1f, 1f);
    }

    private void UpdateShapes()
    {
        Vector3 position = StartingObject.transform.position;
        StartingObject.transform.position = new Vector3(position.x, position.y - 1, 0);
        Debug.Log(StartingObject.transform.position);
    }

    private GameObject activeShape;
}
