using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioClip button;

    public void Play()
    {
        AudioManager.Instance.PlaySFX(button);
        SceneManager.LoadScene("Game");
    }
}
