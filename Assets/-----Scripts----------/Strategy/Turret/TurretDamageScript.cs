using UnityEngine;

public class TurretDamageScript : MonoBehaviour
{
    PlayerShootControl playerShootControl;
    public int enemyHealth;
    TurretDisplays turretDisplays;
    void Start()
    {
        turretDisplays = GetComponent<TurretDisplays>();
        playerShootControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShootControl>();
        if (playerShootControl != null )
        {
            Debug.Log("got player");
        }
    }

    public bool isuu = true;
    public bool kill = false;
    public string tag;
    void Update()
    {
       tag = playerShootControl.hitInfo.transform.tag;
        if (playerShootControl.isFiring)
        {
            if (playerShootControl.hitInfo.transform.CompareTag("Turret"))
            {
                Debug.Log("the collider is......................................................................................... ");

               turretDisplays.DecreaseTurretHealth();
                kill = true;
            }
            else if(playerShootControl.hitInfo.transform == null)
            {
                Debug.Log("the collider is " + playerShootControl.hitInfo.collider.tag);
                isuu = true;
            }
        }
        else
        {
            kill = false;
            isuu = false;
        }
    }
}
