using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip sawFall;
    [SerializeField] AudioClip sawHit;
    [SerializeField] AudioClip playerDeath;

    private void OnEnable()
    {
        CancelInvoke(); // Por si quedó colgado
        Invoke("PlaySawSound", 1.1f);
    }

    private void OnDisable()
    {
        CancelInvoke(); // Limpia el Invoke cuando se recicla
    }

    void PlaySawSound()
    {
        AudioManager.Instance.PlaySFX(sawFall);
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GameManager.instance.IncrementScore();
            AudioManager.Instance.PlaySFX(sawHit);
            gameObject.SetActive(false); // Reutilizable
        }

        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(playerDeath);
            GameManager.instance.GameOver();
        }
    }
}
