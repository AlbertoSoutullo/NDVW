
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttack : StateMachineBehaviour
{
    float attackRange = 6;
    Transform enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(enemy);
        float distance = Vector3.Distance(animator.transform.position, enemy.position);
        if (distance > 20)
            animator.SetBool("isAttacking", false);
        if (distance < attackRange)
            animator.SetBool("isAttacking", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
