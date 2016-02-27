using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        shapeManager = gameObject.GetComponent<ShapeManager>();
    }

    void Update()
    {
        activeShape = shapeManager.ActiveShape;

        if (activeShape != null)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                MoveShapeDown();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                MoveShapeLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                MoveShapeRight();
            }
        }
    }

    private void MoveShapeDown()
    {
        Vector3 position = activeShape.transform.position;
        activeShape.transform.position = new Vector3(position.x, position.y - 1, 0);
    }

    private void MoveShapeLeft()
    {
        Vector3 position = activeShape.transform.position;
        activeShape.transform.position = new Vector3(position.x - 1, position.y, 0);
    }

    private void MoveShapeRight()
    {
        Vector3 position = activeShape.transform.position;
        activeShape.transform.position = new Vector3(position.x + 1, position.y, 0);
    }

    private GameObject activeShape;
    private ShapeManager shapeManager;
}
