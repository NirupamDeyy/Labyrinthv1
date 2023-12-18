using UnityEngine;
using DG.Tweening;


public class MissileShootControl : MonoBehaviour
{
    [SerializeField]
    private CrosshairTarget missileTargetPointScript;

    [SerializeField] 
    private GunSwitchControl gunswitchControl;

    [SerializeField] 
    private Transform _missilePrefab;

    private Transform missileModel;

    [SerializeField] 
    private Transform missileInitiationPoint;



    /*[SerializeField]
    private MissileLightControl missileLightControl;
*/
    public Transform targetPoint; 
    public Transform targetPoint2;
   
    public float[] moveSpeed;
    public float missileForce = .1f;

    public Vector3 rotationSpeed = new Vector3(0, 30, 0);

    bool isRotating = false;

    float duration2;

    bool update;
    private void Awake()
    {
        //missileLightControl = GetComponent<MissileLightControl>();
        
    }
    private void Update()
    {
        if (missileTargetPointScript.canUpdate && update)
        {
            //missileLightControl.Reset() ;
            update = false;
        }
        

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gunswitchControl.canShootGun)
        {
              ShootMissile();
        }
       
    }


    private void ShootMissile()
    {
       // missileLightControl.DecreaseWithTime();

        if (missileTargetPointScript.canUpdate)
        {
            update = true;
            missileTargetPointScript.canUpdate = false;

            //Instantiate missile prefab
            Transform missilePrefab = Instantiate(_missilePrefab, missileInitiationPoint.position, missileInitiationPoint.localRotation);
            missileModel = missilePrefab.GetComponentInChildren<Transform>();
            Rigidbody missileRigidbody = missilePrefab.GetComponent<Rigidbody>();
            float distance = Vector3.Distance(missilePrefab.position, targetPoint.position);
            float duration = distance / moveSpeed[0];
            
            float distance2 = Vector3.Distance(targetPoint.position, targetPoint2.position);
            duration2 = distance2 / moveSpeed[1];
            

            missilePrefab.DORotateQuaternion(targetPoint.rotation, 1.2f);
            missilePrefab.DOMove(targetPoint.position, 1f).SetEase(Ease.InCirc)
                .OnComplete(() =>
                {

                    
                    
                    Debug.Log("The duration is: " + duration2);
                    Invoke("ChangeBool", duration2);
                    //missilePrefab.DOMove(targetPoint2.position, duration2).SetEase(Ease.Linear).OnComplete(() => { Debug.Log("Reached the target2!"); });
                    Vector3 direction = (targetPoint2.position - targetPoint.position).normalized;
                    Debug.DrawRay(targetPoint.position, direction * 20f, Color.red);
                    missileRigidbody.AddForce(direction * missileForce, ForceMode.Impulse);
                    Debug.Log("Reached the target1!");

                    
                });
            
        }
        else
        {
            Debug.Log("wait bro");
        }
    }


    private void ChangeBool()
    {
        missileTargetPointScript.canUpdate = true;
        
    }
}
