using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed;
    private float remainingLifetime = 8.0f;
    private static AudioClip impactSound = null;
    private const float MINIMUM_VOLUME = 0.125f;
    private const float MAXIMUM_VOLUME = 0.25f;
    private const float MINIMUM_PITCH = 2.0f;
    private const float MAXIMUM_PITCH = 4.0f;
    void Start()
    {
        if (impactSound == null)
        {
            impactSound = Resources.Load<AudioClip>(@"SFX\impact");
        }
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
    }
    private void Update()
    {
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime <= 0.0f)
        {
            
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isStatic)
        {
            Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\One Shot Audio Source"), transform.position, transform.rotation).setup(impactSound, Random.Range(MINIMUM_VOLUME, MAXIMUM_VOLUME), Random.Range(MINIMUM_PITCH, MAXIMUM_PITCH));
            Destroy(gameObject);
        }
    }
}
