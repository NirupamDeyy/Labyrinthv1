using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateManager : MonoBehaviour
{
    TurretBaseState currentState;
    public TurretSleeping sleepingState = new();
    public TurretSeekingAndShooting seekingAndShooting = new();
    public TurretDestroying destroyingState = new();

    [SerializeField]
    private Transform centreRaycastOrigin;

    public Transform upperBody;

    public Animator animator;
    void Start()
    {
        //starting state for state machine
        currentState = sleepingState;
        // "this" is a reference to the context (this Exact Monobehavior script)
        currentState.EnterState(this, centreRaycastOrigin);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this);
    }

    public void SwitchState(TurretBaseState state)
    {
        currentState = state;
        state.EnterState(this, centreRaycastOrigin);
    }

    public void PlayAnimayion(bool isTrue, string animation)
    {
        animator.SetBool(animation,isTrue);
    }


    
}
