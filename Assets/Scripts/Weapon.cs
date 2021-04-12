using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float cooldown = 0.0f;
    void Update()
    {
        cooldown = cooldown - Time.deltaTime > 0 ? cooldown - Time.deltaTime : 0;
    }
    public virtual void fire()
    {
        if (cooldown == 0.0f)
        {
            Instantiate(Resources.Load(@"Prefabs\Projectile"), transform.position, transform.Find("Main Camera").rotation);
        }
    }
}
