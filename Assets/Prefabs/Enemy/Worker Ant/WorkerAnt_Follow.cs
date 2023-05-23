using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt_Follow : StateMachineBehaviour
{
    private WorkerAnt workerAnt;
    private Transform transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        workerAnt = animator.transform.GetComponent<WorkerAnt>();
        transform = workerAnt.transform;
        //target = EnemyManager.Instance.target;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 moveDirection = workerAnt.AI.direction;
        Vector2 targetDirection = (workerAnt.target.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.4f,targetDirection, workerAnt.status.detectRange, EnemyManager.Instance.EnemyCollisionLayers);

        if (hit)
        {
            if (hit.transform.gameObject.layer == 7)
            {
                //can see player, move directly towards player without using a* AI
                moveDirection = targetDirection;
            }
        }

        workerAnt.movement.dest = moveDirection;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
