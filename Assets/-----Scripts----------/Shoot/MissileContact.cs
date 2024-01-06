using UnityEngine;
using DG.Tweening;

public class MissileContact : MonoBehaviour
{
    [SerializeField] private Transform explosionPrefab;
    [SerializeField] private float forceAmount = 20;
    [SerializeField]
    private Transform dangerCollider;
    private GameObject missileTargetPoint;
    CrosshairTarget missileTargetScript;
    PlayerHealth playerHealth;
    private void Start()
    {
       // dangerCollider.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        dangerCollider.gameObject.SetActive(true);
        Debug.Log(collision.transform.name);
        if(collision.gameObject.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.AddDeleteBlock(false);
        }
        
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

        //dangerCollider.DOPunchScale(Vector3.one, 0.5f, 1, 1).OnComplete(() => DestoryMissile());
        //DOTween.Clear(explosionPrefab);
        //Destroy(gameObject);
        Invoke("DestoryMissile", 1);
        }

    void DestoryMissile()
    {
        Destroy(gameObject);
    }

}
