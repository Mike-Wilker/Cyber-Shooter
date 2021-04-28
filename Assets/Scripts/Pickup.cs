using System.Collections;
using UnityEngine;
public class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() is PlayerController player && enabled)
        {
            if (GetComponent<Weapon>() is Weapon weapon && weapon.GetType() != player.heldWeapon.GetType())
            {
                player.pickupTarget = this;
            }
            else if (GetComponent<Powerup>() != null)
            {
                player.pickupTarget = this;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() is PlayerController player && enabled && player.pickupTarget == this)
        {
            player.pickupTarget = null;
        }
    }
}
