 
using UnityEngine;

public class CubeInputController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [HideInInspector] public Vector3 cubeDirection;
    [SerializeField] CubeMovementController cubeMovementController;
    [SerializeField] CubeSpawnController cubeSpawnController;

    private void Awake()
    {

        cubeSpawnController = FindAnyObjectByType<CubeSpawnController>();

        Transform lastChild = transform.GetChild(transform.childCount - 1); 
        cubeMovementController = lastChild.GetComponent<CubeMovementController>(); 
    }
    void Start()
    {
        cubeDirection = Vector3.left;

    }
    //TODO: fixed update
    void Update()
    {
        if (!gameManager.isGamePause)
        {
            HandleCubeInput();

        }

    }

    private void HandleCubeInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeCubeDirection();
            cubeMovementController.StopCube();
            if (!gameManager.isGamePause)
            {
                cubeSpawnController.SpawnCube();
                
            }

            Transform lastChild = transform.GetChild(transform.childCount - 1);
            cubeMovementController = lastChild.GetComponent<CubeMovementController>();
             

        }
    }
    private void ChangeCubeDirection()
    {

        if (cubeDirection.x == -1 || cubeDirection.x == 1)
        {
            Debug.Log("Cube Direction x");
            cubeDirection = Vector3.forward;
        }
        else
        {
            Debug.Log("Cube Direction z");
            cubeDirection = Vector3.left;
        }
    }
}
