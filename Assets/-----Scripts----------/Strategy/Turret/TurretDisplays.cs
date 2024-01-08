using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurretDisplays : MonoBehaviour
{
    public List<Transform> turretParts;
    public Image healthBar;
    public float turretHealth = 100;

    private float maxTurretHealth = 100;
    public Material mat;
    public Renderer ImageRenda;

    void Start()
    {
        Debug.Log(healthBar.fillAmount);
         turretHealth = 100;
         maxTurretHealth = 100;
         mat = healthBar.material;
         ImageRenda = healthBar.GetComponent<Renderer>();
    }

    public void DecreaseTurretHealth()
    {
        turretHealth--;
        ImageModifier(turretHealth);
    }
    private bool canTween =  true;
    Color healthColor;
    private void ImageModifier(float health)
    {
        healthBar.fillAmount = health / maxTurretHealth;
        mat.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(10f));
        if(health > 50)
        {
            healthColor = Color.white;
        }
        else if( health < 50)
        {
            healthColor = Color.yellow;
        }
        
        if (canTween)
        {
            Debug.Log("tweening");
            canTween = false;
            healthBar.transform.DOPunchScale(new Vector3(0, 1, 0), 0.1f, 1, 1).OnComplete(() =>
            ChangeImageColor(healthColor));
        }
    }

    private void ChangeImageColor(Color color)
    {
        mat.SetColor("_BaseColor", color);

        mat.SetColor("_EmissionColor", color * Mathf.LinearToGammaSpace(10f));
        canTween = true;
    }
}
