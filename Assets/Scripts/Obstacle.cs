using UnityEngine;
using UnityEngine.Rendering;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}
