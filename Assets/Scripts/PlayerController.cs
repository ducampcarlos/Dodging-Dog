using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float velocityX = rb.linearVelocity.x;

        if (!Mathf.Approximately(velocityX, 0)) // Prevent flipping when stopping
        {
            sr.flipX = velocityX < 0;
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = Vector2.zero;

#if UNITY_STANDALONE || UNITY_EDITOR
        // PC Controls (Mouse)
        if (Mouse.current.leftButton.isPressed)
        {
            float mouseX = Mouse.current.position.x.ReadValue();
            moveDirection = (mouseX < Screen.width / 2f) ? Vector2.left : Vector2.right;
        }
#elif UNITY_ANDROID
        // Android Controls (Touch)
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            float touchX = Touchscreen.current.primaryTouch.position.x.ReadValue();
            moveDirection = (touchX < Screen.width / 2f) ? Vector2.left : Vector2.right;
        }
#endif

        rb.linearVelocity = moveDirection * speed;
    }
}
