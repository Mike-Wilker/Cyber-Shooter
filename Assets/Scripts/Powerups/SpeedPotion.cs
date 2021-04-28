using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Powerup
{
    private float remainingTime = 0.0f;
    const float DURATION = 5.0f;
    const float SLOW_TIME = 0.5f;
    const float MINIMUM_VOLUME = 0.25f;
    const float MAXIMUM_VOLUME = 0.5f;
    const float MINIMUM_PITCH = 0.875f;
    const float MAXIMUM_PITCH = 1.0f;
    private void Update()
    {
        if (remainingTime != 0.0f)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0.0f)
            {
                Time.timeScale /= SLOW_TIME;
                Destroy(gameObject);
            }
        }
    }
    public override void use()
    {
        Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX/Inject"), Random.Range(MINIMUM_VOLUME, MAXIMUM_VOLUME), Random.Range(MINIMUM_PITCH, MAXIMUM_PITCH));
        remainingTime = DURATION;
        Time.timeScale *= SLOW_TIME;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
        GetComponent<Collider>().enabled = false;
        GetComponent<Pickup>().enabled = false;
    }
}
