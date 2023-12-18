using UnityEngine;
using DG.Tweening;

public class MissileContact : MonoBehaviour
{
    [SerializeField] private Transform explosionPrefab;
    [SerializeField] private float forceAmount = 20;
    private GameObject missileTargetPoint;
    CrosshairTarget missileTargetScript;
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();


        missileTargetPoint = GameObject.FindGameObjectWithTag("MissileTargetPoint");
        missileTargetScript = missileTargetPoint.GetComponent<CrosshairTarget>();
        missileTargetScript.canUpdate = true;

        if (rb != null)
        {
            rb.AddForceAtPosition(contact.normal, position, ForceMode.Impulse);
        }
        if(explosionPrefab != null) 
        {
            Instantiate(explosionPrefab, position, rotation);
        }
        
        //Destroy(gameObject);
        
    }

    private void OnDestroy()
    {
        DOTween.Clear();
    }
}
