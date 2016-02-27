using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    public GameObject StartingObject;
    public GameObject ActiveShape { get; private set; }

    private void Start()
    {
        // Initialize shapes
        if (StartingObject == null)
        {
            throw new InvalidOperationException("startingObject must be set in editor.");
        }

        StartingObject.transform.position = new Vector2(0, 10);
        StartingObject = Instantiate(StartingObject);

        ActiveShape = StartingObject;

        // Begin updates, and repeat every second
        InvokeRepeating("UpdateShapes", 1f, 1f);
    }

    private void UpdateShapes()
    {
        Vector3 position = StartingObject.transform.position;
        StartingObject.transform.position = new Vector3(position.x, position.y - 1, 0);
        Debug.Log(StartingObject.transform.position);
    }
}
