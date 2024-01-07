using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurretDisplays : MonoBehaviour
{
    public List<Transform> turretParts;
    public Image healthBar;
    public int turretHealth = 100;
    private int maxTurretHealth = 100;
    void Start()
    {
        Debug.Log(healthBar.fillAmount);
    }

    public void DecreaseTurretHealth()
    {
        turretHealth--;
        ImageModifier(turretHealth);
    }
    float i = 1.00f;
    private void ImageModifier(int health)
    {
        i = i - .01f;
        healthBar.fillAmount = i;
        Debug.Log("health" + health);
        Debug.Log("maxTurretHealth" + maxTurretHealth);


        Debug.Log(healthBar.fillAmount);

        // healthBar.DOFillAmount(health / maxTurretHealth, 0);
    }
}
