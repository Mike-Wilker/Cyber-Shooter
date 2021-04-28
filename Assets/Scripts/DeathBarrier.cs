using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>() is Entity entity)
        {
            entity.die();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Entity>() is Entity entity)
        {
            entity.die();
        }
    }
}
