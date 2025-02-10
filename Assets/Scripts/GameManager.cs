using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;  
     


    public bool isGamePause;
    public TextMeshProUGUI scoreText;
    public GameObject startMenuUI;
    public GameObject gameOverUI;
    public GameObject maxScoreUI;
    public TextMeshProUGUI maxScoreText;

    public int score = 0;

    private void Start()
    {
        startMenuUI.SetActive(true);
        gameOverUI.SetActive(false);
        maxScoreText.text = PlayerPrefs.GetInt("MaxScore", 0).ToString();
    }
    public void StartGame()
    {
        isGamePause = false;
        startMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        maxScoreUI.SetActive(false); 
        score = 0; 
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        isGamePause=true;
        maxScoreUI.SetActive(true);
        gameOverUI.SetActive(true);
        if (PlayerPrefs.GetInt("MaxScore", 0) < score)
        {
            PlayerPrefs.SetInt("MaxScore", (int)score);
        }
        maxScoreText.text = PlayerPrefs.GetInt("MaxScore", 0).ToString();
    }
    public void RestartGame()
    {
        isGamePause=false;
        startMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        maxScoreUI.SetActive(false); 
        SceneManager.LoadScene(0); 
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text =  score.ToString();
        MoveCameraUp();


    }
    private void MoveCameraUp()
    {
        if (score>15)
        {
            cameraTransform.position += new Vector3(0, 1, 0);
            
        }
    }

}
