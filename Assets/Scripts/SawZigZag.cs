using UnityEngine;

public class SawZigZag : MonoBehaviour
{
    [SerializeField] float fallSpeed = 2f;
    [SerializeField] float zigZagAmplitude = 1f;
    [SerializeField] float zigZagFrequency = 2f;

    private float startX;
    private float timeOffset;

    void OnEnable()
    {
        startX = transform.position.x;
        timeOffset = Random.Range(0f, 100f); // Para evitar que todas zigzagueen sincronizadas
    }

    void Update()
    {
        float newX = startX + Mathf.Sin((Time.time + timeOffset) * zigZagFrequency) * zigZagAmplitude;
        transform.position = new Vector3(newX, transform.position.y - fallSpeed * Time.deltaTime, transform.position.z);
    }
}
