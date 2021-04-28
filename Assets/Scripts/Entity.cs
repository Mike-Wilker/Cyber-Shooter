using System.Collections;
using UnityEngine;
public abstract class Entity : MonoBehaviour
{
    public abstract float MAX_HEALTH { get; }
    private float _health;
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (GetComponent<PlayerController>() is PlayerController playerController)
            {
                playerController.hud.healthBar.value = health;
            }
            if (_health <= 0.0f)
            {
                die();
            }
        }
    }
    public abstract void die();
}