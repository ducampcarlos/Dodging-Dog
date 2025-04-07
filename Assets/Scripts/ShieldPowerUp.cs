using System.Collections;
using UnityEngine;

public class ShieldPowerUp : PowerUpBase
{
    public float duration = 5f;

    public override void ApplyEffect(PlayerController player)
    {
        player.ActivateShield();
        player.StartCoroutine(DeactivateAfterTime(player));
    }

    IEnumerator DeactivateAfterTime(PlayerController player)
    {
        yield return new WaitForSeconds(duration);
        player.ConsumeShield();
    }
}
