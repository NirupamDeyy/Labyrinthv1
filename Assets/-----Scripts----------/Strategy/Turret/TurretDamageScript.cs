using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (playerShootControl != null && playerShootControl.isFiring)
        {
            if (playerShootControl.hitInfo.collider.tag == "Turret")
            {
                turretDisplays.DecreaseTurretHealth();
            }
        }
}
}
