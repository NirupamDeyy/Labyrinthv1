using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    GameObject player;
    [SerializeField] private GameObject image;
    TurretStateManager turretStateManager;
    void Start()
    {
        turretStateManager = GetComponent<TurretStateManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.Log("player not found");
    }

    // Update is called once per frame
    void Update()
    {
        if (turretStateManager.isActiveAndEnabled)
        {
           
            image.transform.LookAt(player.transform);
        }
        
    }
}
