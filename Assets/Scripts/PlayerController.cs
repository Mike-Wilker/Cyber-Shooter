using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Weapon))]
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Weapon weapon;
    float WALK_SPEED = 10.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        weapon = gameObject.GetComponent<Weapon>();
    }
    void Update()
    {
        transform.Find("Main Camera").Rotate(new Vector3(Input.GetAxis("Mouse Y"), 0.0f, 0.0f), Space.Self);
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f), Space.World);
        controller.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * WALK_SPEED + transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * WALK_SPEED);
        if (Input.GetButton("Fire1"))
        {
            weapon.fire();
        }
    }
}
