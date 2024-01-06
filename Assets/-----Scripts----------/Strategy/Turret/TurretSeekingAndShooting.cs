using System.Collections;
using UnityEngine;
using DG.Tweening;

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
    Transform muzzle;
    Transform muzzleFirePoint;
    Transform projectilePrefab;
    
    public override void EnterState(TurretStateManager state, Transform centreRayOrigin)
    {
        state.PlayAnimayion(true, "IsWaking");
        state.StartCoroutine(DisableAnimator(state));
        player = GameObject.FindGameObjectWithTag("Player");
        projectilePrefab = state.projectilePrefab;
        
        //Debug.Log(player.name);
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
        muzzle = state.muzzleTransform;
        muzzleFirePoint = muzzle.GetComponentInChildren<Transform>();   
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
                //Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.CompareTag ("Player") )
                {
                    Shoot(direction + new Vector3(0,0.1f,0));
                   // Debug.Log("got player");
                    AimTurretTowardsVector(state, hit.point);
                }
            }
            DrawLine(Color.cyan, hit.point);
            
        }
    }
    private void AimTurretTowardsVector(TurretStateManager state ,Vector3 point)
    {/*
        state.upperBody.transform.LookAt(point);
        Vector3 lookdirection = state.upperBody.transform.forward;
        state.baseBody.forward = new Vector3(lookdirection.x, 0, lookdirection.z);*/

        // Get the direction from the turret's current position to the point
        Vector3 direction = (point - state.upperBody.position).normalized;

        // Create a rotation based on the desired direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Interpolate between the current rotation and the target rotation
        state.upperBody.rotation = Quaternion.Slerp(state.upperBody.rotation, targetRotation, 10 * Time.deltaTime);


        Vector3 lookdirection = state.upperBody.transform.forward;
        state.baseBody.forward = new Vector3(lookdirection.x, 0, lookdirection.z);
    }

    float shotCountDown = 5f;
    /// <summary>
    /// Shooting Mechanism
    /// </summary>
    void Shoot(Vector3 direction)
    {
        shotCountDown -= Time.deltaTime;
        if(shotCountDown <= 0)
        {
            Vector3 muzzlePos = muzzle.transform.position;
            //Debug.Log(muzzlePos);
            //muzzlePos.y = muzzlePos.y - 0.14f;
            ThrowProjectile(direction);

            // muzzle.DOPunchScale(Vector3.one * 0.02f, .5f, 1);
            //muzzle.DOPunchPosition(new Vector3(0,0, -0.1f), 1f, 1, 0f);
            muzzle.DOLocalMoveZ(.16f - .07f, .1f).OnComplete(() =>
            {
                muzzle.DOLocalMoveZ(.16f + .07f, 1.5f);
            });
            shotCountDown = 4f;
        }
    }
    Rigidbody projectileRb;
    void ThrowProjectile(Vector3 direction)
    {
        Quaternion q = Quaternion.LookRotation(direction);  
        Transform projectile = Transform.Instantiate(projectilePrefab, muzzleFirePoint.transform.position,q);
        projectileRb = projectile.GetComponent<Rigidbody>();    
        projectileRb.AddForce(direction * 1, ForceMode.Impulse);

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
