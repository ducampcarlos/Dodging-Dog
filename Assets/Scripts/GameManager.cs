using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] ObstacleSpawner obstacleSpawner;

    int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    bool gameOver = false;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    public void GameOver()
    {
        gameOver = true;
        obstacleSpawner.StopSpawning();
        gameOverPanel.SetActive(true);
        playerController.enabled = false;
    }

    public void IncrementScore()
    {
        if (!gameOver)
        {
            score++;
            scoreText.text = score.ToString();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
