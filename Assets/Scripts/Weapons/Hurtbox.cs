using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Hurtbox : MonoBehaviour
{
    public float damage;
    public Entity owner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>() is Entity entity)
        {
            if (entity != owner)
            {
                entity.health -= damage;
            }
        }
    }
}
