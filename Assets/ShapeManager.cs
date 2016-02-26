using UnityEngine;
using System;

public class ShapeManager : MonoBehaviour
{
    public GameObject startingObject;

    public void Start()
    {
        // Initialize shapes
        if (startingObject == null)
        {
            throw new InvalidOperationException("startingObject must be set in editor.");
        }

        startingObject.transform.position = new Vector2(0, 10);
        startingObject = Instantiate(startingObject);

        // Begin updates, and repeat every second
        InvokeRepeating("UpdateShapes", 1f, 1f);
    }

    private void UpdateShapes()
    {
        Vector3 position = startingObject.transform.position;
        startingObject.transform.position = new Vector3(position.x, position.y - 1, 0);
        Debug.Log(startingObject.transform.position);
    }
}
