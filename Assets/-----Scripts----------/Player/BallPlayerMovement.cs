using Cinemachine.Utility;
using DG;
using DG.Tweening;
using UnityEngine;

public class BallPlayerMovement : MonoBehaviour
{
    [SerializeField] private BoxCollider playerCatcherCollider;
    public ImageFaderScript imageFaderScript;
    public ActionUIconrol actionUIconrol;
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float rotationDamping = 0.5f;
    public bool rotatePlayer = true;
    public Transform outerShell;
    Rigidbody parentRigidBody;
    public Transform gunParent;
    public float Ospeed;
    public bool resetGunDirection;
    [SerializeField] private Transform raycastOriginBottom;
    [SerializeField] private float maxJumpDistance;
    Vector3 forward;
    Quaternion nRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        parentRigidBody = GetComponent<Rigidbody>();    
    }
    public bool isGrounded = true;

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastOriginBottom.position, -raycastOriginBottom.up, out hit, 100f))
        {
            //Debug.DrawRay(raycastOriginBottom.position, -raycastOriginBottom.up);
            //Debug.Log(hit.distance);
            if (hit.distance < maxJumpDistance)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "playerCatcherCollider")
        {
            Debug.Log("why you jumped");
            imageFaderScript.FadeImageMethod(2, false);
            Invoke("ShowPauseMenu", 2);
        }
    }

    private void ShowPauseMenu()
    {
        actionUIconrol.Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            parentRigidBody.AddForce(transform.up * 4, ForceMode.VelocityChange);
        }
        


        forward = Camera.main.transform.forward;
        nRotation = Quaternion.LookRotation(forward, Vector3.up);
        gunParent.rotation = Quaternion.Lerp(gunParent.rotation, nRotation, 10 * Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
       
        if (input.magnitude > 0)
        {
            // Calculate the new position based on input
            Vector3 movement = Camera.main.transform.rotation * input;
            
            movement.y = 0; // Ensure movement is only in the XY plane
            movement.Normalize(); // Normalize for consistent speed

            // Move the object
            transform.position += speed * Time.deltaTime * movement;
            outerShell.transform.Rotate(Vector3.right * Ospeed * Time.deltaTime);

            // Smoothly rotate the object
            if (rotatePlayer)
            {
                float t = Damper.Damp(1, rotationDamping, Time.deltaTime);
                Quaternion newRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, t);

               
            }
        }
    }
}
