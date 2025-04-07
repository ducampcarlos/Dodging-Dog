using UnityEngine;

public class LeafyFall : MonoBehaviour
{
    [SerializeField] float fallSpeed = 1f;
    [SerializeField] float swaySpeed = 2f;
    [SerializeField] float swayAmount = 1f;

    float swayOffset;

    private void OnEnable()
    {
        // Offset aleatorio para que no todos se muevan igual
        swayOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed + swayOffset) * swayAmount;
        transform.position += new Vector3(sway * Time.deltaTime, -fallSpeed * Time.deltaTime, 0f);
    }
}
