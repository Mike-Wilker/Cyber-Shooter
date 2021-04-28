using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadFinish : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Weapon>().finishReload();
    }
}
