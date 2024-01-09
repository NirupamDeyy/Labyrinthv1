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

    public bool isWaking = false;
    public bool trigger = false;
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
       // Debug.Log(currentState.ToString());
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this);
    }
    public void dosomethig()
    {
        isWaking = true;
        trigger = true;
    }
    public void SwitchState(TurretBaseState state)
    {
       // Debug.Log("state changed to " +  state);
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
