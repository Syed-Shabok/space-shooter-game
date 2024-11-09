using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public GameObject enemyOne;
    public GameObject asteroidOne;
    public float spawnRange = 8.0f;
    public float despawnTimer = 10.0f;
    public int playerScore = 0;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI playerScoreDisplay;
    public TextMeshProUGUI startMenuHighScore;
    public TextMeshProUGUI GameOverMenuHighScore;

    // Start is called before the first frame update
    void Start()
    {   
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0.0f;

        startMenuHighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        InvokeRepeating("SpawnEnemy", 1f, 1f);
        InvokeRepeating("SpawnAsteroid", 2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-spawnRange, spawnRange), 6.0f);
        GameObject enemyInstance = Instantiate(enemyOne, spawnPoint, enemyOne.transform.rotation);
        Destroy(enemyInstance, despawnTimer);
    }

    public void SpawnAsteroid()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-spawnRange, spawnRange), 6.0f);
        GameObject asteroidInstance = Instantiate(asteroidOne, spawnPoint, asteroidOne.transform.rotation);
        Destroy(asteroidInstance, despawnTimer);
    }

    public void StartButtonPressed()
    {
        Time.timeScale = 1.0f;
        startMenu.SetActive(false);
    }

    public void PauseButtonPressed()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeButtonPressed()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void PlayAgainButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void UpdatePlayerScore(int points)
    {
        playerScore += points;
        playerScoreDisplay.text = playerScore.ToString();

        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            GameOverMenuHighScore.text = "New High Score: " + playerScore.ToString();
        }
    }
}
