using UnityEngine;

public class TurretStateManager : MonoBehaviour
{
    public TurretBaseState currentState;
    public TurretSleeping sleepingState = new();
    public TurretSeekingAndShooting seekingAndShooting = new();
    public TurretDestroying destroyingState = new();

    [SerializeField]
    private Transform centreRaycastOrigin;
    public LookToPlayer lookTowardsPlayer;
    public TriggerVisualizerScript triggerVisualizerScript;
    public Transform upperBody;
    public Transform baseBody;
    public Transform muzzleTransform;
    public Transform projectilePrefab;

    public Animator animator;
    void Start()
    {
        //starting state for state machine
        currentState = sleepingState;
        // "this" is a reference to the context (this Exact Monobehavior script)
        currentState.EnterState(this, centreRaycastOrigin);
        animator = GetComponent<Animator>();
        lookTowardsPlayer = GetComponent<LookToPlayer>();
        lookTowardsPlayer.enabled = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this);
    }
    public void dosomethig()
    {
        if (currentState == sleepingState)
        {
            SwitchState(seekingAndShooting);
           
        }
        else
        {
            Debug.Log("curretnt state is not sleepin but:" + currentState);
        }
    }
    public void SwitchState(TurretBaseState state)
    {
        Debug.Log("state changed to " +  state);
        currentState = state;
        state.EnterState(this, centreRaycastOrigin);
        if(currentState == seekingAndShooting)
        {
            triggerVisualizerScript.Triggered(true);
        }
        else
        {
            triggerVisualizerScript.Triggered(false);
        }
    }

    public void PlayAnimayion(bool isTrue, string animation)
    {
        animator.SetBool(animation,isTrue);
    }

   
    
}
