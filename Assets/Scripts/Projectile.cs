using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody body;
    const float SPEED = 100.0f; 
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        body.MovePosition(transform.position + transform.forward * Time.deltaTime * SPEED);
    }
}
