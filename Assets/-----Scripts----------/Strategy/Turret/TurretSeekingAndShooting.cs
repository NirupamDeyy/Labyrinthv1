using System.Collections;
using UnityEngine;

public class TurretSeekingAndShooting : TurretBaseState
{
    private Transform centreRaycastOrigin;

    [SerializeField]
    private float triggerDistance = 2f;

    [SerializeField]
    private bool istriggered;
    float currentDistance;

    GameObject player;
    GameObject playerParent;

    bool changeState;
    public override void EnterState(TurretStateManager state, Transform centreRayOrigin)
    {
        state.PlayAnimayion(true, "IsWaking");
        state.StartCoroutine(DisableAnimator(state));
        player = GameObject.FindGameObjectWithTag("Player");
        
        Debug.Log(player.name);
        centreRaycastOrigin = centreRayOrigin;
        changeState = true;
    }

    IEnumerator DisableAnimator(TurretStateManager state)
    {
        yield return new WaitForSeconds(2);//time to complete animation
        if (currentDistance < triggerDistance)
        {
            state.animator.enabled = false;
            //shooting chalu
        }
    }
    public override void UpdateState(TurretStateManager state)
    {
        //check the distance
        currentDistance = Vector3.Distance(centreRaycastOrigin.position, player.transform.position);
        // Debug.Log($"{currentDistance}");    
        if (currentDistance < triggerDistance)// if less than threshold
        {
            //Debug.Log("inside");
            //DrawLine(Color.red);
            time = 0;
            istriggered = true;
        }
        else// if more than threshold 
        {
            //Debug.Log("outside");
            //DrawLine(Color.yellow);
            time = time + Time.deltaTime;
            //Debug.Log(time);
            if (changeState && time > 10f)
            {
                StartTimer(state);
                changeState = false;
            }
        }

        if (istriggered)
        {
            Vector3 direction = (player.transform.position - centreRaycastOrigin.position).normalized;  
            RaycastHit hit;
            Ray ray = new Ray(centreRaycastOrigin.position, direction);
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.CompareTag ("Player") )
                {
                    Debug.Log("got player");
                    AimTurretTowardsVector(state, hit.point);
                }
                
            }
            DrawLine(Color.cyan, hit.point);
            
        }
    }
    
    private void AimTurretTowardsVector(TurretStateManager state ,Vector3 point)
    {
        state.upperBody.transform.LookAt(point);
        Vector3 lookdirection = state.upperBody.transform.forward;
        state.baseBody.forward = new Vector3(lookdirection.x, 0, lookdirection.z);
    }

    float time;
    private void StartTimer(TurretStateManager state)
    {
        
        if (currentDistance > triggerDistance)
        {
            changeState = true;
            state.SwitchState(state.sleepingState);
        }
        
    }
    private void DrawLine(Color color, Vector3 point)
    {
        Debug.DrawLine(centreRaycastOrigin.position, point, color);
    }
    public override void OnCollisionEnter(TurretStateManager state) { }
}
