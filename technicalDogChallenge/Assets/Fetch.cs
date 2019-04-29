using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fetch : StateMachineBehaviour
{
    private Transform targetTransform;
    public float targetDistanceTolerance = 3.0f;
    GameObject dog;
    GameObject target;
    private float movementSpeed;
    private float rotationSpeed;
    private float distanceFromTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dog = animator.gameObject;
        movementSpeed = 10.0f;
        rotationSpeed = 2.0f;
        targetTransform = GameObject.FindGameObjectWithTag("Target").transform; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //targetTransform = GameObject.FindGameObjectWithTag("Target").transform;
        //if (Vector3.Distance(dog.transform.position, targetTransform.position) < targetDistanceTolerance)
        //{
        //    return;
        //}

        Vector3 targetPosition = targetTransform.position;
        targetPosition.y = dog.transform.position.y;
        Vector3 direction = targetPosition - dog.transform.position;

        Quaternion tarRot = Quaternion.LookRotation(direction);
        dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, tarRot, rotationSpeed * Time.deltaTime);

        dog.transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
        distanceFromTarget = Vector3.Distance(targetTransform.position, dog.transform.position);
        animator.SetFloat("distanceFromTarget", distanceFromTarget);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
