using UnityEngine;

public class SawGhost : MonoBehaviour
{
    [SerializeField] float disappearY = 4f;
    [SerializeField] float reappearY = -2f;

    private SpriteRenderer sr;
    private Collider2D col;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        sr.enabled = true;
        if (col != null)
            col.enabled = true;
    }

    void Update()
    {
        float y = transform.position.y;

        // Se vuelve invisible en cierto tramo
        if (y < disappearY && y > reappearY)
        {
            sr.enabled = false;
            if (col != null) col.enabled = false; // Opcional, si querés que no te pueda dañar mientras es invisible
        }
        else
        {
            sr.enabled = true;
            if (col != null) col.enabled = true;
        }
    }
}
