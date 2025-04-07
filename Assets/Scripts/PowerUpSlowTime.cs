using UnityEngine;

public class PowerUpSlowTime : PowerUpBase
{
    [SerializeField] float slowFactor = 0.3f;
    [SerializeField] float slowDuration = 3f;

    public override void ApplyEffect(PlayerController player)
    {
        PowerUpEffectManager.Instance.ApplySlowMotion(slowFactor, slowDuration);
    }
}