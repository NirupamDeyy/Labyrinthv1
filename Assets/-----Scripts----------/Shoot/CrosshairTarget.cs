using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;
    public Color gizmoColor = Color.yellow;
    public bool canUpdate;
    void Start()
    {
        canUpdate = true;
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (canUpdate)
        {
            ray.origin = mainCamera.transform.position;
            ray.direction = mainCamera.transform.forward;
            Physics.Raycast(ray, out hitInfo);
            transform.position = hitInfo.point;
        }

    }

    private void OnDrawGizmos()
    {
       
            Gizmos.color = gizmoColor; // Set Gizmo color

            // Draw a sphere Gizmo at the point's position with a radius of 0.2 units
            Gizmos.DrawSphere(transform.position,.01f);
        
    }
}

