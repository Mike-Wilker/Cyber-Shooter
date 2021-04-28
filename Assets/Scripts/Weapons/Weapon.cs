using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public abstract class Weapon : Item
{
    protected float cooldown = 0.0f;
    protected abstract bool SINGLE_FIRE { get; }
    public abstract int MAX_AMMO { get; }
    protected abstract int NUM_SHOTS { get; }
    protected abstract float FIRE_DELAY { get; }
    protected abstract float SPREAD { get; }
    protected abstract float MINIMUM_VOLUME { get; }
    protected abstract float MAXIMUM_VOLUME { get; }
    protected abstract float MINIMUM_PITCH { get; }
    protected abstract float MAXIMUM_PITCH { get; }
    protected abstract GameObject PROJECTILE { get; }
    protected abstract AudioClip SHOT_SOUND { get; }
    protected abstract AudioClip RELOAD_SOUND { get; }
    private float INACCURACY = 10.0f;
    private int _ammo;
    public int ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = value;
            if (owner is PlayerController playerController)
            {
                playerController.hud.ammoCounter.text = ammo + " / " + MAX_AMMO;
            }
        }
    }

    void Update()
    {
        if (owner is Enemy enemy)
        {
            GetComponent<Animator>().SetBool("Held", true);
            if (SINGLE_FIRE)
            {
                cooldown = cooldown - (Time.deltaTime / 4.0f) > 0.0f ? cooldown - (Time.deltaTime / 4.0f) : 0.0f;
            }
            else
            {
                cooldown = cooldown - Time.deltaTime > 0.0f ? cooldown - Time.deltaTime : 0.0f;
            }
            if (FindObjectOfType<PlayerController>() is PlayerController player)
            {
                if (ammo > 0 && cooldown == 0.0f && enemy.canSee(player.gameObject))
                {
                    transform.LookAt(player.transform.position);
                    transform.Rotate(new Vector3(Random.Range(-INACCURACY, INACCURACY), Random.Range(-INACCURACY, INACCURACY), Random.Range(-INACCURACY, INACCURACY)));
                    fire();
                }
            }
        }
        else if (owner is PlayerController player)
        {
            GetComponent<Animator>().SetBool("Held", true);
            cooldown = cooldown - Time.deltaTime > 0.0f ? cooldown - Time.deltaTime : 0.0f;
            if (ammo > 0 && cooldown == 0.0f && (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1") &! SINGLE_FIRE))
            {
                fire();
            }
            if (ammo < MAX_AMMO && ammo != 0.0f && Input.GetButtonDown("Reload"))
            {
                reload();
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("Held", false);
        }
    }
    public void fire()
    {
        for (int projectile = 0; projectile < NUM_SHOTS; projectile++)
        {
            Instantiate(PROJECTILE, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(Random.Range(-SPREAD, SPREAD), Random.Range(-SPREAD, SPREAD), Random.Range(-SPREAD, SPREAD)))).GetComponent<Hurtbox>().owner = owner;
        }
        Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\One Shot Audio Source"), transform).setup(SHOT_SOUND, Random.Range(MINIMUM_VOLUME, MAXIMUM_VOLUME), Random.Range(MINIMUM_PITCH, MAXIMUM_PITCH));
        GetComponent<Animator>().SetTrigger("Fire");
        GetComponent<ParticleSystem>().Play();
        cooldown = FIRE_DELAY;
        ammo--;
        if (ammo == 0)
        {
            reload();
        }
    }
    public void reload()
    {
        if (ammo < MAX_AMMO)
        {
            ammo = 0;
            Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\One Shot Audio Source"), transform).setup(RELOAD_SOUND, 1.0f, 1.0f);
            GetComponent<Animator>().SetTrigger("Reload");
        }
    }
    public void finishReload()
    {
        ammo = MAX_AMMO;
    }
}
