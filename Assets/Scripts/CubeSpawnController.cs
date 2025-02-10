
using UnityEngine;

public class CubeSpawnController : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] GameManager gameManager;
    public GameObject cubesParent;
    public Transform spawnPosition;
    public Transform previousCube;
    [SerializeField] private Color[] cubeColors;  
    private int currentColorIndex = 0;

    public void SpawnCube()
    {
        if (gameManager.score % 5 ==0 )
        {
            ChangeCubeColor();
        }

        //spawnPosition.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + 1, spawnPosition.position.z);
        GameObject newCube = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity , cubesParent.transform);
        Renderer cubeRenderer = newCube.GetComponent<Renderer>();
        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = cubeColors[currentColorIndex]; // Küpü mevcut renkle ayarla
        }
    }
    // Renk deðiþimini kontrol et
    public void ChangeCubeColor()
    {
        currentColorIndex++;
        if (currentColorIndex >= cubeColors.Length)
        {
            currentColorIndex = 0; // Renkler bittiðinde sýfýrla
        }
    }
}
