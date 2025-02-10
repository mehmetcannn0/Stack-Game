
using UnityEngine;

public class CubeMovementController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] CubeInputController cubeInputController;
    [SerializeField] CubeSpawnController cubeSpawnController;

    [SerializeField] GameObject yeniPrefab;

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
 
        //yonum z ýse

        bool intersection = true;
        if (cubeInputController.cubeDirection.z == -1 || cubeInputController.cubeDirection.z == 1)
        {
            float altLeft = cubeSpawnController.previousCube.position.x - cubeSpawnController.previousCube.localScale.x / 2;
            float altRight = cubeSpawnController.previousCube.position.x + cubeSpawnController.previousCube.localScale.x / 2;
            float ustLeft = transform.position.x - transform.localScale.x / 2;
            float ustRight = transform.position.x + transform.localScale.x / 2;

            if (altRight < ustLeft || altLeft > ustRight)
            {
                intersection = false;
            }
            else
            {
                float yeniLeft = Mathf.Max(altLeft, ustLeft);
                float yeniRight = Mathf.Min(altRight, ustRight);
                float randomSpawnpositionZ = Random.Range(-10, 11);
                Debug.Log("Random spawn position z: " + randomSpawnpositionZ);

                cubeSpawnController.spawnPosition.position = new Vector3((yeniLeft + yeniRight) / 2, transform.position.y + 1, randomSpawnpositionZ);
                cubeSpawnController.spawnPosition.localScale = new Vector3(Mathf.Abs(yeniRight - yeniLeft), transform.localScale.y, transform.localScale.z);

                gameObject.transform.localScale = new Vector3(Mathf.Abs(yeniRight - yeniLeft), transform.localScale.y, transform.localScale.z);
                gameObject.transform.position = new Vector3((yeniLeft + yeniRight) / 2, transform.position.y, transform.position.z);


            }

        }//yonum x ise
        else
        {

            float altLeft = cubeSpawnController.previousCube.position.z - cubeSpawnController.previousCube.localScale.z / 2;
            float altRight = cubeSpawnController.previousCube.position.z + cubeSpawnController.previousCube.localScale.z / 2;
            float ustLeft = transform.position.z - transform.localScale.z / 2;
            float ustRight = transform.position.z + transform.localScale.z / 2;
            if (altRight < ustLeft || altLeft > ustRight)
            {
                intersection = false;

            }
            else
            {

                float yeniLeft = Mathf.Max(altLeft, ustLeft);
                float yeniRight = Mathf.Min(altRight, ustRight);
                float randomSpawnpositionX = Random.Range(-10, 11);
                Debug.Log("Random spawn position x: " + randomSpawnpositionX);

                cubeSpawnController.spawnPosition.position = new Vector3(randomSpawnpositionX, transform.position.y + 1, (yeniLeft + yeniRight) / 2);
                cubeSpawnController.spawnPosition.localScale = new Vector3(transform.localScale.z, transform.localScale.y, Mathf.Abs(yeniRight - yeniLeft));

                gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Abs(yeniRight - yeniLeft));
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, (yeniLeft + yeniRight) / 2);


            }
        }

        if (!intersection)
        {
            Debug.Log("Küp negatif boyuta sahip, oyun sonlanýyor.");
            gameManager.isGamePause = true;
            gameManager.GameOver();
        }
        else
        {
            Debug.Log("Küp pozitif boyuta sahip, oyun devam ediyor.");
            gameManager.isGamePause = false;
            gameManager.UpdateScore();
        }


        cubeSpawnController.previousCube = transform;

    }
}
