using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        shapeManager = gameObject.GetComponent<ShapeManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            shapeManager.MoveActiveShapeDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            shapeManager.MoveActiveShapeLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            shapeManager.MoveActiveShapeRight();
        }
    }

    private GameObject activeShape;
    private ShapeManager shapeManager;
}
