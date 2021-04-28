using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Boss : Enemy
{
    private const float STOP_DISTANCE = 3.0f;
    public override float MAX_HEALTH
    {
        get
        {
            return 150.0f;
        }
    }
}
