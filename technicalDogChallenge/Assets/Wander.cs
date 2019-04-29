using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : StateMachineBehaviour {
    GameObject dog;
    private GameObject ball;
    private float movementSpeed = 5.0f;
    private float rotationSpeed = 2.0f;
    private float targetPositionTolerance = 3.0f;
    private Vector3 targetPosition;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;


    private void Awake()
    {
        minX = -15.0f;
        maxX = 15.0f;

        minZ = -15.0f;
        maxZ = 15.0f;
        GetNextPosition();
    }

    void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        dog = animator.gameObject;
        animator.SetBool("isMouseClicked", false);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        int button = 0; 
        if (Input.GetMouseButtonDown(button))
        {
            animator.SetBool("isMouseClicked", true);
            Debug.Log("Clicked!");
        }
        if (Vector3.Distance(targetPosition, dog.transform.position) <= targetPositionTolerance)
        {
            GetNextPosition();
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - dog.transform.position);
        dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        dog.transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }

	 //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
