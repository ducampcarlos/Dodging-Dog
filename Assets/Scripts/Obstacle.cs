using UnityEngine;
using UnityEngine.Rendering;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip sawFall;
    [SerializeField] AudioClip sawHit;
    [SerializeField] AudioClip playerDeath;

    private void OnEnable()
    {
        Invoke("PlaySawSound", 1.1f);
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.instance.IncrementScore();

            AudioManager.Instance.PlaySFX(sawHit);

            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(playerDeath);
            GameManager.instance.GameOver();
        }
    }
}
