using System.Collections;
using UnityEngine;
using System.Threading;


public class TurretSeekingAndShooting : TurretBaseState
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
        state.PlayAnimayion(true, "IsWaking");
        state.StartCoroutine(DisableAnimator(state));
        player = GameObject.FindGameObjectWithTag("player");
        centreRaycastOrigin = centreRayOrigin;
        changeState = true;
    }

    IEnumerator DisableAnimator(TurretStateManager state)
    {
        yield return new WaitForSeconds(3);//time to complete animation
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
            RaycastHit hit;
            Ray ray = new Ray(centreRaycastOrigin.position, player.transform.position);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.transform.name);
                if(hit.collider.CompareTag ("player"))
                {
                    Debug.Log("got player");
                    AimTurretTowardsGivenVector(state, player.transform);
                }
            }
            DrawLine(Color.cyan);
        }
    }
    
    private void AimTurretTowardsGivenVector(TurretStateManager state ,Transform player)
    {
        state.upperBody.transform.LookAt(player);
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
    private void DrawLine(Color color)
    {
        Debug.DrawLine(centreRaycastOrigin.position, player.transform.position, color);
    }
    public override void OnCollisionEnter(TurretStateManager state) { }
}
