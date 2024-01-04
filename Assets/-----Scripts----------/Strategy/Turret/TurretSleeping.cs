using UnityEngine;

public class TurretSleeping : TurretBaseState
{
    private Transform centreRaycastOrigin;

    [SerializeField]
    private float triggerDistance = 2f;

    [SerializeField]
    private bool istriggered;
    float currentDistance;

    GameObject player;
    bool changeState;
    public override void EnterState(TurretStateManager state, Transform centreRayOrigin) 
    {
        state.animator = state.GetComponent<Animator>();
        state.animator.enabled = true;
        state.PlayAnimayion(false, "IsWaking");
        player = GameObject.FindGameObjectWithTag("Player");
        centreRaycastOrigin = centreRayOrigin;
        changeState = true;
    }
    public override void UpdateState(TurretStateManager state) 
    {
        //check the distance
        currentDistance = Vector3.Distance(centreRaycastOrigin.position, player.transform.position);
       // Debug.Log($"{currentDistance}");    
        if (currentDistance < triggerDistance)// if less than threshold
        {
            Debug.Log("inside");
            DrawLine(Color.red);
            if (changeState)
            {
                state.SwitchState(state.seekingAndShooting);
                changeState = false;
            }
        }
        else// if more than threshold 

        {
            Debug.Log("outside");
            DrawLine(Color.yellow);
        }

       /* if (istriggered)
        {
            RaycastHit hit;
            Ray ray = new Ray(centreRaycastOrigin.position, centreRaycastOrigin.forward);

            if (Physics.Raycast(ray, out hit))
            {
                currentDistance = hit.distance;
            }
            DrawLine(Color.red);
        }
        else
        {
            currentDistance = Vector3.Distance(centreRaycastOrigin.position, player.transform.position);
            DrawLine(Color.yellow);
        }*/
        
        

    }

    private void DrawLine(Color color)
    {
        Debug.DrawLine(centreRaycastOrigin.position, player.transform.position, color);
    }
    public override void OnCollisionEnter(TurretStateManager state) { }

   
}
