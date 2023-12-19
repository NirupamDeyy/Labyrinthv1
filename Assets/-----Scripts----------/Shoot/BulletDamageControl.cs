
using UnityEngine;

public class BulletDamageControl : MonoBehaviour
{
    [SerializeField] private Transform breakables;
    [SerializeField] private PlayerShootControl playerShootControl;
    [SerializeField] private float forceAmount = 10f;
    Rigidbody rb;
    void Start()
    {
        rb = breakables.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            // Apply force at the hit point based on raycast intersection
           // rb.AddForceAtPosition(ray.direction * forceAmount, hit.point, ForceMode.Impulse);
        }
    }

}
