using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity
{
    CharacterController controller = null;
    private Pickup _pickupTarget = null;
    public Pickup pickupTarget
    {
        get
        {
            return _pickupTarget;
        }
        set
        {
            if (value == null)
            {
                hud.centerMessage.text = "";
            }
            else
            {
                hud.centerMessage.text = "Press E to pick up " + value.name;
            }
            _pickupTarget = value;
        }
    }
    public HUD hud = null;
    public Weapon heldWeapon = null;
    public override float MAX_HEALTH { get { return 100.0f; } }
    private const float GROUND_VELOCITY_DAMPING_RATE = 8.0f;
    private const float AIR_VELOCITY_DAMPING_RATE = 1.0f;
    private const float WALK_SPEED = 32.0f;
    private const float SPRINT_SPEED_COEFFICIENT = 1.5f;
    private const float AIR_CONTROL_COEFFICIENT = 0.125f;
    private const float JUMP_FORCE = 6.0f;
    Vector3 velocity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        hud.healthBar.maxValue = MAX_HEALTH;
        health = MAX_HEALTH;
        velocity = Vector3.zero;
    }
    void Update()
    {
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), 0.0f, 0.0f), Space.Self);
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f), Space.World);
        velocity += (new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized * Input.GetAxis("Vertical") * WALK_SPEED + new Vector3(transform.right.x, 0.0f, transform.right.z).normalized * Input.GetAxis("Horizontal") * WALK_SPEED) * Time.deltaTime * (Input.GetButton("Sprint") ? SPRINT_SPEED_COEFFICIENT : 1.0f) * (controller.isGrounded ? 1.0f : AIR_CONTROL_COEFFICIENT);
        velocity += controller.isGrounded ? Vector3.zero : Physics.gravity * Time.deltaTime;
        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            velocity += Vector3.up * JUMP_FORCE;
        }
        velocity *= 1.0f - (Time.deltaTime * (controller.isGrounded ? GROUND_VELOCITY_DAMPING_RATE : AIR_VELOCITY_DAMPING_RATE));
        CollisionFlags collision = controller.Move(velocity * Time.deltaTime);
        if ((collision & CollisionFlags.Below) != 0)
        {
            velocity.y = 0.0f;
        }
        if (Input.GetButtonDown("UsePowerup"))
        {
            GetComponent<Powerup>().use();
        }
        if (Input.GetButtonDown("Pickup") && pickupTarget != null)
        {
            if (pickupTarget.GetComponent<Weapon>() is Weapon weapon)
            {
                pickupTarget.transform.parent = transform;
                pickupTarget.transform.localPosition = heldWeapon.transform.localPosition;
                pickupTarget.transform.localEulerAngles = Vector3.zero;
                if (heldWeapon != null)
                {
                    heldWeapon.transform.parent = null;
                    heldWeapon.GetComponent<Pickup>().enabled = true;
                    heldWeapon.GetComponent<Collider>().enabled = true;
                    heldWeapon.owner = null;
                }
                heldWeapon = pickupTarget.GetComponent<Weapon>();
                weapon.owner = this;
                hud.ammoCounter.text = weapon.ammo + " / " + weapon.MAX_AMMO;
                pickupTarget.GetComponent<Pickup>().enabled = false;
                pickupTarget.GetComponent<Collider>().enabled = false;
            }
            else if (pickupTarget.GetComponent<Powerup>() is Powerup powerup)
            {
                powerup.use();
            }
            pickupTarget = null;
        }
    }
    public override void die()
    {
        transform.Find("Main Camera").parent = null;
        Destroy(gameObject);
        hud.centerMessage.text = "You died!";
        SceneManager.LoadSceneAsync("GameOver");
    }
}
