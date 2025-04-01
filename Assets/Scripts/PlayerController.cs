using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    SpriteRenderer sr;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void OnDisable()
    {
        anim.SetBool("IsWalking", false);
    }

    void Update()
    {
        float velocityX = rb.linearVelocity.x;

        if (!Mathf.Approximately(velocityX, 0)) // Prevent flipping when stopping
        {
            sr.flipX = velocityX < 0;
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }   
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = Vector2.zero;

#if UNITY_ANDROID || UNITY_EDITOR                              
        // Android Controls (Touch)
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            float touchX = Touchscreen.current.primaryTouch.position.x.ReadValue();
            moveDirection = (touchX < Screen.width / 2f) ? Vector2.left : Vector2.right;
        }
#elif UNITY_STANDALONE || UNITY_EDITOR
        // PC Controls (Mouse)
        if (Mouse.current.leftButton.isPressed)
        {
            float mouseX = Mouse.current.position.x.ReadValue();
            moveDirection = (mouseX < Screen.width / 2f) ? Vector2.left : Vector2.right;
        }
#endif


        rb.linearVelocity = moveDirection * speed;
    }
}
