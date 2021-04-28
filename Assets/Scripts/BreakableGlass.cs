using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGlass : Entity
{
    public override float MAX_HEALTH
    {
        get
        {
            return 1.0f;
        }
    }
    public override void die()
    {
        Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\glass"), 0.25f, 1.0f);
        Destroy(gameObject);
    }
}
