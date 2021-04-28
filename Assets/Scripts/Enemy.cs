using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Entity
{
    NavMeshAgent navMeshAgent = null;
    Animator characterAnimator = null;
    private const float STOP_DISTANCE = 5.0f;
    public override float MAX_HEALTH
    {
        get
        {
            return 50.0f;
        }
    }
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterAnimator = transform.Find("Character").GetComponent<Animator>();
        health = MAX_HEALTH;
    }
    void Update()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        if (FindObjectOfType<PlayerController>() is PlayerController targetedPlayer)
        {
            if (canSee(targetedPlayer.gameObject))
            {
                Vector3 target = targetedPlayer.transform.position;
                if ((target - transform.position).magnitude < STOP_DISTANCE)
                {
                    navMeshAgent.SetDestination(transform.position);
                }
                else
                {
                    if (navMeshAgent.CalculatePath(target, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                    {
                        navMeshAgent.SetPath(navMeshPath);
                    }
                }
            }
        }
        characterAnimator.SetFloat("MoveX", navMeshAgent.desiredVelocity.normalized.x);
        characterAnimator.SetFloat("MoveY", navMeshAgent.desiredVelocity.normalized.z);
    }
    public override void die()
    {
        if (GetComponentInChildren<Weapon>() is Weapon weapon)
        {
            weapon.transform.parent = null;
            weapon.owner = null;
            weapon.GetComponent<Pickup>().enabled = true;
            weapon.GetComponent<Collider>().enabled = true;
        }
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(Resources.Load(@"Prefabs\Powerups\Health Potion"), transform.position, transform.rotation).name = "Health Potion";
        }
        else
        {
            Instantiate(Resources.Load(@"Prefabs\Powerups\Speed Potion"), transform.position, transform.rotation).name = "Speed Potion";
        }
        Destroy(gameObject);
    }
    public bool canSee(GameObject target)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
        {
            return hit.collider.gameObject == target.gameObject;
        }
        else
        {
            return false;
        }
    }
}
