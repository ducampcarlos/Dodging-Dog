using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    public float fallSpeed = 1f;

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.GetComponent<PlayerController>());
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    public abstract void ApplyEffect(PlayerController player);
}
