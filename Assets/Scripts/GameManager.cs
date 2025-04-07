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

    [SerializeField] AudioClip button;

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

    public void GameOver()
    {
        gameOver = true;
        obstacleSpawner.StopSpawning();
        gameOverPanel.SetActive(true);
        playerController.PlayParticleDeath();
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
        AudioManager.Instance.PlaySFX(button);
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX(button);
        SceneManager.LoadScene("Menu");
    }
}
