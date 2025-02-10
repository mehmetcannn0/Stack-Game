
using UnityEngine;

public class CubeMovementController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] CubeInputController cubeInputController;
    [SerializeField] CubeSpawnController cubeSpawnController;
     
    public float cubeSpeed;
     
    private float minX = -10f, maxX = 10f;
    private float minZ = -10f, maxZ = 10f;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        cubeSpawnController = FindAnyObjectByType<CubeSpawnController>();
        cubeInputController = FindAnyObjectByType<CubeInputController>();
        transform.localScale = cubeSpawnController.previousCube.localScale;
    }
     
    void Update()
    {
        if (!gameManager.isGamePause)
        {
            SetCubeMovement();
            
        }
         
    }

    //private void SetCubeMovement()
    //{
    //    transform.position += cubeInputController.cubeDirection * cubeSpeed * Time.deltaTime;

    //    if (transform.position.x >= maxX || transform.position.x <= minX)
    //    {
    //        cubeInputController.cubeDirection.x *= -1;
    //    }

    //    if (transform.position.z >= maxZ || transform.position.z <= minZ)
    //    {
    //        cubeInputController.cubeDirection.z *= -1;
    //    }
    //}
    private void SetCubeMovement()
    { 
        Vector3 newPosition = transform.position + cubeInputController.cubeDirection * cubeSpeed * Time.deltaTime;
         
        if (newPosition.x >= maxX)
        {
            newPosition.x = maxX;  
            cubeInputController.cubeDirection.x *= -1;  
        }
        else if (newPosition.x <= minX)
        {
            newPosition.x = minX;  
            cubeInputController.cubeDirection.x *= -1;   
        }

        if (newPosition.z >= maxZ)
        {
            newPosition.z = maxZ;  
            cubeInputController.cubeDirection.z *= -1;  
        }
        else if (newPosition.z <= minZ)
        {
            newPosition.z = minZ; 
            cubeInputController.cubeDirection.z *= -1;   
        } 
        transform.position = newPosition;
    }
    public void StopCube()
    {
        cubeSpeed = 0; 
        gameManager.isGamePause = true;

        if (cubeInputController.cubeDirection.z == -1 || cubeInputController.cubeDirection.z == 1)
        {
            gameObject.transform.localScale = new Vector3( 
                cubeSpawnController.previousCube.localScale.x-Mathf.Abs(transform.position.x),
                transform.localScale.y,transform.localScale.z);
            Debug.Log(gameObject.transform.position);
            gameObject.transform.position = new Vector3(transform.position.x / 2, transform.position.y, transform.position.z);
            cubeSpawnController.spawnPosition.position = new Vector3 (transform.position.x,transform.position.y+1,10);
  
        }
        else
        {
            gameObject.transform.localScale = new Vector3( 
                transform.localScale.x,transform.localScale.y, 
                cubeSpawnController.previousCube.localScale.z - Mathf.Abs(transform.position.z));

            Debug.Log(gameObject.transform.position);
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z / 2);
            cubeSpawnController.spawnPosition.position = new Vector3(-10, transform.position.y + 1, transform.position.z);

        }
        gameManager.isGamePause = false;

        if (gameObject.transform.localScale.x < 0 || gameObject.transform.localScale.y < 0 || gameObject.transform.localScale.z < 0)
        {
            Debug.Log("Küp negatif boyuta sahip, oyun sonlanýyor."); 
            gameManager.isGamePause = true;
            gameManager.GameOver();
        }
        else
        {
            gameManager.UpdateScore();
        }


        cubeSpawnController.previousCube = transform;
    }
}
