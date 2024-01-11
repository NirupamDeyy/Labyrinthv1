using UnityEngine;
using DG.Tweening;
using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager;
using System.Collections.Generic;

public class MissileContact : MonoBehaviour
{
    [SerializeField] private Transform explosionPrefab;
    [SerializeField] private float forceAmount = 20;
    /*[SerializeField]
    private Transform //dangerCollider;*/
    [SerializeField]
    private Transform rayEmitter;
    private GameObject missileTargetPoint;
    CrosshairTarget missileTargetScript;
    PlayerHealth playerHealth;
    TurretHealth turretHealth;
    private bool istriggered = false;
    GameObject player;
    [SerializeField] float impactDistance = 2f;
    public LayerMask damagableLayer;
    public List<Transform> turrets = new();
    private void Start()
    {
        // dangerCollider.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] objWithTurretTags = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject obj in objWithTurretTags)
        {
            turretHealth = obj.GetComponent<TurretHealth>();
            if (turretHealth != null)
            {
                turrets.Add(obj.transform);   
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        istriggered = true;
        //dangerCollider.gameObject.SetActive(true); 
        //Debug.Log(collision.transform.name);

        List<Transform> turretsinRange = GetTurretsInRange(turrets);
        foreach (Transform t in turretsinRange)
        {
            turretHealth = t.GetComponent<TurretHealth>();
            turretHealth.DecreaseTurretHealth(10);
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

         Invoke("DestoryMissile", 0.5f);
    }

    private List<Transform> GetTurretsInRange( List<Transform> turrets)
    {
        List<Transform> list = new List<Transform>();
        foreach(Transform t in turrets) 
        {
            float Distance = Vector3.Distance(transform.position, t.position);
            Debug.Log("Distance is: " + Distance);
            if (Distance < impactDistance)
            {
                list.Add(t);
            }
        }
        return list;
    }
    private void Update()
    {
        if (istriggered)
        {
            Vector3 direction = (player.transform.position - rayEmitter.position).normalized;
            RaycastHit hit;
            Ray ray = new Ray(rayEmitter.position, direction);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.CompareTag("Player") && hit.distance < impactDistance)
                {
                    // Debug.Log("impac distance" + hit.distance);
                    playerHealth = hit.transform.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {

                        DrawLine(Color.cyan, hit.point);
                        DecreasePlayerHealth();
                    }
                    else
                    {
                        Debug.Log("playerhealthNotfound");
                    }
                }

            }

        }

    }

    void DecreasePlayerHealth()
    {
        playerHealth.AddDeleteBlock(false);
        istriggered = false;
    }

    void DrawLine(Color color, Vector3 point)
    {
        Debug.DrawLine(rayEmitter.position, point, color);
    }


    private void DestoryMissile()
    {
        Destroy(gameObject);
    }
    
}

