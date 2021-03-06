using UnityEditor;
using UnityEngine;
public class Pistol : Weapon
{
    protected override bool SINGLE_FIRE { get { return true; } }
    protected override float FIRE_DELAY { get { return 0.2f; } }
    protected override float SPREAD { get { return 0.5f; } }
    public override int MAX_AMMO { get { return 12; } }
    protected override int NUM_SHOTS { get { return 1; } }
    protected override float MINIMUM_VOLUME { get { return 0.25f; } }
    protected override float MAXIMUM_VOLUME { get { return 0.5f; } }
    protected override float MINIMUM_PITCH { get { return 0.8f; } }
    protected override float MAXIMUM_PITCH { get { return 1.25f; } }
    private GameObject _projectile;
    protected override GameObject PROJECTILE { get { return _projectile; } }
    private AudioClip _shotSound = null;
    protected override AudioClip SHOT_SOUND { get { return _shotSound; } }
    private AudioClip _reloadSound = null;
    protected override AudioClip RELOAD_SOUND { get { return _reloadSound; } }
    void Awake()
    {
        ammo = MAX_AMMO;
        _projectile = Resources.Load<GameObject>(@"Prefabs\Weapons\Pistol_Bullet");
        _shotSound = Resources.Load<AudioClip>(@"SFX\gunshot0");
        _reloadSound = Resources.Load<AudioClip>(@"SFX\reload0");
    }
}