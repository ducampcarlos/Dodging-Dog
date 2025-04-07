using UnityEngine;
using System.Collections;

public class PowerUpEffectManager : MonoBehaviour
{
    public static PowerUpEffectManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ApplySlowMotion(float slowFactor, float duration)
    {
        StartCoroutine(SlowTimeCoroutine(slowFactor, duration));
    }

    private IEnumerator SlowTimeCoroutine(float factor, float duration)
    {
        Time.timeScale = factor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
