using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthPotion : Powerup
{
    const float HEALING_AMOUNT = 50.0f;
    const float MINIMUM_VOLUME = 0.25f;
    const float MAXIMUM_VOLUME = 0.5f;
    const float MINIMUM_PITCH = 0.875f;
    const float MAXIMUM_PITCH = 1.0f;
    public override void use()
    {
        Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX/Inject"), Random.Range(MINIMUM_VOLUME, MAXIMUM_VOLUME), Random.Range(MINIMUM_PITCH, MAXIMUM_PITCH));
        PlayerController player = FindObjectOfType<PlayerController>();
        player.health = player.health + HEALING_AMOUNT >= player.MAX_HEALTH ? player.MAX_HEALTH : player.health + HEALING_AMOUNT;
        Destroy(gameObject);
    }
}